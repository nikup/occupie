
$(document).ready(function () {
    setVisibilityLeft();
    setVisibilityRight();
    setVisibilityDiv();
    setHeadingText();
});

$("#right_arrow").click(onRightArrowClicked);
$("#left_arrow").click(onLeftArrowClicked);

function setHeadingText() {
    var current = $("div.current").attr('id');
    var parts = current.split('_');
    var num = parts[1];
    $("h2").text('За Occupie (' + num + '/10)');
}

function onRightArrowClicked() {

    var formerCurr = $("div.current");
    $("div.current").removeClass("current");
    formerCurr.next().addClass("current");

    setVisibilityLeft();
    setVisibilityRight();
    setVisibilityDiv();
    setHeadingText();
}

function onLeftArrowClicked() {

    var formerCurr = $("div.current");
    $("div.current").removeClass("current");
    formerCurr.prev().addClass("current");

    setVisibilityLeft();
    setVisibilityRight();
    setVisibilityDiv();
    setHeadingText();
}

function setVisibilityDiv() {
    $("div.slide").css("display", "none");
    $("div#slides").css("display", "block");
    $("div.nav").css("display", "block");
    $(".current").css("display", "block");
    $(".current").css("visibility", "visible");
}

function setVisibilityRight() {
    var asd = $("div.current").next().attr('id');
    if (!$("div.current").is(':last-child')) {
        $("img#right_arrow").css("visibility", "visible");
    }
    else {
        $("img#right_arrow").css("visibility", "hidden");
    }
}

function setVisibilityLeft() {
    if (!$("div.current").is(':first-child')) {
        $("img#left_arrow").css("visibility", "visible");
    }
    else {
        $("img#left_arrow").css("visibility", "hidden");
    }
}