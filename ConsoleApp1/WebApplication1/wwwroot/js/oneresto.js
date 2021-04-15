$(document).ready(function () {
    $('.backto').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');
        var name = clicked.attr('data-name');
        if (name == "back") {
            var url = '/Restorans/BackToAvailable?name='+name;
        }
       // $.get(url).done();
    });
});
