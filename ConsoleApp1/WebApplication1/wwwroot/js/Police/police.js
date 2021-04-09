$(document).ready(function () {
    let selectedUserId;

    $('.goToJail').click(function () {
        const url = '/police/gotojail/',
            clickedRow = this;
        selectedUserId = $(this).closest('.user').data('userid');
        console.log($(this).closest('.user'));

        $.ajax({
            url: url + selectedUserId,
            type: "POST",
            success: () => {
                clickedRow.closest('.user').remove();
                $.toast({
                    heading: 'Success',
                    text: 'The user was hidden in jail',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
            },
            error: (err) => {
                $.toast({
                    heading: 'Error',
                    text: err.responseText,
                    showHideTransition: 'fade',
                    icon: 'error',
                    position: 'top-right'
                })
            }
        })
    });

    $("#modal-custom").iziModal({
        overlayClose: false,
        overlayColor: 'rgba(0, 0, 0, 0.6)'
    });

    $(".add-violation").on("click", function () {
        selectedUserId = $(this).closest('.user').data('userid');
    });

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

    $(".addViolation").click(function () {
        const url = '/police/addSeverity';
        let formData = new FormData();
        formData.append("SeverityViolation", $("[name=SeverityViolation]").val());
        formData.append("DateExpired", $("[name=DateExpired]").val());
        formData.append("Description", $("[name=Description]").val());
        formData.append("CitizenId", selectedUserId);

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                $.toast({
                    heading: 'Success',
                    text: 'You added vioalation',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
                $("#modal-custom").iziModal('close');
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

    $(".amnesty").click(function () {
        const url = '/police/amnestyAllSeverity/';
        selectedUserId = $(this).closest('.user').data('userid');

        $.ajax({
            type: 'DELETE',
            url: url + selectedUserId,
            data: {},
            processData: false,
            contentType: false,
            success: function (response) {
                $.toast({
                    heading: 'Success',
                    text: 'You remove all vioalation',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
            },
        });
    });
});
