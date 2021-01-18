var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_vehicle = (function () {
    /**
     * Устанавливает переданное значение в setVisible полей isdamaged, ownerscount, km
    */
    function setUsedFieldsVisible (ctx, bool) {
        let formContext = ctx.getFormContext();

        formContext.getControl("autodeal_isdamaged").setVisible(bool);
        formContext.getControl("autodeal_ownerscount").setVisible(bool);
        formContext.getControl("autodeal_km").setVisible(bool);
    }

    /**
     * обновляет информацию о видимости полей isdamaged, ownerscount, km
    */
    function updateUsedVisibleState (ctx) {
        let formContext = ctx.getFormContext();

        setUsedFieldsVisible(ctx, formContext.getAttribute("autodeal_used").getValue());
    }

    /**
     * добавляет обработчики событий
    */
    function addEventHandlers (ctx) {
        let formContext = ctx.getFormContext();

        formContext.getAttribute("autodeal_used").addOnChange(updateUsedVisibleState);
    }

    return {
        onLoad: function (ctx) {
            updateUsedVisibleState(ctx);
            addEventHandlers(ctx);
        }
    };
})();
