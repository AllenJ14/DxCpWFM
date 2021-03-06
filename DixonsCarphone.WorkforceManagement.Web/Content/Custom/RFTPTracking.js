﻿$(document).on('keyup keypress', 'form input[type="text"]', function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        return false;
    }
});

$(function () {
    $('#rModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('caseid');

        var modal = $(this);
        modal.find('#rCaseID').val(id);
    });

    $('#oModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('caseid');

        var modal = $(this);
        modal.find('#oCaseID').val(id);
    });

    $('#cModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('caseid');
        var name = button.data('name');

        var modal = $(this);
        modal.find('#cCaseID').val(id);
        modal.find('#cName').html(name);
    });

    $('#aModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('caseid');
        var action = button.data('action');

        var modal = $(this);
        modal.find('#aCaseID').val(id);
        modal.find('#aActionType').val(action);
        modal.find('#aActionText').html(action);
    });

    $('#empNumber').change(function () {
        if ($(this).val() === "LeftRegion") {
            $('#rSearchCont').slideDown();
        }
        else {
            $('#rSearchCont').slideUp();
        };
    });

    $('#oReason').change(function () {
        if ($(this).val() === "Exceptional circumstance (please specify)") {
            $('#oCommentContainer').slideDown();
            $('#oComment').prop('required', true);
        }
        else {
            $('#oCommentContainer').slideUp();
            $('#oComment').prop('required', false);
        };
    });

    $('#rSearchSubmit').on('click', function () {
        var criteria = $('#rSearchCrit').val();
        $.get('/RightFirstTime/_employeeSearch', { crit: criteria }, function (result) {
            setTimeout(function () {
                $('#rSearchResult').html(result);
            }, 250);
        });
    });

    $('#rSearchCont').on('click', '.rSelected', function () {
        var name = $(this).data('name');
        var number = $(this).data('number');

        $('#empNumber').prepend('<option value="' + number + '">' + name + '</option>')
        $('#empNumber').val(number).change();

        $('#rSearchCont').slideUp();
        $('#rSearchCrit').val('');
        $('#searchResult').html('');
    });
});