var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_communication = (function () {
    /**
     * Скрывает поля email и phone
    */
    function hideAllCommunicationFields (ctx) {
        let formContext = ctx.getFormContext();

        formContext.getControl("autodeal_email").setVisible(false);
        formContext.getControl("autodeal_phone").setVisible(false);
    }

    /**
     * Делает видимым поле в зависимости от выбранного типа коммуникации
    */
    function showAllowedField (ctx) {
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

    /**
     * Добавляет обработчики событий
    */
    function addEventHandlers (ctx) {
        let formContext = ctx.getFormContext();

        formContext.getAttribute("autodeal_type").addOnChange(onTypeChanged);
    }

    /**
     * Обработчик события изменения поля type
    */
    function onTypeChanged (ctx) {
        //hide already showed field
        hideAllCommunicationFields(ctx);
        showAllowedField(ctx);
    }

    return {
        onLoad: function (ctx) {
            hideAllCommunicationFields(ctx);
            showAllowedField(ctx);
            addEventHandlers(ctx);
        }
    };
})();
