// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Shared code
$("#deleteModal").dialog({
    autoOpen: false,
    resizable: false,
    height: "auto",
    width: 400,
    modal: true,
    buttons: {
        "Delete": function() {
            $("#deleteItem").closest("form").attr("action", window.location.href + "?handler=Delete");
            $("#deleteItem").closest("form").submit();
            $("#deleteModal").css("visibility", "hidden");
            $(this).dialog("close");
        },
        Cancel: function() {
            $("#deleteModal").css("visibility", "hidden");
            $(this).dialog("close");
        }
    }
});

$("#deleteItem").click(function(event) {
    event.preventDefault();
    $("#deleteModal").css("visibility", "visible");
    $("#deleteModal").dialog("open");
});

function get(url) {
    return new Promise(function(resolve, reject) {
        let req = new XMLHttpRequest();
        req.open("GET", url);

        req.onload = function() {
            if (req.status === 200) {
                resolve(req.response);
            } else {
                reject(Error(req.statusText));
            }
        }

        req.onerror = function() {
            reject(Error("Network error?"));
        }

        req.send();
    })
}