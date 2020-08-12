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
                        <td>${dateToDMY(v.checkinDate)}</td>
                        <td>${dateToDMY(v.checkoutDate)}</td>
                        <td>${digitGrouping(v.serviceAmount + v.roomAmount)}₫</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="booking.get(${v.bookingId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="booking.delete(${v.bookingId}, '${v.bookingCustomer.name}')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>`
                );
            });
        }
    });
}

booking.add = function () {
    booking.reset();
    $('.modal-title').text('Đặt phòng');
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

booking.reset = function () {
    $('#Name').val('');
    $('#BookingId').val(0);
    $('#CustomerId').val(0);
    $('#NumberofAdults').val('');
    $('#NumberofChildren').val('');
    $('#PhoneNumber').val('');
    $('#Email').val('');
    $('#CouponId').val('');
}

booking.get = function (id) {
    booking.reset();
    $.ajax({
        url: `/Booking/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('.modal-title').text('Đổi thông tin đặt phòng');
            $('#BookingId').val(data.result.bookingId);
            $('#CheckinDate').val(dateToYMD(data.result.checkinDate));
            $('#CheckoutDate').val(dateToYMD(data.result.checkoutDate));
            $('#Name').val(data.result.bookingCustomer.name);
            $('#PhoneNumber').val(data.result.bookingCustomer.phoneNumber);
            $('#Email').val(data.result.bookingCustomer.email);
            $('#NumberofAdults').val(data.result.numberofAdults);
            $('#NumberofChildren').val(data.result.numberofChildren);
            $('#CouponId').val(data.result.couponId);
            $('#CustomerId').val(data.result.customerId);
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        }
    });
}

booking.save = function () {
    var bookingObj = {};
    var customerObj = {};
    bookingObj.BookingId = parseInt($('#BookingId').val());
    //bookingObj.BookingCustomer.Name = $('#Name').val();
    //bookingObj.BookingCustomer.PhoneNumber = $('#PhoneNumber').val();
    //bookingObj.BookingCustomer.Email = $('#Email').val();
    //bookingObj.BookingCustomer.CustomerId = parseInt($('#CustomerId').val());
    customerObj.Name = $('#Name').val();
    customerObj.PhoneNumber = $('#PhoneNumber').val();
    customerObj.Email = $('#Email').val();
    customerObj.CustomerId = parseInt($('#CustomerId').val());
    bookingObj.NumberofChildren = parseInt($('#NumberofChildren').val());
    bookingObj.NumberofAdults = parseInt($('#NumberofAdults').val());
    bookingObj.CouponId = parseInt($('#CouponId').val());
    bookingObj.CustomerId = parseInt($('#CustomerId').val());
    bookingObj.CheckinDate = new Date($('#CheckinDate').val());
    bookingObj.CheckoutDate = new Date($('#CheckoutDate').val());
    bookingObj.BookingCustomer = customerObj;
    $.ajax({
        url: `/Booking/Save/`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(bookingObj),
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
        message: 'Bạn có thực sự muốn hủy đặt phòng của khách hàng ' + name + '?',
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
                    url: `/Booking/Delete/${id}`,
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
digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}