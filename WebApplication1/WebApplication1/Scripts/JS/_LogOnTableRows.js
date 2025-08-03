

let currentLogOnId = 0;
let currentUserBarcode = '';

function refreshLogOnTable() {
    getLogOns();
}

function bindLogOnEvents() {
    $('.production-indicator').off('click').on('click', function () {
        const materialNumber = $(this).data('material');
        const isReaded = $(this).data('isreaded') === true || $(this).data('isreaded') === 'true';
        currentLogOnId = $(this).data('id');
        currentUserBarcode = $('#userBarcode').val();

        $.get('/Instruction/GetProductionInstruction', {
            materialNumber: materialNumber,
        }, function (res) {
            let html = '';

            if (!res.success) {
                html = `<div class="text-danger">${res.message}</div>`;
            } else {
                if (res.data && res.data.length > 0) {
                    html += `<h5>📘 作業指示</h5><ul>`;
                    res.data.forEach(item => {
                        html += `<li><strong>${item.Title}</strong>: ${item.Content}</li>`;
                    });
                    html += `</ul>`;
                } else {
                    html = '<div class="text-muted">沒有相關指示。</div>';
                }
            }

            $('#instructionModalBody').html(html);

            if (isReaded) {
                $('#btnAcknowledge').hide();
            } else {
                $('#btnAcknowledge').show();
            }

            $('#instructionModal').modal('show');
        }).fail(function () {
            $('#instructionModalBody').html(`<div class="text-danger">無法載入資料。</div>`);
            $('#instructionModal').modal('show');
        });
    });

    // 標示為已讀
    $('#btnAcknowledge').on('click', function () {
        if (!currentLogOnId) {
            Swal.fire({ icon: 'error', title: '錯誤', text: '工單資料ID有問題' });
            return;
        }

        if (!currentUserBarcode) {
            Swal.fire({ icon: 'error', title: '錯誤', text: '缺少人員工號' });
            return;
        }

        $.post('/LogOn/AcknowledgeSOPRead', {
            id: currentLogOnId,
            userBarcode: currentUserBarcode
        }, function (res) {
            Swal.fire({
                icon: res.success ? 'success' : 'error',
                title: res.message
            });

            if (res.success) {
                $('#instructionModal').modal('hide');
                refreshLogOnTable();
            }
        }).fail(function () {
            Swal.fire({
                icon: 'error',
                title: '錯誤',
                text: '無法標示為已讀'
            });
        });
    });



    // 取消
    $('.btn-danger').on('click', function () {
        let id = $(this).data('id');
        let userBarcode = $('#userBarcode').val();

        Swal.fire({
            title: '確認取消',
            text: `確定要取消這筆作業嗎？`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: '確定',
            cancelButtonText: '取消'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post('/LogOn/Cancel', { id, userBarcode }, function (res) {
                    Swal.fire({
                        icon: res.success ? 'success' : 'error',
                        title: res.message
                    }).then(() => {
                        if (res.success) refreshLogOnTable();
                    });
                });
            }
        });
    });

    // 編輯
    $('.btn-success').off('click').on('click', function () {
        const id = $(this).data('id');
        const userBarcode = $('#userBarcode').val();

        // 這裡可以先確認權限、或驗證資料存在
        $.post('/LogOn/Edit', { id, userBarcode }, function (res) {
            if (res.success) {
                // ✅ 載入 Partial View，返回的是 Modal HTML
                $.get('/LogOn/EditPartialView', { id }, function (html) {
                    const $container = $('#editModalContainer');
                    $container.empty().append(html);

                    // ✅ Modal 初始化（需用原生 DOM）
                    const modalEl = document.getElementById('editLogOnModal');
                    if (modalEl) {
                        const modal = new bootstrap.Modal(modalEl);
                        modal.show();
                        bindEditModalEvents(); 
                    } else {
                        Swal.fire('錯誤', '找不到編輯視窗', 'error');
                    }
                }).fail(() => {
                    Swal.fire('錯誤', '載入編輯畫面失敗', 'error');
                });
            } else {
                Swal.fire('錯誤', res.message || '取得資料失敗');
            }
        });
    });

    // 完成
    $('.btn-finish-logon').on('click', function () {
        let id = $(this).data('id');
        let userBarcode = $('#userBarcode').val();

        $.post('/LogOn/Finish', { id, userBarcode }, function (res) {
            Swal.fire({
                icon: res.success ? 'success' : 'error',
                title: res.message
            }).then(() => {
                if (res.success) refreshLogOnTable();
            });
        });
    });

}
