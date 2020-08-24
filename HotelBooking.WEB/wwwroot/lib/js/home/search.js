var bookingRoom = {} || bookingRoom;

digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

$(document).ready(function () {
    bookingRoom.init();
})

bookingRoom.init = function () {
    showSelections();
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

showSelections = function () {
    var roomTypes;
    $.ajax({
        url: "/RoomTypesManager/GetAllWithImagesAndFacilities",
        method: "GET",
        dataType: "json",
        async: false,
        success: function (roomTypesData) {
            roomTypes = roomTypesData.result;
        },
    });
    var searchRequest = JSON.parse(localStorage.getItem('searchRequest'));
    $('#checkInDate').append(`Ngày nhận phòng: ${formatDate(new Date(searchRequest.CheckInDate))}`)
    $('#checkOutDate').append(`Ngày trả phòng: ${formatDate(new Date(searchRequest.CheckOutDate))}`)
    for (let i = 0; i < searchRequest.Rooms.length; i++) {
        $('#tbRoomBooking').append(
            `<div class="card my-2">
                <div class="row">
                    <div class="col-3 text-center my-auto">
                        <h4>Phòng ${i + 1}</h4>
                        <p>(${searchRequest.Rooms[i].Adults} người lớn, ${searchRequest.Rooms[i].Children} trẻ em)</p>
                    </div>
                    <div class="col-9" id="room${i}">
                    </div>
                </div>
            </div>
            <hr>`
        )
        let searchRequestObj = {};
        searchRequestObj.CheckInDate = new Date(searchRequest.CheckInDate);
        searchRequestObj.CheckOutDate = new Date(searchRequest.CheckOutDate);
        searchRequestObj.NumberOfAdults = searchRequest.Rooms[i].Adults;
        searchRequestObj.NumberOfChildren = searchRequest.Rooms[i].Children;
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
            },
        });
        for (let j = 0; j < roomTypeResult.length; j++) {
            for (let k = 0; k < roomTypes.length; k++) {
                if (roomTypeResult[j].roomTypeId == roomTypes[k].roomTypeId) {
                    let facilities = '';
                    for (let l = 0; l < roomTypes[k].facilities.length; l++)
                        facilities += `<img src=${roomTypes[k].facilities[l].facilityImage} class="mx-1 facilities">`

                    var roomPrice = '<div class="row"><div class="col-md-2 col-sm-4">Giá:</div><div class="col-lg-10 col-sm-8">';
                    for (let d = searchRequestObj.CheckInDate.getTime(); d < searchRequestObj.CheckOutDate.getTime(); d += 86400000) {
                        var date = new Date(d);
                        var getPromotionRequest = {};
                        getPromotionRequest.RoomTypeId = roomTypeResult[j].roomTypeId;
                        getPromotionRequest.Date = date;
                        console.log(getPromotionRequest);
                        $.ajax({
                            url: `/PromotionsManager/GetAvailableForDateAndRoomTypeId/`,
                            method: "POST",
                            dataType: "json",
                            contentType: "application/json",
                            async: false,
                            data: JSON.stringify(getPromotionRequest),
                            success: function (promotionData) {
                                console.log(promotionData);
                                if (promotionData.result.discountRates == 0) {
                                    roomPrice += `<div>${date.getDate()}/${date.getMonth() + 1}: ${digitGrouping(roomTypes[k].defaultPrice)}₫</div>`
                                } else {
                                    roomPrice += `<div>${date.getDate()}/${date.getMonth() + 1}: <del class="text-danger">${digitGrouping(roomTypes[k].defaultPrice)}₫</del> ${digitGrouping(roomTypes[k].defaultPrice * (1 - promotionData.result.discountRates))}₫</div>`
                                }
                            },
                        });
                    }
                    roomPrice += '</div></div>';
                    $(`#room${i}`).append(
                        `<div class="row mt-2">
                            <div class="col-lg-3">
                                <img class="w-75 my-auto" src="${roomTypes[k].image}">
                            </div>
                            <div class="col-lg-8">
                                <h4>${roomTypes[k].name}</h4>
                                <p>${roomTypes[k].description}</p>
                                <p>Tiện nghi: ${facilities}</p>
                                ${roomPrice}
                            </div>
                            <div class="col-lg-1 my-auto">
                                <input type="radio" name="room${i + 1}" value="${roomTypes[k].roomTypeId}">
                            </div>
                        </div>
                        <hr>`
                    )
                };
            }
        }
    }
}