$("#scheduleDeliveries").click(function() {
    console.log("Button clicked!");
    get(window.location.href + "?handler=QueueDeliveryOptimization").then(function(response) {
        console.log(response);
    },
    function(error) {
        console.error("AJAX request failed!", error);
    });
});