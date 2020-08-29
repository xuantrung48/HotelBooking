var roomType = {} || roomType;

$(document).ready(function () {
    roomType.init();
})

digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") + '₫';
}

roomType.init = function () {
    roomType.drawTable();
    roomType.validation();
}
roomType.validation = function () {
    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            return this.optional(element) || regexp.test(value);
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
    $('#form').validate({
        rules: {
            Name: {
                required: true,
                regex: /^[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ ]+(([',. -][a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ ])?[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]*)*$/
            },
            DefaultPrice: {
                required: true,
                min: 1
            },
            adult: {
                required: true,
                min: 1
            },
            children: {
                required: true,
                min: 0
            },
            people: {
                required: true,
                min: 1
            },
            Quantity: {
                required: true,
                min: 1
            },
            facilities: {
                required: true
            },
            Description: {
                required: true
            },
            RoomTypeImages: {
                extension: "jpg,jpeg,png"
            }
        },
        messages: {
            Name: {
                required: "Bạn phải nhập tên loại phòng",
                regex: "Tên loại phòng không chứa chữ số và kí tự đặc biệt"
            },
            DefaultPrice: {
                required: "Phải nhập giá",
                min: "Giá tối thiểu là 10.000đ"
            },
            adult: {
                required: "Bạn phải nhập số lượng người lớn",
                min: "Số lượng người lớn tối thiểu là 1"
            },
            children: {
                required: "Bạn phải nhập số lượng trẻ em",
                min: "Số lượng trẻ em tối thiểu là 0"
            },
            people: {
                required: "Bạn phải nhập số tổng số người",
                min: "Tổng số người tối thiểu là 1"
            },
            Quantity: {
                required: "Bạn phải nhập số lượng",
                min: "Số lượng tối thiểu là 1"
            },
            facilities: {
                required: "Chọn một tiện nghi"
            },
            Description: {
                required: "Chọn một loại dịch vụ"
            },
            RoomTypeImages: {
                extension: "Bạn phải đưa ảnh vào"
            }
        }
    })
}
roomType.drawTable = function () {
    $('#roomTypesTable').empty();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/RoomTypesManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#roomTypesTable').append(
                    `<tr>
                        <td>${v.roomTypeId}</td>
                        <td>${v.name}</td>
                        <td class="text-center">${digitGrouping(v.defaultPrice)}</td>
                        <td class="text-center">${v.quantity}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="roomType.get(${v.roomTypeId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="roomType.delete(${v.roomTypeId}, '${v.name}')"><i class="fas fa-trash"></i></a>
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

roomType.get = function (id) {
    roomType.reset();

    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/RoomTypesManager/GetWithImagesAndFacilities/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result.images, function (i, v) {
                $("#imgsData").append(
                    `<img src="${v.imageData}" style="height:150px" class="mx-2 my-2"><a class="remove-image" onclick="roomType.deleteImage('${v.roomTypeImageId}')" style="display: inline;">&#215;</a>`
                );
            });
            $.ajax({
                url: `/FacilitiesManager/GetAll`,
                method: "GET",
                dataType: "json",
                success: function (facilities) {
                    $.each(facilities.result, function (i, f) {
                        $('#facilities').append(
                            `<option value="${f.facilityId}" id="facility${f.facilityId}">${f.facilityName}</option>`
                        );
                        $.each(data.result.facilities, function (j, u) {
                            if (f.facilityId == u.facilityId) {
                                $(`#facility${u.facilityId}`).attr('selected', 'selected');
                            }
                        })
                    })
                    $('#facilities').select2();
                }
            });
            $('.modal-title').text('Đổi thông tin loại phòng');
            $('#Name').val(data.result.name);
            $('#RoomTypeId').val(data.result.roomTypeId);
            $('#DefaultPrice').val(data.result.defaultPrice);
            $('#Description').val(data.result.description);
            $('#adult').val(data.result.maxAdult);
            $('#children').val(data.result.maxChildren);
            $('#people').val(data.result.maxPeople);
            $('#Quantity').val(data.result.quantity);
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

roomType.save = function () {
    if ($('#form').valid()) {
        var imgsNo = parseInt($("#imgsNo").val());
        var roomTypeObj = {};
        roomTypeObj.Name = $('#Name').val().trim();
        roomTypeObj.RoomTypeId = parseInt($('#RoomTypeId').val());
        roomTypeObj.DefaultPrice = parseInt($('#DefaultPrice').val());
        roomTypeObj.MaxAdult = parseInt($('#adult').val());
        roomTypeObj.MaxChildren = parseInt($('#children').val());
        roomTypeObj.MaxPeople = parseInt($('#people').val());
        roomTypeObj.Quantity = parseInt($('#Quantity').val());
        roomTypeObj.Description = $('#Description').val();
        roomTypeObj.Facilities = $('#facilities').val();
        roomTypeObj.Images = [];
        for (let i = 0; i < imgsNo; i++) {
            roomTypeObj.Images[i] = $(`#img${i}`).val();
        };
        if (roomTypeObj.RoomTypeId != 0) {
            $.ajax({
                url: `/FacilityApply/DeleteByRoomTypeId/${roomTypeObj.RoomTypeId}`,
                method: "GET",
                dataType: "json"
            });
        }
        $.ajax({
            beforeSend: function () {
                $('#modal-loader').css("visibility", "visible");
            },
            url: `/RoomTypesManager/Save/`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(roomTypeObj),
            success: function (data) {
                $('#mediumModal').modal('hide');
                bootbox.alert(data.result.message);
                roomType.drawTable();
            },
            complete: function () {
                $('#modal-loader').css("visibility", "hidden");
            }
        });
    }
}

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
                    beforeSend: function () {
                        $('.ajax-loader').css("visibility", "visible");
                    },
                    url: `/RoomTypesManager/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        roomType.drawTable();
                    },
                    complete: function () {
                        $('.ajax-loader').css("visibility", "hidden");
                    }
                });
            }
        }
    });
}

roomType.add = function () {
    roomType.reset();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/FacilitiesManager/GetAll`,
        method: "GET",
        dataType: "json",
        success: function (facilities) {
            $.each(facilities.result, function (i, v) {
                $('#facilities').append(
                    `<option value="${v.facilityId}" id="facility${v.facilityId}">${v.facilityName}</option>`
                );
            });
            $('#facilities').select2();
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
    $('.modal-title').text('Thêm loại phòng');
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

roomType.reset = function () {
    $('#facilities').empty();
    $('#Name').val('');
    $('#RoomTypeId').val(0);
    $('#DefaultPrice').val('');
    $('#adult').val('');
    $('#children').val('');
    $('#people').val('');
    $('#Quantity').val('');
    $('#Description').val('');
    $(".custom-file-label").text("Chọn tập tin");
    $("#imgsPreview").empty();
    $('#imgsData').empty();
}

readFiles = function () {
    $("#imgsPreview").empty();

    if ($('#RoomTypeId').val() == '0') {
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

roomType.deleteImage = function (roomTypeImageId) {
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
                    url: `/RoomTypeImage/Delete/${roomTypeImageId}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        $("#imgsData").empty();

                        $.ajax({
                            url: `/RoomTypeImage/GetByRoomTypeId/${$('#RoomTypeId').val()}`,
                            method: "GET",
                            dataType: "json",
                            success: function (data) {
                                $.each(data.result, function (i, v) {
                                    $("#imgsData").append(
                                        `<img src="${v.imageData}" style="height:150px" class="mx-2 my-2"><a class="remove-image" onclick="roomType.deleteImage('${v.roomTypeImageId}')" style="display: inline;">&#215;</a>`
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