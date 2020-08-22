var facility = {} || facility;

$(document).ready(function () {
    facility.init();
})

facility.init = function () {
    facility.drawTable();
    facility.validation();
}
facility.validation = function () {
    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            return this.optional(element) || regexp.test(value.trim());
        },
        "Please check your input."
    );
    $('#form').validate({
        rules: {
            FacilityName: {
                required: true,
                regex: /^[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]+(([',. -][a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ ])?[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]*)*$/
            },
            FacilityImage: {
                extension: "jpg,jpeg,png"
            }
        },
        messages: {
            FacilityName: {
                required: "Bạn phải nhập tên tiện nghi",
                regex: "Tên tiện nghi chứa chữ số và kí tự đặc biệt"
            },
            FacilityImage: {
                extension: "Bạn phải đưa một đường dẫn ảnh"
            }
        }
    })
}
facility.drawTable = function () {
    $('#facilitiesTable').empty();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/FacilitiesManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#facilitiesTable').append(
                    `<tr>
                        <td>${v.facilityName}</td>
                        <td><img src="${v.facilityImage}" height="50"></td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="facility.get(${v.facilityId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="facility.delete(${v.facilityId}, '${v.facilityName}')"><i class="fas fa-trash"></i></a>
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

facility.get = function (id) {
    console.log(id);
    facility.reset();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/FacilitiesManager/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            console.log(data);
            $('.modal-title').text('Đổi thông tin tiện nghi');
            $('#FacilityName').val(data.result.facilityName.trim());
            $('#FacilityId').val(data.result.facilityId);
            $('#FacilityImage').val(data.result.facilityImage);
            $('#imgPreview').append(
                `<img src=${data.result.facilityImage} height="50">`
            );
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

facility.save = function () {
    if ($('#form').valid) {
        var facilityObj = {};
        facilityObj.FacilityName = $('#FacilityName').val().trim();
        facilityObj.FacilityId = parseInt($('#FacilityId').val());
        facilityObj.FacilityImage = $('#FacilityImage').val();
        $.ajax({
            beforeSend: function () {
                $('#modal-loader').css("visibility", "visible");
            },
            url: `/FacilitiesManager/Save/`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(facilityObj),
            success: function (data) {
                $('#mediumModal').modal('hide');
                bootbox.alert(data.result.message);
                facility.drawTable();
            },
            complete: function () {
                $('#modal-loader').css("visibility", "hidden");
            }
        });
    }
}

facility.delete = function (id, name) {
    bootbox.confirm({
        title: "Xoá tiện nghi",
        message: 'Bạn có thực sự muốn xoá tiện nghi "' + name + '"?',
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
                    url: `/FacilitiesManager/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        facility.drawTable();
                    },
                    complete: function () {
                        $('.ajax-loader').css("visibility", "hidden");
                    }
                });
            }
        }
    });
}

facility.add = function () {
    facility.reset();
    $('.modal-title').text('Thêm tiện nghi');
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

facility.reset = function () {
    $('#FacilityName').val('');
    $('#FacilityId').val(0);
    $('#FacilityImage').val('');
    $('#imgPreview').empty();
    $('#inp').val('');
}


readFile = function () {
    $('#imgPreview').empty();
    if (this.files && this.files[0]) {

        var FR = new FileReader();

        FR.addEventListener("load", function (e) {
            $('#imgPreview').append(
                `<img src=${e.target.result} height="50">`
            );
            $('#FacilityImage').val(e.target.result);
        });

        FR.readAsDataURL(this.files[0]);
    }
}

document.getElementById("inp").addEventListener("change", readFile);

$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});