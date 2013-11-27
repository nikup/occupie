function manageEndFields(i) {
    $("#Jobs_" + i + "__IsCurrent").click(function () {
        if ($(this).is(":checked")) {
            $("#endFields" + i).fadeOut();
        } else {
            $("#endFields" + i).fadeIn();
        }
    });
}

function hideEndField(i) {
    $("#endFields" + i).fadeOut();
}