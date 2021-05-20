﻿$(document).ready(function () {
    $('.delete').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var id = clicked.attr('data-name');
        var url = '/FiremanTeam/Remove?id=' + id;
        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.team').remove();
            }
        })

    })
})