
$(document).ready(function () {
    $('.delete').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var title = clicked.attr('data-model');
        var url = '/TripRoute/Remove?title=' + title;
        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.route').remove();
            }
        });
    });




});