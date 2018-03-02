$(document).ready(function () {
    var checkbox = $('.form-group.fancy-checkbox>input[type=checkbox]');

    if(checkbox.checked)
    {
        $('.form-group.fancy-checkbox>.btn-group>.btn.btn-default>span').addClass("glyphicon-ok");
    }
    else {
        $('.form-group.fancy-checkbox.btn-group>.btn.btn-default>span').removeClass("glyphicon-ok");
    }

    $('.form-group.fancy-checkbox>.btn-group').click(function () {
        
        if (checkbox.prop('checked') == true) {
            $('.form-group.fancy-checkbox>.btn-group>.btn.btn-default>.glyphicon').removeClass("glyphicon-ok");
            checkbox.prop('checked', false);
        }
        else {
            $('.form-group.fancy-checkbox>.btn-group>.btn.btn-default>.glyphicon').addClass("glyphicon-ok");
            checkbox.prop('checked', true);
       
        }

    });
});