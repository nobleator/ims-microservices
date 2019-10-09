class Site {
    siteId;
    address;
    description;
    latitude;
    longitude;
}

// Global variables (probably should change this...)
var siteSelectorMap;
var sitePin;
var siteObjects;

// Page ready
$(document).ready(function() {
    get(window.location.href + "?handler=ApiKey").then(function(response) {
        let token = response.toString();
        let startLat = Number($("[name='Transaction.AssociatedSite.Latitude']").val());
        let startLon = Number($("[name='Transaction.AssociatedSite.Longitude']").val());
        let startZoom = 13;
        siteSelectorMap = L.map("siteSelectorMap").setView([startLat, startLon], startZoom);
        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox.streets',
            accessToken: token
        }).addTo(siteSelectorMap);
        // Setting type to "any" to stop TypeScript complaining. Need to fix underlying issue probably?
        // TODO: Make draggable depend on dropdown selection (i.e., only on "Add new site")
        sitePin = L.marker([startLat, startLon], {
            title: "Here is a map pin",
            draggable: true
        }).addTo(siteSelectorMap);
        sitePin.on("dragend", function() {
            // Fill in lat/long values in form when pin is moved
            let currentPosition = sitePin.getLatLng();
            $("#modalTempLatitude").val(currentPosition.lat);
            $("#modalTempLongitude").val(currentPosition.lng);
        });
        // Per https://github.com/Leaflet/Leaflet/issues/694
        siteSelectorMap.invalidateSize();
    },
    function(error) {
        console.error("AJAX request failed!", error);
    });

    get(window.location.href + "?handler=SiteObjects").then(function(response) {
        siteObjects = JSON.parse(response.toString());
    },
    function(error) {
        console.error("AJAX request failed!", error);
    });

    if (document.getElementById("productTable")) {
        $("#productTable").DataTable();
    }
    if (document.getElementById("clientTable")) {
        $("#clientTable").DataTable();
    }
    if (document.getElementById("transactionTable")) {
        $("#transactionTable").DataTable();
    }

    sitePin.dragging.disable();
});

$("#siteSelectBox").change(function() {
    let selectedSiteId = $(this).val().toString();
    // Existing site
    if (selectedSiteId) {
        $("[name='Transaction.AssociatedSite.Address']").prop("readonly", true);
        $("[name='Transaction.AssociatedSite.Description']").prop("readonly", true);
        sitePin.dragging.disable();
        let selectedSite;
        for (var i = 0; i < siteObjects.length; i++) {
            if (siteObjects[i].siteId.toLocaleLowerCase() === selectedSiteId.toLocaleLowerCase()) {
                selectedSite = siteObjects[i];
                break;
            }
        }
        $("[name='Transaction.AssociatedSite.SiteId']").val(selectedSite.siteId);
        $("[name='Transaction.AssociatedSite.Address']").val(selectedSite.address);
        $("[name='Transaction.AssociatedSite.Description']").val(selectedSite.description);
        $("[name='Transaction.AssociatedSite.Latitude']").val(selectedSite.latitude);
        $("[name='Transaction.AssociatedSite.Longitude']").val(selectedSite.longitude);
        let newPinLocation = new L.LatLng(selectedSite.latitude, selectedSite.longitude);
        sitePin.setLatLng(newPinLocation);
        siteSelectorMap.panTo(newPinLocation);
    }
    // New site
    else {
        $("[name='Transaction.AssociatedSite.SiteId']").val("00000000-0000-0000-0000-000000000000");
        $("[name='Transaction.AssociatedSite.Address']").prop("readonly", false);
        $("[name='Transaction.AssociatedSite.Description']").prop("readonly", false);
        sitePin.dragging.enable();
    }
});

$("#addRow").click(function() {
    get(window.location.href + "?handler=NewTableRow").then(function(response) {
        let newRow = document.getElementById("datagrid").getElementsByTagName("tbody")[0].insertRow(-1);
        newRow.innerHTML = response.toString();
    },
    function(error) {
        console.error("AJAX request failed!", error);
    });
});