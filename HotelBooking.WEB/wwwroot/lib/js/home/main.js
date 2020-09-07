digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") + '₫';
}

showRoomTypes = function () {
    $.ajax({
        beforeSend: function () {
            $('#ajax-loader-roomTypes').css("visibility", "visible");
        },
        url: "/RoomTypesManager/GetAllWithImages",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.ajax({
                url: `/PromotionsManager/GetAvailable`,
                method: "GET",
                dataType: "json",
                success: function (roomTypePromotions) {
                    for (let i = 0; i < data.result.length; i++) {
                        if (i == 0) {
                            $('#roomTypeIndicators').append(
                                `<li data-target="#roomTypes" data-slide-to="${i}" class="active"></li>`
                            );
                            $('#roomTypeInner').append(
                                `<div class="carousel-item active">
                                    <img src="${data.result[i].image}" alt="${data.result[i].name}" width="100%" height="auto">
                                    <div class="carousel-caption">
                                        <h3><a href="/Rooms/Details/${data.result[i].roomTypeId}">${data.result[i].name}</a></h3>
                                        <p class="text-warning" id="price${data.result[i].roomTypeId}">${digitGrouping(data.result[i].defaultPrice)}<span>/đêm</span></p>
                                        <a href="#bookroom">Đặt ngay</a>
                                    </div>
                                </div>`
                            );
                        } else {
                            $('#roomTypeIndicators').append(
                                `<li data-target="#roomTypes" data-slide-to="${i}"></li>`
                            );
                            $('#roomTypeInner').append(
                                `<div class="carousel-item">
                            <img src="${data.result[i].image}" alt="${data.result[i].name}" width="100%" height="auto">
                            <div class="carousel-caption">
                                <h3><a href="/Rooms/Details/${data.result[i].roomTypeId}">${data.result[i].name}</a></h3>
                                <p class="text-warning" id="price${data.result[i].roomTypeId}">${digitGrouping(data.result[i].defaultPrice)}<span>/đêm</span></p>
                                <a href="#bookroom">Đặt ngay</a>
                            </div>
                        </div>`
                            );
                        }
                        for (let k = 0; k < roomTypePromotions.result.length; k++) {
                            if (data.result[i].roomTypeId == roomTypePromotions.result[k].roomTypeId) {
                                $(`#price${data.result[i].roomTypeId}`).empty();
                                $(`#price${data.result[i].roomTypeId}`).append(
                                    `<small><del class="text-danger">${digitGrouping(data.result[i].defaultPrice)}</del></small> ${digitGrouping(data.result[i].defaultPrice * (1 - roomTypePromotions.result[k].discountRates))}/đêm`
                                )
                            }
                        }
                    }
                }
            })
        },
        complete: function () {
            $('#ajax-loader-roomTypes').css("visibility", "hidden");
        }
    });
    $.ajax({
        beforeSend: function () {
            $('#ajax-loader-services').css("visibility", "visible");
        },
        url: "/ServicesManager/GetAllWithImages",
        method: "GET",
        dataType: "json",
        success: function (data) {
            for (let i = 0; i < data.result.length; i++) {
                if (i == 0) {
                    $('#servicesIndicators').append(
                        `<li data-target="#services" data-slide-to="${i}" class="active"></li>`
                    );
                    $('#servicesInner').append(
                        `<div class="carousel-item active">
                            <img src="${data.result[i].image}" alt="${data.result[i].serviceName}" width="100%" height="auto">
                            <div class="carousel-caption">
                                <h3 class="text-success">${data.result[i].serviceName}</h3>
                                <p class="text-warning"">${digitGrouping(data.result[i].price)}<span></span></p>
                            </div>
                        </div>`
                    );
                } else {
                    $('#servicesIndicators').append(
                        `<li data-target="#services" data-slide-to="${i}"></li>`
                    );
                    $('#servicesInner').append(
                        `<div class="carousel-item">
                            <img src="${data.result[i].image}" alt="${data.result[i].serviceName}" width="100%" height="auto">
                            <div class="carousel-caption">
                                <h3 class="text-success">${data.result[i].serviceName}</h3>
                                <p class="text-warning">${digitGrouping(data.result[i].price)}<span></span></p>
                            </div>
                        </div>`
                    );
                }
            }
        },
        complete: function () {
            $('#ajax-loader-services').css("visibility", "hidden");
        }
    });
}

$('#minusRoom').click(function () {
    if ($('#numberOfRooms').val() > 1) {
        $('#numberOfRooms').val(parseInt($('#numberOfRooms').val()) - 1);
    };
    changeNumberOfRooms();
});

$('#addRoom').click(function () {
    $('#numberOfRooms').val(parseInt($('#numberOfRooms').val()) + 1);
    changeNumberOfRooms();
});

changeNumberOfRooms = function () {
    $('#selectRooms').empty();
    var rooms = parseInt($('#numberOfRooms').val());
    for (let i = 0; i < rooms; i++) {
        $('#selectRooms').append(
            `<h4>Phòng ${i + 1}:</h4>
            <div class"row">
                <p class="col-4 d-inline-block">Người lớn:</p>
                <input type="button" class="col-1 d-inline-block bg-dark text-light" value="  -" onclick="minusAdults(${i + 1})">
                <input type="number" readonly value="1" step="1" min="1" id="adults${i + 1}" class="col-3 d-inline-block" inputmode="numeric" />
                <input type="button" class="col-1 d-inline-block bg-dark text-light" value="  +" onclick="addAdults(${i + 1})">
            </div>
            <div class"row">
                <p class="col-4 d-inline-block">Trẻ em:</p>
                <input type="button" class="col-1 d-inline-block bg-dark text-light" value="  -" onclick="minusChildren(${i + 1})">
                <input type="number" readonly value="0" step="1" min="1" id="children${i + 1}" class="col-3 d-inline-block" inputmode="numeric" />
                <input type="button" class="col-1 d-inline-block bg-dark text-light" value="  +" onclick="addChildren(${i + 1})">
            </div>`
        );
    };
    changePeople();
}

changePeople = function () {
    var rooms = parseInt($('#numberOfRooms').val());
    $('#NumberOfRooms').text(rooms);
    var adults = 0;
    var children = 0
    for (let i = 0; i < rooms; i++) {
        adults += parseInt($(`#adults${i + 1}`).val());
        children += parseInt($(`#children${i + 1}`).val());
    };
    $('#totalPeople').text(`(${adults} người lớn, ${children} trẻ em)`);
    localStorage.setItem('numofPeople', JSON.stringify({ 'adults': adults,'children': children}));
}

minusAdults = function (room) {
    if ($(`#adults${room}`).val() > 1) {
        $(`#adults${room}`).val(parseInt($(`#adults${room}`).val()) - 1);
        changePeople();
    };
}

addAdults = function (room) {
    $(`#adults${room}`).val(parseInt($(`#adults${room}`).val()) + 1);
    changePeople();
}

minusChildren = function (room) {
    if ($(`#children${room}`).val() > 0) {
        $(`#children${room}`).val(parseInt($(`#children${room}`).val()) - 1);
        changePeople();
    };
}

addChildren = function (room) {
    $(`#children${room}`).val(parseInt($(`#children${room}`).val()) + 1);
    changePeople();
}

Search = function () {
    var searchRequest = {};
    searchRequest.CheckInDate = (new Date(convertDateFormat($('.check__in').val()))).getTime() + (7 * 60 * 60 * 1000);
    searchRequest.CheckOutDate = (new Date(convertDateFormat($('.check__out').val()))).getTime() + (7 * 60 * 60 * 1000);
    searchRequest.RoomTypeSearchRequests = [];
    for (let i = 0; i < parseInt($('#numberOfRooms').val()); i++) {
        searchRequest.RoomTypeSearchRequests[i] = {};
        searchRequest.RoomTypeSearchRequests[i].Adults = parseInt($(`#adults${i + 1}`).val());
        searchRequest.RoomTypeSearchRequests[i].Children = parseInt($(`#children${i + 1}`).val());
    }
    localStorage.setItem('searchRequest', JSON.stringify(searchRequest));
    location.replace("/Search")
}

convertDateFormat = function (date) {
    var ddSplit = date.split('-');
    return (ddSplit[1] + '-' + ddSplit[0] + '-' + ddSplit[2]);
}