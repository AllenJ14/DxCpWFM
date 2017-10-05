$(function () {
    var $grp = $('.grp');
    var $empList = $(".empList");
    var $timeList = $(".timeList");
    var $script = $('#wfMaster');
    var grpLimit = $script.attr('data-grplimit');
    var qCount = $script.attr('data-qcount');
    var grpCount = 0;

    if ($empList.length) {
        var action = '/APITest/_empList';
        $.get(action, function (result) {
            $empList.empty;
            $empList.html(result);
        });
    };

    if($timeList.length){
        var action = '/APITest/_timeList';
        $.get(action, function(result){
            $timeList.empty;
            $timeList.html(result);
        });
    };        

    $('input[type=date]').attr({
        value: $script.attr('today'),
        min: $script.attr('min')
    });

    $grp.wrapAll('<div id="grp' + grpCount + '" class="col-sm-12"></div>');
    $grp = $('#grp0');
    $grp.prepend('<hr/>')

    $('.branchValidate').blur(function(){
        if($(this).val() !== 0){
            var criteria = $(this).val();
            var action = '/APITest/_branchValidate?branchNum='+criteria;
            $.get(action, function(result){
                $('#_branchValidate').replaceWith(result);
            });
        };
    });

    $('#addGrp').click(function () {
        grpCount++;
        var toAppend = $grp.clone().attr('id', 'grp' + grpCount).prepend('<span class="glyphicon glyphicon-minus-sign text-danger remGrp" style="cursor:pointer;position:absolute"></span>');
        toAppend.find('.alert').remove();
        toAppend.find('.form-group').each(function (i) {
            $(this).find(':input').each(function (j) {
            if($(this).attr('name')==='q.Index'){
                $(this).val(qCount)
            }
            else if ($(this).attr('type') === "hidden") {
                $(this).attr('name', 'q[' + qCount + '].QuestionID');
            }
            else {
                $(this).attr('name', 'q[' + qCount + '].Answer');
            }
            });
            qCount++;
        });
        qCount++;
        $('#addGrpContainer').before(toAppend);
        if (grpCount == grpLimit) {
            console.log('here');
            $('#addGrp').css('display','none');
        };
    });

    $('#formContainer').on('click', '.remGrp', function () {
        grpCount--;
        $(this).parent().remove();
        if (grpCount < grpLimit) {
            $('#addGrp').css('display', 'inline');
        };
    });
});