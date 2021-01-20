var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_vehicle_brand = (function () {
    return {
        onLoad: function (ctx) {
            let formContext = ctx.getFormContext();
            let creditListCtrl = formContext.getControl("WebResource_autodeal_brand_related_credit_list");

            creditListCtrl.getContentWindow().then(
                function (contentWindow) {
                    if (contentWindow.AutoDealer.autodeal_vehicle_brand_credit_list) {
                        contentWindow.AutoDealer.autodeal_vehicle_brand_credit_list.init(ctx, Xrm);
                    }
                    else {
                        console.error("contentWindow.AutoDealer.autodeal_vehicle_brand_credit_list not found");
                    }
                },
                function (err) {
                    console.error(err.message);
                }
            );
        }
    };
})();
