$(document).ready(function () {
    $('.cancelCertificate').click(function () {
        var clicked = $(this);
        var iin = clicked.attr('data-name');
        var certificateType = $('#selectedCertificate option:selected').val();

        var url = '/Person/CancelCertificate?iin=' + iin + '&certificateType=' + certificateType;

        $.get(url).done(function (answer) {
            if (answer) {
                window.location.reload(true);
            }
        });
    });

    $('.addNewCertificateType').click(function () {
        var clicked = $(this);
        var iin = clicked.attr('data-name');
        var selectedCertificateType = $('#selectedCertificateType option:selected').val();

        var url = '/Person/AddNewCertificate?iin=' + iin + '&selectedCertificateType=' + selectedCertificateType;

        $.get(url).done(function (answer) {
            if (answer) {                
                window.location.reload(true);
            }
        });
    });
});
