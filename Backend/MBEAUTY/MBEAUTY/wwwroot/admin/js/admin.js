(function ($) {
    "use strict";

    // Delete image
    $(document).on("click", ".delete-img", function () {
        const btn = $(this);
        const id = btn.attr('id');

        $.ajax({
            type: "Post",
            url: `/Admin/${getAction()}/DeleteImage?id=${id}`,
            success: function (res) {
                if (res) {
                    btn.closest(".image").remove();
                } else {
                    Swal.fire({
                        title: "Must have at least one image and one main image!",
                        icon: "warning",
                        confirmButtonColor: "#3085d6",
                        confirmButtonText: "Ok"
                    });
                }
            }
        });
    });

    // Edit image type
    $(document).on("click", ".main-img", function () {
        const btn = $(this);
        const id = btn.attr('id');

        $.ajax({
            type: "Post",
            url: `/Admin/${getAction()}/EditImageType?id=${id}`,
            success: function (res) {
                $(".image").each((index, img) => {
                    $(img).css({'border': 'none'});
                });

                btn.closest(".image").css({ 'border': '3px solid red' });
            }
        });
    });

    // Delete
    $(document).on("click", ".delete-item", function () {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                const btn = $(this);
                const id = btn.attr('id');

                $.ajax({
                    type: "Post",
                    url: `/Admin/${getAction()}/Delete?id=${id}`,
                    success: () => {
                        btn.closest("tr").remove();
                    }
                });
            }
        });
    });

    function getAction() {
        var url = window.location.href;
        var parts = url.split('/');
        var adminIndex = parts.indexOf("Admin");
        if (adminIndex !== -1 && parts.length > adminIndex + 1) {
            return parts[adminIndex + 1];
        }
        return null;
    }
})(jQuery);