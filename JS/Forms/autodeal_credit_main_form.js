var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_credit = (function () {
    /**
     * Список валидаторов формы
    */
    const FORM_VALIDATORS = [validateDateConsistency];

    /**
     * Количество миллисекунд в году. Пусть будет тут, не хочу тащить библиотеки ради простейшей операции
    */
    const MS_IN_YEAR = 1000 * 60 * 60 * 24 * 365; //високосными годами можно пренебречь

    /**
     * Сообщение об неконсистентности даты
    */
    const DATE_INCONSISTENT_MSG = "Дата окончания должна быть больше даты начала не менее, чем на год";

    /**
     * Валидирует консистентность даты по бизнес-правилам:
     * 1) дата окончания должна быть больше даты начала не менее, чем на год
     * returns: true - валидное состояние, false - невалидное
    */
    function validateDateConsistency(ctx) {
        let formContext = ctx.getFormContext();

        let dateStartCtrl = formContext.getControl("autodeal_datestart");
        let dateEndCtrl = formContext.getControl("autodeal_dateend");
        dateEndCtrl.clearNotification(); // removing deprecated ntf

        let dateStart = dateStartCtrl.getAttribute().getValue();
        let dateEnd = dateEndCtrl.getAttribute().getValue();

        if (dateEnd - dateStart < MS_IN_YEAR) {
            dateEndCtrl.setNotification(DATE_INCONSISTENT_MSG);
            return false;
        }

        return true;
    }

    /**
     * Валидирует данные на форме с помощью валидаторов из FORM_VALIDATORS
    */
    function validateForm(ctx) {
        let event = ctx.getEventArgs();
        let valid = true;

        FORM_VALIDATORS.forEach(
            validator => {
                valid &= validator(ctx);
            }
        );

        //if it's the save event then the form must be prevented from save
        if (!valid && typeof event.preventDefault === "function") {
            event.preventDefault();
        }
    };

    /**
     * добавляет обработчики событий
    */
    function addEventHandlers(ctx) {
        let formContext = ctx.getFormContext();

        formContext.data.entity.addOnSave(validateForm);
        formContext.getAttribute("autodeal_datestart").addOnChange(validateDateConsistency);
        formContext.getAttribute("autodeal_dateend").addOnChange(validateDateConsistency);
    }

    return {
        onLoad: function (ctx) {
            addEventHandlers(ctx);
        }
    };
})();
