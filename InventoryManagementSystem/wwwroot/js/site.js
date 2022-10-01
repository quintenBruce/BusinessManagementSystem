/*
event listener on search by name input form to check if the input
value is a valid search string
*/

$(document).ready(function () {

    $('textarea').each(function () {
        $(this).height(0).height(this.scrollHeight);
    })
    

    console.log("ready!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

    $('textarea').on('change keyup paste cut', function () {
        if (/Mobi|Android/i.test(navigator.userAgent)) {
            $(this).height(0).height(this.scrollHeight);
        }
    }).find('textarea').trigger("change");

});



$("#search-by-name-form").submit(function (event) {
    if ($("#search-by-name-input").val().trim() == "" || $("#search-by-name-input").val().trim() == null || $("#search-by-name-input").val().trim() == " ") {
        event.preventDefault()
        alert("Invalid Search String")
    }
})   