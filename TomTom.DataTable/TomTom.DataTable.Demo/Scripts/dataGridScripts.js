DataGrid = function ()
{
    var obj = {
        initGrid: function (tableId, hasPaging, url) {
            var form = $('#ItemsList-' + tableId + " .DataGridFilters");
            if (!url) {
                url = form.attr('action');
            }
            $.ajax({
                url: url,
                type: "POST",
                success: function (resp) {
                    $('#ItemsList-' + tableId + " .grid-body").html(resp);
                },
                error: function (resp, err, errorThrown) {
                    console.log(resp);
                    console.log(err);
                    console.log(errorThrown);
                },
                data: form.serialize()
            });
        },
        show_grid_details: function (url, id) {
            var det = $('#details_' + id);
            if (det.closest('tr').is(':visible')) {
                det.closest('tr').hide();
            } else {
                if (det.attr('initialized') == 0) {
                    $.ajax({
                        url: url,
                        type: "GET",
                        beforeSend: function () {
                            //IBankGeneral.show_loading($(det).closest('table'));
                        },
                        success: function (response) {
                            det.html(response);
                            det.closest('tr').show();
                            det.attr('initialized', 1);
                        },
                        error: function (er) {
                            console.log(er.status);
                        },
                        complete: function () {
                            //IBankGeneral.hide_loading($(det).closest('table'));
                        }
                    });
                } else {
                    det.closest('tr').show();

                }
            }
        },
        initPagination: function (tableId) {

            var selector = '.pagination-' + tableId + ' .grid-pagination';
            $('body').off('click', selector);
            $('body').on('click', selector, function () {
                $('#ItemsList-' + tableId + " .DataGridFilters").find('.offset').val($(this).data('skip'));
                DataGrid.initGrid(tableId);
            });

        },
        order_by: function (columnName, tableId) {
            var oderbyElement = $('#ItemsList-' + tableId + " .DataGridFilters").find('.orderby');
            var sortDirectionElement = $('#ItemsList-' + tableId + " .DataGridFilters").find('.sortDirection');
            if (oderbyElement.val() != columnName) {
                oderbyElement.val(columnName);
                sortDirectionElement.val('False');
            } else {
                if (sortDirectionElement.val() == 'False') {
                    sortDirectionElement.val('True');
                } else {
                    sortDirectionElement.val('False');
                }
            }
            DataGrid.initGrid(tableId);
        },
    };
    return obj;
}();