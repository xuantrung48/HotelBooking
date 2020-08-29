var service = {} || service;

$(document).ready(function () {
    service.init();
})
digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") + '₫';
}
service.init = function () {
    service.drawTable();
    service.validation();
}
service.validation = function () {
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
        'Must be greater than {1}.'
    );
    //jQuery.validator.addMethod("fileExtensions",
    //    function (file) {
    //        const acceptedImageTypes = ['image/gif', 'image/jpeg', 'image/png'];
    //        return file && $.inArray(file['type'], acceptedImageTypes)
    //    },
    //    'Must be image'
    //);
    $('#form').validate({
        rules: {
            ServiceName: {
                required: true,
                regex: /^[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]+(([',. -][a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ ])?[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]*)*$/
            },
            Price: {
                required: true,
                min: 0
            },
            Description: {
                required: true
            },
            ServiceImages: {
                extension: "jpg,jpeg,png"
            }
        },
        messages: {
            ServiceName: {
                required: "Bạn phải nhập tên dịch vụ",
                regex: "Tên dịch vụ không chứa chữ số và kí tự đặc biệt"
            },
            Price: {
                required: "Bạn phải nhập giá",
                min: "Giá tối thiểu là 0đ"
            },
            Description: {
                required: "Bạn phải nhập phần mô tả dịch vụ"
            },
            ServiceImages: "Bạn phải đưa ảnh vào"

        }
    })
}
service.drawTable = function () {
    $('#serviceTable').empty();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/ServicesManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#serviceTable').append(
                    `<tr>
                        <td>${v.serviceId}</td>
                        <td>${v.serviceName}</td>
                        <td>${digitGrouping(v.price)}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="service.get(${v.serviceId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="service.delete(${v.serviceId}, '${v.serviceName}')"><i class="fas fa-trash"></i></a>
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
    $('#Description').val('');
    $(".custom-file-label").text("Chọn tập tin");
    $("#imgsPreview").empty();
    $('#imgsData').empty();
}

service.get = function (id) {
    service.reset();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/ServicesManager/GetWithImages/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result.images, function (i, v) {
                $("#imgsData").append(
                    `<img src="${v.imageData}" style="height:150px" class="mx-2 my-2"><a class="remove-image" onclick="service.deleteImage('${v.serviceImageId}')" style="display: inline;">&#215;</a>`
                );
            });
            $('.modal-title').text('Đổi thông tin dịch vụ');
            $('#ServiceName').val(data.result.serviceName);
            $('#ServiceId').val(data.result.serviceId);
            $('#Price').val(data.result.price);
            $('#Description').val(data.result.description);
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

service.save = function () {
    if ($('#form').valid()) {
        var imgsNo = parseInt($("#imgsNo").val());
        var serviceObj = {};
        serviceObj.ServiceId = parseInt($('#ServiceId').val());
        serviceObj.ServiceName = $('#ServiceName').val().trim();
        serviceObj.Price = parseInt($('#Price').val());
        serviceObj.Description = $('#Description').val();
        serviceObj.Images = [];
        for (let i = 0; i < imgsNo; i++) {
            serviceObj.Images[i] = $(`#img${i}`).val();
        };
        $.ajax({
            beforeSend: function () {
                $('#modal-loader').css("visibility", "visible");
            },
            url: `/ServicesManager/Save/`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(serviceObj),
            success: function (data) {
                $('#mediumModal').modal('hide');
                bootbox.alert(data.result.message);
                service.drawTable();
            },
            complete: function () {
                $('#modal-loader').css("visibility", "hidden");
            }
        });
    }
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
                    beforeSend: function () {
                        $('.ajax-loader').css("visibility", "visible");
                    },
                    url: `/ServicesManager/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        service.drawTable();
                    },
                    complete: function () {
                        $('.ajax-loader').css("visibility", "hidden");
                    }
                });
            }
        }
    });
}

readFiles = function () {
    $("#imgsPreview").empty();

    if ($('#ServiceId').val() == '0') {
        $('#imgsData').empty();
    }

    if (this.files && this.files[0]) {
        for (let i = 0; i < this.files.length; i++) {
            var FR = new FileReader();

            FR.addEventListener("load", function (e) {
                $("#imgsPreview").append(
                    `<img src="${e.target.result}" style="height:150px" class="mx-2 my-2">`
                );
                $("#imgsPreview").append(
                    `<input hidden value="${e.target.result}" id="img${i}">`
                );
            });

            FR.readAsDataURL(this.files[i]);
        };
        $("#imgsPreview").append(
            `<input hidden value="${this.files.length}" id="imgsNo">`
        );
    }

}

service.deleteImage = function (serviceImageId) {
    bootbox.confirm({
        title: "Xoá ảnh",
        message: "Bạn có thực sự muốn xoá ảnh này?",
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
                        $('#modal-loader').css("visibility", "visible");
                    },
                    url: `/ServiceImage/Delete/${serviceImageId}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        $("#imgsData").empty();

                        $.ajax({
                            url: `/ServiceImage/GetByServiceId/${$('#ServiceId').val()}`,
                            method: "GET",
                            dataType: "json",
                            success: function (data) {
                                $.each(data.result, function (i, v) {
                                    $("#imgsData").append(
                                        `<img src="${v.imageData}" style="height:150px" class="mx-2 my-2"><a class="remove-image" onclick="service.deleteImage('${v.serviceImageId}')" style="display: inline;">&#215;</a>`
                                    );
                                });
                            }
                        });
                    },
                    complete: function () {
                        $('#modal-loader').css("visibility", "hidden");
                    }
                });
            }
        }
    });
}