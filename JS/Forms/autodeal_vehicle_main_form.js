var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_vehicle = (function () {
    /**
     * Устанавливает переданное значение в setVisible полей isDamaged, ownersCount, km
    */
    let setUsedFieldsVisible = function (ctx, bool) {
        let formContext = ctx.getFormContext();

        formContext.getControl("autodeal_isDamaged").setVisible(bool);
        formContext.getControl("autodeal_ownersCount").setVisible(bool);
        formContext.getControl("autodeal_km").setVisible(bool);
    }

    /**
     * обновляет информацию о видимости полей isDamaged, ownersCount, km
    */
    let updateUsedVisibleState = function (ctx) {
        let formContext = ctx.getFormContext();

        setUsedFieldsVisible(ctx, formContext.getAttribute("autodeal_used").getValue());
    }

    /**
     * добавляет обработчики событий
    */
    let addEventHandlers = function (ctx) {
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
