digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

showRoomTypes = function () {
    $.ajax({
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
                            $('.carousel-indicators').append(
                                `<li data-target="#roomTypes" data-slide-to="${i}" class="active"></li>`
                            );
                            $('.carousel-inner').append(
                                `<div class="carousel-item active">
                                    <img src="${data.result[i].image}" alt="${data.result[i].name}" width="100%" height="auto">
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
                            <img src="${data.result[i].image}" alt="${data.result[i].name}" width="100%" height="auto">
                            <div class="carousel-caption">
                                <h3 class="text-success">${data.result[i].name}</h3>
                                <p class="text-warning" id="price${data.result[i].roomTypeId}">${digitGrouping(data.result[i].defaultPrice)}₫<span>/đêm</span></p>
                                <a href="/#bookroom">Đặt ngay</a>
                            </div>
                        </div>`
                            );
                        }
                    }
                    for (let j = 0; j < data.result.length; j++) {
                        for (let k = 0; k < roomTypePromotions.result.length; k++) {
                            if (data.result[j].roomTypeId == roomTypePromotions.result[k].roomTypeId) {
                                $(`#price${data.result[j].roomTypeId}`).empty();
                                $(`#price${data.result[j].roomTypeId}`).append(
                                    `<small><del class="text-danger">${digitGrouping(data.result[j].defaultPrice)}₫</del></small> ${digitGrouping(data.result[j].defaultPrice * (1 - roomTypePromotions.result[k].discountRates))}₫/đêm`
                                )
                            }
                        }
                    }
                }
            })
        }
    });
}