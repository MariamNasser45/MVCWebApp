$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this) //  catch object which i need to delet it

        console.log(btn.data('productId'))
    });
})