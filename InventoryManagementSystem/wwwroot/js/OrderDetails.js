
document.getElementById("edit-switch").checked = false











function toggleEdit() {
    
    if (document.getElementById("edit-switch").checked == true) {

        //enable order and customer information inputs

        $(".order-details-input-inactive").each(function () {
            $(this).addClass("order-details-input-active")
                   .removeClass("order-details-input-inactive")
                   .prop("disabled", false) 
        })


        //add submit button to update customers/order form

        if (!$("#order-customer-information-save-changes-button").length) {
            var orderCustomerSubmitButtonRow = $("<div></div>")
            orderCustomerSubmitButtonRow.addClass("row pb-4").attr("id", "order-customer-submit-button-row")
            var orderCustomerSubmitButtonChildDiv = $("<div></div>")
            orderCustomerSubmitButtonChildDiv.addClass("col-12 col-sm-6 d-flex align-items-center")
            orderCustomerSubmitButtonRow.append(orderCustomerSubmitButtonChildDiv)
            var orderCustomerSubmitButton = $("<input></input>")
            orderCustomerSubmitButton.attr("type", "submit").attr("onclick", "return confirm('Are you sure you want to update this order?')")
            orderCustomerSubmitButton.addClass("btn").val("Save Changes")
            orderCustomerSubmitButton.attr("id", "order-customer-information-save-changes-button")
            orderCustomerSubmitButton.css({ 'color': '#6c757d', 'background-color': '#f0f0f0', 'border': 'none' })
            orderCustomerSubmitButtonChildDiv.append(orderCustomerSubmitButton)
            $("#order-details-customer-section").append(orderCustomerSubmitButtonRow)
        }

        

        //remove create product/payment functionality
        $("#add-product-button").hide()
        $("#add-payment-button").hide()

        //remove create product/payment rows from DOM
        $(".new-product-row, .new-payment-row").each(function (index, element) {
            element.remove()
        })


        //set product/payment input to disabled style
        $(".order-details-product-info-input-inactive").each(function (index, element) {
            element = $(element)
            if (!$(element).hasClass("new-product-property")) { //if the element is not a new product
                element.addClass("order-details-product-info-input-active")
                       .removeClass("order-details-product-info-input-inactive")
            }
            

        })
        $(".order-details-payment-info-input-inactive").each(function (index, element) {
            element = $(element)
            if (!$(element).hasClass("new-payment-property")) { //if the element is not a new payment
                element.addClass("order-details-payment-info-input-active")
                       .removeClass("order-details-payment-info-input-inactive")
            }


        })


        //remove add product/payment submit button
        $("#add-products-submit-button-row-clone, #add-payments-submit-button-row-clone").remove()


        //enable product/payment inputs
        //do not enable template inputs or submit buttons
        $("#product-rows-container, #payment-rows-container").find("input").filter(
            function () {
                
                return !($(this).hasClass("new-product-property") || $(this).hasClass("new-payment-property")
                    || $(this).attr("id") == "add-payments-submit-button" || $(this).attr("id") == "edit-payments-submit-button"
                    || $(this).attr("id") == "add-products-submit-button" || $(this).attr("id") == "edit-products-submit-button"
                    || $(this).attr('type') == "hidden")
            }).prop("disabled", false)
        $("#product-rows-container, #payment-rows-container").find("select").filter(
            function () {
                return !($(this).hasClass("new-product-property") || $(this).hasClass("new-payment-property"))
            }).prop("disabled", false)
        $("#product-rows-container, #payment-rows-container").find("textarea").filter(
            function () {
                return !($(this).hasClass("new-product-property") || $(this).hasClass("new-payment-property"))
            }).prop("disabled", false)
        


        //clone edit product submit button and append it to product section
        var editProductSubmitButtonRow = $("#edit-products-submit-button-row").clone()
        editProductSubmitButtonRow.attr("id", "edit-products-submit-button-row-clone")
        editProductSubmitButtonRow.show()
        $("#edit-products-submit-button-row-clone").remove() //remove previous submit button
        $("#product-rows-container").append(editProductSubmitButtonRow) //append new submit button

        //clone edit payment submit button and append it to payment section
        var editPaymentsSubmitButtonRow = $("#edit-payments-submit-button-row").clone()
        editPaymentsSubmitButtonRow.attr("id", "edit-payments-submit-button-row-clone")
        editPaymentsSubmitButtonRow.show()
        $("#edit-payments-submit-button-row-clone").remove() //remove previous submit button
        $("#payment-rows-container").append(editPaymentsSubmitButtonRow) //append new submit button

        


        

    }

    else {

        //disable order/customer inputs

        $(".order-details-input-active").each(function () {
            $(this).addClass("order-details-input-inactive")
                   .removeClass("order-details-input-active")
                   .prop("disabled", true) 
        })


        //remove save changes button for customer/order information form
        $("#order-customer-submit-button-row").remove()

        //enable product and payment creation
        $("#add-product-button, #add-payment-button").show()


        //set product/payment input to enabled style
        $(".order-details-product-info-input-active").each(function (index, element) {
            element = $(element)
            if (!$(element).hasClass("new-product-property")) {
                element.addClass("order-details-product-info-input-inactive")
                element.removeClass("order-details-product-info-input-active")
            }
        })
        $(".order-details-payment-info-input-active").each(function (index, element) {
            element = $(element)
            if (!$(element).hasClass("new-payment-property")) {
                element.addClass("order-details-payment-info-input-inactive")
                element.removeClass("order-details-payment-info-input-active")
            }
        })

        //remove edit product/payment button rows
        $("#edit-products-submit-button-row-clone, #edit-payments-submit-button-row-clone").remove()


        //disable product/payment inputs
        //do not disable template inputs or submit buttons
        $("#product-rows-container, #payment-rows-container").find("input").filter(
            function () {
                
                return !($(this).hasClass("new-product-property") || $(this).hasClass("new-payment-property")
                       || $(this).attr("id") == "add-payments-submit-button" || $(this).attr("id") == "edit-payments-submit-button"
                       || $(this).attr("id") == "add-products-submit-button" || $(this).attr("id") == "edit-products-submit-button"
                       || $(this).attr('type') == "hidden")
            }).prop("disabled", true)

        $("#product-rows-container, #payment-rows-container").find("textarea").filter(
            function () {
                return !($(this).hasClass("new-product-property") || $(this).hasClass("new-payment-property"))
            }).prop("disabled", true)
        $("#product-rows-container, #payment-rows-container").find("select").filter(
            function () {
                return !($(this).hasClass("new-product-property") || $(this).hasClass("new-payment-property"))
            }).prop("disabled", true)

    }


}







var toggleEditCall = function () {
    document.getElementById("edit-switch").checked = false
    toggleEdit()
}




var paymentUpdate = function () {

    document.getElementById("edit-switch").checked = false
    toggleEdit()

    paymentCount = 0;
    var totalPayments = 0

    $(".set-payment-amount").each(function () {
        
       
        totalPayments += parseFloat($(this).val())
    })

    //subtract total payments variable from balance input value 
    $("input").filter(function () {
        return ($(this).attr("name") == "updatedOrder.Balance")
    }).each(function () {
        
        var totalPrice = parseFloat($("#total-price-input").val())
        $(this).val(totalPrice -= totalPayments)
    })

}


var productUpdate = function () {

    document.getElementById("edit-switch").checked = false
    toggleEdit()


    productCount = 0;

    var totalPrice = 0


    //add each product price to totalPrice variable
    totalPrice += parseFloat($("#delivery-fee-input").val())
    console.log("this~~~~")
    console.log(parseFloat($("#delivery-fee-input").val()))
    $(".set-product-price").each(function () {
        
        totalPrice += parseFloat($(this).val())
    })


    //set total and balance input values to totalPrice variable
    $("input").filter(function () {
        return ($(this).attr("name") == "updatedOrder.Balance" || $(this).attr("name") == "updatedOrder.Total")
    }).each(function () {
        
        $(this).val(totalPrice)
    })

    paymentUpdate()

}
















var productCount = 0
var productGreyRow = false

function addProduct() {
    //clone product row template
    var newProductRow = $("#sample-product").clone()

    //if adding first product and that product has the grey class
    if (productCount == 0 && newProductRow.hasClass("table-row-grey")) 
        productGreyRow = true //grey row is true
    

  
    //if grey row is false then remove grey class, else, add grey class
    productGreyRow == false ? newProductRow.removeClass("table-row-grey") : newProductRow.addClass("table-row-grey")


    newProductRow.css("display", "flex")
    newProductRow.addClass("new-product-row")
    newProductRow.find(".product-name").attr("name", ("products[" + productCount.toString() + "].Name"))
    newProductRow.find(".product-dimensions").attr("name", ("products[" + productCount.toString() + "].Dimensions"))
    newProductRow.find(".product-description").attr("name", ("products[" + productCount.toString() + "].Description"))
    newProductRow.find(".product-price").attr("name", ("products[" + productCount.toString() + "].Price"))
    newProductRow.find(".product-category").attr("name", ("products[" + productCount.toString() + "].Category.Name"))

    //append new product
    $("#product-rows-container").append(newProductRow)



    
    
    var addProductsSubmitButtonRow = $("#add-products-submit-button-row").clone()
    addProductsSubmitButtonRow.attr("id", "add-products-submit-button-row-clone")
    addProductsSubmitButtonRow.show()
    $("#add-products-submit-button-row-clone").remove() //remove previous button
    $("#product-rows-container").append(addProductsSubmitButtonRow) //append new button


    productCount += 1;
    productGreyRow = !productGreyRow //switch row color for next prow
}

var paymentCount = 0
var paymentGreyRow = false

function addPayment() {
    //clone payment row template
    var newPaymentRow = $("#sample-payment").clone()

    //if adding first payment and that product has the grey class
    if (paymentCount == 0 && newPaymentRow.hasClass("table-row-grey"))
        paymentGreyRow = true //grey row is true

    //if grey row is false then remove grey class, else, add grey class
    paymentGreyRow == false ? newPaymentRow.removeClass("table-row-grey") : newPaymentRow.addClass("table-row-grey")

    newPaymentRow.css("display", "flex")
    newPaymentRow.addClass("new-product-row")
    newPaymentRow.find(".payment-amount").attr("name", ("payments[" + paymentCount.toString() + "].PaymentAmount"))
    newPaymentRow.find(".payment-type").attr("name", ("payments[" + paymentCount.toString() + "].PaymentType"))

    //append new product
    $("#payment-rows-container").append(newPaymentRow)

    var addPaymentSubmitButtonRow = $("#add-payments-submit-button-row").clone()
    addPaymentSubmitButtonRow.attr("id", "add-payments-submit-button-row-clone")
    addPaymentSubmitButtonRow.show()
    $("#add-payments-submit-button-row-clone").remove() //remove previous button
    $("#payment-rows-container").append(addPaymentSubmitButtonRow) //add new button

    paymentCount += 1;
    paymentGreyRow = !paymentGreyRow //switch row color for next prow

}