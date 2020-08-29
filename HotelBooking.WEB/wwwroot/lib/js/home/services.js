showServices = function () {
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/ServicesManager/GetAllWithImages",
        method: "GET",
        dataType: "json",
        success: function (data) {
            console.log(data);
            for (let i = 0; i < data.result.length; i++) {
                $('#services').append(
                    `<div class="col-lg-3 col-md-6 col-sm-6 p-0">
                        <div class="home__room__item set-bg" style="background-image:url('${data.result[i].image}')">
                            <div class="home__room__title roomInfo">
                                <h4>${data.result[i].serviceName}</h4>
                                <p class="text-warning">${digitGrouping(data.result[i].price)}</p>
                            </div>
                            <a href="#bookroom" class="roomInfo">Đặt ngay</a>
                        </div>
                    </div>`
                );
            }
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}