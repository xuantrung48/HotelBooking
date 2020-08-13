digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

showRoomTypes = function () {
    $.ajax({
        url: "/Promotion/GetAll",
        method: "GET",
        dataType: "json",
        success: function (promotions) {
            $.ajax({
                url: "/RoomType/GetAllWithImagesAndFacilities",
                method: "GET",
                dataType: "json",
                success: function (data) {
                    var now = new Date();
                    for (let i = 0; i < data.result.length; i++) {
                        if (i == 0) {
                            $('.carousel-indicators').append(
                                `<li data-target="#roomTypes" data-slide-to="${i}" class="active"></li>`
                            );
                            $('.carousel-inner').append(
                                `<div class="carousel-item active">
                                    <img src="${data.result[i].images[0].imageData}" alt="${data.result[i].name}" width="100%" height="auto">
                                    <div class="carousel-caption">
                                        <h3 class="text-success">${data.result[i].name}</h3>
                                        <p class="text-warning" id="price${data.result[i].roomTypeId}">${digitGrouping(data.result[i].defaultPrice)}₫<span>/đêm</span></p>
                                        <a href="/#bookroom">Đặt ngay</a>
                                    </div>
                                </div>`
                                    );
                                } else {
                                    $('.carousel-indicators').append(
                                        `<li data-target="#roomTypes" data-slide-to="${i}"></li>`
                                    );
                                    $('.carousel-inner').append(
                                        `<div class="carousel-item">
                                    <img src="${data.result[i].images[0].imageData}" alt="${data.result[i].name}" width="100%" height="auto">
                                    <div class="carousel-caption">
                                        <h3 class="text-success">${data.result[i].name}</h3>
                                        <p class="text-warning" id="price${data.result[i].roomTypeId}">${digitGrouping(data.result[i].defaultPrice)}₫<span>/đêm</span></p>
                                        <a href="/#bookroom">Đặt ngay</a>
                                    </div>
                                </div>`
                            );
                        }
                        $.ajax({
                            url: `/PromotionApply/GetByRoomTypeId/${data.result[i].roomTypeId}`,
                            method: "GET",
                            dataType: "json",
                            success: function (roomTypePromotions) {
                                for (let j = 0; j < promotions.result.length; j++) {
                                    for (let k = 0; k < roomTypePromotions.result.length; k++) {
                                        if (promotions.result[j].promotionId == roomTypePromotions.result[k].promotionId && now >= new Date(promotions.result[j].startDate) && now <= new Date(promotions.result[j].endDate)) {
                                            $(`#price${data.result[i].roomTypeId}`).empty();
                                            $(`#price${data.result[i].roomTypeId}`).append(
                                                `<small><del class="text-danger">${digitGrouping(data.result[i].defaultPrice)}₫</del></small> ${digitGrouping(data.result[i].defaultPrice * promotions.result[j].discountRates)}₫`
                                            )
                                        }
                                    }
                                }
                            }
                        })
                    }
                }
            });
        }
    })
}
