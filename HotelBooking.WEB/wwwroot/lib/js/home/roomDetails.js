showRoomDetails = function (id) {
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/RoomTypesManager/GetWithImagesAndFacilities/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('title').text(data.result.name);
            for (let i = 0; i < data.result.images.length; i++) {
                if (i == 0) {
                    $('.carousel-indicators').append(
                        `<li data-target="#roomImages" data-slide-to="${i}" class="active"></li>`
                    );
                    $('.carousel-inner').append(
                        `<div class="carousel-item active">
                            <img src="${data.result.images[i].imageData}" alt="${data.result.name}" width="100%" height="auto">
                        </div>`
                    );
                } else {
                    $('.carousel-indicators').append(
                        `<li data-target="#roomImages" data-slide-to="${i}"></li>`
                    );
                    $('.carousel-inner').append(
                        `<div class="carousel-item">
                            <img src="${data.result.images[i].imageData}" alt="${data.result.name}" width="100%" height="auto">
                        </div>`
                    );
                }
            }
            $(`#roomPrice`).append(`${digitGrouping(data.result.defaultPrice)}<span class="text-secondary">/đêm</span>`);
            $.ajax({
                url: `/PromotionsManager/GetAvailable`,
                method: "GET",
                dataType: "json",
                success: function (roomTypePromotions) {
                    $.each(roomTypePromotions.result, function (i, v) {
                        if (data.result.roomTypeId == v.roomTypeId) {
                            $('#roomPrice').empty();
                            $('#roomPrice').append(
                                `<small><del class="text-danger">${digitGrouping(data.result.defaultPrice)}</del></small> ${digitGrouping(data.result.defaultPrice * (1 - v.discountRates))}<span class="text-secondary">/đêm</span>&nbsp;<sup class="badge badge-danger">Khuyến mãi</sup>`
                            )
                        }
                    });
                }
            });
            $('#roomName').html(data.result.name);
            $('#description').append(data.result.description);
            $.each(data.result.facilities, function (i, v) {
                $('#facilities').append(
                    `<div class="col-xl-2 col-lg-3 col-md-4 col-6 room__details__more__facilities__item">
                        <div class="icon">
                            <img src="${v.facilityImage}" alt="${v.facilityName}">
                        </div>
                        <h6>${v.facilityName}</h6>
                    </div>`
                )
            });
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}