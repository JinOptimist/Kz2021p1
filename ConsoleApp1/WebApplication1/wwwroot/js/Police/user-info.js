$(document).ready(function () {
    const UserId = $("#user-card").data("userid");
    console.log(UserId);
    $("#modal-custom").iziModal()

    $('.goToJail').click(function () {
        const url = '/police/gotojail/',
            clickedRow = this;
        console.log($(this).closest('.user'));

        $.ajax({
            url: url + UserId,
            type: "POST",
            success: () => {
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

    $(".addViolation").click(function () {
        const url = '/police/addSeverity';
        console.log($("[name=Reason]"));
        let formData = new FormData();
        formData.append("SeverityViolation", $("[name=SeverityViolation]").val());
        formData.append("DateExpired", $("[name=DateExpired]").val());
        formData.append("Description", $("[name=Reason]").val());
        formData.append("CitizenId", UserId);

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                insertTable(response);
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
    });

    $(document).on('click', '.amnesty', function () {
        const url = '/police/amnestySeverity/';
        const clickedViolation = $(this);
        const violationiId = clickedViolation.data('violationid')
        $.ajax({
            type: 'DELETE',
            url: url + violationiId,
            data: {},
            processData: false,
            contentType: false,
            success: function (response) {
                $.toast({
                    heading: 'Success',
                    text: 'You removed vioalation',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
                clickedViolation.closest('.violation').remove();
            },
        });
    });


    function insertTable(id) {
        const markup = `
        <tr class='violation'>
            <td> ${$("[name=SeverityViolation] :selected").text()}</td>
			<td> ${$("[name=DateExpired]").val()} </td>
			<td> ${$("[name=Reason]").val()} </td>
			<td><button class='btn btn-outline-info amnesty' data-violationid='${id}'>Amnesty</button></td>
		</tr >`

        $(".violations-body").append(markup);
    }

    $("#avatar").change(function () {
        const url = '/police/uploadUserPhoto';
        const fileList = $("#avatar").prop('files');
        let formData = new FormData();


        for (var i = 0; i < fileList.length; i++) {
            let file = fileList[i];
            formData.append("file", file);
        }
        formData.append('citizenId', $('#user-card').data('userid'))
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                const img = $('.personal-card-img img');
                img.attr('src', `${response}`)
            },
            error: function(error) {
                alert("errror");
            }
        })
    });
        
})