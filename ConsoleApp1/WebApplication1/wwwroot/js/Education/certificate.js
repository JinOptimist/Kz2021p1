$(document).ready(function () {
    $('.cancelCertificate').click(function () {
        var clicked = $(this);
       // clicked.attr('disabled', 'disabled');

        var iin = clicked.attr('data-name');
        var certificateType = clicked.attr('id');
        
        console.log(iin);
        console.log(certificateType);
        
        var url = '/Person/CancelCertificate?iin=' + iin + '&certificateType=' + certificateType;

        $.get(url).done(function (answer) {
            if (answer) {
                clicked.closest('.cancelBtn').remove();
            }
        });
    });
});
