var booking = {} || booking;

$(document).ready(function () {
    booking.init();
})

booking.init = function () {
    booking.drawTable();
    booking.validation();
}
booking.validation = function () {
    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            return this.optional(element) || regexp.test(value.trim());
        },
        "Please check your input."
    );
    jQuery.validator.addMethod("greaterThan",
        function (value, element, params) {
        if (!/Invalid|NaN/.test(new Date(value))) {
            return new Date(value) > new Date($(params[0]).val());
        }
        return isNaN(value) && isNaN($(params[0]).val()) || (Number(value) > Number($(params[0]).val()));
    },
        'Must be greater than {1}.');
    //$('#Name').val(`${$('#Name').val().trim()}`);
    $('#form').validate({
        rules: {
            Name: {
                required: true,
                regex:  /^[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]+(([',. -][a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ ])?[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]*)*$/
            },
            PhoneNumber: {
                required: true,
                regex: /^\(?(0|[3|5|7|8|9])+([0-9]{8})$/,

            },
            Email: {
                required: true,
                regex: /^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/
            },
            NumberofAdults: {
                required: true,
                min: 1
            },
            NumberofChildren: {
                required: true,
                min: 0
            },
            CheckinDate: "required",
            CheckoutDate: {
                required: true,
                greaterThan: ["#CheckinDate", "CheckinDate"]
            },
            RoomType: {
                required: true
            },
            RoomQuantity: {
                required: true,
                min: 1
            },
            ServiceType: {
                required: true
            },
            ServiceQuantity: {
                required: true,
                min: 1
            }
        },
        messages: {
            Name: {
                required: "Bạn phải nhập tên khách hàng",
                regex: "Tên khách hàng không chứa chữ số và kí tự đặc biệt"
            },
            PhoneNumber: {
                required: "Bạn phải nhập số điện thoại",
                regex: "Số điện thoại không hợp lệ",
                range: "Số điện thoại không quá 10 số"
            },
            Email: {
                required: "Bạn phải nhập địa chỉ email",
                regex: "Địa chỉ email không hợp lệ"
            },
            NumberofAdults: {
                required: "Bạn phải nhập số lượng người lớn",
                min: "Số lượng người lớn tối thiểu là 1"
            },
            NumberofChildren: {
                min: "Số lượng trẻ em tối thiểu là 0"
            },
            CheckinDate: "Bạn phải nhập ngày đến",
            CheckoutDate: {
                required: "Bạn phải nhập ngày ngày đi",
                greaterThan: "Ngày đi phải sau ngày đến"
            },
            RoomQuantity: {
                required: "Bạn phải nhập số lượng",
                min: "Số lượng tối thiểu là 1"
            },
            ServiceQuantity: {
                required: "Bạn phải nhập số lượng",
                min: "Số lượng tối thiểu là 1"
            },
            RoomType: {
                required: "Chọn một loại phòng"
            },
            ServiceType: {
                required: "Chọn một loại dịch vụ"
            }
        }
    })
}
booking.drawTable = function () {
    $('#bookingTable').empty();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/BookingsManager/GetAll",
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
                        <td>${digitGrouping(v.serviceAmount + v.roomAmount)}</td>
                        <td>
                            <a href="BookingsManager/BookingDetails/${v.bookingId}" class="btn btn-primary"
                                       ><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="booking.delete(${v.bookingId}, '${v.bookingCustomer.name}')"><i class="fas fa-trash"></i></a>
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
//onclick = "return booking.get(${v.bookingId})"

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
    $('#CheckinDate').val('');
    $('#CheckoutDate').val('');
}

booking.get = function (id) {
    booking.reset();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/BookingsManager/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            //location.assign(`Booking/BookingDetails/${id}`);
            //$('.modal-title').text('Đổi thông tin đặt phòng');
            $('#BookingId').val(data.result.bookingId);
            $('#CheckinDate').val(dateToYMD(data.result.checkinDate));
            $('#CheckoutDate').val(dateToYMD(data.result.checkoutDate));
            $('#Name').val(data.result.bookingCustomer.name).trim();
            $('#PhoneNumber').val(data.result.bookingCustomer.phoneNumber).trim();
            $('#Email').val(data.result.bookingCustomer.email).trim();
            $('#NumberofAdults').val(data.result.numberofAdults);
            $('#NumberofChildren').val(data.result.numberofChildren);
            $('#CouponId').val(data.result.couponId);
            $('#CustomerId').val(data.result.customerId);
            //$('#mediumModal').appendTo("body");
            //$('#mediumModal').modal('show');
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });

}

booking.save = function () {
    var bookingObj = {};
    var customerObj = {};
    var serviceDetails = [];
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
    bookingObj.bookingServiceDetails = $('#ServiceDetails').val();
    bookingObj.BookingCustomer = customerObj;
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/BookingsManager/Save/`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(bookingObj),
        success: function (data) {
            bootbox.alert(data.result.message, function () {
                location.assign(`/Booking`);
            });
            booking.drawTable();
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

booking.delete = function (id, name) {
    bootbox.confirm({
        title: "Xoá đặt phòng",
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
                    beforeSend: function () {
                        $('.ajax-loader').css("visibility", "visible");
                    },
                    url: `/BookingsManager/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        $.ajax({
                            url: `/BookingServiceDetails/DeleteByBookingId/${id}`,
                            method: "GET",
                            dataType: "json"
                        });
                        $.ajax({
                            url: `/BookingRoomDetails/DeleteByBookingId/${id}`,
                            method: "GET",
                            dataType: "json"
                        });
                        booking.drawTable();
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
digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}