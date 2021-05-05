$(document).ready(function () {
    $('.delete').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var id = clicked.attr('estab-id');
        var url = '/HCEstablishments/Remove?id=' + id;

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.user').remove();
            }
        });
    });
});
