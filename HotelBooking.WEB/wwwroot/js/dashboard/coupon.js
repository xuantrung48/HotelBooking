var coupon = {} || coupon;

$(document).ready(function () {
    coupon.init();
})

coupon.init = function () {
    coupon.drawTable();
}

coupon.drawTable = function () {
    $('#couponsTable').empty();
    $.ajax({
        url: "/Coupon/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#couponsTable').append(
                    `<tr>
                        <td>${v.couponId}</td>
                        <td>${v.couponCode}</td>
                        <td>${v.remain}</td>
                        <td>${v.reduction * 100}%</td>
                        <td>${dateToDMY(v.endDate)}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="coupon.get(${v.couponId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="coupon.delete(${v.couponId}, '${v.couponCode}')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>`
                );
            });
        }
    });
}

coupon.add = function () {
    coupon.reset();
    $('.modal-title').text('Thêm phiếu giảm giá');
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

coupon.reset = function () {
    $('#CouponCode').val('');
    $('#CouponId').val(0);
    $('#Remain').val('');
    $('#Reduction').val('');
    $('#EndDate').val('');
}

coupon.get = function (id) {
    coupon.reset();
    $.ajax({
        url: `/Coupon/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('.modal-title').text('Đổi thông tin phiếu giảm giá');
            $('#CouponCode').val(data.result.couponCode);
            $('#CouponId').val(data.result.couponId);
            $('#Remain').val(data.result.remain);
            $('#EndDate').val(dateToYMD(data.result.endDate));
            $('#Reduction').val(data.result.reduction * 100);
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        }
    });
}

coupon.save = function () {
    var couponObj = {};
    couponObj.CouponId = parseInt($('#CouponId').val());
    couponObj.CouponCode = $('#CouponCode').val();
    couponObj.Remain = parseInt($('#Remain').val());
    couponObj.EndDate = new Date ($('#EndDate').val());
    couponObj.Reduction = parseFloat($('#Reduction').val() / 100);
    $.ajax({
        url: `/Coupon/Save/`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(couponObj),
        success: function (data) {
            $('#mediumModal').modal('hide');
            bootbox.alert(data.result.message);
            coupon.drawTable();
        }
    });
}

coupon.delete = function (id, name) {
    bootbox.confirm({
        title: "Xoá khuyến mãi",
        message: 'Bạn có thực sự muốn xoá thẻ giảm giá "' + name + '"?',
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
                    url: `/Coupon/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        coupon.drawTable();
                    }
                });
            }
        }
    });
}
dateToDMY = function (date) {
    date = new Date(date);
    var d = date.getDate();
    var m = date.getMonth() + 1;
    var y = date.getFullYear();
    return '' + (d <= 9 ? '0' + d : d) + '-' + (m <= 9 ? '0' + m : m) + '-' + y;
}
dateToYMD = function (date) {
    date = new Date(date);
    var d = date.getDate();
    var m = date.getMonth() + 1;
    var y = date.getFullYear();
    return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d);
}