$(document).ready(function () {

    $("#tbElections").on("click", "#deleteElectionBtn", function() {
        var clicked = $(this);
        
        clicked.attr('disabled', 'disabled');
        
        var id = clicked.attr('data-name');
        
        var url = '/Elections/DeleteElections?id=' + id;
        
        $.get(url).done(function (answer) {
            if(answer)
            {
                clicked.closest("tr").remove();
                console.log(answer)
            }
            console.log( "something went wrong", answer)

        })
    });

    $("#tbElections").on("click", "#detailsBtn", function() {

        var id = $(this).attr('data-id');

        var url = '/JsonDataProvider?id=' + id;

        $.get(url).done(function (answer) {

        })
    });
    
});
