var bookingRoom = {} || bookingRoom;
var searchRequest = JSON.parse(localStorage.getItem('searchRequest'));
var roomSelected = [];
var roomTypes;
var searchResult = {};

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
    var search = {};
    search.CheckInDate = new Date(searchRequest.CheckInDate);
    search.CheckOutDate = new Date(searchRequest.CheckOutDate);
    search.RoomTypeSearchRequests = searchRequest.RoomTypeSearchRequests;
    $.ajax({
        url: `/Search/Search`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        async: false,
        data: JSON.stringify(search),
        success: function (data) {
            searchResult = data.result;
        }
    });
    $('#checkInDate').text(`Nhận phòng: ${formatDate(new Date(searchRequest.CheckInDate))}`);
    $('#checkOutDate').text(`Trả phòng: ${formatDate(new Date(searchRequest.CheckOutDate))}`);
    var adults = 0;
    var children = 0;
    for (let i = 0; i < searchResult.roomSearchResults.length; i++) {
        adults += searchResult.roomSearchResults[i].adults;
        children += searchResult.roomSearchResults[i].children;
        $('#tbRoomBooking').append(
            `<div class="card my-3">
                <div class="row">
                    <div class="col-md-3 col-sm-12 text-center my-auto">
                        <h4>Phòng ${i + 1}</h4>
                        <p>(${searchResult.roomSearchResults[i].adults} người lớn, ${searchResult.roomSearchResults[i].children} trẻ em)</p>
                    </div>
                    <div class="col-md-9 col-sm-12 my-auto" id="room${i}">
                    </div>
                </div>
            </div>`
        )
        if (!searchResult.roomSearchResults[i].roomTypeSearchResults)
            $(`#room${i}`).append('<div class="text-danger">Xin lỗi, chúng tôi đã hết phòng để chứa đủ số lượng người này!</div>')
        else
            for (let j = 0; j < searchResult.roomSearchResults[i].roomTypeSearchResults.length; j++) {
                for (let k = 0; k < roomTypes.length; k++)
                    if (searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomTypeId == roomTypes[k].roomTypeId) {
                        roomTypes[k].minRemain = searchResult.roomSearchResults[i].roomTypeSearchResults[j].minRemain;
                        let facilities = '';
                        for (let l = 0; l < roomTypes[k].facilities.length; l++)
                            facilities += `<img src=${roomTypes[k].facilities[l].facilityImage} class="mx-1 facilities">`

                        var roomPriceStr = '<div class="row"><div class="col-lg-2 col-md-12 col-4">Giá:</div><div class="col-lg-10 col-md-12 col-8">';
                        var price = 0;
                        for (let d = 0; d < searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults.length; d++) {
                            var date = new Date(searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults[d].date);
                            price += searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults[d].price;
                            if (searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults[d].price == roomTypes[k].defaultPrice)
                                roomPriceStr += `<div>${date.getDate()}/${date.getMonth() + 1}: ${digitGrouping(roomTypes[k].defaultPrice)}</div>`
                            else
                                roomPriceStr += `<div>${date.getDate()}/${date.getMonth() + 1}: <del class="text-danger">${digitGrouping(roomTypes[k].defaultPrice)}</del> ${digitGrouping(searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults[d].price)}</div>`
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
                                <p class="text-dark">Tổng giá phòng: ${digitGrouping(price)}</p>
                                <input type="number" hidden value="${price}" id="roomPrice-${i + 1}-${roomTypes[k].roomTypeId}">
                            </div>
                            <div class="col-md-1 my-auto text-center">
                                <input type="radio" value="${roomTypes[k].roomTypeId}" name="room-${i + 1}" id="room-${i + 1}-${roomTypes[k].roomTypeId}" onclick="getRoomsValue()">
                            </div>
                        </div>`
                        )
                        if (j < searchResult.roomSearchResults[i].roomTypeSearchResults.length - 1)
                            $(`#room${i}`).append('<hr style="height:2px;">');
                    };
            }
    }
    $('#totalPeople').text(`Tổng số người: ${adults + children} (${adults} người lớn, ${children} trẻ em)`);
}

getRoomsValue = function () {
    var totalAmount = 0;
    roomSelected.length = 0;
    for (let i = 0; i < roomTypes.length; i++) {
        var minRemain = roomTypes[i].minRemain;
        for (let j = 0; j < searchResult.roomSearchResults.length; j++)
            if (document.querySelector(`input[id="room-${j + 1}-${roomTypes[i].roomTypeId}"]:checked`)) {
                roomSelected.push(parseInt(document.querySelector(`input[id="room-${j + 1}-${roomTypes[i].roomTypeId}"]:checked`).value));
                totalAmount += parseInt($(`#roomPrice-${j + 1}-${roomTypes[i].roomTypeId}`).val());
                minRemain -= 1;
                if (minRemain == 0)
                    disableUnavailableRoomTypes(roomTypes[i].roomTypeId);
                if (minRemain == 1)
                    enableAvailableRoomTypes(roomTypes[i].roomTypeId);
            }
    }
    $('#TotalAmount').text(digitGrouping(totalAmount));
}

disableUnavailableRoomTypes = function (roomTypeId) {
    for (let i = 0; i < searchResult.roomSearchResults.length; i++)
        if (!document.getElementById(`room-${i + 1}-${roomTypeId}`).checked) {
            document.getElementById(`room-${i + 1}-${roomTypeId}`).disabled = true;
            $(`#room-available-${i + 1}-${roomTypeId}`).text('(Hết phòng)');
        }
}

enableAvailableRoomTypes = function (roomTypeId) {
    for (let i = 0; i < searchResult.roomSearchResults.length; i++)
        if (!document.getElementById(`room-${i + 1}-${roomTypeId}`).checked) {
            document.getElementById(`room-${i + 1}-${roomTypeId}`).disabled = false;
            $(`#room-available-${i + 1}-${roomTypeId}`).text('');
        }
}

bookRoom = function () {
    var bookingRoomRequest = searchRequest;
    bookingRoomRequest.bookingRoom = roomSelected;
    var allRoomSelected = true;
    for (let i = 0; i < searchResult.roomSearchResults.length; i++)
        if ($(`input[name=room-${i + 1}]:checked`).length == 0) {
            bootbox.alert(`Bạn chưa chọn loại phòng cho phòng ${i + 1}!`);
            allRoomSelected = false;
            break;
        }
    if (allRoomSelected)
        console.log(bookingRoomRequest);
}