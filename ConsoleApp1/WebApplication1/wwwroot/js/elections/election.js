$(document).ready(function () {

    $("#tbElections").on("click", "#deleteElectionBtn", function() {
        var clicked = $(this);
        
        clicked.attr('disabled', 'disabled');
        
        var id = clicked.attr('data-name');
        
        var url = '/Elections/DeleteElections?id=' + id;
        
        $.get(url).done(function (answer) {
            clicked.closest("tr").remove()

        })
    });

    
});
