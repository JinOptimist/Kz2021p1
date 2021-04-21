$(document).ready(function () {

    $("#tbCandidates").on("click", "#deleteCandidateBtn", function() {
       
        var clicked = $(this);

        clicked.attr('disabled', 'disabled');

        var candidateId = clicked.attr('data-candidateId');
        var electionId = clicked.attr('data-electionId');
        
    /*    var url = '/Elections/DeleteCandidate?candidateId=' + candidateId + '&electionId=' + electionId;*/
      //  var url = '/Elections/DeleteCandidate/' + candidateId + '/' + electionId;

        var url = `/Elections/DeleteCandidate/${candidateId}/${electionId}`

        console.log("url is", url)

        $.post(url).done(function (answer) {
             if (answer) {
                 clicked.closest("tr").remove()
                 console.log("answer Right")
             }
            console.log("answer No")
           
        });

    
        
    });

    

   
    $('#voteBtn').click(function () {
        var clicked = $(this);

        clicked.attr('disabled', 'disabled');

        var name = clicked.attr('data-name');
        var url = '/Elections/Vote?name=' + name;

        $.get(url).done(function (answer) {
            if (answer) {
                console.log("You voted " , name)
            }
        });
    });

    

});
