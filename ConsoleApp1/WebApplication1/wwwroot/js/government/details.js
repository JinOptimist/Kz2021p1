$(document).ready(function () {

    $("#tbCandidates").on("click", "#deleteCandidateBtn", function() {

        var clicked = $(this);

        clicked.attr('disabled', 'disabled');

        var id = clicked.attr('data-id');

        var url = `/Elections/DeleteCandidate?id=${id}`

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest("tr").remove()
                location.reload();
            }

            console.log(answer)
        });
    });

    $("#tbCandidates").on("click", "#voteBtn", function() {

        $('.voteBtn').addClass('disabled');

        var clicked = $(this);
        var election = clicked.attr('data-election');
        var candidate = clicked.attr('data-candidate');

        var url = `/Elections/Vote/${election}/${candidate}`;

        $.get(url).done(function (answer) {
            if (answer.message == "👍") {

                location.reload();
                var message = answer.message

                $('#alert-notify').show().html(message)
            }
            var message = answer.message

            $('#alert-notify').show().html(message)
           
        });
    });


    var chart = Highcharts.chart('container', {

        data : {
            table: 'datatable'
        },
        chart : {
            type: 'pie'
        },
        plotOptions : {
            pie: {
                shadow: false,
                center: ['50%', '50%']
            }
        },
        title : {
            text: `Ход голосования`
        },
        subtitle : {
            text:  new Date($.now())
        },
        yAxis : {
            allowDecimals: false,
            title: {
                text: 'Units'
            }
        },
        tooltip : {valueSuffix: '%'},
        credits : {
            enabled: true
        },
        legend : {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
    });
});
