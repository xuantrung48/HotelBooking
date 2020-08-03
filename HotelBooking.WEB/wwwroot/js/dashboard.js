var roomType = {} || roomType;

$(document).ready(function () {
    roomType.init();
});

digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
};

roomType.init = function () {
    roomType.drawTable();
};

roomType.drawTable = function () {
    $.ajax({
        url: "/RoomType/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('#datasTable').empty();
            $.each(data.result, function (i, v) {
                let people = '';
                if (v.capacity % 1 == 0) {
                    for (let i = 0; i < v.capacity; i++) {
                        people += '<i class="fas fa-male" style="font-size: 1.5em"></i> ';
                    };
                }
                else {
                    for (let i = 0; i < v.capacity - 0.5; i++) {
                        people += '<i class="fas fa-male" style="font-size: 1.5em"></i> ';
                    };
                    people += '<i class="fas fa-child"></i>';
                }
                $('#datasTable').append(
                    `<tr>
                        <td>${v.roomTypeId}</td>
                        <td>${v.name}</td>
                        <td>${v.price}</td>
                        <td>${people}</td>
                        <td>${v.quantity}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="roomType.get(${v.roomTypeId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="roomType.delete(${v.roomTypeId}, '${v.name}')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>`
                );
            });
        }
    });
};

roomType.delete = function (id, name) {
    bootbox.confirm({
        title: "Xoá loại phòng",
        message: "Bạn có thực sự muốn xoá loại phòng " + name + "?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Huỷ'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Xác nhận'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: `/RoomType/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        roomType.drawTable();
                    }
                });
            }
        }
    });
};