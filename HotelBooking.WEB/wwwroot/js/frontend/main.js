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
            $.each(data.result, function (i, v) {
                console.log(v.roomTypeId);
                $('#roomTypes').append(
                    `<div class="col-lg-3 col-md-6 col-sm-6 p-0">
                        <div class="home__room__item set-bg" data-setbg="${getRoomTypeImage(v.roomTypeId)}">
                            <div class="home__room__title">
                                <h4>${v.name}</h4>
                                <h2>${digitGrouping(v.defaultPrice)}₫<span>/đêm</span></h2>
                            </div>
                            <a href="#">Đặt ngay</a>
                        </div>
                    </div>`
                    /*`<tr>
                        <td>${v.roomTypeId}</td>
                        <td>${v.name}</td>
                        <td>${digitGrouping(v.defaultPrice)}</td>
                        <td>${people}</td>
                        <td>${v.quantity}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="roomType.get(${v.roomTypeId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="roomType.delete(${v.roomTypeId}, '${v.name}')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>`*/
                );
            });
        }
    });
}

getRoomTypeImage = function (roomTypeId) {
    $.ajax({
        url: `/RoomTypeImage/GetByRoomTypeId/${roomTypeId}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            /*imgsNo = data.result.length;*/
            /*$.each(data.result, function (i, v) {
                $("#imgsData").append(
                    `<img src="${v.imageData}" style="height:150px" class="mx-2 my-2"><a class="remove-image" onclick="roomType.deleteImage('${v.roomTypeImageId}')" style="display: inline;">&#215;</a>`
                );
            });*/
            console.log(data.result[0].imageData);
            return data.result[0].imageData;
        }
    });
}