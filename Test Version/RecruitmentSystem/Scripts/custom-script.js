var PendingApplicant = 0;
var CancelledManpower = 'False';
var classification = '';
var imageloading = Loading;
$(document).ready(function () {
    $('.read-only').keypress(function (e) {
        return false;
    });
    $('#EducationalAttainment').click(function (e) {

        if ($(this).val() == 'High School Graduate' || $(this).val() == 'Elementary Graduate') {
            $('#course_').attr('disabled', '');
            $('button.btn.btn-default.btn-default-browse').removeAttr('data-target');
        }
        else {
            $('#course_').removeAttr('disabled');
            $('button.btn.btn-default.btn-default-browse').attr('data-target', '#modal-course-list');
        }
    });
    $('.educational-attainment').click(function (e) {

        if ($(this).val() == 'High School Graduate' || $(this).val() == 'Elementary Graduate') {
            $('.course').attr('disabled', '');
            $('.tesda').attr('disabled', '');
            //$('button.btn.btn-default.btn-default-browse').removeAttr('data-target');
        }
        else {
            $('.course').removeAttr('disabled');
            $('.tesda').removeAttr('disabled');

        }
    });
    $('.delete-applicant').click(function () {
        var url = $(this).attr('data-url');
        $.confirm({
            title: 'Confirm',
            content: 'Do you want to proceed?',
            buttons: {
                YES: function () {
                    window.location = url;
                },
                No: function () {

                }
            }
        });
    });
    PendingApplicant = $('.pending-applicant').text();
    if (PendingApplicant == '') {
        PendingApplicant = 1
    }
    classification = $('.classification').text()

    $('.show-password').hover(function () {
        $('#password').prop('type', 'text');

    });
    $('.show-password').mouseleave(function () {
        $('#password').prop('type', 'password');
    });
    $('#password').prop('type', 'password');

    $(".number-only").keypress(function (e) {
        if (e.which > 8) {
            if (e.which < 48 || e.which > 57) {
                $(this).focus();
                return false;
            }
        }

    });
    $('.requirement').keypress(function (e) {
        var l = $(this).val().length;
        var len = $(this).attr('data-val-maxlength-max');
        var min = $(this).attr('data-val-minlength-min');


        if (e.which > 8) {
            if (l >= len) {

                return false;
            }
        }
        else {

        }
    });
    if ($('#Searchby').val() == "Daily") {
        $('.daily').show();
    } else if ($('#Searchby').val() == "Monthly") {
        $('.monthly').show();
    }
    else if ($('#Searchby').val() == "Yearly") {
        $('.yearly').show();
    }
    $('#Searchby').change(function () {
        $('.daily').hide();
        $('.monthly').hide();
        $('.yearly').hide();
        if ($('#Searchby').val() == "Daily") {
            $('.daily').show();
        } else if ($('#Searchby').val() == "Monthly") {
            $('.monthly').show();
        }
        else if ($('#Searchby').val() == "Yearly") {
            $('.yearly').show();
        }
        else {
            $('.daily').show();
        }
    });
    if ($('#SearchBy').val() == "Daily") {
        $('.daily').removeClass('hidden');
    } else if ($('#SearchBy').val() == "Monthly") {
        $('.monthly').removeClass('hidden');
    }
    else if ($('#SearchBy').val() == "Yearly") {
        $('.yearly').removeClass('hidden');
    }
    $('#SearchBy').click(function () {
        $('.daily').addClass('hidden');
        $('.monthly').addClass('hidden');
        $('.yearly').addClass('hidden');
        $('.daily input').val('');
        $('.monthly select').val('');
        $('.yearly select').val('');
        if ($('#SearchBy').val() == "Daily") {
            $('.daily').removeClass('hidden');
        } else if ($('#SearchBy').val() == "Monthly") {
            $('.monthly').removeClass('hidden');
        }
        else if ($('#SearchBy').val() == "Yearly") {
            $('.yearly').removeClass('hidden');
        }
        else {
            $('.daily').removeClass('hidden');
        }
    });
    $('#form-login').keydown(function (e) {
        if (e.which == 13) {
            formlogin(this);
        }
    });
    $('.decimal').keypress(function (e) {
        if (e.which != 46) {



        }
        else {
            var val = $(this).val();
            if (val.indexOf('.') <= 0) {
                $(this).val(val + '.');
            }


        }
    });
});

function optionchange(url, ctrl) {
    if ($(ctrl).val() != "") {
        window.location = url + '?userlevel=' + $(ctrl).val()
    }

}

function modalclose() {

    $('.modal').modal('hide');
    $('.modal-content').empty();

}
function showmodal(url, container) {

    $.ajax({
        url: url,
        success: function (e) {
            $(container).html(e);
        }
    });
}

function ApproveForm(form, url) {
    $(form).attr('action', url);
    $('#Remarks').removeAttr('required');
    FormSubmit(form);
}
function ChangeUrlFormSubmit(ctrl, form, confirm, validation) {
    var url = $(ctrl).attr('data-url');
    $(form).attr('action', url);
    FormSubmit(form, confirm, validation);
}
function FormSubmit(form, confirm, validation) {
    if (typeof validation == 'undefined' && validation == 'yes') {
        $.validator.unobtrusive.parse(form);
    }

    var validator = $(form).validate();
    $('input').popover('destroy');
    if ($(form).valid()) {
        if (typeof confirm == 'undefined' || confirm == 'yes') {
            $.confirm({
                title: 'Confirm',
                content: 'Do you want to proceed?',
                //confirm: function () {

                //    //
                //}
                buttons: {
                    YES: function () {
                        $(form).submit();
                    },
                    No: function () {

                    }
                }
            });
        } else {
            $(form).submit();
        }


    }
    else if (!$(form).valid() && (typeof validation == 'undefined' || validation == 'yes')) {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);

        $('#' + validator.errorList[0].element.name).focus();
        var ctrl = ('#' + validator.errorList[0].element.name);
        var error = validator.errorList[0].message;
        if ($(ctrl).val() != "") {
            error = 'Field requires ' + $(ctrl).attr('data-val-minlength-min') + ' digits';
        }
        $('#' + validator.errorList[0].element.name).popover({
            content: error,
            placement: 'top',
            trigger: 'click|focus'
        });
        $('input').popover('toggle');
        $('#' + validator.errorList[0].element.name).focus();

    }
}
function FormSubmitwithalert(form) {

    var validator = $(form).validate();
    if ($(form).valid()) {
        $.confirm({
            title: 'Confirm',
            content: 'Do you want to proceed?',
            //confirm: function () {
            //    $(form).submit();
            //}
            buttons: {
                YES: function () {
                    $(form).submit();
                },
                No: function () {

                }
            }

        });

    }
    else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
        $('.alert').show();
    }
}
function formlogin(form) {
    var validator = $(form).validate();


    if ($(form).valid()) {

        $(form).submit();



    }
    else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }
}
function formforgotpassword(form) {
    if ($(form).valid()) {
        $(form).submit()
    }
    else {
        $('#forgot-password-alert').show();
    }

}
function redirecturl(url) {

    window.location = url;
}
function redirecturltab(url) {
    window.open(url);
}
function downloadmanpowerstatusreport(url, form) {
    $(form).attr('action', url);
    $(form).attr('method', 'post');
    $(form).submit();
    $(form).removeAttr('method');
}
function printcontract(url, checkbox) {
    var list = "";
    $('.alert.alert-danger').hide();
    $('.alert.alert-danger').empty();
    $(checkbox).each(function () {
        if (this.checked) {
            list += $(this).val() + ',';
        }
    });

    if (list != "") {

        list = list.substring(list.length - 1, 0);
        redirecturl(url + "&applicants=" + list);
    }
    else {
        $('.alert.alert-danger').show();
        $('.alert.alert-danger').html('Select Applicant First');
    }
}
function redirecturlsubmit(url, form) {
    $(form).attr('action', url);
    $(form).submit();
}
function search(form, url, destination) {
    $(destination).html(' <div class="row"><div class="col-lg-12"><div class="center-loading-icon"><img src="/Images/loading.gif" height="32" /></div></div>');
    $.ajax({
        url: url,
        data: $(form).serialize(),
        success: function (e) {
            $(destination).html(e);
        }
    });
}
function search(form, url, destination, imgurl) {
    $(destination).html(' <div class="row"><div class="col-lg-12"><div class="center-loading-icon"><img src="' + imgurl + '" height="32" /></div></div>');
    $('img').attr('src', imgurl);
    $.ajax({
        url: url,
        data: $(form).serialize(),
        success: function (e) {
            $(destination).html(e);
        }
    });
}
function searchtablewithloading(form, url, destination, colspan, imageurl) {
    $(destination).html(' <tr><td><img src="/Images/loading.gif" height="32" /></td></tr>');
    $('img').attr('src', imageurl);
    $('td').attr('colspan', colspan);
    $('td').attr('align', 'center');
    $.ajax({
        url: url,
        data: $(form).serialize(),
        success: function (e) {
            $(destination).html(e);
            //$('body').html(JSON.stringify(e));
        }
    });
}
function searchTablewithvalidation(form, url, destination, colspan, imageurl) {
    var validator = $(form).validate();
    $('.alert').hide();
    colspan = countColumn();
    if ($(form).valid()) {

        $(destination).html(' <tr><td><img src="/Images/loading.gif" height="32" /></td></tr>');
        $('img').attr('src', imageurl);
        $('td').attr('colspan', colspan);
        $('td').attr('align', 'center');
        $.ajax({
            url: url,
            data: $(form).serialize(),
            success: function (e) {
                $(destination).html(e);
            }
        });

    }
    else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
        $('.alert').show();
    }

}
function searchtablewithloadingwithvalidation(form, url, destination, colspan, imageurl) {
    var validator = $(form).validate();
    if ($(form).valid()) {
        $(destination).html(' <tr><td><img src="/Images/loading.gif" height="32" /></td></tr>');
        $('img').attr('src', imageurl);
        $('td').attr('colspan', colspan);
        $('td').attr('align', 'center');

        $.ajax({
            url: url,
            data: $(form).serialize(),
            success: function (e) {
                $(destination).html(e);
            }
        });
    }
    else {

        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }

}
function searchwithvalidation(form, url, destination, imgurl) {


    var validator = $(form).validate();
    if ($(form).valid()) {
        searchtablewithloading(form, url, destination, 8, imgurl);
        //search(form, url, destination,imgurl)
        //alert(destination);
    }
    else {

        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }
}
function FormSearchValidation(form, Destination, colspan) {
    var validator = $(form).validate();
    if ($(form).valid()) {
        $(Destination).html(' <tr><td><img src="' + imageloading + '" height="32" /></td></tr>');
        $('img').attr('src', imageloading);
        $('td').attr('colspan', colspan);
        $('td').attr('align', 'center');
        $.ajax({
            url: $(form).attr('action'),
            type: $(form).attr('method'),
            data: $(form).serialize(),
            success: function (e) {
                $(Destination).html(e);
            }
        })
        //search(form, url, destination,imgurl)
        //alert(destination);
    }
    else {

        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }
}
function DisableonSubmit(ctrl) {
    $(ctrl).on('keyup keypress', 'form input[type="text"]', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            return false;
        }
    });

}
function manageform(url, container, ctrl) {
    $('.tab-pane').empty();
    var image = "<div style='text-align:center'><img src='" + $(ctrl).attr('data-image-url') + "' height='32'/></div>"
    $(container).html(image);

    $.ajax({
        url: url,

        success: function (e) {
            $(container).html(e);

        }
    });
}
function loadform(url, container) {

    $.ajax({
        url: url,
        success: function (e) {
            $(container).html(e);
        },
        complete: function () {
            $.validator.unobtrusive.parse('form');
        }
    });
}
function loadformwithloading(url, container, imgsrc) {
    $(container).html(' <div class="row"><div class="col-lg-12"><div class="center-loading-icon"><img src="~/Images/loading.gif" height="32" /></div></div>');
    $('img').attr('src', imgsrc);
    $.ajax({
        url: url,
        success: function (e) {
            $(container).html(e);
        },
        complete: function () {
            $.validator.unobtrusive.parse('form');
        }
    });
}
function LoadNavigationBar(url, container, ctrl) {
    $(container).empty();
    $(container).html(' <div class="row"><div class="col-lg-12"><div class="center-loading-icon"><img src="~/Images/loading.gif" height="32" /></div></div>');
    var imgsrc = $(ctrl).attr('data-image');
    $('img').attr('src', imgsrc);

    $.ajax({
        url: url,
        success: function (e) {
            $(container).html(e);
        },
        complete: function () {
            $.validator.unobtrusive.parse('#form-applicant');
        }

    });
}
function addform(url, form, urldestination, container, alertcontainer, value) {
    var validator = $(form).validate();


    if ($(form).valid()) {
        $.confirm({
            title: 'Confirm',
            content: 'Do you want to proceed?',
            buttons: {
                YES: function () {
                    $.ajax({
                        url: url,
                        data: $(form).serialize(),
                        type: 'POST',
                        success: function () {
                            $('.modal-backdrop').remove();
                            $('.modal').modal('hide');
                            $('body').removeClass('moda-open');
                            $('body').removeAttr('style');
                            $('.alert').html('Successfully Added ' + $(value).val());
                            $('.alert').show();

                        }
                    });
                },
                NO: function () {

                }
            }
        });
    } else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }

}

function EditAccount(form, btn) {
    var update = $(btn).val();
    if (update == 'Update User') {
        $.confirm({
            title: 'Confirm',
            content: 'Do you want to proceed?',
            buttons: {
                YES: function () {
                    $('.form-control').removeAttr('disabled');
                    $(form).submit();
                },
                NO: function () {

                }
            }
        });

    }
    else {
        $(btn).val('Update User');
        $(btn).text('Update User');
        $('.form-control').removeAttr('disabled');
    }
}
function countsms(txtrow, counter) {
    var count = 160 - parseInt($(txtrow).val().length);
    $(counter).text("Characters:" + count);
}
function SelectSkillType(skilltype) {

    var type = $(skilltype).val();

    $('#SpecificSkill').val('');
    $('#Certification').val('');
    $('#Certification').attr('disabled', true);
    $('#SpecificSkill').attr('disabled', true);
    $('#SpecificSkill').removeAttr('required');
    $('#Certification').removeAttr('required');
    if (type == 'S') {
        $('#SpecificSkill').removeAttr('disabled');
        $('#SpecificSkill').attr('required', true);
    }
    else if (type == 'SW') {
        $('#SpecificSkill').removeAttr('disabled');
        $('#Certification').removeAttr('disabled');
        $('#SpecificSkill').attr('required', true);
        $('#Certification').attr('required', true);
    }


}
function SelectClassification(classification) {
    var classification = $(classification).val();
    $('.projectname').hide();
    $('#ProjectName').removeAttr('required');
    if (classification == 'PB') {
        $('.projectname').show();
        $('#ProjectName').attr('required', true);
    }
}
function clickfilebrowse(file) {

    $(file).val("");
    $(file).click();
}
function onchangeimportfilebrowse(file, text) {
    $(text).val($(file).val());
}
function onchangefilebrowse(file, text, form) {
    $(text).val($(file).val());
    $.confirm({
        title: 'Confirm',
        content: 'Do you want to proceed?',
        buttons: {
            YES: function () {
                $(form).submit();
            },
            NO: function () {

            }
        }
    });
}
function cleartxt(document) {
    $(document).ready(function () {
        $('input').val("");
    });
}
function MarkAll(btn, checkall, checkbox) {
    var val = $(btn).text();

    $(checkbox).prop('checked', true);

    if (val == "Mark All") {
        $(btn).text("Unmark All");
        $(checkall).prop('checked', true);
        $(checkbox).prop('checked', true);

    }
    else {
        $(btn).text("Mark All");
        $(checkall).prop('checked', false);
        $(checkbox).prop('checked', false);
    }

}

function applicantlistinvite(checkbox, mrfid, url) {
    var sendsms = false;
    if (classification == 'Continuous' || PendingApplicant > 0) {
        if (CancelledManpower == 'False') {
            var list = "";
            $(checkbox).each(function () {
                if (this.checked) {
                    list += $(this).val() + ',';
                }
            });
            list = list.substring(list.length - 1, 0);
            if (list != "") {
                $('.alert').empty();
                $('.alert').hide();
                $.confirm({
                    title: 'Confirm',
                    content: 'Do you want to proceed?',
                    buttons: {
                        YES: function () {
                            $.ajax({
                                url: url,
                                data: { mrfid: $(mrfid).val(), applicant: list, sendsms: sendsms },

                                success: function (e) {
                                    $('.alert').addClass("alert-success");
                                    $('.alert').removeClass("alert-danger");
                                    $('.alert').show();
                                    $('.alert').html(e);
                                }, error: function (e) {
                                    alert(JSON.stringify(e));
                                }
                            });
                        },
                        NO: function () {

                        }
                    }
                });
            }
            else {
                $('.alert').addClass("alert-danger");
                $('.alert').removeClass("alert-success");
                $('.alert').show();
                $('.alert').html("Select applicant first");
            }
        }
    }
    else {

        $('.alert').addClass("alert-danger");
        $('.alert').html("Manpower Request is already completed");
        $('.alert').show();
    }
}
function applicantlist(checkbox, mrfid, url) {
    var sendsms = false;

    if (classification == 'Continuous' || PendingApplicant > 0) {
        if (CancelledManpower == 'False') {
            var list = "";
            $(checkbox).each(function () {
                if (this.checked) {
                    list += $(this).val() + ',';
                }
            });
            list = list.substring(list.length - 1, 0);
            if (list != "") {
                $('.alert').empty();
                $('.alert').hide();
                $.confirm({
                    title: 'Confirm',
                    content: 'Do you want to proceed?',
                    buttons: {
                        YES: function () {
                            $.confirm({
                                title: 'Send SMS',
                                content: 'Do you want to send SMS',
                                buttons: {
                                    YES: function () {

                                        sendsms = true;
                                        $.ajax({
                                            url: url,
                                            data: { mrfid: $(mrfid).val(), applicant: list, sendsms: sendsms },

                                            success: function (e) {
                                                $('.alert').addClass("alert-success");
                                                $('.alert').removeClass("alert-danger");
                                                $('.alert').show();
                                                $('.alert').html(e);
                                            }, error: function (e) {
                                                alert(JSON.stringify(e));
                                            }
                                        });
                                    }, NO: function () {
                                        sendsms = false;
                                        $.ajax({
                                            url: url,
                                            data: { mrfid: $(mrfid).val(), applicant: list, sendsms: sendsms },
                                            success: function (e) {
                                                $('.alert').addClass("alert-success");
                                                $('.alert').removeClass("alert-danger");
                                                $('.alert').show();
                                                $('.alert').html(e);
                                            }
                                        });
                                    }
                                }

                            });
                        },
                        NO: function () {

                        }

                    }
                });
            }
            else {
                $('.alert').addClass("alert-danger");
                $('.alert').removeClass("alert-success");
                $('.alert').show();
                $('.alert').html("Select applicant first");
            }
        }
    }
    else {

        $('.alert').addClass("alert-danger");
        $('.alert').html("Manpower Request is already completed");
        $('.alert').show();
    }
}
function ImportApplicant(fileimport, form) {

    if (classification == 'Continuous' || PendingApplicant > 0) {
        if (CancelledManpower == 'False') {
            var fileupload = $(fileimport).get(0);
            var files = fileupload.files;
            var data = new FormData();
            var url = $(form).attr('action');
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            if (files.length > 0) {
                $.confirm({
                    title: 'Confirm',
                    content: 'Do you want to proceed?',
                    buttons: {
                        YES: function () {
                            $.ajax({
                                url: url,
                                data: data,
                                contentType: false,
                                processData: false,
                                type: "POST",
                                success: function (e) {
                                    $('.modal').modal('hide');
                                    $('.modal-backdrop').remove();
                                    $('.alert').addClass("alert-success");
                                    $('.alert').removeClass("alert-danger");
                                    $('.alert').show();
                                    $('.alert').html(e);
                                }
                            });
                        }, NO: function () {

                        }
                    }
                });
            }
            else {
                $('input[type=file]').click();
            }
        }
        else {
            $('.alert').addClass("alert-danger");
            $('.alert').html("Manpower Request is cancelled");
            $('.alert').show();
        }
    }
    else {
        $('.alert').addClass("alert-danger");
        $('.alert').html("Manpower Request is already completed");
        $('.alert').show();
    }

}
function selectCourse(coursedisplay, course) {
    var val = $('input[type=radio]:checked');
    $(coursedisplay).val(val.attr('data-text'));
    $(course).val(val.val());
}
function selectCourseApplicantregistration(course) {
    var val = $('input[name=radioname]:checked');
    $(course).val(val.attr('data-text'));
    //$(course).val(val.val());
}
function ExportApplicant(url, mrfid) {
    window.location = url + "?mrfid=" + mrfid;
}

function RejectManpower(form, url) {
    $(form).attr('action', url);
    $('#Remarks').attr('required', true);
    var validator = $(form).validate();
    if ($(form).valid()) {
        $.confirm({
            title: 'Confirm',
            content: 'Do you want to reject this manpower request?',
            //confirm: function () {
            //    $(form).submit();
            //}, cancel: function () {
            //    $('#Remarks').removeAttr('required');
            //},
            buttons: {
                "YES": function () {
                    $(form).submit();
                }, "NO": function () {
                    $('#Remarks').removeAttr('required');
                }
            }


        });
    }
    else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
        var ctrl = ('#' + validator.errorList[0].element.name);
        var error = validator.errorList[0].message;
        if ($(ctrl).val() != "") {
            error = 'Field requires ' + $(ctrl).attr('data-val-minlength-min') + ' digits';
        }
        $('#' + validator.errorList[0].element.name).popover({
            content: error,
            placement: 'top',
            trigger: 'focus|click'
        });
        $('input').popover('toggle');
        $('#' + validator.errorList[0].element.name).focus();
    }

}
function CancelManpower(url) {
    $.confirm({
        title: 'Confirm',
        content: 'Do you want to cancel this manpower request?',

        buttons: {
            YES: function () {
                window.location = url;
            }, NO: function () {

            }
        }
    });
}

function ApplicantSurverySearch(url, form, container) {
    $.ajax({
        url: url,
        data: $(form).serialize(),
        success: function (e) {
            $(container).html(e);

        },
        complete: function () {
            $.validator.unobtrusive.parse('form');
            $('.29').addClass('hidden');
            $('.30').addClass('hidden');
            $('.31').addClass('hidden');


            if ($('.is_31').val() == 'True') {
                $('.29').removeClass('hidden');
                $('.30').removeClass('hidden');
                $('.31').removeClass('hidden');
            }
            else if ($('.month').val() == '2' && $('.is_leap_year').val() == 'True') {
                $('.29').removeClass('hidden');
            }
            else if ($('.month').val() == '2') {

            }
            else {
                $('.29').removeClass('hidden');
                $('.30').removeClass('hidden');
            }


        }

    });
}
function ApplicantSurveySummaryReport(form, urleducation, urlhowdidyouknow, urlinvitedby, urljobfair, educational, howdidyouknow, invitedby, jobfair, imgsrc) {
    $.validator.unobtrusive.parse(form);
    var validator = $(form).validate();
    if ($(form).valid()) {
        $(educational).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $(howdidyouknow).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $(invitedby).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $(jobfair).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $('img').attr('src', imgsrc)
        ApplicantSurverySearch(urleducation, form, educational);
        ApplicantSurverySearch(urlhowdidyouknow, form, howdidyouknow);
        ApplicantSurverySearch(urlinvitedby, form, invitedby);
        ApplicantSurverySearch(urljobfair, form, jobfair);
        $('.table-header').attr('width', 280)
        if ($('#month').val() == '1' || $('#month').val() == 3 || $('#month').val() == 5 || $('#month').val() == 7 || $('#month').val() == 8 || $('#month').val() == 10 || $('#month').val() == 12) {

            $('.31').show();
        }
        else {

            $('.31').hide();
        }
    } else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }
}

function ManpowerSummaryReportSearch(url, form, container) {


    $.ajax({
        url: url,
        data: $(form).serialize(),
        success: function (e) {
            $(container).html(e);

        },
        complete: function () {
            $.validator.unobtrusive.parse('form');
        }

    });


}

function ManpowerSummaryReport(form, url1, url2, url3, url4, url5, table1, table2, table3, table4, table5, imgurl) {
    $.validator.unobtrusive.parse(form);
    var validator = $(form).validate();
    if ($(form).valid()) {
        $(table1).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $(table2).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $(table3).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $(table4).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $(table5).html(' <tr><td colspan="32" align="center"><img src="/Images/loading.gif" height="32" /></td></tr>');
        $('img').attr('src', imgurl);
        ManpowerSummaryReportSearch(url1, form, table1);
        ManpowerSummaryReportSearch(url2, form, table2);
        ManpowerSummaryReportSearch(url3, form, table3);
        ManpowerSummaryReportSearch(url4, form, table4);
        ManpowerSummaryReportSearch(url5, form, table5);
    } else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }
}
function printContractModal(baseurl, mrfid, classification, checkbox) {
    //alert($(classification).val());
    //var list = "";
    //$(checkbox).each(function () {
    //    if (this.checked) {
    //        list += $(this).val() + ',';
    //    }
    //});
    //alert(baseurl + "&applicants=" + list + "&mrfid=" + mrfid + "&classification=" + $(classification).attr('value'));

    var list = "";
    $('.alert.alert-danger').hide();
    $('.alert.alert-danger').empty();
    $(checkbox).each(function () {
        if (this.checked) {
            list += $(this).val() + ',';
        }
    });

    if (list != "") {

        list = list.substring(list.length - 1, 0);
        redirecturl(baseurl + "?applicants=" + list + "&mrfid=" + mrfid + "&classification=" + $(classification).val());
    }
    else {
        $('.alert.alert-danger').show();
        $('.alert.alert-danger').html('Select Applicant First');
    }

}
function ExportSummaryStatus(form, url, accountmanager, dt1, dt2) {
    $.validator.unobtrusive.parse(form);
    var validator = $(form).validate();
    if ($(form).valid()) {
        window.location = url + "?accountmanager=" + $(accountmanager).val() + "&datefrom=" + $(dt1).val() + "&dateto=" + $(dt2).val() + "";
    } else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }
}
function ExportApplicantSurvey(form, url, month, year) {
    $.validator.unobtrusive.parse(form);
    var validator = $(form).validate();
    if ($(form).valid()) {
        window.location = url + "?month=" + $(month).val() + "&year=" + $(year).val() + "";
    } else {
        $('html, body').animate({
            scrollTop: $('#' + validator.errorList[0].element.name).offset().top - 100
        }, 500);
        $('#' + validator.errorList[0].element.name).focus();
    }
}
function NoApplicantList() {
    $('.alert.alert-danger').show();
    $('.alert.alert-danger').html('Select Applicant First');
}
function downloadEndorsement(ctrl, checkbox) {
    var list = "";
    url = $(ctrl).attr('data-url');
    $(checkbox).each(function () {
        if (this.checked) {
            list += $(this).val() + ',';
        }
    });
    list = list.substring(list.length - 1, 0);
    if (list != "") {
        $('.alert').empty();
        $('.alert').hide();
        window.location = url + '&applicantlist=' + list;
    }
    else {
        $('.alert').addClass("alert-danger");
        $('.alert').removeClass("alert-success");
        $('.alert').show();
        $('.alert').html("Select applicant first");
    }
}
function countColumn() {

    var cols = $("table>thead>tr:first>th");
    var count = cols.length;
    return count;
}
function DropdownSearch(ctrl, ul) {
    $.ajax({
        url: $(ctrl).attr('data-url'),
        data: $(ctrl).serialize(),
        success: function (e) {
            $(ul).html(e)
        }

    });
}
function SelectDropdownItem(ctrl, dest) {

    $(dest).val($(ctrl).text());
}
function applicantlistprocess(checkbox, mrfid, ctrl) {
    var url = $(ctrl).attr('data-url');
    var sendsms = false;

    if (classification == 'Continuous' || PendingApplicant > 0) {
        if (CancelledManpower == 'False') {
            var list = "";
            $(checkbox).each(function () {
                if (this.checked) {
                    list += $(this).val() + ',';
                }
            });
            list = list.substring(list.length - 1, 0);
            if (list != "") {
                $('.alert').empty();
                $('.alert').hide();
                $.confirm({
                    title: 'Confirm',
                    content: 'Do you want to proceed?',
                    buttons: {
                        YES: function () {
                            $.confirm({
                                title: 'Send SMS',
                                content: 'Do you want to send SMS',
                                buttons: {
                                    YES: function () {

                                        sendsms = true;
                                        $.ajax({
                                            url: url,
                                            data: { mrfid: $(mrfid).val(), applicant: list, sendsms: sendsms },

                                            success: function (e) {
                                                $('.alert').addClass("alert-success");
                                                $('.alert').removeClass("alert-danger");
                                                $('.alert').show();
                                                $('.alert').html(e);
                                            }, error: function (e) {
                                                alert(JSON.stringify(e));
                                            }
                                        });
                                    }, NO: function () {
                                        sendsms = false;
                                        $.ajax({
                                            url: url,
                                            data: { mrfid: $(mrfid).val(), applicant: list, sendsms: sendsms },
                                            success: function (e) {
                                                $('.alert').addClass("alert-success");
                                                $('.alert').removeClass("alert-danger");
                                                $('.alert').show();
                                                $('.alert').html(e);
                                            }
                                        });
                                    }
                                }

                            });
                        },
                        NO: function () {

                        }

                    }
                });
            }
            else {
                $('.alert').addClass("alert-danger");
                $('.alert').removeClass("alert-success");
                $('.alert').show();
                $('.alert').html("Select applicant first");
            }
        }
    }
    else {

        $('.alert').addClass("alert-danger");
        $('.alert').html("Manpower Request is already completed");
        $('.alert').show();
    }
}
function getColumnCount(table) {
    var columnCount = 0;
    $(table).each(function () {
        var colspanValue = $(this).attr('colspan');
        if (colspanValue == undefined) {
            columnCount++;
        } else {
            columnCount = columnCount + parseInt(colspanValue);
        }
    });
    return columnCount;
}
function ShowModal(ctrl, size, headertitle) {
    var modalid = "";
    modalid = Math.random().toString(36).substring(7);
    $('div').remove('.modal');
    //var modal = '<div class="modal fade" id="' + modalid + '"> <div class="modal-dialog"><div class="modal-content"><div class="text-align-center"><img id="img-loading" width="48"></div></div></div></div>';
    var modal = '';
    modal += '<div class="modal fade" id="' + modalid + '">';
    modal += '<div class="modal-dialog"><div class="modal-content">';
    if (typeof headertitle != 'undefined' && headertitle != '') {
        modal += '<div class="modal-header bg-red"><button class="close" data-dismiss="modal">&times;</button><h4 class="modal-title">' + headertitle + '</h4></div>';
        modal += '<div class="modal-body"><div class="text-align-center"><img id="img-loading" width="48"></div></div>';
    }
    else {
        modal += '<div class="modal-body"><div class="text-align-center"><img id="img-loading" width="48"></div></div>';
    }
    modal += "</div></div>";
    //end of modal container
    modal += "</div>";
    $('body').append(modal);
    $('#img-loading').attr('src', imageloading);
    $('#' + modalid).modal('show');
    $.ajax({
        url: $(ctrl).attr('data-url'),
        success: function (e) {
            if (typeof headertitle == 'undefined' || headertitle == '') {
                $('#' + modalid + '>.modal-dialog>.modal-content').html(e);
            }
            else {
                $('#' + modalid + ' .modal-content>.modal-body').html(e);
            }
        }
    });
    if (typeof size != 'undefined') {
        if (size == 'sm') {
            $('#' + modalid + '>.modal-dialog').addClass('modal-sm');
        }
        else {
            $(modalid + '>.modal-dialog').addClass('modal-lg');
        }

    }
}
function LoadHtml(url, target) {
    var loading = "<img id='img-loadhtml' class='img-responsive'/>"
    $(target).html(loading);
    $('#img-loadhtml').attr('src', imageloading);
    $.ajax({
        url: url,
        success: function (e) {
            $(target).html(e);
        }
    });
}
function AjaxReturnString(ctrl,target)
{
    $.ajax({
        url: $(ctrl).attr('data-url'),
        success:function(e)
        {
            $(target).html(e);
            $(target).addClass("alert-success");
            $(target).show();
        }
    })
}
function UrlRedirect(ctrl)
{
    
}