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
                        $('#rooms').append(
                            `<div class="col-lg-3 col-md-6 col-sm-6 p-0">
                                <div class="home__room__item set-bg" style="background-image:url('${data.result[i].image}')">
                                    <div class="home__room__title roomInfo">
                                        <h4>${data.result[i].name}</h4>
                                        <p class="text-warning" id="price${data.result[i].roomTypeId}">${digitGrouping(data.result[i].defaultPrice)}₫<span>/đêm</span></p>
                                    </div>
                                    <a href="/BookingRoom/Index" class="roomInfo">Đặt ngay</a>
                                </div>
                            </div>`
                        );

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
        }
    });
}