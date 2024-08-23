(function ($) {
    "use strict"

    // Search
    $(document).on("keyup", ".search-product-input", function () {
        const value = $(this).val().trim();

        if (value !== "") {
            $(".paginate").css("display", "none");
        } else {
            $(".paginate").css("display", "block");
        }

        $(".product-items .product-item").slice(0).remove();

        $.ajax({
            type: "Get",
            url: `Shop/Search?searchText=${value}`,
            success: function (res) {
                $(".product-items").append(res);
            }
        });
    });

    // Category filter
    $('.category-filter').on('click', function () {
        const categoryId = $(this).attr('category-id');

        $(".paginate").css("display", "none");

        $(".product-items .product-item").slice(0).remove();

        $.ajax({
            type: "Get",
            url: `Shop/CategoryFilter?id=${categoryId}`,
            success: function (res) {
                $('.product-items').append(res);
            },
        });
    });

    // Brand filter
    $('.brand-filter').on('click', function () {
        const brandId = $(this).attr('brand-id');

        $(".paginate").css("display", "none");

        $(".product-items .product-item").slice(0).remove();

        $.ajax({
            type: "Get",
            url: `Shop/BrandFilter?id=${brandId}`,
            success: function (res) {
                $('.product-items').append(res);
            },
        });
    });

    // Contact
    $('.add-contact').on('click', (e) => {
        e.preventDefault();

        const fullnameVal = $(".fullname").val();
        const emailVal = $(".email").val();
        const subjectVal = $(".subject").val();
        const messageVal = $(".message").val();

        if (fullnameVal === "" || emailVal === "" || subjectVal === "" || messageVal === "") {
            Swal.fire({
                title: "Inputs must be full!",
                icon: "warning",
                confirmButtonColor: "#3085d6",
                confirmButtonText: "Ok",
                timer: 1500
            });
            return;
        }

        $.ajax({
            type: "Post",
            url: "/Contact/Add",
            data: {
                fullname: fullnameVal,
                email: emailVal,
                subject: subjectVal,
                message: messageVal
            },
            success: () => {
                Swal.fire({
                    icon: "success",
                    title: "Your application has been accepted!",
                    showConfirmButton: false,
                    timer: 1500
                });
            },
        });
    });
})(jQuery);