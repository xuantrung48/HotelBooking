digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

showRoomTypes = function () {
    $.ajax({
        url: "/RoomType/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $.ajax({
                    url: `/RoomTypeImage/GetByRoomTypeId/${v.roomTypeId}`,
                    method: "GET",
                    dataType: "json",
                    success: function (imgs) {
                        console.log(imgs.result[0].imageData);
                        $('#roomTypes').append(
                            `<div class="col-lg-3 col-md-6 col-sm-6 p-0">
                                <div class="home__room__item set-bg" data-setbg="${imgs.result[0].imageData}">
                                    <div class="home__room__title">
                                        <h4>${v.name}</h4>
                                        <h2>${digitGrouping(v.defaultPrice)}₫<span>/đêm</span></h2>
                                    </div>
                                    <a href="#">Đặt ngay</a>
                                </div>
                            </div>`
                        );
                    }
                });
            });
        }
    });
}
