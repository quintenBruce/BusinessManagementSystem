var productIndex = _productIndex //Defined in html file


if (productIndex == 0)
    document.getElementById("remove-product").style.display = "none"

function updateTotalAndBalance() {
    var totalPrice = 0
    var totalPriceSpans = $(".total-price")
    var balanceSpans = $(".balance")

    var j;
    for (j = 0; j < productIndex + 1; j++) {
        var name = "input[name='Products[" + j.toString() + "].Price']";
        var currentPrice = parseInt($(name).val())
                                           
        if (currentPrice)
            totalPrice += currentPrice
    }

    if (parseInt($("#DeliveryFee").val()))
        totalPrice += parseInt($("#DeliveryFee").val()) //add delivery fee to total price

    isNaN(totalPrice) ? totalPriceSpans.html("0") : totalPriceSpans.html(totalPrice.toString())

    $("#DownPaymentAmount").val() == "" ? balanceSpans.html(totalPrice.toString()) : balanceSpans.html(totalPrice - parseInt($("#DownPaymentAmount").val()))
}







$("#Product\\.Price, #DeliveryFee, #DownPaymentAmount").each(function () { //add change event listener to update total price
    $(this).change(function () {
        updateTotalAndBalance()
    })
})


$(document).ready(function () {
    updateTotalAndBalance()

});












function addProduct() {
    productIndex += 1
    if (productIndex == 0) {
        document.getElementById("remove-product").style.display = "none";
    }

    else {
        document.getElementById("remove-product").style.display = "initial";
    }

    
    var buttonRow = $("#button-row").parent();
    var newProductRow = $("#initial-product-row").clone();

    productIndex % 2 == 1 ? newProductRow.removeClass("table-row-grey") : newProductRow.addClass("table-row-grey")

    

    newProductRow.find("input").filter(function () {
        console.log($(this).attr('id'))
        return $(this).attr('id') == "Product.Name";
    }).attr("name", "Products[" + productIndex.toString() + "].Name")
        .val("")
        .attr("id") == "";

    newProductRow.find("textarea").filter(function () {
        return $(this).attr('id') == "Product.Description";
    }).attr("name", "Products[" + productIndex.toString() + "].Description")
        .val("").attr("id") == "";

    newProductRow.find("input").filter(function () {
        return $(this).attr('id') == "Product.Dimensions";
    }).attr("name", "Products[" + productIndex.toString() + "].Dimensions")
        .val("").attr("id") == "";

    newProductRow.find("select").filter(function () {
        return $(this).attr('id') == "Product.Category";
    }).attr("name", "Products[" + productIndex.toString() + "].Category.Id")
        .val("").attr("id") == "";

    newProductRow.find("input").filter(function () {
        return $(this).attr('id') == "Product.Price";
    }).attr("name", "Products[" + productIndex.toString() + "].Price")
        .val("")
        .change(function () { //add change event listener
            updateTotalAndBalance()
        }).attr("id") == ""

    newProductRow.attr("id", "product-input-" + productIndex.toString())
    newProductRow.insertBefore(buttonRow)
}



function removeProduct() {
    productIndex -= 1;

    productIndex <= 0 ? document.getElementById("remove-product").style.display = "none" : document.getElementById("remove-product").style.display = "initial";

    document.getElementById("product-input-" + (productIndex + 1).toString()).remove();
    updateTotalAndBalance()
}