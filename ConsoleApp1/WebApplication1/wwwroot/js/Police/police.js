$(document).ready(function () {
    $("#academy-request").iziModal();

    $(".sendPoliceAcademyRequest").click(function () {
        const url = '/police/requestPoliceAcademy';
        let formData = new FormData();
        formData.append("FirstName", $("[name=FirstName]").val());
        formData.append("LastName", $("[name=LastName]").val());
        formData.append("BirthDate", $("[name=BirthDate]").val());
        formData.append("EMail", $("[name=EMail]").val());
        formData.append("PhoneNumber", $("[name=PhoneNumber]").val());

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                $.toast({
                    heading: 'Success',
                    text: 'Your request accepted, please wait',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
            },
            error: (response) => {
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

    $(".police-form-field").on("input", function () {
        const input = $(this);
        const errors = input.siblings();

        for (var i = 0; i < errors.length; i++) {
            $(errors[i]).hide();
        }
    })
});
