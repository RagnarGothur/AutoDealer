var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_agreement = (function () {
    let hideTabs = function (ctx) {
        let formContext = ctx.getFormContext();

        formContext.ui.tabs.get("credit_tab").setVisible(false);
        //formContext.ui.tabs.get("payment_tab").setVisible(false);
    };

    let disableCreditTabFields = function (ctx) {
        let formContext = ctx.getFormContext();

        let creditTab = formContext.ui.tabs.get("credit_tab");

        //disable credit tab fields except autodeal_creditid
        creditTab.sections.forEach(section => {
            section.controls.forEach(control => {
                if (control.getName() !== "autodeal_creditid") {
                    control.setDisabled(true);
                }
            })
        });
    }

    let enableCreditTabFields = function (ctx) {
        let creditTab = ctx.getFormContext().ui.tabs.get("credit_tab");

        creditTab.sections.forEach(section => {
            section.controls.forEach(control => {
                control.setDisabled(false);
            })
        });
    }

    let showCreditTab = function (ctx) {
        let formContext = ctx.getFormContext();
        let contact = formContext.getAttribute("autodeal_contact").getValue();
        let automobile = formContext.getAttribute("autodeal_autoid").getValue();

        if (contact !== null && contact[0].id && automobile !== null && automobile[0].id) {
            formContext.ui.tabs.get("credit_tab").setVisible(true);
        }
    }

    let clearNumber = function (ctx) {
        let formContext = ctx.getFormContext();
        let inputedNumber = formContext.getControl("autodeal_name").getValue();
        let cleared = "";

        for (let c of inputedNumber) {
            if (c >= "0" && c <= "9" || c === "-") {
                cleared += c;
            }
        }

        formContext.getAttribute("autodeal_name").setValue(cleared);
    }

    let addEventHandlers = function (ctx) {
        let formContext = ctx.getFormContext();

        formContext.getAttribute("autodeal_contact").addOnChange(showCreditTab);
        formContext.getAttribute("autodeal_autoid").addOnChange(showCreditTab);
        formContext.getAttribute("autodeal_creditid").addOnChange(enableCreditTabFields);
        formContext.getAttribute("autodeal_name").addOnChange(clearNumber);
    };

    return {
        onLoad: function (ctx) {
            hideTabs(ctx);
            disableCreditTabFields(ctx);
            addEventHandlers(ctx);
        }
    };
})();
