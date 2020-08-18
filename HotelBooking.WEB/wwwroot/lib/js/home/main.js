digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

showRoomTypes = function () {
    $.ajax({
        beforeSend: function () {
            $('#ajax-loader-roomTypes').css("visibility", "visible");
        },
        url: "/RoomType/GetAllWithImages",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.ajax({
                url: `/Promotion/GetAvailable`,
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
                                        <p class="text-warning" id="price${data.result[i].roomTypeId}">${digitGrouping(data.result[i].defaultPrice)}₫<span>/đêm</span></p>
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
                                <p class="text-warning" id="price${data.result[i].roomTypeId}">${digitGrouping(data.result[i].defaultPrice)}₫<span>/đêm</span></p>
                                <a href="#bookroom">Đặt ngay</a>
                            </div>
                        </div>`
                            );
                        }
                        for (let k = 0; k < roomTypePromotions.result.length; k++) {
                            if (data.result[i].roomTypeId == roomTypePromotions.result[k].roomTypeId) {
                                $(`#price${data.result[i].roomTypeId}`).empty();
                                $(`#price${data.result[i].roomTypeId}`).append(
                                    `<small><del class="text-danger">${digitGrouping(data.result[i].defaultPrice)}₫</del></small> ${digitGrouping(data.result[i].defaultPrice * (1 - roomTypePromotions.result[k].discountRates))}₫/đêm`
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
        url: "/Service/GetAllWithImages",
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
                                <p class="text-warning"">${digitGrouping(data.result[i].price)}₫<span></span></p>
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
                                <p class="text-warning">${digitGrouping(data.result[i].price)}₫<span></span></p>
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