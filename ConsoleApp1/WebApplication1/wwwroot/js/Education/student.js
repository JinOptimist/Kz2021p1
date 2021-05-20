$(document).ready(function () {
    $('.deleteStudent').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var iin = clicked.attr('data-name');
        var url = '/Person/RemoveStudent?iin=' + iin;
        console.log(url);

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.user').remove();
            }
        });
    });

    $('.grant').click(function () {
        var clicked = $(this);

        var studentId = clicked.attr('data-name');
        var url = '/Person/StudentGrantIndividual?studentId=' + studentId;

        if (this.value == "Забрать грант") {
            $.get(url).done(function (answer) {
                if (answer) {
                    this.value("Выдать грант");
                }
            });
        }
        else {
            $.get(url).done(function (answer) {
                if (answer) {
                    this.value("Забрать грант");
                }
            });
        }
        $window.location.reload(true);
    });

});
