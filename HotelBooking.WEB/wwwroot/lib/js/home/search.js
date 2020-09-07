var bookingRoom = {} || bookingRoom;
var searchRequest = JSON.parse(localStorage.getItem('searchRequest'));
var roomSelected = [];
var roomTypes;
var searchResult = {};
var coupon = {};
var totalAmount = 0;
var totalAmoutRoom = JSON.parse(localStorage.getItem('TotalAmount'));
var totalAmountBooking = totalAmoutRoom;
var serviceDetails = [];

$(document).ready(function () {
    bookingRoom.init();
})

bookingRoom.init = function () {
    getRoomTypes();
    showSelections();
    getRoomsValue();
}

formatDate = function (date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [day, month, year].join('-');
}

getRoomTypes = function () {
    $.ajax({
        url: "/RoomTypesManager/GetAllWithImagesAndFacilities",
        method: "GET",
        dataType: "json",
        async: false,
        success: function (roomTypesData) {
            roomTypes = roomTypesData.result;
        },
    });
}

showSelections = function () {
    var search = {};
    search.CheckInDate = new Date(searchRequest.CheckInDate);
    search.CheckOutDate = new Date(searchRequest.CheckOutDate);
    search.RoomTypeSearchRequests = searchRequest.RoomTypeSearchRequests;
    $.ajax({
        url: `/Search/Search`,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        async: false,
        data: JSON.stringify(search),
        success: function (data) {
            searchResult = data.result;
        }
    });
    $('#checkInDate').text(`Nhận phòng: ${formatDate(new Date(searchRequest.CheckInDate))}`);
    $('#checkOutDate').text(`Trả phòng: ${formatDate(new Date(searchRequest.CheckOutDate))}`);
    var adults = 0;
    var children = 0;
    for (let i = 0; i < searchResult.roomSearchResults.length; i++) {
        adults += searchResult.roomSearchResults[i].adults;
        children += searchResult.roomSearchResults[i].children;
        $('#tbRoomBooking').append(
            `<div class="card my-3">
                <div class="row">
                    <div class="col-md-3 col-sm-12 text-center my-auto">
                        <h4>Phòng ${i + 1}</h4>
                        <p>(${searchResult.roomSearchResults[i].adults} người lớn, ${searchResult.roomSearchResults[i].children} trẻ em)</p>
                    </div>
                    <div class="col-md-9 col-sm-12 my-auto" id="room${i}">
                    </div>
                </div>
            </div>`
        )
        if (!searchResult.roomSearchResults[i].roomTypeSearchResults.length)
            $(`#room${i}`).append('<div class="text-danger">Xin lỗi, chúng tôi đã hết phòng để chứa đủ số lượng người này!</div>')
        else
            for (let j = 0; j < searchResult.roomSearchResults[i].roomTypeSearchResults.length; j++) {
                for (let k = 0; k < roomTypes.length; k++)
                    if (searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomTypeId == roomTypes[k].roomTypeId) {
                        roomTypes[k].minRemain = searchResult.roomSearchResults[i].roomTypeSearchResults[j].minRemain;
                        let facilities = '';
                        for (let l = 0; l < roomTypes[k].facilities.length; l++)
                            facilities += `<img src=${roomTypes[k].facilities[l].facilityImage} class="mx-1 facilities">`

                        var roomPriceStr = '<div class="row"><div class="col-lg-2 col-md-12 col-4">Giá:</div><div class="col-lg-10 col-md-12 col-8">';
                        var price = 0;
                        for (let d = 0; d < searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults.length; d++) {
                            var date = new Date(searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults[d].date);
                            price += searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults[d].price;
                            if (searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults[d].price == roomTypes[k].defaultPrice)
                                roomPriceStr += `<div>${date.getDate()}/${date.getMonth() + 1}: ${digitGrouping(roomTypes[k].defaultPrice)}</div>`
                            else
                                roomPriceStr += `<div>${date.getDate()}/${date.getMonth() + 1}: <del class="text-danger">${digitGrouping(roomTypes[k].defaultPrice)}</del> ${digitGrouping(searchResult.roomSearchResults[i].roomTypeSearchResults[j].roomPriceSearchResults[d].price)}</div>`
                        }
                        roomPriceStr += '</div></div>';
                        $(`#room${i}`).append(
                            `<div class="row my-2">
                            <div class="col-md-4">
                                <img class="my-auto" src="${roomTypes[k].image}">
                            </div>
                            <div class="col-md-7">
                                <a href="/Rooms/Details/${roomTypes[k].roomTypeId}" target="_blank"><h4 class="text-warning">${roomTypes[k].name} ${(j == 0) ? '<span class="badge badge-success">Giá tốt nhất</span>' : ''} <span class="text-danger" id="room-available-${i + 1}-${roomTypes[k].roomTypeId}"></span></h4></a>
                                <p>${roomTypes[k].description}</p>
                                <p class="text-dark">Tiện nghi: ${facilities}</p>
                                ${roomPriceStr}
                                <p class="text-dark">Tổng giá phòng: ${digitGrouping(price)}</p>
                                <input type="number" hidden value="${price}" id="roomPrice-${i + 1}-${roomTypes[k].roomTypeId}">
                            </div>
                            <div class="col-md-1 my-auto text-center">
                                <input type="radio" value="${roomTypes[k].roomTypeId}" name="room-${i + 1}" id="room-${i + 1}-${roomTypes[k].roomTypeId}" onclick="getRoomsValue()">
                            </div>
                        </div>`
                        )
                        if (j < searchResult.roomSearchResults[i].roomTypeSearchResults.length - 1)
                            $(`#room${i}`).append('<hr style="height:2px;">');
                    };
            }
    }
    $('#totalPeople').text(`Tổng số người: ${adults + children} (${adults} người lớn, ${children} trẻ em)`);
}

getRoomsValue = function () {
    roomSelected.length = 0;
    totalAmount = 0;
    for (let i = 0; i < roomTypes.length; i++) {
        var minRemain = roomTypes[i].minRemain;
        for (let j = 0; j < searchResult.roomSearchResults.length; j++)
            if (document.querySelector(`input[id="room-${j + 1}-${roomTypes[i].roomTypeId}"]:checked`)) {
                roomSelected.push(parseInt(document.querySelector(`input[id="room-${j + 1}-${roomTypes[i].roomTypeId}"]:checked`).value));
                totalAmount += parseInt($(`#roomPrice-${j + 1}-${roomTypes[i].roomTypeId}`).val());
                minRemain -= 1;
                if (minRemain == 0)
                    disableUnavailableRoomTypes(roomTypes[i].roomTypeId);
                if (minRemain == 1)
                    enableAvailableRoomTypes(roomTypes[i].roomTypeId);
            }
    }
    $('#TotalAmount').text(digitGrouping(totalAmount));
}

disableUnavailableRoomTypes = function (roomTypeId) {
    for (let i = 0; i < searchResult.roomSearchResults.length; i++)
        if (!document.getElementById(`room-${i + 1}-${roomTypeId}`).checked) {
            document.getElementById(`room-${i + 1}-${roomTypeId}`).disabled = true;
            $(`#room-available-${i + 1}-${roomTypeId}`).text('(Hết phòng)');
        }
}

enableAvailableRoomTypes = function (roomTypeId) {
    for (let i = 0; i < searchResult.roomSearchResults.length; i++)
        if (!document.getElementById(`room-${i + 1}-${roomTypeId}`).checked) {
            document.getElementById(`room-${i + 1}-${roomTypeId}`).disabled = false;
            $(`#room-available-${i + 1}-${roomTypeId}`).text('');
        }
}

bookRoom = function () {
    var bookingRoomRequest = searchRequest;
    bookingRoomRequest.bookingRoom = roomSelected;
    var allRoomSelected = true;
    for (let i = 0; i < searchResult.roomSearchResults.length; i++)
        if ($(`input[name=room-${i + 1}]:checked`).length == 0) {
            bootbox.alert(`Bạn chưa chọn loại phòng cho phòng ${i + 1}!`);
            allRoomSelected = false;
            break;
        }
    if (allRoomSelected) {
        createBookingObj();
        localStorage.setItem('TotalAmount', totalAmount);
        location.assign("/Search/BookingDetails");
    }
}

confirm = function () {
    if ($('#form').valid()) {
        var bookingObj = {};
        var customerObj = {};
        customerObj = addCustomer();
        bookingObj = JSON.parse(localStorage.getItem('CreateBooking'));
        bookingObj.BookingCustomer = customerObj;
        bookingObj.CustomerId = customerObj.CustomerId;
        bookingObj.CouponId = parseInt($('#CouponId').val());
        bookingObj.bookingServiceDetails = serviceDetails;
        //bookingObj.NumberofAdults = ;
        //bookingObj.NumberofChildren = ;
        console.log(bookingObj);
        $.ajax({
            url: `/BookingsManager/Save/`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(bookingObj),
            success: function (data) {
                bootbox.alert({
                    message: data.result.message,
                    callback: function () {
                        location.assign("/Home/Index");
                    }
                })
            }
        }).done(function () {

        });
    }
}

showRoomTypeDetails = function () {
    var roomTypeDetails = JSON.parse(localStorage.getItem('CreateBooking')).bookingRoomDetails;
    $.each(roomTypeDetails, function (i, v) {
        $.ajax({
            beforeSend: function () {
                $('.ajax-loader').css("visibility", "visible");
            },

            url: `/RoomTypesManager/Get/${v.RoomTypeId}`,
            method: "GET",
            dataType: "json",
            success: function (data) {
                $('#tbRoomBookingCreate').append(
                    `<div class="my-3">
                            <div class="row form-group ">
                                <div class="col-md-5 col-sm-12 my-auto">
                                    <h4>${data.result.name}</h4>
                                </div>
                                <div class="col-md-5 col-sm-12 text-center">
                                    <h4>Số lượng: ${v.RoomQuantity}</h4>
                                </div>
                            </div>
                        </div>`
                );
            },

            complete: function () {
                $('.ajax-loader').css("visibility", "hidden");
            }
        });
    });
}

showService = function () {
    $('#tbService').empty();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/ServicesManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (i, v) {
                $('#tbService').append(
                    `<div class="my-3">
                        <div class="row form-group">
                            <div class="col-md-3 col-sm-12 my-auto">
                                <h5>${v.serviceName}</h5>
                            </div>
                            <div class="col-md-9 col-sm-12">
                                <div class="col-3 col-sm-12 my-1">
                                    <h5>Giá dịch vụ: ${digitGrouping(v.price)}</h5>
                                </div>
                                <div class="col-9 col-sm-12 my-1">
                                    <input type="number" id="ServiceQuantity${v.serviceId}" name="ServiceQuantity" onchange ="changeServiceQuantity(${v.price} ,${v.serviceId})" placeholder="Số lượng" class="form-control" />
                                    <input type="hidden" id="ServicePrice${v.serviceId}" value="0"/> 
                                </div>
                            </div>
                        </div>
                    </div>`
                );
            });
            displayTotalAmountBooking(totalAmountBooking, "#TotalAmountBooking");
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}


changeServiceQuantity = function (p, i) {
    var quantity = parseInt($(`#ServiceQuantity${i}`).val());
    if (Number.isNaN(quantity)) {
        var money = 0 * parseInt(p);
        $(`#ServicePrice${i}`).val(parseInt(money));
        calculateTotalServiceMoney();
    } else {
        if (quantity < 0) {
            var money = 0 * parseInt(p);
            $(`#ServicePrice${i}`).val(parseInt(money));
            calculateTotalServiceMoney();
        } else {
            var money = quantity * parseInt(p);
            $(`#ServicePrice${i}`).val(parseInt(money));
            calculateTotalServiceMoney();
        }
        //var money = quantity * parseInt(p);
        //    $(`#ServicePrice${i}`).val(parseInt(money));
        //    calculateTotalServiceMoney();
    }
}

calculateTotalServiceMoney = function () {

    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: "/ServicesManager/GetAll",
        method: "GET",
        dataType: "json",
        success: function (data) {
            var totalServiceMoney = 0;
            serviceDetails = [];
            $.each(data.result, function (i, v) {
                totalServiceMoney += parseInt($(`#ServicePrice${v.serviceId}`).val());
                if ($('#formService').valid()) {
                    var serviceQuantity = parseInt($(`#ServiceQuantity${v.serviceId}`).val());
                    if (!Number.isNaN(serviceQuantity)) {
                        var serviceDetail = {};
                        serviceDetail.ServiceId = v.serviceId;
                        serviceDetail.ServiceQuantity = serviceQuantity;
                        serviceDetail.BookingId = 0;
                        serviceDetails.push(serviceDetail);
                    }
                }
            });
            totalAmountBooking = totalServiceMoney + totalAmoutRoom;
            displayTotalAmountBooking(totalAmountBooking, "#TotalAmountBooking");
            $('#TotalServicePrice').text((digitGrouping(totalServiceMoney)));
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

reset = function () {
    $('#Name').val('');
    $('#CustomerId').val(0);
    $('#PhoneNumber').val('');
    $('#Email').val('');
}

groupRoomType = function () {
    var roomDetails = new Array();
    var i = 0;
    var j = 0;
    while (i < roomSelected.length) {
        var roomDetail = {};
        roomDetail.RoomTypeId = roomSelected[i];
        roomDetail.RoomQuantity = 1;
        for (j = i + 1; j < roomSelected.length; j++) {
            if (roomSelected[i] == roomSelected[j]) {
                roomDetail.RoomQuantity++;
                i = j;
                continue;
            }
        }
        roomDetails.push(roomDetail);
        i++
    }
    return roomDetails;
}

groupPeople = function () {
    return JSON.parse(localStorage.getItem('numofPeople'));
}

addCustomer = function () {
    var customerObj = {}
    customerObj.Name = $('#Name').val().trim();
    customerObj.PhoneNumber = $('#PhoneNumber').val().trim();
    customerObj.Email = $('#Email').val().trim();
    customerObj.CustomerId = parseInt($('#CustomerId').val());
    return customerObj;
}

checkCouponCode = function () {
    var couponCode;
    couponCode = $('#CouponCode').val().toUpperCase();
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/CouponsManager/Search/${couponCode}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('#CouponCode').val(couponCode.toUpperCase());
            bootbox.alert({
                message: data.result.message,
                callback: function () {
                    if (data.result.couponId > 0) {
                        $('#CouponId').val(data.result.couponId);
                        totalAmoutRoom = JSON.parse(localStorage.getItem('TotalAmount')) * (1 - data.result.reduction);
                        displayTotalAmountBooking(totalAmoutRoom, "#TotalRoomPrice");
                        calculateTotalServiceMoney();
                    }

                }
            })
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

addCouponCode = function (id) {
    $.ajax({
        beforeSend: function () {
            $('.ajax-loader').css("visibility", "visible");
        },
        url: `/CouponsManager/Get/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            coupon = data.result;
            console.log(coupon);
        },
        complete: function () {
            $('.ajax-loader').css("visibility", "hidden");
        }
    });
}

createBookingObj = function () {
    var roomDetails = groupRoomType();
    var bookingObj = {};
    bookingObj.NumberofChildren = groupPeople().children;
    bookingObj.NumberofAdults = groupPeople().adults;
    bookingObj.CheckinDate = new Date(searchRequest.CheckInDate);
    bookingObj.CheckoutDate = new Date(searchRequest.CheckOutDate);
    bookingObj.bookingRoomDetails = roomDetails;
    bookingObj.BookingId = 0;
    localStorage.setItem('CreateBooking', JSON.stringify(bookingObj));
}

displayTotalAmountBooking = function (amount, id) {
    $(id).text(digitGrouping(amount))
}

validation = function () {
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
                regex: /^[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]+(([',. -][a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ ])?[a-zắằẳẵặăấầẩẫậâáàãảạđếềểễệêéèẻẽẹíìỉĩịốồổỗộôớờởỡợơóòõỏọứừửữựưúùủũụýỳỷỹỵA-ZẮẰẲẴẶĂẤẦẨẪẬÂÁÀÃẢẠĐẾỀỂỄỆÊÉÈẺẼẸÍÌỈĨỊỐỒỔỖỘÔỚỜỞỠỢƠÓÒÕỎỌỨỪỬỮỰƯÚÙỦŨỤÝỲỶỸỴ]*)*$/
            },
            PhoneNumber: {
                required: true,
                regex: /^\(?(0|[3|5|7|8|9])+([0-9]{8})$/,

            },
            Email: {
                required: true,
                regex: /^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/
            },
            ServiceQuantity: {
                min: 0
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
            ServiceQuantity: {
                min: "Bạn phải nhập số lớn hơn 0"
            }
        }
    });
    $('#formService').validate({
        rules: {
            ServiceQuantity: {
                min: 1
            }
        },
        messages: {
            ServiceQuantity: {
                min: "Bạn phải nhập số lớn hơn 0"
            }
        }
    });
}