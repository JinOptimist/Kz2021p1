$(document).ready(function () {
    $("#police-call-modal").iziModal();

    $(".createCall").click(function () {
        const url = '/police/callThePolice';

        let formData = new FormData();
        formData.append("CallPriority", $("[name=CallPriority]").val());
        formData.append("Description", $("[name=Description]").val());

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                $.toast({
                    heading: 'Success',
                    text: 'Your call was accepted',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
                $("[name=Description]").val('');
                $("#police-call-modal").iziModal("close");
            },
            error: function (response) {
                const errors = response.responseJSON.errors;
                for (let key in errors) {
                    for (var i = 0; i < errors[key].length; i++) {
                        $(`.error-${key}`).show();
                        $(`.error-${key}`).text(errors[key][i]);
                    }
                }
            }
        });
    })
})