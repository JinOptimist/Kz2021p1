$(document).ready(function () {
    if ($("#users-tab").length != 0) {
        loadUsers();
    }


    $("#users-tab").click(function () {
        loadUsers();
    })

    function loadUsers() {
        const url = 'users-list';
        $.ajax({
            url: url,
            type: "GET",
            dataType: "html",
            success: function (result) {
                $("#users").html(result)
            },
            error: function () {
                alert("error");
            }
        });
    };

    $("#policmen-tab").click(function () {
        const url = 'policemen-list';
        $.ajax({
            url: url,
            type: "GET",
            dataType: "html",
            success: function (result) {
                $("#policemen").html(result)
                $("#set-shift-form").iziModal();
            },
            error: function () {
                alert("error");
            }
        });

    });

    $("#trainees-tab").click(function () {
        const url = 'get-applicants';
        $.ajax({
            url: url,
            type: "GET",
            dataType: "html",
            success: function (result) {
                $("#trainees").html(result)
            },
            error: function () {
                alert("error");
            }
        });
    });

    function getQuizerPage(placeForAttach) {
        const url = 'get-quizer';
        $.ajax({
            url: url,
            type: "GET",
            dataType: "html",
            success: function (result) {
                $(placeForAttach).html(result)
            },
            error: function () {
                alert("error");
            }
        });
    }

    $("#quizer-tab").click(function () {
        getQuizerPage("#quizer");
    });

    $("#quiz-tab").click(function () {
        getQuizerPage("#quiz");
    })

    $(document).on('click', '#dismiss-policeman', function () {
        const url = "dismiss-policeman/";
        const policemanCell = $(this).closest(".policeman");
        const policemanId = policemanCell.data("policemanid")

        $.ajax({
            url: url + policemanId,
            type: "DELETE",
            success: function (result) {
                $.toast({
                    heading: 'Success',
                    text: 'You dismissed policeman',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
                policemanCell.remove();
            },
            error: function () {
                alert("error");
            }
        });
    });

    $(document).on('click', '#accept-applicant', function () {
        const url = "accept-applicant/";
        const applicantCell = $(this).closest(".applicant");
        const applicantId = applicantCell.data("applicantid")

        $.ajax({
            url: url + applicantId,
            type: "PATCH",
            success: function (result) {
                $.toast({
                    heading: 'Success',
                    text: 'You applied request',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
                applicantCell.remove();
            },
            error: function () {
                alert("error");
            }
        });
    })

    $(document).on('click', '#create-question-btn', function () {
        $(this).fadeOut();
    })


    $(document).on('click', '#create-answer', function () {
        const url = "add-new-question/";

        $(this).fadeOut();
        $(this).siblings().fadeOut();

        let formData = new FormData();
        const question = $(this).siblings('input').val();
        formData.append("Description", question);
        const parent = $(this).parent();
        // createAnswer(question, parent, 1);
        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                console.log(question, parent, response)
                createAnswer(question, parent, response);
            },
            error: (response) => {
                alert(response);
            },
        });
    })

    function createAnswer(questionTitle, card, questionId) {
        console.log('title', questionTitle, 'card', card)
        const markUp = `
            <h3>Question: ${questionTitle}</h3>
            <p>Answers: </p>
            <div class="input-group-prepend">
                <div class="input-group-text" style="width: 100%;">
                    <input type="radio" class="mx-2 my-2 radio-answer selected" name="answer" checked>
                    <input type="text" class="form-control answer" data-questionid="${questionId}" placeholder="Enter your answer">
                </div>
            </div>
            <button id="add-answer" class="btn btn-success mt-2">Add answer</button>
            <button id="save-answer" class="btn btn-success mt-2">Save</button>
        `;

        card.html(markUp);
    }

    $(document).on('change', '.radio-answer', function () {
        $('.selected').removeClass('selected')
        $(this).addClass('selected');
    })

    $(document).on('click', '#add-answer', function () {
        const elem = $(this).siblings('.input-group-prepend');
        if (elem.length >= 4) {
            $.toast({
                heading: 'Error',
                text: '4 answers it`s max length',
                showHideTransition: 'slide',
                icon: 'error',
                position: 'top-right'
            })
            return;
        }
        addAnswerInput(elem[elem.length - 1]);
    });

    $(document).on('click', '#save-answer', function () {
        const answers = $(".answer");
        const url = "add-new-answers";
        let sendData = [];
        for (let i = 0; i < answers.length; i++) {
            const answerInput = $(answers[i]);
            let answer = {
                "Description": answerInput.val(),
                "QuestionId": answerInput.data("questionid"),
                "IsRight": answerInput.siblings().hasClass('selected')
            }
            sendData.push(answer);
        }
        sendData = JSON.stringify(sendData);
        $.ajax({
            url: url,
            type: 'POST',
            data: sendData,
            dataType: 'json',
            contentType: 'application/json;',
            success: function (result) {
                $("#quizer").empty();
                $("#quizer").html(result)
            }          
        });
    })

    function addAnswerInput(element) {
        const questionId = $(element).find(".answer").data("questionid");
        const input = `
        <div class="input-group-prepend mt-2">
             <div class="input-group-text" style="width: 100%;">
                <input type="radio" class="mx-2 my-2 radio-answer" name="answer">
                <input type="text" class="form-control answer" data-questionid="${questionId}" placeholder="Enter your answer">
            </div>
        </div>    
`
        $(element).after(input);
    }
    let maxQuestions = 0;
    let currentQuestion = 0;

    function getQuiz() {
        const url = "get-quiz";

        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json;',
            success: function (result) {
                $("#quiz").empty();
                createQuizer(result);
                maxQuestions = result.length;
            },
            error: function (error) {
                alert('Error: ' + error.statusText);
            }
        });
    }

    $(document).on('click', '#start-quiz', function () {
        getQuiz();
    })


    function createQuizer(questionAndAnswer) {
        console.log(questionAndAnswer);
        let questionNumber = 0;
        for (let key in questionAndAnswer) {
            questionNumber++;
            let question = questionAndAnswer[key];
            generateTitleAnswer(question["questionDesc"],  questionNumber);
            generateBodyAnswer(question["answers"], question["questionId"], questionNumber);
        }
    }

    function generateTitleAnswer(title, questionNumber) {
        const markup = ` 
            <div class="title-answer"> 
                <h3>Question: ${title}</h3>
                <p>Answers: </p>  
            <button" class="btn btn-success mt-2 next-question">Next</button>
            </div>
        `;

        if (questionNumber !== 1) {
            $(markup).insertBefore($("#quiz")).hide();
        } else {
            $(markup).insertBefore($("#quiz"));
        }
    }

    function generateBodyAnswer(answers, questionId, questionNumber) {
        const buttons = $('.next-question');
        const lastButton = $(buttons).length - 1;
        for (let answer in answers) {
            if (questionNumber !== 1) {
                $(createAnswerForTrainee(answers[answer]["description"], questionId, questionNumber, answers[answer]["answerId"])).insertBefore(buttons[lastButton]).hide()
            } else {
                $(createAnswerForTrainee(answers[answer]["description"], questionId, questionNumber, answers[answer]["answerId"])).insertBefore(buttons[lastButton]);
            }
        }
    }

    function createAnswerForTrainee(description, questionId, questionNumber, answerId) {
        return ` <div class="input-group-prepend mt-2 ">
                    <div class="input-group-text" style="width: 100%;">
                        <input type="radio" class="mx-2 my-2 radio-answer" 
                            data-questionid="${questionId}" 
                            data-answerid="${answerId}"
                            data-questionNumber="${questionNumber}" 
                            name="answer">
                        <input type="text" class="form-control answer"
                            placeholder="${description}" disabled>
                    </div>
                </div>   `;
    }

    let incorrectAnswers = [];

    $(document).on('click', '.next-question', function () {
        const selectedAnswer = $(this).siblings().find('.selected');
        const url = 'check-answer/';

        $.ajax({
            url: url + selectedAnswer.data('answerid'),
            method: 'GET',
            success: function (result) {
                
                currentQuestion++;
                if (result) {
                    selectedAnswer.siblings('input').css("background-color", "#99de65");
                } else {
                    incorrectAnswers.push(
                        {
                            "questionNumber": currentQuestion,
                            "isRight": result
                        }
                    )
                    selectedAnswer.siblings('input').css("background-color", "#ffb1b1")
                }

                if (currentQuestion != maxQuestions) {
                    setTimeout(() => {
                        selectedAnswer.closest('.title-answer').fadeOut();
                        const newQuestion = selectedAnswer.closest('.title-answer').next('.title-answer');
                        newQuestion.fadeIn();
                        newQuestion.children().fadeIn();
                    }, 100)
                } else {
                    createResult();
                }
            },
            error: function (error) {
                console.log(error);
            }
        })
    })

    function createResult() {
        console.log(incorrectAnswers);
        currentQuestion = 0;
        maxQuestions = 0;
        if (incorrectAnswers.length > 0) {
            const nextBtn = $(".next-question").last();
            $(`<p>You have incorrect answers. Try again.</p><button class="btn btn-success" id="reload-quiz">Reload</button>`).insertAfter(nextBtn);
            nextBtn.remove();
        }
        else {
            const url = 'end-quiz';
            $.ajax({
                url: url,
                method: "PATCH",
                success: function () {
                    window.location.reload();
                }
            })
        }

        incorrectAnswers = [];
    }

    $(document).on('click', '#reload-quiz', function () {
        $('.title-answer').remove();
        getQuiz();
    })

    let selectedPoliceman;
    $(document).on('click', '#set-shift', function () {
        console.log(this);
        selectedPoliceman = $(this).closest('.policeman').data('policemanid');
    })

    $(document).on('click', '#send-shift', function () {
        const url = "set-shift";

        let sendData = {
            'policemanId': selectedPoliceman,
            'startDate': $("input[name='startDate']").val(),
            'endDate': $("input[name='endDate']").val()
        }
        sendData = JSON.stringify(sendData);

        $.ajax({
            url: url,
            type: 'POST',
            data: sendData,
            dataType: 'json',
            contentType: 'application/json;',
            success: function (result) {
                $.toast({
                    heading: 'Success',
                    text: 'You set shift for this policeman',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
            },
            error: function (error) {
                alert('Error: ' + error.statusText);
            }
        });
    })

    $("#shifts-tab").click(function () {
        const url = "get-shift";

        $.ajax({
            url: url,
            type: 'GET',
            success: function (result) {
                $("#shifts").html(result);
                $("#set-shift-form").iziModal();
            },
            error: function (error) {
                alert('Error: ' + error.statusText);
            }
        });
    })

    let shiftId;

    $(document).on('click', '#change-shift', function () {
        console.log(this);
        $("#send-shift").after(`<button id="update-shift" class="btn btn-success">Update</button>`)
        $("#send-shift").remove();
        shiftId = $(this).closest('.policeman').data('shiftid');
        console.log(shiftId);
    });

    $(document).on('click', '#update-shift', function () {
        const url = "update-shift";
 
        let sendData = {
            "id": shiftId,
            'startDate': $("input[name='startDate']").val(),
            'endDate': $("input[name='endDate']").val()
        };

        sendData = JSON.stringify(sendData);

        $.ajax({
            url: url,
            type: 'PATCH',
            data: sendData,
            dataType: 'json',
            contentType: 'application/json;',
            success: function (result) {
                $.toast({
                    heading: 'Success',
                    text: 'You updated shift for this policeman',
                    showHideTransition: 'slide',
                    icon: 'success',
                    position: 'top-right'
                })
                $("#set-shift-form").iziModal('close');
                updateShiftDate(result);
            },
            error: function (error) {
                alert('Error: ' + error.statusText);
            }
        });
    })

    function updateShiftDate(newDate) {
        const cell = $(`[data-shiftid="${newDate['id']}"]`);
        cell.find('.start-date').text(newDate['startDate']);
        cell.find('.end-date').text(newDate['endDate']);
        console.log(cell);
    }
})