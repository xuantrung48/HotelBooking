var promotion = {} || promotion;

$(document).ready(function () {
    promotion.init();
})

promotion.init = function () {
    promotion.drawTable();
    promotion.validation();
}
promotion.validation = function () {
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
    $('#form').validate({
        rules: {
            PromotionName: {
                required: true,
                regex: /^[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]+(([',. -][a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ ])?[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]*)*$/
            },
            DiscountRates: {
                required: true,
                min: 1
            },
            StartDate: "required",
            EndDate: {
                required: true,
                greaterThan: ["#StartDate", "StartDate"]
            },
            roomTypes: {
                required: true
            }
        },
        messages: {
            PromotionName: {
                required: "Bạn phải nhập tên chương trình",
                regex: "Tên chương trình không chứa chữ số và kí tự đặc biệt"
            },
            DiscountRates: {
                required: "Bạn phải nhập số mức giảm giá",
                min: "Mức giảm giá tối thiểu là 1"
            },
            StartDate: "Bạn phải nhập ngày bắt đầu",
            EndDate: {
                required: "Bạn phải nhập ngày kêt thúc",
                greaterThan: "Ngày kết thúc phải sau ngày bắt đầu"
            },
            roomTypes: {
                required: "Chọn một loại phòng"
            }
        }
    })
}
promotion.drawTable = function () {
    $('#promotionsTable').empty();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/PromotionsManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#promotionsTable').append(
                    `<tr>
                        <td>${v.promotionId}</td>
                        <td>${v.promotionName}</td>
                        <td class="text-center">${dateToDMY(v.startDate)}</td>
                        <td class="text-center">${dateToDMY(v.endDate)}</td>
                        <td class="text-center">${v.discountRates * 100}%</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="promotion.get(${v.promotionId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="promotion.delete(${v.promotionId}, '${v.promotionName}')"><i class="fas fa-trash"></i></a>
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

promotion.add = function () {
    promotion.reset();
    $('.modal-title').text('Thêm chương trình khuyến mãi');
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/RoomTypesManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#roomTypes').append(
                    `<option value="${v.roomTypeId}" id="roomType${v.roomTypeId}">${v.name}</option`
                );
            });
            $('#roomTypes').select2();
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

promotion.reset = function () {
    $('#PromotionName').val('');
    $('#PromotionId').val(0);
    $('#DefaultPrice').val('');
    $('#StartDate').val('');
    $('#EndDate').val('');
    $('#roomTypes').empty();
}

promotion.get = function (id) {
    promotion.reset();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/PromotionsManager/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.ajax({
                url: `/PromotionApply/GetByPromotionId/${data.result.promotionId}`,
                method: "GET",
                dataType: "json",
                success: function (promotionApply) {
                    $.ajax({
                        url: "/RoomTypesManager/GetAll",
                        method: "GET",
                        dataType: "json",
                        success: function (roomTypes) {
                            $.each(roomTypes.result, function (i, v) {
                                $('#roomTypes').append(
                                    `<option value="${v.roomTypeId}" id="roomType${v.roomTypeId}">${v.name}</option>`
                                );
                                $.each(promotionApply.result, function (j, u) {
                                    if (v.roomTypeId == u.roomTypeId) {
                                        $(`#roomType${u.roomTypeId}`).attr('selected', 'selected');
                                    }
                                })
                            });
                            $('#roomTypes').select2();
                        }
                    });
                }
            });
            $('.modal-title').text('Đổi thông tin khuyến mãi');
            $('#PromotionName').val(data.result.promotionName);
            $('#PromotionId').val(data.result.promotionId);
            $('#DiscountRates').val(data.result.discountRates * 100);
            $('#StartDate').val(dateToYMD(data.result.startDate));
            $('#EndDate').val(dateToYMD(data.result.endDate));
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

promotion.save = function () {
    if ($('#form').valid()) {
        var promotionObj = {};
        promotionObj.PromotionId = parseInt($('#PromotionId').val());
        promotionObj.PromotionName = $('#PromotionName').val().trim();
        promotionObj.StartDate = new Date($('#StartDate').val());
        promotionObj.EndDate = new Date($('#EndDate').val());
        promotionObj.DiscountRates = parseFloat($('#DiscountRates').val() / 100);
        promotionObj.RoomTypeIds = $('#roomTypes').val();
        if (promotionObj.PromotionId != 0) {
            $.ajax({
                url: `/PromotionApply/DeleteByPromotionId/${promotionObj.PromotionId}`,
                method: "GET",
                dataType: "json"
            });
        }
        $.ajax({
            beforeSend: function () {
                $('#modal-loader').css("visibility", "visible");
            },
            url: `/PromotionsManager/Save/`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(promotionObj),
            success: function (data) {
                $('#mediumModal').modal('hide');
                bootbox.alert(data.result.message);
                promotion.drawTable();
            },
            complete: function () {
                $('#modal-loader').css("visibility", "hidden");
            }
        });
    }
}

promotion.delete = function (id, name) {
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
                    beforeSend: function () {
                        $('.ajax-loader').css("visibility", "visible");
                    },
                    url: `/PromotionsManager/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        promotion.drawTable();
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