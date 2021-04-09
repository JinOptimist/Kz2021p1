
$(document).ready(function () {
    $('.delete').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var model = clicked.attr('data-model');
        var url = '/Bus/Remove?model=' + model;
        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.bus').remove();
            }
        });
    });
});