var dataTable;

$(document).ready(function() {
    loadDataTable();
});

function loadDataTable() {
    dataTable=$('#DT_load').DataTable({
        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "author", "width": "25%" },
            { "data": "isbn", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/BookList/Edit?id=${data}" class="btn btn-success text-white" style="cursor:pointer;width:70px"> Edit </a>
                            &nbsp;
                            <a onclick="Delete('/api/book?id='+${data})" class="btn btn-danger text-white" style="cursor:pointer;width:70px"> Delete </a>
                        </div>
                    `
                }, "width":"25%"
            }
        ],
        "language": {
            "emptyTable": "No data found!"
        },
        "width": "100%"
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#b0b0b0',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        Swal.fire(
                            'Deleted!',
                            'Your book has been deleted.',
                            'success'
                        ).then(() => {
                            dataTable.ajax.reload();
                        })
                    }
                    else {
                        swalWithBootstrapButtons.fire(
                            'Error!!!',
                            'Something went wrong while deleting!',
                            'error'
                        )
                    }
                }
            })
        }
    })
}