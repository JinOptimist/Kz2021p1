$(document).ready(function () {

    $("#tbCandidates").on("click", "#deleteCandidateBtn", function() {

        var clicked = $(this);

        clicked.attr('disabled', 'disabled');

        console.log('delete candidate')

        var id = clicked.attr('data-id');
        
        var url = `/Elections/DeleteCandidate?id=${id}`

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest("tr").remove();
                console.log("from delete candidate", answer)
            }

            console.log( "something went wrong")
        });
    });

    $("#tbCandidates").on("click", "#voteBtn", function() {
        
        $('.voteBtn').addClass('disabled');
        var clicked = $(this);
        var election = clicked.attr('data-election');
        var candidate = clicked.attr('data-candidate');

        var url = `/Elections/Vote/${election}/${candidate}`;

        $.get(url).done(function (answer) {
            if (answer) {
                $('#alert-notify').show().html("Ваш голос принят")
            }
            else {
                $('#alert-notify').show().html("Вы уже проголосовали")
            }
            
            location.reload(true);
        });
    });


    var chart = Highcharts.chart('container', {

        data : {
            table: 'datatable'
        },
        chart : {
            type: 'pie'
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
        credits : {
            enabled: true
        },
        legend : {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },


        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.point.y + ' ' + this.point.name.toLowerCase();
            }
        }
    });
});
