$(document).ready(function () {
    $('.edit').click(function () {
    

        var id = clicked.attr('data-name');
        var url = '/Fireman/Edit?id=' + id;
        $.load(url)

    })   
})