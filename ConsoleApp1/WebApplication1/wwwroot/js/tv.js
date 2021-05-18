$(document).ready(function () {
    $('.delete').click(function () {
        var clicked = $(this);
        var isConfirmed = confirm('Are u sure to delete?');

        if (isConfirmed) {
            clicked.attr('disabled', 'disabled');

            var name = clicked.attr('data-name');
            var url = '/TvProgramme/Delete?id=' + name;

            $.get(url).done(function (answer) {
                if (answer) {
                    clicked.closest('.programme').remove();
                }
            });
        }

    });
    $('.delete-schedule').click(function () {
        var clicked = $(this);

        clicked.attr('disabled', 'disabled');

        var name = clicked.attr('data-name');
        var url = '/TvSchedule/Delete?id=' + name;

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.schedule').remove();
            }
        });

    });

    $('.loadStaff').click(function () {
        debugger
        var clicked = $(this);
        var name = clicked.attr('data-name');
        $.ajax({
            type: 'GET',
            url: '/TvStaff/StaffListOfProgramme',
            data: { 'programmeName': name },
            success: function (data) {
                $('#partialView').html(data);
            }
        });  
    });

    $('.loadCelebrity').click(function () {
        var clicked = $(this);
        var name = clicked.attr('data-name');
        $.ajax({
            type: 'GET',
            url: '/TvCelebrity/CelebrityListOfProgramme',
            data: { 'programmeName': name },
            success: function (data) {
                $('#partialViewCelebrity').html(data);
            }
        });
    });
});
