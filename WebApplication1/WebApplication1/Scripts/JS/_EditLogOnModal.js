


function bindEditModalEvents() {

    // 綁定數字按鈕點擊事件
    $('#editLogOnModal').off('click', '.append-key').on('click', '.append-key', function () {
        const val = $(this).data('val');
        const $qty = $('#editQty');
        let current = $qty.val() || '';

        if (/^0+$/.test(current)) {
            $qty.val(val);
        } else {
            $qty.val(current + val);
        }

        const qty = parseFloat($qty.val()) || 0;
        const workOrderBarcode = $('#editBarcode').val(); 
        checkQuantity(qty, workOrderBarcode); 

    });

    // 綁定清除按鈕
    $('#btnClearQty').off('click').on('click', function () {
        $('#editQty').val('0');
    });

    // 儲存
    $('#btnSaveEditLogOn').off('click').on('click', function () {
        const model = {
            id: $('#editLogOnId').val(),
            qty: $('#editQty').val(),
            productionStyle: $('input[name="ProductionStyle"]:checked').val(),
            userBarcode: $('#userBarcode').val()
        };

        $.post('/LogOn/SaveEdit', model, function (res) {
            if (res.success) {
                Swal.fire({ icon: 'success', title: '更新成功' });
                $('#editLogOnModal').modal('hide');
                refreshLogOnTable();
            } else {
                Swal.fire({ icon: 'error', title: res.message });
            }
        });
    });
}



