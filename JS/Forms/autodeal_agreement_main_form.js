var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_agreement = (function () {
    /**
     * Скрывает вкладки
    */
    let hideTabs = function (ctx) {
        let formContext = ctx.getFormContext();

        formContext.ui.tabs.get("credit_tab").setVisible(false);
        //formContext.ui.tabs.get("payment_tab").setVisible(false);
    };

    /**
     * Блокирует поля на таблице кредита
    */
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

    /**
     * Разблокирует поля на таблице кредита
    */
    let enableCreditTabFields = function (ctx) {
        let creditTab = ctx.getFormContext().ui.tabs.get("credit_tab");

        creditTab.sections.forEach(section => {
            section.controls.forEach(control => {
                control.setDisabled(false);
            })
        });
    }

    /**
     * Делает видимой вкладку кредита
    */
    let showCreditTab = function (ctx) {
        let formContext = ctx.getFormContext();
        let contact = formContext.getAttribute("autodeal_contact").getValue();
        let automobile = formContext.getAttribute("autodeal_autoid").getValue();

        if (contact !== null && contact[0].id && automobile !== null && automobile[0].id) {
            formContext.ui.tabs.get("credit_tab").setVisible(true);
        }
    }

    /**
     * Фильтрует кредитные программы по полю автомобиль оставляя сущности, связанные с автомобилем N to N
    */
    let filterCreditPrograms = function (ctx) {
        let formContext = ctx.getFormContext();
        let automobile = formContext.getAttribute("autodeal_autoid").getValue();
        let credit = formContext.getControl("autodeal_creditid");

        if (automobile) {
            credit.addCustomFilter(
                "<filter type='and'><condition attribute='autodeal_vehicleid' operator='eq' value='" + automobile[0].id + "'/></filter>"
            );
        }
    }

    /**
     * Разблокирует поля на таблице кредита
    */
    let clearNumber = function (ctx) {
        let formContext = ctx.getFormContext();
        let inputedNumber = formContext.getControl("autodeal_name").getValue();

        if (inputedNumber) {
            let cleared = "";

            for (let c of inputedNumber) {
                if (c >= "0" && c <= "9" || c === "-") {
                    cleared += c;
                }
            }

            formContext.getAttribute("autodeal_name").setValue(cleared);
        }
    }

    /**
     * Добавляет обработчики событий
    */
    let addEventHandlers = function (ctx) {
        let formContext = ctx.getFormContext();

        formContext.getAttribute("autodeal_contact").addOnChange(showCreditTab);
        formContext.getAttribute("autodeal_autoid").addOnChange(showCreditTab);
        formContext.getAttribute("autodeal_creditid").addOnChange(enableCreditTabFields);
        formContext.getAttribute("autodeal_name").addOnChange(clearNumber);

        formContext.getControl("autodeal_creditid").addPreSearch(filterCreditPrograms);
    };

    return {
        onLoad: function (ctx) {
            hideTabs(ctx);
            disableCreditTabFields(ctx);
            addEventHandlers(ctx);
        }
    };
})();
