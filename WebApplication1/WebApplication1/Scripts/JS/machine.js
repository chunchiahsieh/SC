document.addEventListener('DOMContentLoaded', function () {
    const filter = document.getElementById('statusFilter');
    if (filter) {
        filter.addEventListener('change', getMachines);
    }
});

function getMachines() {
    const workCenterId = document.getElementById('statusFilter').value;

    fetch('/Machine/GetMachinesByWorkCenter?workCenterId=' + workCenterId)
        .then(response => response.text())
        .then(html => {
            document.querySelector('#machineTableBody').innerHTML = html;
        })
        .catch(error => console.error('發生錯誤:', error));
}
