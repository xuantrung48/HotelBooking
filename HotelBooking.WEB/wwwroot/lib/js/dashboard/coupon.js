var coupon = {} || coupon;

$(document).ready(function () {
    coupon.init();
})

coupon.init = function () {
    coupon.drawTable();
    coupon.validation();
}

coupon.validation = function () {
    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            return this.optional(element) || regexp.test(value.trim());
        },
        "Please check your input."
    );
    $('#form').validate({
        rules: {
            CouponCode: {
                required: true,
                regex: /^[A-Z]+$/
            },
            Remain: {
                required: true,
                min: 1
            },
            Reduction: {
                required: true,
                min: 1
            },
            EndDate: "required"
        },
        messages: {
            CouponCode: {
                required: "Bạn phải nhập code",
                regex: "Bạn phải nhập chữ hoa không có khoảng trắng"
            },
            Remain: {
                required: "Bạn phải nhập số lượnng",
                min: "Số lượng phải là số lớn hơn 1"
            },
            Reduction: {
                required: "Bạn phải nhập mức giảm giá",
                min: "Mức giảm giá là số lớn hơn 1"
            },
            EndDate: "Bạn phải nhập ngày tháng"
        }
    })
}
coupon.drawTable = function () {
    $('#couponsTable').empty();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/CouponsManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                let remain = (v.remain >= 0) ? v.remain : '∞'
                $('#couponsTable').append(
                    `<tr>
                        <td>${v.couponId}</td>
                        <td>${v.couponCode}</td>
                        <td class="text-center">${remain}</td>
                        <td class="text-center">${v.reduction * 100}%</td>
                        <td class="text-center">${dateToDMY(v.endDate)}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="coupon.get(${v.couponId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="coupon.delete(${v.couponId}, '${v.couponCode}')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>`
                );
            });
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
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
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/CouponsManager/Get/${id}`,
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
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

coupon.save = function () {
    if ($('#form').valid()) {
        var couponObj = {};
        couponObj.CouponId = parseInt($('#CouponId').val());
        couponObj.CouponCode = $('#CouponCode').val().trim();
        couponObj.Remain = parseInt($('#Remain').val());
        couponObj.EndDate = new Date($('#EndDate').val());
        couponObj.Reduction = parseFloat($('#Reduction').val() / 100);
        $.ajax({
            beforeSend: function () {
                $('#modal-loader').css("visibility", "visible");
            },
            url: `/CouponsManager/Save/`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(couponObj),
            success: function (data) {
                $('#mediumModal').modal('hide');
                bootbox.alert(data.result.message);
                coupon.drawTable();
            },
            complete: function () {
                $('#modal-loader').css("visibility", "hidden");
            }
        });
    }
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
                    beforeSend: function () {
                        $('.ajax-loader').css("visibility", "visible");
                    },
                    url: `/CouponsManager/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        coupon.drawTable();
                    },
                    complete: function () {
                        $('.ajax-loader').css("visibility", "hidden");
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