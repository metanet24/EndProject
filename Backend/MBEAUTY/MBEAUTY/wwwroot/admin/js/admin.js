(function ($) {
    "use strict";

    // Delete image
    $(document).on("click", ".delete-img", function () {
        const btn = $(this);
        const id = btn.attr('id');

        $.ajax({
            type: "Post",
            url: `/Admin/Product/DeleteImage?id=${id}`,
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
            url: `/Admin/Product/EditImageType?id=${id}`,
            success: function (res) {
                $(".image").each((index, img) => {
                    $(img).removeClass("border border-5");
                });

                btn.closest(".image").addClass("border border-5");
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
                Swal.fire({
                    title: "Deleted!",
                    text: "Your product has been deleted.",
                    icon: "success"
                });

                const btn = $(this);
                const id = btn.attr('id');

                $.ajax({
                    type: "Post",
                    url: `/Admin/${action()}/Delete?id=${id}`,
                    success: () => {
                        btn.closest("tr").remove();
                    }
                });
            }
        });
    });

    function action() {
        const url = window.location.href;
        const pathname = new URL(url).pathname;
        const parts = pathname.split('/');
        const actionName = parts[parts.length - 1];

        return actionName;
    }
})(jQuery);