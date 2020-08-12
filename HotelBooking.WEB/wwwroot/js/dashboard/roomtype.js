var roomType = {} || roomType;

$(document).ready(function () {
    roomType.init();
})

digitGrouping = function (price) {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}

roomType.init = function () {
    roomType.drawTable();
}

roomType.drawTable = function () {
    $('#roomTypesTable').empty();
    $.ajax({
        url: "/RoomType/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#roomTypesTable').append(
                    `<tr>
                        <td>${v.roomTypeId}</td>
                        <td>${v.name}</td>
                        <td>${digitGrouping(v.defaultPrice)}</td>
                        <td>${v.quantity}</td>
                        <td>
                            <a href="javascripts:;" class="btn btn-primary"
                                       onclick="roomType.get(${v.roomTypeId})"><i class="fas fa-edit"></i></a> 
                            <a href="javascripts:;" class="btn btn-danger"
                                        onclick="roomType.delete(${v.roomTypeId}, '${v.name}')"><i class="fas fa-trash"></i></a>
                        </td>
                    </tr>`
                );
            });
        }
    });
}

roomType.get = function (id) {
    roomType.reset();

    if (id != 0) {
        roomType.showImages(id);
    }
    $.ajax({
        url: `/RoomType/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.ajax({
                url: `/Facility/GetAll`,
                method: "GET",
                dataType: "json",
                success: function (facilities) {
                    $.ajax({
                        url: `/FacilityApply/Get/${id}`,
                        method: "GET",
                        dataType: "json",
                        success: function (facilitiesApply) {
                            for (let i = 0; i < facilities.result.length; i++) {
                                $('#facilities').append(
                                    `<option value="${facilities.result[i].facilityId}" id="facility${facilities.result[i].facilityId}">${facilities.result[i].facilityName}</option>`
                                );
                                for (let j = 0; j < facilitiesApply.result.length; j++) {
                                    if (facilities.result[i].facilityId == facilitiesApply.result[j].facilityId) {
                                        $(`#facility${facilities.result[i].facilityId}`).attr('selected', 'selected');
                                    }
                                }
                            }

                            $('#facilities').select2();
                        }
                    });
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
        }
    });
}

roomType.save = function () {
    var imgsNo = parseInt($("#imgsNo").val());
    var roomTypeObj = {};
    roomTypeObj.Name = $('#Name').val();
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
        url: `/RoomType/Save/`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(roomTypeObj),
        success: function (data) {
            /*var facilities = $('#facilities').val();
            for (let i = 0; i < facilities.length; i++) {
                let facilityApplyObj = {};
                facilityApplyObj.RoomTypeId = parseInt(data.result.id);
                facilityApplyObj.FacilityId = parseInt(facilities[i]);
                console.log(facilityApplyObj);
                $.ajax({
                    url: `/FacilityApply/Save/`,
                    method: "POST",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify(facilityApplyObj)
                });
            }*/
            $('#mediumModal').modal('hide');
            bootbox.alert(data.result.message);
            roomType.drawTable();
        }
    });
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
                    url: `/RoomType/Delete/${id}`,
                    method: "GET",
                    dataType: "json",
                    success: function (data) {
                        bootbox.alert(data.result.message);
                        roomType.drawTable();
                    }
                });
            }
        }
    });
}

roomType.add = function () {
    roomType.reset();
    $.ajax({
        url: `/Facility/GetAll`,
        method: "GET",
        dataType: "json",
        success: function (facilities) {
            for (let i = 0; i < facilities.result.length; i++) {
                $('#facilities').append(
                    `<option value="${facilities.result[i].facilityId}" id="facility${facilities.result[i].facilityId}">${facilities.result[i].facilityName}</option>`
                );

                $('#facilities').select2();
            }
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
    $('#child').val('0');
    $('#child').removeAttr('checked');
    $('#Quantity').val('');
    $('#Description').val('');
    $(".custom-file-label").text("Chọn tập tin");
    $("#imgsPreview").empty();
    $('#imgsData').empty();
}

$('#child').click(function () {
    if ($('#child').is(":checked")) {
        $('#child').val('0.5');
    } else {
        $('#child').val('0');
    }
})

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

roomType.showImages = function (roomTypeId) {
    $.ajax({
        url: `/RoomTypeImage/GetByRoomTypeId/${roomTypeId}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            /*imgsNo = data.result.length;*/
            $.each(data.result, function (i, v) {
                $("#imgsData").append(
                    `<img src="${v.imageData}" style="height:150px" class="mx-2 my-2"><a class="remove-image" onclick="roomType.deleteImage('${v.roomTypeImageId}')" style="display: inline;">&#215;</a>`
                );
            });
        }
    });
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
                                /*imgsNo = data.result.length;*/
                                $.each(data.result, function (i, v) {
                                    $("#imgsData").append(
                                        `<img src="${v.imageData}" style="height:150px" class="mx-2 my-2"><a class="remove-image" onclick="roomType.deleteImage('${v.roomTypeImageId}')" style="display: inline;">&#215;</a>`
                                    );
                                });
                            }
                        });
                    }
                });
            }
        }
    });
}