$(document).ready(function () {
    $('.delete').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var name = clicked.attr('data-name');
        var url = '/Restorans/Remove?name=' + name;        

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.resto').remove();
            }
        });
    });
});
