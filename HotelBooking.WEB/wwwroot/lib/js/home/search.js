var bookingRoom = {} || bookingRoom;
var searchRequest = JSON.parse(localStorage.getItem('searchRequest'));
var roomSelected = [];
var roomTypes;

digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

$(document).ready(function () {
    bookingRoom.init();
})

bookingRoom.init = function () {
    getRoomTypes();
    showSelections();
    getRoomsValue();
}

formatDate = function (date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [day, month, year].join('-');
}

getRoomTypes = function () {
    $.ajax({
        url: "/RoomTypesManager/GetAllWithImagesAndFacilities",
        method: "GET",
        dataType: "json",
        async: false,
        success: function (roomTypesData) {
            roomTypes = roomTypesData.result;
        },
    });
}

showSelections = function () {
    $('#checkInDate').text(`Ngày nhận phòng: ${formatDate(new Date(searchRequest.CheckInDate))}`)
    $('#checkOutDate').text(`Ngày trả phòng: ${formatDate(new Date(searchRequest.CheckOutDate))}`)
    var adults = 0;
    var children = 0;
    for (let i = 0; i < searchRequest.Rooms.length; i++) {
        $('#tbRoomBooking').append(
            `<div class="card my-3">
                <div class="row">
                    <div class="col-md-3 col-sm-12 text-center my-auto">
                        <h4>Phòng ${i + 1}</h4>
                        <p>(${searchRequest.Rooms[i].Adults} người lớn, ${searchRequest.Rooms[i].Children} trẻ em)</p>
                    </div>
                    <div class="col-md-9 col-sm-12" id="room${i}">
                    </div>
                </div>
            </div>`
        )
        let searchRequestObj = {};
        searchRequestObj.CheckInDate = new Date(searchRequest.CheckInDate);
        searchRequestObj.CheckOutDate = new Date(searchRequest.CheckOutDate);
        searchRequestObj.NumberOfAdults = searchRequest.Rooms[i].Adults;
        searchRequestObj.NumberOfChildren = searchRequest.Rooms[i].Children;
        adults += searchRequestObj.NumberOfAdults;
        children += searchRequestObj.NumberOfChildren;
        var roomTypeResult;
        $.ajax({
            url: `/RoomTypesManager/Search/`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            async: false,
            data: JSON.stringify(searchRequestObj),
            success: function (roomTypeResultData) {
                roomTypeResult = roomTypeResultData.result;

                for (let i = 0; i < roomTypes.length; i++)
                    for (let j = 0; j < roomTypeResultData.result.length; j++)
                        if (roomTypes[i].roomTypeId == roomTypeResultData.result[j].roomTypeId)
                            roomTypes[i].minRemain = roomTypeResultData.result[j].minRemain;
            },
        });
        for (let j = 0; j < roomTypeResult.length; j++)
            for (let k = 0; k < roomTypes.length; k++)
                if (roomTypeResult[j].roomTypeId == roomTypes[k].roomTypeId) {
                    let facilities = '';
                    for (let l = 0; l < roomTypes[k].facilities.length; l++)
                        facilities += `<img src=${roomTypes[k].facilities[l].facilityImage} class="mx-1 facilities">`

                    var roomPriceStr = '<div class="row"><div class="col-lg-2 col-md-12 col-4">Giá:</div><div class="col-lg-10 col-md-12 col-8">';
                    var price = 0;
                    for (let d = searchRequestObj.CheckInDate.getTime(); d < searchRequestObj.CheckOutDate.getTime(); d += 86400000) {
                        var date = new Date(d);
                        var getPromotionRequest = {};
                        getPromotionRequest.RoomTypeId = roomTypeResult[j].roomTypeId;
                        getPromotionRequest.Date = date;
                        $.ajax({
                            url: `/PromotionsManager/GetAvailableForDateAndRoomTypeId/`,
                            method: "POST",
                            dataType: "json",
                            contentType: "application/json",
                            async: false,
                            data: JSON.stringify(getPromotionRequest),
                            success: function (promotionData) {
                                if (promotionData.result.discountRates == 0) {
                                    roomPriceStr += `<div>${date.getDate()}/${date.getMonth() + 1}: ${digitGrouping(roomTypes[k].defaultPrice)}₫</div>`
                                    price += roomTypes[k].defaultPrice;
                                } else {
                                    roomPriceStr += `<div>${date.getDate()}/${date.getMonth() + 1}: <del class="text-danger">${digitGrouping(roomTypes[k].defaultPrice)}₫</del> ${digitGrouping(roomTypes[k].defaultPrice * (1 - promotionData.result.discountRates))}₫</div>`
                                    price += roomTypes[k].defaultPrice * (1 - promotionData.result.discountRates);
                                }
                            },
                        });
                    }
                    roomPriceStr += '</div></div>';
                    $(`#room${i}`).append(
                        `<div class="row my-2">
                            <div class="col-md-4">
                                <img class="my-auto" src="${roomTypes[k].image}">
                            </div>
                            <div class="col-md-7">
                                <a href="/Rooms/Details/${roomTypes[k].roomTypeId}" target="_blank"><h4 class="text-warning">${roomTypes[k].name} <span class="text-danger" id="room-available-${i + 1}-${roomTypes[k].roomTypeId}"></span></h4></a>
                                <p>${roomTypes[k].description}</p>
                                <p class="text-dark">Tiện nghi: ${facilities}</p>
                                ${roomPriceStr}
                                <p class="text-dark">Tổng giá phòng: ${digitGrouping(price)} ₫</p>
                                <input type="number" hidden value="${price}" id="roomPrice-${i + 1}-${roomTypes[k].roomTypeId}">
                            </div>
                            <div class="col-md-1 my-auto text-center">
                                <input type="radio" value="${roomTypes[k].roomTypeId}" name="room-${i + 1}" id="room-${i + 1}-${roomTypes[k].roomTypeId}" onclick="getRoomsValue()">
                            </div>
                        </div>`
                    )
                    if (j < roomTypeResult.length - 1)
                        $(`#room${i}`).append('<hr style="height:2px;">');
                };
    }
    $('#totalPeople').text(`Tổng số người: ${adults + children} (${adults} người lớn, ${children} trẻ em)`);
}

getRoomsValue = function () {
    roomSelected = [];
    var totalAmount = 0;
    for (let j = 0; j < roomTypes.length; j++) {
        var minRemain = roomTypes[j].minRemain;
        for (let i = 0; i < searchRequest.Rooms.length; i++) {
            if (document.querySelector(`input[id="room-${i + 1}-${roomTypes[j].roomTypeId}"]:checked`)) {
                roomSelected.push(parseInt(document.querySelector(`input[id="room-${i + 1}-${roomTypes[j].roomTypeId}"]:checked`).value));
                totalAmount += parseInt($(`#roomPrice-${i + 1}-${roomTypes[j].roomTypeId}`).val());
                $(`.minRemain${roomTypes[j].roomTypeId}`).val(minRemain - 1);
                minRemain -= 1;
                if (minRemain == 0) {
                    disableUnavailableRoomTypes(roomTypes[j].roomTypeId);
                }
                if (minRemain ==1) {
                    enableAvailableRoomTypes(roomTypes[j].roomTypeId);
                }
            }
        }
    }
    $('#TotalAmount').text(digitGrouping(totalAmount) + '₫');
}

disableUnavailableRoomTypes = function (roomTypeId) {
    for (let j = 0; j < searchRequest.Rooms.length; j++) {
        if (document.getElementById(`room-${j + 1}-${roomTypeId}`).checked) {
        } else {
            document.getElementById(`room-${j + 1}-${roomTypeId}`).disabled = true;
            $(`#room-available-${j + 1}-${roomTypeId}`).text('(Hết phòng)');
        }
    }
}

enableAvailableRoomTypes = function (roomTypeId) {
    console.log(roomTypeId);
    for (let j = 0; j < searchRequest.Rooms.length; j++) {
        if (document.getElementById(`room-${j + 1}-${roomTypeId}`).checked) {
        } else {
            document.getElementById(`room-${j + 1}-${roomTypeId}`).disabled = false;
            $(`#room-available-${j + 1}-${roomTypeId}`).text('');
        }
    }
}

bookRoom = function () {
    var bookingRoomRequest = searchRequest;
    bookingRoomRequest.bookingRoom = roomSelected;
    console.log(bookingRoomRequest);
}