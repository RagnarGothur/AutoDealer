var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_agreement = (function () {
    /**
     * Скрывает вкладки
    */
    function hideTabs(ctx) {
        let formContext = ctx.getFormContext();

        formContext.ui.tabs.get("credit_tab").setVisible(false);
    };

    /**
     * изменяет состояние всех полей вкладки на переданное в disabled
     * @param ctx = контекст,
     * @param tabName = название таблицы,
     * @param enabled = включить поля - bool,
     * @param except = за исключением полей - [fieldName]. optional
    */
    function changeTabFieldsState(ctx, tabName, enabled, except = []) {
        let formContext = ctx.getFormContext();

        let creditTab = formContext.ui.tabs.get(tabName);

        //disable tab fields except $except
        creditTab.sections.forEach(section => {
            section.controls.forEach(control => {
                let fieldName = control.getName();
                if (!(except.includes(fieldName))) {
                    control.setDisabled(!enabled);
                }
            })
        });
    }

    /**
     * Обработчик события изменения кредитной программы.
     * По ТЗ:
     * Если кредитная программа не выбрана - отключить прочие поля на вкладке кредита.
    */
    function onCreditChange(ctx) {
        let credit = ctx.getFormContext().getAttribute("autodeal_creditid").getValue();

        changeTabFieldsState(ctx, "credit_tab", credit != null, ["autodeal_creditid"]);
    }

    /**
     * Делает видимой вкладку кредита
    */
    function showCreditTab(ctx) {
        let formContext = ctx.getFormContext();
        let contact = formContext.getAttribute("autodeal_contact").getValue();
        let automobile = formContext.getAttribute("autodeal_autoid").getValue();

        if (contact && contact[0].id && automobile && automobile[0].id) {
            formContext.ui.tabs.get("credit_tab").setVisible(true);
        }
    }

    /**
     * Фильтрует кредитные программы по полю автомобиль оставляя сущности, связанные с автомобилем N to N
    */
    function filterCreditPrograms(ctx) {
        let formContext = ctx.getFormContext();
        let vehicle = formContext.getAttribute("autodeal_autoid").getValue();
        let credit = formContext.getControl("autodeal_creditid");

        if (vehicle) {
            let fetchXml = [
                "<fetch version='1.0' mapping='logical'>",
                "  <entity name='autodeal_credit'>",
                "    <link-entity name='autodeal_autodeal_credit_autodeal_vehicle' from='autodeal_creditid' to='autodeal_creditid' intersect='true'>",
                "      <filter>",
                "        <condition attribute='autodeal_vehicleid' operator='eq' value='", vehicle[0].id, "'/>",
                "      </filter>",
                "    </link-entity>",
                "  </entity>",
                "</fetch>",
            ]
                .join("");

            let layout = [
                "<grid name='resultset' object='10238' jump='autodeal_name' select='1' icon='1' preview='1' >",
                "  <row name='result' id='autodeal_creditid' >",
                "    <cell name='autodeal_name' width='300' />",
                "    <cell name='autodeal_datestart' width='100' />",
                "    <cell name='autodeal_dateend' width='100' />",
                "    <cell name='autodeal_percent' width='100' />",
                "  </row>",
                "</grid>"
            ]
                .join("");

            credit.addCustomView(
                credit.getDefaultView(),
                "autodeal_credit",
                "Доступные кредитные программы по автомобилю " + vehicle[0].name,
                fetchXml,
                layout,
                true
            );
        }
    }

    /**
     * Разблокирует поля на таблице кредита
    */
    function clearNumber(ctx) {
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
    function addEventHandlers(ctx) {
        let formContext = ctx.getFormContext();

        formContext.getAttribute("autodeal_contact").addOnChange(showCreditTab);
        formContext.getAttribute("autodeal_autoid").addOnChange(showCreditTab);
        formContext.getAttribute("autodeal_autoid").addOnChange(filterCreditPrograms);
        formContext.getAttribute("autodeal_creditid").addOnChange(onCreditChange);
        formContext.getAttribute("autodeal_name").addOnChange(clearNumber);
    };

    return {
        onLoad: function (ctx) {
            hideTabs(ctx);
            changeTabFieldsState(ctx, tabName = "credit_tab", enabled = false, except = ["autodeal_creditid"]);
            addEventHandlers(ctx);
            filterCreditPrograms(ctx);
        }
    };
})();
