var Site = {
    Init: function () {
        this.Handlers.Init();
    },
    Settings: {
        Order: {
            ActiveItem: {}
        }
    },
    Handlers: {
        Init: function () {
            $(".btnAddToCart").click(function (e) {
                var btn = $(this);
                e.preventDefault();

                Site.AddProductToCart(btn);
            });

            $('.cart').on('click', '.btnCreateOrder', function (e) {
                var btn = $(this);
                e.preventDefault();

                Site.CreateOrder();
            });

            $(".order").click(function (e) {
                var btn = $(this);
                e.preventDefault();

                if (Site.Settings.Order.ActiveItem)
                    $(Site.Settings.Order.ActiveItem).removeClass("active");

                Site.Settings.Order.ActiveItem = btn;

                $(btn).addClass("active");

                Site.LoadOrder(btn);
            });

            $('.cart').on('click', '.btnDeleteCartItem', function (e) {
                var btn = $(this);
                e.preventDefault();

                Site.DeleteCartItem(btn);
            });

            $('.orderInfoWrapp').on('click', '.btnCompleteOrder', function (e) {
                var btn = $(this);
                e.preventDefault();

                Site.CompleteOrder(btn);
            });
        }
    },
    AddProductToCart: function (btn) {
        var tr = $(btn).closest('tr');

        var uid = $(tr).attr('uid');
        var count = $(tr).find('#count').val();

        var data = { "ProductUid": uid, "Count": count };

        $.ajax({
            type: "POST",
            url: "/Cart/Create",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (response) {
                if (response.code == 200) {
                    $(tr).find('#count').val('1');
                    Site.LoadCart();
                } else { alert(response.error); }
            },
            failure: function (response) {
                alert(response);
            }
        });
    },
    CreateOrder: function () {
        $.ajax({
            type: "POST",
            url: "/Order/Create",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                Site.LoadCart();
            },
            failure: function (response) {
                alert(response);
            }
        });
    },
    LoadCart: function () {
        $.ajax({
            type: "GET",
            url: '/Cart/List',
            contentType: "text/html",
            dataType: "html",
            success: function (response) {
                Site.RenderCart(response);
            },
            failure: function (response) {
                alert(response);
            }
        });
    },
    RenderCart: function (data) {
        var container = $('.cart');

        $(container).html(data);
    },
    LoadOrder: function (item) {

        var id = $(item).attr('id');

        $.ajax({
            type: "GET",
            url: "/Order/GetById?id=" + id,
            contentType: "text/html",
            dataType: "html",
            success: function (response) {
                Site.RenderOrder(response);
            },
            failure: function (response) {
                alert(response);
            }
        });
    },
    RenderOrder: function (data) {
        if (data) {
            var wrapp = $('.orderInfoWrapp');

            $(wrapp).html(data);
        }
    },
    CompleteOrder: function (item) {
        var id = $(item).attr('id');

        $.ajax({
            type: "PUT",
            url: "/Order/Complete?id=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.code == 200) {
                    Site.LoadOrder(item);
                }
                else { alert(response.error); }
            },
            failure: function (response) {
                alert(response);
            }
        });
    },
    DeleteCartItem: function (item) {
        var uid = $(item).attr('uid');

        $.ajax({
            type: "DELETE",
            url: "/Cart/Delete?uid=" + uid,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.code == 200) {
                    Site.LoadCart();
                }
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
}