
/*
event listener on search by name input form to check if the input
value is a valid search string
*/

$("#search-by-name-form").submit(function (event) {
    if ($("#search-by-name-input").val().trim() == "" || $("#search-by-name-input").val().trim() == null || $("#search-by-name-input").val().trim() == " ") {
        event.preventDefault()
        alert("Invalid Search String")
    }


})

