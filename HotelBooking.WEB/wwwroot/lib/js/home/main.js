digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

showRoomTypes = function () {
    $.ajax({
        url: "/RoomType/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            console.log(data);
            /*for (let i = 0; i < data.result.length; i++) {
                console.log(i);
                $.ajax({
                    url: `/RoomTypeImage/GetByRoomTypeId/${data.result[i].roomTypeId}`,
                    method: "GET",
                    dataType: "json",
                    success: function (imgs) {
                        console.log(i);
                        console.log(imgs);
                        if (i = 0) {
                            $('.carousel-indicators').append(
                                `<li data-target="#roomTypes" data-slide-to="${i}" class="active"></li>`
                            );
                        } else {
                            $('.carousel-indicators').append(
                                `<li data-target="#roomTypes" data-slide-to="${i}"></li>`
                            );
                        }

                        if (i = 0) {
                            $('.carousel-inner').append(
                                `<div class="carousel-item active">
                                    <img src="${imgs.result[0].imageData}" alt="${data.result[i].name}" width="1100" height="500">
                                </div>`
                            );
                        } else {
                            $('.carousel-inner').append(
                                `<div class="carousel-item">
                                    <img src="${imgs.result[0].imageData}" alt="${data.result[i].name}" width="1100" height="500">
                                </div>`
                            );
                        }
                    }
                });
            }*/
            /*$.each(data.result, function (i, v) {
                console.log(i);
                $.ajax({
                    url: `/RoomTypeImage/GetByRoomTypeId/${v.roomTypeId}`,
                    method: "GET",
                    dataType: "json",
                    success: function (imgs) {
                        if (i = 0) {
                            $('.carousel-indicators').append(
                                `<li data-target="#roomTypes" data-slide-to="${i}" class="active"></li>`
                            );
                        } else {
                            $('.carousel-indicators').append(
                                `<li data-target="#roomTypes" data-slide-to="${i}"></li>`
                            );
                        }

                        if (i = 0) {
                            $('.carousel-inner').append(
                                `<div class="carousel-item active">
                                    <img src="${imgs.result[0].imageData}" alt="${v.name}" width="1100" height="500">
                                </div>`
                            );
                        } else {
                            $('.carousel-inner').append(
                                `<div class="carousel-item">
                                    <img src="${imgs.result[0].imageData}" alt="${v.name}" width="1100" height="500">
                                </div>`
                            );
                        }

                        *//*$('#roomTypes').append(
                            `<div class="col-lg-3 col-md-6 col-sm-6 p-0">
                                <div class="home__room__item set-bg" style="background-image: url('${imgs.result[0].imageData}');">
                                    <div class="home__room__title">
                                        <h4>${v.name}</h4>
                                        <h2>${digitGrouping(v.defaultPrice)}₫<span>/đêm</span></h2>
                                    </div>
                                    <a href="#">Đặt ngay</a>
                                </div>
                            </div>`
                        );*//*
                    }
                });
            });*/
        }
    });
}
