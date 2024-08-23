(function ($) {
    "use strict";

    // Add
    $(document).on("click", ".add-basket", function () {
        const productId = $(this).attr('product-id');

        $.ajax({
            type: "Post",
            url: `/Basket/Add?productId=${productId}`,
            success: (res) => {
                if (!res.redirectUrl) {
                    Swal.fire({
                        icon: "success",
                        title: "Added to cart!",
                        showConfirmButton: false,
                        timer: 1500
                    });

                    const count = $(".product-count");
                    count.text(parseInt(count.html()) + 1);
                } else {
                    window.location.href = res.redirectUrl;
                }
            }
        });
    });

    // Increase
    $(document).on("click", ".btn-plus", function () {
        const id = $(this).attr('id');
        const btn = $(this);

        $.ajax({
            type: "Post",
            url: `Basket/Increase?id=${id}`,
            success: function (res) {
                btn.closest(".basket-item").find(".total").text(res.totalPrice + ' $');

                const currentVal = parseInt(btn.closest(".basket-item").find(".quantity input").val());
                btn.closest(".basket-item").find(".quantity input").val(currentVal + 1);

                const count = $(".product-count");
                count.text(parseInt(count.html()) + 1);

                $(".total-price").text('$' + res.total);
            }
        });
    });

    // Decrease
    $(document).on("click", ".btn-minus", function () {
        const btn = $(this);
        const id = $(this).attr('id');
        const basketItem = btn.closest(".basket-item");
        const quantityInput = basketItem.find(".quantity input");
        const quantity = basketItem.find(".quantity input").val();

        if (quantity == 1) {
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
                    $.ajax({
                        type: "Post",
                        url: `Basket/Decrease?id=${id}`,
                        success: function (res) {
                            basketItem.remove();
                            const count = $(".product-count");
                            count.text(parseInt(count.html()) - 1);

                            $(".total-price").text('$' + res.total);
                        }
                    });
                } else {
                    quantityInput.val(1);
                }
            });
        } else {
            $.ajax({
                type: "Post",
                url: `Basket/Decrease?id=${id}`,
                success: function (res) {

                    basketItem.find(".total").text(res.totalPrice + ' $');

                    const count = $(".basket-count");
                    count.text(parseInt(count.html()) - 1);

                    const currentVal = parseInt(btn.closest(".basket-item").find(".quantity input").val());
                    btn.closest(".basket-item").find(".quantity input").val(currentVal - 1);

                    $(".total-price").text('$' + res.total);
                }
            });
        }
    });

    // Change quantity
    $(document).on("keyup", ".quantity input", function () {
        const input = $(this);
        const quantity = input.val();
        const basketItem = input.closest(".basket-item");
        const id = basketItem.attr('id');
        const count = $(".product-count");


        if (quantity == 0) {
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
                    $.ajax({
                        type: "Post",
                        url: `Basket/ChangeQuantity?id=${id}&quantity=${quantity}`,
                        success: function (res) {
                            basketItem.remove();
                            count.text(res.basketCount);

                            $(".total-price").text('$' + res.total);
                        }
                    });
                } else {
                    input.val(1);
                }
            });
        } else {
            $.ajax({
                type: "Post",
                url: `Basket/ChangeQuantity?id=${id}&quantity=${quantity}`,
                success: function (res) {
                    basketItem.find(".total").text(res.totalPrice + ' $');
                    count.text(res.basketCount);

                    $(".total-price").text('$' + res.total);
                }
            });
        }
    });

    // Delete
    $(document).on("click", ".delete-basket", function () {
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
                const id = $(this).attr('id');
                const btn = $(this);

                $.ajax({
                    type: "Post",
                    url: `Basket/Delete?id=${id}`,
                    success: function (res) {
                        btn.closest(".basket-item").remove();

                        const count = $(".basket-count");
                        count.text(parseInt(count.html()) - 1);

                        $(".total-price").text("$" + res);
                    }
                });
            }
        });
    });

})(jQuery);