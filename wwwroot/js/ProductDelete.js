$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this) //  catch object which i need to delet it

        //Alert To Delete
        //must add css, js files of thislibrary to avoid error of Swal not found : rightclick => addclientside library >search about this library => add them in layout
        const deleteAlert = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger mx-2",
                cancelButton: "btn btn-success mx-2"
            },
            buttonsStyling: false
        });

        deleteAlert.fire({
            title: "Are you sure To Delete This Product?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({

                    //url of action which botton go to it
                    url: `/Products/Delete/${btn.data('id')}`,
                    method: 'DELETE',

                    success: function () {
                        deleteAlert.fire({
                            title: "Deleted!",
                            text: "Product has been deleted.",
                            icon: "success"
                        });

                        btn.parents('tr').fadeOut();// to disable product after delete not still appear
                    },
                    error: function () {
                        alert('Error Occure While Delete');
                    }


                });

            }
        });



        


    });
})