var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_communication = (function () {
    let hideCommunicationFields = function (ctx) {
        let formContext = ctx.getFormContext();

        formContext.getControl("autodeal_email").setVisible(false);
        formContext.getControl("autodeal_phone").setVisible(false);
    }

    let addEventHandlers = function (ctx) {
        let formContext = ctx.getFormContext();

        formContext.getAttribute("autodeal_type").addOnChange(onAutodealChange);
    }

    let onAutodealChange = function (ctx) {
        //hide already showed fields
        hideCommunicationFields(ctx);

        let formContext = ctx.getFormContext();
        let communicationType = formContext.getAttribute("autodeal_type");

        switch (communicationType.getSelectedOption().value % 10) {
            case 0: //телефон
                formContext.getControl("autodeal_phone").setVisible(true);
                break;
            case 1: //email
                formContext.getControl("autodeal_email").setVisible(true);
                break;
        }
    }

    return {
        onLoad: function (ctx) {
            hideCommunicationFields(ctx);
            addEventHandlers(ctx);
        }
    };
})();
