var productIndex = 0;
if (productIndex == 0) {
    document.getElementById("remove-product").style.display = "none";
}



var priceInputElement0 = document.getElementById("orderGroup_products_0__Price");
priceInputElement0.addEventListener("change", updateTotalPrice);

function updateTotalPrice(e) {
    var totalPrice = 0;
    var totalPriceSpan = document.getElementById("total-price");
    var j;
    for (j = 0; j < productIndex + 1; j++) {
        var currentPrice = parseInt(document.getElementById("orderGroup_products_" + j.toString() + "__Price").value);
        if (isNaN(currentPrice) == false) {
            totalPrice += currentPrice;
        }

    }
    totalPriceSpan.innerHTML = totalPrice.toString();
}



const form = document.getElementById("order-form");
form.addEventListener("submit", (e) => {
    console.log(errors);
    var errors = 0;

    if (customerName.value === "" || customerName.value == null) {
        errors += 1;
        document.getElementById("customer_First_name_validation").innerHTML = "Customer First Name Is Required";
    }

    else 
        document.getElementById("customer_First_name_validation").innerHTML = "";
    

    if (communcationThread.value == "Select a communication thread") {
        errors += 1;
        document.getElementById("order_Com_thread_validation").innerHTML = "Communication Thread Is Required";
    }

    else 
        document.getElementById("order_Com_thread_validation").innerHTML = "";
    


    if (orderFulfillmentDate.value === "" || orderFulfillmentDate.value == null) {
        errors += 1;
        document.getElementById("order_Order_fulfillment_date_validation").innerHTML = "Order Fulfillment Date Is required";
    }

    else
        document.getElementById("order_Order_fulfillment_date_validation").innerHTML = "";

    var i;
    for (i = 0; i < productIndex + 1; i++) {
        if (document.getElementById("orderGroup_products_" + i.toString() + "__Name").value === "" || document.getElementById("orderGroup_products_" + i.toString() + "__Name").value == null) {
            errors += 1;
            if (document.getElementById("products_" + i.toString() + "__Name_validation") == null) {
                var message = document.createElement("span");
                message.id = "products_" + i.toString() + "__Name_validation";
                message.className = "text-danger";
                message.innerHTML = "Product Name Is Required";
                var parent = document.getElementById("orderGroup_products_" + i.toString() + "__Name").parentNode;
                parent.appendChild(message);
            }
        }
        else {
            if (document.getElementById("products_" + i.toString() + "__Name_validation") != null) {
                document.getElementById("products_" + i.toString() + "__Name_validation").remove();
            }
        }




        if (document.getElementById("orderGroup_products_" + i.toString() + "__Category_Id").value == "Select a category") {
            errors += 1;

            if (document.getElementById("categoryIds_" + i.toString() + "_validation") == null) {

                var message = document.createElement("span");
                message.id = "categoryIds_" + i.toString() + "_validation";
                message.className = "text-danger";
                message.innerHTML = "Category Is Required";
                var parent = document.getElementById("orderGroup_products_" + i.toString() + "__Category_Id").parentNode;
                parent.appendChild(message);
            }
        }
        else {

            if (document.getElementById("categoryIds_" + i.toString() + "_validation") != null) {
                document.getElementById("categoryIds_" + i.toString() + "_validation").remove();
            }
        }

        if (document.getElementById("orderGroup_products_" + i.toString() + "__Price").value === "" || document.getElementById("orderGroup_products_" + i.toString() + "__Price").value == null) {
            errors += 1;

            if (document.getElementById("products_" + i.toString() + "__Price_validation") == null) {

                var message = document.createElement("span");
                message.id = "products_" + i.toString() + "__Price_validation";
                message.className = "text-danger";
                message.innerHTML = "Product Price Is Required";
                var parent = document.getElementById("orderGroup_products_" + i.toString() + "__Price").parentNode;
                parent.appendChild(message);
            }
        }
        else {

            if (document.getElementById("products_" + i.toString() + "__Price_validation") != null) {
                document.getElementById("products_" + i.toString() + "__Price_validation").remove();
            }
        }

    }





    console.log(errors);

    if (errors > 0) {
        e.preventDefault();
    }

    else {
        console.log($("#customer-first-name-input").val())
        if ($("#customer-middle-name-input").val() != "")
            $("#customer-first-name-input").val($("#customer-first-name-input").val().concat(" ", $("#customer-middle-name-input").val()))
        if ($("#customer-last-name-input").val() != "")
            $("#customer-first-name-input").val($("#customer-first-name-input").val().concat(" ", $("#customer-last-name-input").val()))

        console.log($("#customer-first-name-input").val())
    }

})





var customerName = document.getElementById("customer-first-name-input");
var communcationThread = document.getElementById("orderGroup_order_Com_thread");
var orderFulfillmentDate = document.getElementById("orderGroup_order_Order_fulfillment_date");





function addProduct() {
    productIndex += 1;
    if (productIndex == 0) {
        document.getElementById("remove-product").style.display = "none";
    }

    else {
        document.getElementById("remove-product").style.display = "initial";
    }
    var form = document.getElementById("order-form");
    var buttonRow = document.getElementById("button-row");
    var productDiv = document.getElementById("product-input-0").cloneNode(true);
    productDiv.id = "product-input-" + productIndex.toString();
    var productDivChildNodes = productDiv.children;
    productDivChildNodes[0].children[0].children[1].id = "orderGroup_products_" + productIndex.toString() + "__Name"; //product name input id and name update
    productDivChildNodes[0].children[0].children[1].setAttribute("name", "orderGroup.products[" + productIndex.toString() + "].Name");
    productDivChildNodes[0].children[0].children[1].value = "";
    if (productDivChildNodes[0].children[0].children[2] != null) {
        productDivChildNodes[0].children[0].children[2].remove();
    }
    productDivChildNodes[0].children[1].children[1].id = "orderGroup_products_" + productIndex.toString() + "__Description"; //product description id and name update
    productDivChildNodes[0].children[1].children[1].setAttribute("name", "orderGroup.products[" + productIndex.toString() + "].Description");
    productDivChildNodes[0].children[1].children[1].value = "";
    productDivChildNodes[0].children[2].children[1].id = "orderGroup_products_" + productIndex.toString() + "__Dimensions"; //product description id and name update
    productDivChildNodes[0].children[2].children[1].setAttribute("name", "orderGroup.products[" + productIndex.toString() + "].Dimensions");
    productDivChildNodes[0].children[2].children[1].value = "";
    productDivChildNodes[0].children[3].children[1].id = "orderGroup_products_" + productIndex.toString() + "__Category_Id"; //product description id and name update
    productDivChildNodes[0].children[3].children[1].setAttribute("name", "orderGroup.products[" + productIndex.toString() + "].Category.Id");
    if (productDivChildNodes[0].children[3].children[2] != null) {
        productDivChildNodes[0].children[3].children[2].remove();
    }
    productDivChildNodes[0].children[4].children[1].children[1].id = "orderGroup_products_" + productIndex.toString() + "__Price"; //product description id and name update
    productDivChildNodes[0].children[4].children[1].children[1].setAttribute("name", "orderGroup.products[" + productIndex.toString() + "].Price");
    productDivChildNodes[0].children[4].children[1].children[1].value = "";
    productDivChildNodes[0].children[4].children[1].children[1].addEventListener("change", updateTotalPrice);
    if (productDivChildNodes[0].children[4].children[1].children[2] != null) {
        productDivChildNodes[0].children[4].children[1].children[2].remove();
    }

    form.insertBefore(productDiv, buttonRow);

}

function removeProduct() {
    productIndex -= 1;

    if (productIndex <= 0) {
        document.getElementById("remove-product").style.display = "none";
        document.getElementById("product-input-" + (productIndex + 1).toString()).remove();
    }
    else {
        document.getElementById("remove-product").style.display = "initial";
        document.getElementById("product-input-" + (productIndex + 1).toString()).remove();
    }
}
