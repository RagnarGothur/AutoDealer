var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_communication = (function () {
    let hideAllCommunicationFields = function (ctx) {
        let formContext = ctx.getFormContext();
        
        formContext.getControl("autodeal_email").setVisible(false);
        formContext.getControl("autodeal_phone").setVisible(false);
    }

    let showAllowedFields = function (ctx) {
        let formContext = ctx.getFormContext();
        let communicationType = formContext.getAttribute("autodeal_type");
        
        //if no option selected - do nothing at all
        if (communicationType.getSelectedOption()) {
            switch (communicationType.getSelectedOption().value % 10) {
                case 0: //телефон
                    formContext.getControl("autodeal_phone").setVisible(true);
                    break;
                case 1: //email
                    formContext.getControl("autodeal_email").setVisible(true);
                    break;
            }
        }
    }

    let addEventHandlers = function (ctx) {
        let formContext = ctx.getFormContext();

        formContext.getAttribute("autodeal_type").addOnChange(onAutodealChange);
    }

    let onAutodealChange = function (ctx) {
        //hide already showed field
        hideAllCommunicationFields(ctx);
        showAllowedFields(ctx);
    }

    return {
        onLoad: function (ctx) {
            hideAllCommunicationFields(ctx);
            showAllowedFields(ctx);
            addEventHandlers(ctx);
        }
    };
})();
