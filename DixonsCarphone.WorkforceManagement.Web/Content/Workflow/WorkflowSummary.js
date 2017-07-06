$(function () {
    var $summaryTable = $('#summaryTable');
    $summaryTable.dataTable({
        stateSave: true,
        stateDuration: 60 * 10
    });

    var $summaryBody = $('#summaryBody');
    var $typeFilter = $('#typeFilter')
    var $statusFilter = $('#statusFilter');
    var $TPCFilter = $('#TPCFilter');

    var summaryUpdate = function () {
        $summaryTable.dataTable().fnDestroy();
        $summaryBody.html('<tr class="info" id="Loading"><td colspan="8" class="text-center"><strong>Loading...</strong></td></tr>');
        $.get('/Workflow/Workflow/_UpdateSummary', { status: $statusFilter.val(), type: $typeFilter.val(), TPC: $TPCFilter.val() }, function (result) {
            setTimeout(function () {
                $('#Loading').replaceWith(result);
                $summaryTable.dataTable();
            }, 250);
        });
    };

    $typeFilter.change(summaryUpdate);
    $statusFilter.change(summaryUpdate);
    $TPCFilter.change(summaryUpdate);
});