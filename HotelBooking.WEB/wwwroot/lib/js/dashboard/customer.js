var customer = {} || customer;

$(document).ready(function () {
    customer.init();
})

customer.init = function () {
    customer.drawTable();
    customer.validation();
}
customer.validation = function () {
    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            return this.optional(element) || regexp.test(value.trim());
        },
        "Please check your input."
    );
    $('#form').validate({
        rules: {
            Name: {
                required: true,
                regex: /^[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]+(([',. -][a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ ])?[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]*)*$/
            },
            PhoneNumber: {
                required: true,
                regex: /^\(?(0|[3|5|7|8|9])+([0-9]{5})$/,
            },
            Email: {
                required: true,
                regex: /^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/
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
            }
        }
    })
}
customer.drawTable = function () {
    $('#customersTable').empty();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/CustomersManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#customersTable').append(
                    `<tr>
                        <td>${v.customerId}</td>
                        <td>${v.name}</td>
                        <td>${v.phoneNumber}</td>
                        <td>${v.email}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="customer.get(${v.customerId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="customer.delete(${v.customerId}, '${v.name}')"><i class="fas fa-trash"></i></a>
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

customer.add = function () {
    customer.reset();
    $('.modal-title').text('Thêm khách hàng');
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

customer.reset = function () {
    $('#Name').val('');
    $('#CustomerId').val(0);
    $('#PhoneNumber').val('');
    $('#Email').val('');
}

customer.get = function (id) {
    customer.reset();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/CustomersManager/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('.modal-title').text('Đổi thông tin khách hàng');
            $('#Name').val(data.result.name);
            $('#CustomerId').val(data.result.customerId);
            $('#PhoneNumber').val(data.result.phoneNumber);
            $('#Email').val(data.result.email);
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

customer.save = function () {
    if ($('#form').valid()) {
        var customerObj = {};
        customerObj.CustomerId = parseInt($('#CustomerId').val());
        customerObj.Name = $('#Name').val().trim();
        customerObj.Email = $('#Email').val().trim();
        customerObj.PhoneNumber = $('#PhoneNumber').val().trim();
        $.ajax({
            beforeSend: function () {
                $('#modal-loader').css("visibility", "visible");
            },
            url: `/CustomersManager/Save/`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(customerObj),
            success: function (data) {
                $('#mediumModal').modal('hide');
                bootbox.alert(data.result.message);
                customer.drawTable();
            },
            complete: function () {
                $('#modal-loader').css("visibility", "hidden");
            }
        });
    }
}

customer.delete = function (id, name) {
    bootbox.confirm({
        title: "Xoá khuyến mãi",
        message: 'Bạn có thực sự muốn xoá khách hàng "' + name + '"?',
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
                    url: `/CustomersManager/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        customer.drawTable();
                    },
                    complete: function () {
                        $('.ajax-loader').css("visibility", "hidden");
                    }
                });
            }
        }
    });
}