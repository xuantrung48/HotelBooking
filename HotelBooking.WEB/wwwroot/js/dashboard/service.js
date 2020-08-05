var service = {} || service;

$(document).ready(function () {
    service.init();
})
digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}
service.init = function () {
    service.drawTable();
}

service.drawTable = function () {
    $('#serviceTable').empty();
    $.ajax({
        url: "/Service/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#serviceTable').append(
                    `<tr>
                        <td>${v.serviceId}</td>
                        <td>${v.serviceName}</td>
                        <td>${digitGrouping(v.price)}&#8363;</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="service.get(${v.serviceId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="service.delete(${v.serviceId}, '${v.serviceName}')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>`
                );
            });
        }
    });
}

service.add = function () {
    service.reset();
    $('.modal-title').text('Thêm dịch vụ');
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

service.reset = function () {
    $('#ServiceName').val('');
    $('#ServiceId').val(0);
    $('#Price').val('');
}

service.get = function (id) {
    service.reset();
    $.ajax({
        url: `/Service/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('.modal-title').text('Đổi thông tin dịch vụ');
            $('#ServiceName').val(data.result.serviceName);
            $('#ServiceId').val(data.result.serviceId);
            $('#Price').val(data.result.price);
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        }
    });
}

service.save = function () {
    var serviceObj = {};
    serviceObj.ServiceId = parseInt($('#ServiceId').val());
    serviceObj.ServiceName = $('#ServiceName').val();
    serviceObj.Price = parseInt($('#Price').val());
    $.ajax({
        url: `/Service/Save/`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(serviceObj),
        success: function (data) {
            $('#mediumModal').modal('hide');
            bootbox.alert(data.result.message);
            service.drawTable();
        }
    });
}
service.delete = function (id, name) {
    bootbox.confirm({
        title: "Xoá dịch vụ",
        message: 'Bạn có thực sự muốn xoá dịch vụ "' + name + '"?',
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
                    url: `/Service/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        service.drawTable();
                    }
                });
            }
        }
    });
}
