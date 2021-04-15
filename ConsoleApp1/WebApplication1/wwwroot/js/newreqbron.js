$(document).ready(function () {
    $('.delete').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var number = clicked.attr('data-number');
        var url = '/Restorans/RemoveBron?number='+ number;

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.bron').remove();
            }
        });
    });
});
