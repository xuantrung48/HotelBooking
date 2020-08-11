var booking = {} || booking;

$(document).ready(function () {
    booking.init();
})

booking.init = function () {
    booking.drawTable();
}
booking.drawTable = function () {
    $('#bookingTable').empty();
    $.ajax({
        url: "/Booking/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#bookingTable').append(
                    `<tr>
                        <td>${v.bookingId}</td>
                        <td>${v.bookingCustomer.name}</td>
                        <td>${dateToDMY(v.createDate)}</td>
                        <td>${v.serviceAmount}</td>
                        <td>${v.roomAmount}</td>
                        <td>${v.couponId != null ? v.bookingCoupon.couponCode : `Not applicable`}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="promotion.get(${v.bookingId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="promotion.delete(${v.bookingId}, '${v.bookingCustomer.name}')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>`
                );
            });
        }
    });
}

booking.add = function () {
    //booking.reset();
    $('.modal-title').text('Đặt phòng');
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

booking.reset = function () {
    $('#PromotionName').val('');
    $('#PromotionId').val(0);
    $('#DefaultPrice').val('');
    $('#StartDate').val('');
    $('#EndDate').val('');
}

booking.get = function (id) {
    booking.reset();
    $.ajax({
        url: `/Booking/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('.modal-title').text('Đổi thông tin đặt phòng');
            $('#PromotionName').val(data.result.promotionName);
            $('#PromotionId').val(data.result.promotionId);
            $('#DiscountRates').val(data.result.discountRates * 100);
            $('#StartDate').val(dateToYMD(data.result.startDate));
            $('#EndDate').val(dateToYMD(data.result.endDate));
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        }
    });
}

booking.save = function () {
    var promotionObj = {};
    promotionObj.PromotionId = parseInt($('#PromotionId').val());
    promotionObj.PromotionName = $('#PromotionName').val();
    promotionObj.StartDate = new Date($('#StartDate').val());
    promotionObj.EndDate = new Date($('#EndDate').val());
    promotionObj.DiscountRates = parseFloat($('#DiscountRates').val() / 100);
    $.ajax({
        url: `/Promotion/Save/`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(promotionObj),
        success: function (data) {
            $('#mediumModal').modal('hide');
            bootbox.alert(data.result.message);
            booking.drawTable();
        }
    });
}

booking.delete = function (id, name) {
    bootbox.confirm({
        title: "Xoá khuyến mãi",
        message: 'Bạn có thực sự muốn xoá chương trình khuyến mãi "' + name + '"?',
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
                    url: `/Promotion/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        booking.drawTable();
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