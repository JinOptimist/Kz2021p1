$(document).ready(function () {
    $('.deletePupil').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var iin = clicked.attr('data-name');
        var url = '/Person/RemovePupil?iin=' + iin;        

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.user').remove();
            }
        });
    });
});
