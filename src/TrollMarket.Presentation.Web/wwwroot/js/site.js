(function () {
    appearUserDetail();
    manageRole();
    addCloseButtonListener();
    addCartButtonListener();
    addShipmentButtonListener();
    addSumbitShipmentListener()
    addBalanceButtonListener();
    submitBalanceButtonListener();
    addProductDetailButtonListener();
    addUpdateButtonListener();
    submitToCartButtonListener();
    getCartData();
}());

function appearUserDetail() {
    $('.check-user').click(function (event) {
        let username = $('#username').val();
        $.ajax({
            url: `/api/${username}`,
            method: 'GET',
            success: function (response) {
                $('#firstName').val(response.firstName);
                $('#lastName').val(response.lastName);
                $('#address').val(response.address);
            },
        });
    });
}

function manageRole() {
    let role = document.querySelector(".role");
    if (role != null) {
        let roleName = role.textContent;
        let formatted = roleName.replace('[', '').replace(']', '');
        role.textContent = formatted;
        let mainBody = document.querySelector(".main-body");
        mainBody.setAttribute("data-role", formatted);
    }
}

function addCloseButtonListener() {
    $('.close-button').click(function (event) {
        $('.modal-layer').removeClass('modal-layer--opened');
        $('.popup-dialog').removeClass('popup-dialog--opened');
        $('.popup-dialog input').val("");
        $('.popup-dialog textarea').val("");
        $('.popup-dialog .validation-message').text("");
    });
}

function addBalanceButtonListener() {
    $('.balance-button').click(function (event) {
        event.preventDefault();
        $('.modal-layer').addClass('modal-layer--opened');
        $('.balance-dialog').addClass('popup-dialog--opened');
    });
}

function submitBalanceButtonListener() {
    $('.submit-balance').click(function (event) {
        let username = $(this).attr('data-username');
        let balance = $('.balance').val();
        $.ajax({
            url: `/api/profile/${username}?balance=${balance}`,
            method: 'PATCH',
            success: function (response) {
                location.reload();
            },
            error: function ({ status, responseJSON }) {
               
                alert('Your input is invalid');
                
            }
        });
    });
}

function addProductDetailButtonListener() {
    $('#tableBody').on('click', '.info-button', function (event) {
        let id = $(this).attr('data-id');
        let role = document.querySelector(".role");
        let roletext;
        if (role != null) {
            let roleName = role.textContent;
            roletext = roleName.replace('[', '').replace(']', '');
        }
        $.ajax({
            url: (roletext == "Seller") ? `/api/merchandise/${id}` : `/api/shop/${id}`,
            method: 'GET',
            success: function (product) {
                let isDiscontinued = product.isDiscontinued.toString();
                if (isDiscontinued == "true") {
                    isDiscontinued = "Yes"
                } else { isDiscontinued = "No" }
                $('.product-dialog .product-name').text(product.productName);
                $('.product-dialog .category').text(product.category);
                $('.product-dialog .description').text(product.description);
                $('.product-dialog .price').text("Rp. " + (new Intl.NumberFormat('id-ID').format(product.price)));
                $('.product-dialog .discontinue').text(isDiscontinued);
                $('.product-dialog .seller-name').text(product.fullName);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.product-dialog').addClass('popup-dialog--opened');
            }
        });
    });
}

function addShipmentButtonListener() {
    $('#create-button').click(function (event) {
        event.preventDefault();
        $('.modal-layer').addClass('modal-layer--opened');
        $('.form-dialog').addClass('popup-dialog--opened');
    });
}


function addSumbitShipmentListener() {
    $('.form-dialog button').click(function (event) {
        let vm = collectInputForm();
        let requestMethod = (vm.id === 0) ? 'POST' : 'PUT';
        let url = (requestMethod == 'POST') ? "/api/shipment" : `/api/shipment/${vm.id}`;
        $.ajax({
            method: requestMethod,
            url: url,
            data: JSON.stringify(vm),
            contentType: 'application/json',
            success: function (response) {
                location.reload();
            },
            error: function ({ status, responseJSON }) {
                if (status === 400) {
                    //$('form-dialog [data-for=]').text('Shipment is invalid');
                    writeValidationMessage(responseJSON.errors,'shipment');
                }
                else {
                    alert(`There is an error with code: ${status}`);
                }
            }
        });
    });
}

function collectInputForm() {
    let shipmentId = $('.form-dialog .id').val()
    let isChecked;
    if ($('.form-dialog .isService').is(":checked")) {
        isChecked = true
    } else {
        isChecked = false
    }
    vm = {
        id: (shipmentId === "") ? 0 : shipmentId,
        shipperName: $('.form-dialog .shipmentName').val(),
        price: $('.form-dialog .price').val() === '' ? 0 : $('.form-dialog .price').val(),
        isService: isChecked,
    }
    return vm;
}

function writeValidationMessage(errorMessages, formType) {
    let validationMessage = document.querySelectorAll('.validation-message');
    for (let validation of validationMessage) {
        validation.innerHTML = '';
    }
    Object.keys(errorMessages).forEach(field => {
        let messages = errorMessages[field];

        console.log(`Property: ${field}`);
        field = field.charAt(0).toLowerCase() + field.slice(1);
        messages.forEach(message => {
            console.log(message);
            if (formType == 'shipment') {
                $(`.form-dialog [data-for=${field}]`).text(message);
            } else {
                $(`.cart-dialog [data-for=${field}]`).text(message);
            }
            
        });


    });
    //for(let error of errorMessages){
    //    let {field, message} = error;
    //    $(`.form-dialog [data-for=${field}]`).text(message);
    //}

}

function addCartButtonListener() {
    $('.cart-button').click(function (event) {
        event.preventDefault();
        let merchanidiseId = $(this).attr('data-id');
        $('.cart-dialog .id').val(merchanidiseId);
        $('.modal-layer').addClass('modal-layer--opened');
        $('.cart-dialog').addClass('popup-dialog--opened');
    });
}

function submitToCartButtonListener() {
    $('.cart-dialog button').click(function (event) {
        let vm = collectCartForm();
        let requestMethod = 'POST';
        let url =  "/api/shop";
        $.ajax({
            method: requestMethod,
            url: url,
            data: JSON.stringify(vm),
            contentType: 'application/json',
            success: function (response) {
                location.reload();
            },
            error: function ({ status, responseJSON }) {
                if (status === 400) {
                    writeValidationMessage(responseJSON.errors,'cart')
                }
                else {
                    alert(`There is an error with code: ${status}`);
                }
            }
        });
    });
    function collectCartForm() {
        let vm = {
            merchaniseId: $('.cart-dialog .id').val(),
            quantity: $('.cart-dialog .quantity').val() === '' ? 0 : $('.cart-dialog .quantity').val(),
            shipmentId: $('.cart-dialog .shipmentList').val() === '' ? 0 : $('.cart-dialog .shipmentList').val()
        }
        return vm;
    }
}

function addUpdateButtonListener() {
    $('.update-button').click(function (event) {
        event.preventDefault();
        let shipmentId = $(this).attr('data-id');
        $.ajax({
            url: `/api/shipment/${shipmentId}`,
            method: 'GET',
            success: function (response) {
                populateInputForm(response);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.form-dialog').addClass('popup-dialog--opened');
            }
        })
    });
    function populateInputForm(response) {
        $('.form-dialog .id').val(response.shipmentId);
        $('.form-dialog .shipmentName').val(response.shipperName);
        $('.form-dialog .price').val(response.price);
        if (response.isService == false) {
            $('.form-dialog .isService').hide();
            $('.form-dialog .isServiceLabel').hide();
        } 
    }

}

function getCartData() {
    $('#purchase-button').click(function (event) {
        let cartIndexVm = { carts: [], pageNumber: 0, totalPage: 0 };
        $('#cartTable tbody tr').each(function (index) {
            let cartVm = {
                buyerName: $(this).find('input[type=hidden][name=buyerName]').val(),
                merchaniseId: $(this).find('input[type=hidden][name=productId]').val(),
                shipmentId: $(this).find('input[type=hidden][name=shipmentId]').val(),
                quantity: $(this).find('input[type=hidden][name=quantity]').val(),
                sellerName: $(this).find('input[type=hidden][name=sellerName]').val()
            }
            cartIndexVm.carts.push(cartVm);
        });
        $.ajax({
            method: 'POST',
            url: '/Cart',
            data: JSON.stringify(cartIndexVm),
            contentType: 'application/json',
            success: function (response) {
                location.reload();
            },
            error: function ({ status, responseJSON }) {
                if (status === 400) {
                    window.location.href = "/Cart/Sorry";
                }
                else {
                    alert(`There is an error with code: ${status}`);
                }
            }
        });
    });
}