var promotion = {} || promotion;

$(document).ready(function () {
    promotion.init();
})

promotion.init = function () {
    promotion.drawTable();
}

promotion.drawTable = function () {
    $('#promotionsTable').empty();
    $.ajax({
        url: "/Promotion/GetAll",
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
        }
    });
}

promotion.add = function () {
    promotion.reset();
    $('.modal-title').text('Thêm chương trình khuyến mãi');
    $('#mediumModal').appendTo("body");
    $('#mediumModal').modal('show');
}

promotion.reset = function () {
    $('#PromotionName').val('');
    $('#PromotionId').val(0);
    $('#DefaultPrice').val('');
    $('#StartDate').val('');
    $('#EndDate').val('');
}

promotion.get = function (id) {
    promotion.reset();
    $.ajax({
        url: `/Promotion/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('.modal-title').text('Đổi thông tin khuyến mãi');
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

promotion.save = function () {
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
            promotion.drawTable();
        }
    });
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
                    url: `/Promotion/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        promotion.drawTable();
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