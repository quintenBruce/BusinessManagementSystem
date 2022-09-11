if (productIndex == 0)
    document.getElementById("remove-product").style.display = "none"

function updateTotalAndBalance() {
    var totalPrice = 0
    var totalPriceSpan = $("#total-price")
    var balanceSpan = $("#balance")

    var j;
    for (j = 0; j < productIndex + 1; j++) {
        var currentPrice = parseInt($("#orderGroup_products_" + j.toString() + "__Price").val())
        if (currentPrice)
            totalPrice += currentPrice
    }

    if (parseInt($("#order-deliver-fee").val()))
        totalPrice += parseInt($("#order-deliver-fee").val()) //add delivery fee to total price

    isNaN(totalPrice) ? totalPriceSpan.html("0") : totalPriceSpan.html(totalPrice.toString())

    $("#downpayment-amount").val() == "" ? balanceSpan.html(totalPrice.toString()) : balanceSpan.html(totalPrice - parseInt($("#downpayment-amount").val()))
}

$("#orderGroup_products_0__Price, #order-deliver-fee, #downpayment-amount").each(function () { //add change event listener to update total price
    $(this).change(function () {
        updateTotalAndBalance()
    })
})

const form = document.getElementById("order-form");

form.addEventListener("submit", (e) => {
    var errors = 0;

    var customerName = $("#customer-full-name").val().trim()
    var communcationThread = $("#customer-com-thread").val()
    var fulfillmentDate = $("#order-fulfillment-date").val();

    if (customerName == "") {
        errors += 1;
        $("#customer_First_name_validation").html("Customer First Name Is Required")
    }

    else
        $("#customer_First_name_validation").html("")

    if (communcationThread == "Select a communication thread") {
        errors += 1;
        $("#order_Com_thread_validation").html("Communication Thread Is Required")
    }

    else
        $("#order_Com_thread_validation").html("")

    if (fulfillmentDate == "") {
        errors += 1;
        $("#order_Order_fulfillment_date_validation").html("Order Fulfillment Date Is required")
    }

    else
        $("#order_Order_fulfillment_date_validation").html("")

    var i;
    for (i = 0; i < productIndex + 1; i++) {
        if ($("#orderGroup_products_" + i.toString() + "__Name").val() === "") {
            errors += 1;
            if (!$("#products_" + i.toString() + "__Name_validation").length) { //create error message span
                var message = $("<span>Product Name Is Required</span>")
                    .addClass("text-danger")
                    .attr("id", "products_" + i.toString() + "__Name_validation")

                var parent = $("#orderGroup_products_" + i.toString() + "__Name").parent();
                parent.append(message)
            }
        }
        else { //delete error message span
            if ($("#products_" + i.toString() + "__Name_validation").length) {
                $("#products_" + i.toString() + "__Name_validation").remove();
            }
        }

        if (!$("#orderGroup_products_" + i.toString() + "__Category_Id").val()) {
            errors += 1;

            if (!$("#categoryIds_" + i.toString() + "_validation").length) {
                var message = $("<span>Category Is Required</span>")
                    .addClass("text-danger")
                    .attr("id", "categoryIds_" + i.toString() + "_validation")

                var parent = $("#orderGroup_products_" + i.toString() + "__Category_Id").parent();
                parent.append(message)
            }
        }
        else {
            if ($("#categoryIds_" + i.toString() + "_validation").length) {
                $("#categoryIds_" + i.toString() + "_validation").remove();
            }
        }

        if ($("#orderGroup_products_" + i.toString() + "__Price").val() === "") {
            errors += 1;

            if (!$("#products_" + i.toString() + "__Price_validation").length) {
                var message = $("<span>Product Price Is Required</span>")
                    .addClass("text-danger")
                    .attr("id", "products_" + i.toString() + "__Price_validation");

                var parent = $("#orderGroup_products_" + i.toString() + "__Price").parent().parent();
                parent.append(message)
            }
        }
        else {
            if ($("#products_" + i.toString() + "__Price_validation"))
                $("#products_" + i.toString() + "__Price_validation").remove();
        }
    }

    if (errors > 0)
        e.preventDefault();
})

var productIndex = 0;

function addProduct() {
    productIndex += 1
    if (productIndex == 0) {
        document.getElementById("remove-product").style.display = "none";
    }

    else {
        document.getElementById("remove-product").style.display = "initial";
    }

    var container = $("#create-order-div");
    var buttonRow = $("#button-row").parent();
    var newProductRow = $("#initial-product-row").clone();

    productIndex % 2 == 1 ? newProductRow.removeClass("table-row-grey") : newProductRow.addClass("table-row-grey")

    newProductRow.find("span").each(function () {
        if ($(this).html() != "$")
            $(this).html("")
    })

    newProductRow.find("input").filter(function () {
        console.log($(this).attr('id'))
        return $(this).attr('id') == "orderGroup_products_0__Name";
    }).attr("id", "orderGroup_products_" + productIndex.toString() + "__Name")
        .attr("name", "Order.Products[" + productIndex.toString() + "].Name")
        .val("")

    newProductRow.find("textarea").filter(function () {
        return $(this).attr('id') == "orderGroup_products_0__Description";
    }).attr("id", "orderGroup_products_" + productIndex.toString() + "__Description")
        .attr("name", "Order.Products[" + productIndex.toString() + "].Description")
        .val("")

    newProductRow.find("input").filter(function () {
        return $(this).attr('id') == "orderGroup_products_0__Dimensions";
    }).attr("id", "orderGroup_products_" + productIndex.toString() + "__Dimensions")
        .attr("name", "Order.Products[" + productIndex.toString() + "].Dimensions")
        .val("")

    newProductRow.find("select").filter(function () {
        return $(this).attr('id') == "orderGroup_products_0__Category_Id";
    }).attr("id", "orderGroup_products_" + productIndex.toString() + "__Category_Id")
        .attr("name", "Order.Products[" + productIndex.toString() + "].Category.Id")
        .val("")

    newProductRow.find("input").filter(function () {
        return $(this).attr('id') == "orderGroup_products_0__Price";
    }).attr("id", "orderGroup_products_" + productIndex.toString() + "__Price")
        .attr("name", "Order.Products[" + productIndex.toString() + "].Price")
        .val("")
        .change(function () { //add change event listener
            updateTotalAndBalance()
        })

    newProductRow.attr("id", "product-input-" + productIndex.toString())
    newProductRow.insertBefore(buttonRow)
}

function removeProduct() {
    productIndex -= 1;

    productIndex <= 0 ? document.getElementById("remove-product").style.display = "none" : document.getElementById("remove-product").style.display = "initial";

    document.getElementById("product-input-" + (productIndex + 1).toString()).remove();
    updateTotalAndBalance()
}