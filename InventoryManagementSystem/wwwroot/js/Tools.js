

$(document).mouseup(function (e) {
    var container = $("#winner-alert-container");

    // if the target of the click isn't the container nor a descendant of the container
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        container.hide();
    }
});

function saveDrawingCookie() {
    console.log("jsdfkjhsdfjuhdsfjhsdfkhsdf")
    const date = new Date();
    date.setTime(date.getTime() + (24 * 60 * 60 * 50000))
    cookieValue = $("#drawing-form").serialize()
    document.cookie = "drawingCookie=" + cookieValue + "; expires=" + date.toUTCString()
}


$(document).ready(function () {
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');

    for (var i = 0; i < ca.length; i++) {
        cookiePair = ca[i].split("=");
        if (cookiePair[0].trim() == "drawingCookie") {
            cookieValueArray = ca[i].split("&")
            cookieValueArray[0] = cookieValueArray[0].replace("drawingCookie=", "")
            $("#num-winners-input").val(cookieValueArray[0].split("=")[1])
            cookieValueArray.splice(0, 1)
             
            for (var i = 0; i < cookieValueArray.length; i = i + 2) {
                $("#entry-container").css("display", "block") //show container

                var rowClone = $("#entry-row-prototype").clone(true)
                                                        .css("display", "flex")
                                                        .removeClass("prototype")
                                                        .removeAttr('id')


                var index = $("#entry-container").children().length
                rowClone.children().children().filter(".name-input").attr("name", "entry" + index + "Name").attr("form", "drawing-form").change(function () { saveDrawingCookie() }).val(cookieValueArray[i].split("=")[1])
                rowClone.children().children().filter(".num-entries-input").attr("name", "entry" + index + "NumEntries").attr("form", "drawing-form").change(function () { saveDrawingCookie() }).val(cookieValueArray[i+1].split("=")[1])

                $("#entry-container").append(rowClone)
                
            }
            break;
        }
    } 
});




$("#draw-button").click(function (form) {
    if ($("#num-winners-input").val() == null || $("#num-winners-input").val() == "" || parseInt($("#num-winners-input").val()) < 1 || parseInt($("#num-winners-input").val()) >= 4) {
        alert("Please enter the number of winners (1-3)")
        return
    }
    else {
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');

        for (var i = 0; i < ca.length; i++) {
            cookiePair = ca[i].split("=");
            if (cookiePair[0].trim() == "drawingCookie") {
                cookieValueArray = ca[i].split("&")
                cookieValueArray[0] = cookieValueArray[0].replace("drawingCookie=", "")
                var numWinners = cookieValueArray[0].split("=")[1]
                var winners = [];
                cookieValueArray.splice(0, 1)
                var entries = [];

                for (var i = 0; i < cookieValueArray.length; i = i + 2) {
                    for (var j = 0; j < cookieValueArray[i + 1].split("=")[1]; j++) {
                        entries.push(cookieValueArray[i].split("=")[1])
                    }
                }
                for (var i = 0; i < parseInt($("#num-winners-input").val()); i++) {
                    winners[i] = entries[Math.floor(Math.random() * entries.length)];
                    entries = entries.filter(item => !(item == winners[i]))
                }

                console.log(winners)
                var winnerAlertContainer = $("#winner-alert-container")
                winnerAlertContainer.children().children()[1].innerHTML = typeof winners[0] === 'undefined' ? " " : "1st Place: " + winners[0]
                winnerAlertContainer.children().children()[2].innerHTML = typeof winners[1] === 'undefined' ? " " : "2nd Place: " + winners[1]
                winnerAlertContainer.children().children()[3].innerHTML = typeof winners[2] === 'undefined' ? " " : "3rd Place: " + winners[2]
                winnerAlertContainer.show()
                break;
            }
        }
    }

    
});



$("#add-entry-button").click(function () {

    $("#entry-container").css("display", "block") //show container
   
    var rowClone = $("#entry-row-prototype").clone(true)
                                            .css("display", "flex")
                                            .removeClass("prototype")
                                            .removeAttr('id')
                                            

    var index = $("#entry-container").children().length
    rowClone.children().children().filter(".name-input").attr("name", "entry" + index + "Name").attr("form", "drawing-form").change(function () { saveDrawingCookie() })
    rowClone.children().children().filter(".num-entries-input").attr("name", "entry" + index + "NumEntries").attr("form", "drawing-form").change(function () { saveDrawingCookie() })

    $("#entry-container").append(rowClone)
});
 
$("#clear-button").click(function () {
    
    $(".entry-row").filter(function () {
        return !($(this).hasClass("prototype"))
    }).remove() //remove all entry rows that are not the prototype

    $("#entry-container").css("display", "none") //remove container from UI
    saveDrawingCookie()
});

$(".remove-entry").click(function () {
    $(this).parent().parent().remove()
    saveDrawingCookie()
});

$("#num-winners-input").change(function () {
    saveDrawingCookie()
});

