var bookingRoom = {} || bookingRoom;

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
    var rooms;
    $.ajax({
        url: "/RoomTypesManager/GetAllWithImagesAndFacilities",
        method: "GET",
        dataType: "json",
        success: function (data) {
            rooms = data.result;
            console.log(rooms);
            var searchRequest = JSON.parse(localStorage.getItem('searchRequest'));
            $('#checkInDate').append(`Ngày nhận phòng: ${formatDate(new Date(searchRequest.CheckInDate))}`)
            $('#checkOutDate').append(`Ngày trả phòng: ${formatDate(new Date(searchRequest.CheckOutDate))}`)
            for (let i = 0; i < searchRequest.Rooms.length; i++) {
                $('#tbRoomBooking').append(
                    `<div class="card my-2">
                        <div class="row">
                            <div class="col-3 text-center my-auto"">
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
                $.ajax({
                    url: `/RoomTypesManager/Search/`,
                    method: "POST",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify(searchRequestObj),
                    success: function (data) {
                        console.log(data.result);
                        for (let j = 0; j < data.result.length; j++) {
                            for (let k = 0; k < rooms.length; k++) {
                                let facilities = '';
                                for (let l = 0; l < rooms[k].facilities.length; l++) {
                                    facilities += `<img src=${rooms[k].facilities[l].facilityImage} class="mx-1">`
                                }
                                if (data.result[j].roomTypeId == rooms[k].roomTypeId) {
                                    $(`#room${i}`).append(
                                        `<div class="row mt-2">
                                            <div class="col-lg-3">
                                                <img class="w-75 my-auto" src="${rooms[k].image}">
                                            </div>
                                            <div class="col-lg-8">
                                                <h4>${rooms[k].name}</h4>
                                                <p>${rooms[k].description}</p>
                                                <p>Tiện nghi: ${facilities}</p>
                                            </div>
                                            <div class="col-lg-1 my-auto">
                                                <input type="radio" name="room${i + 1}" value="${rooms[k].roomTypeId}">
                                            </div>
                                        </div>
                                        <hr>`
                                    )
                                }
                            }
                        }
                    },
                });
            }
        },
    })
}