var facility = {} || facility;

$(document).ready(function () {
    facility.init();
})

facility.init = function () {
    facility.drawTable();
}

facility.drawTable = function () {
    $('#facilitiesTable').empty();
    $.ajax({
        url: "/Facility/GetAll",
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
        }
    });
}

facility.get = function (id) {
    facility.reset();
    $.ajax({
        url: `/Facility/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('.modal-title').text('Đổi thông tin tiện nghi');
            $('#FacilityName').val(data.result.facilityName);
            $('#FacilityId').val(data.result.facilityId);
            $('#FacilityImage').val(data.result.facilityImage);
            $('#imgPreview').append(
                `<img src=${data.result.facilityImage} height="50">`
            );
            $('#mediumModal').appendTo("body");
            $('#mediumModal').modal('show');
        }
    });
}

facility.save = function () {
    var facilityObj = {};
    facilityObj.FacilityName = $('#FacilityName').val();
    facilityObj.FacilityId = parseInt($('#FacilityId').val());
    facilityObj.FacilityImage = $('#FacilityImage').val();
    $.ajax({
        url: `/Facility/Save/`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(facilityObj),
        success: function (data) {
            $('#mediumModal').modal('hide');
            bootbox.alert(data.result.message);
            facility.drawTable();
        }
    });
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
                    url: `/Facility/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        facility.drawTable();
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