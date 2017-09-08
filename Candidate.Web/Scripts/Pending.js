$(function () {
    load();

    $('.table').on('click', '.btnConfirm', function () {
        $.post("/home/addConfirm", { id: $(this).data('id') }, function (result) {
            $("#Pending").text(`Pending(${result.pendingCount})`);
            $("#Confirmed").text(`Confirmed(${result.confirmedCount})`);
            load();
        });
    });

    $('.table').on('click', '.btnReject', function () {
        $.post("/home/addReject", { id: $(this).data('id') }, function (result) {
            $("#Pending").text(`Pending(${result.pendingCount})`);
            $("#Rejected").text(`Rejected(${result.rejectedCount})`);
            load();
        });
    });

    function load() {
        $('tr:gt(0)').remove();
        $.get("/home/getPending", function (result) {
            result.forEach(a => $('.table').append(`
                <tr>
                    <td>${a.firstName}</td>
                    <td>${a.lastName}</td>
                    <td>${a.email}</td>
                    <td>${a.number}</td>
                    <td>${a.note}</td>
                    <td>
                        <button data-id=${a.id} class="btn btn-primary btnConfirm">Confirm</button>
                        <button data-id=${a.id} class="btn btn-primary btnReject">Reject</button>
                    </td>
                </tr>
             `));
        });
    };
});