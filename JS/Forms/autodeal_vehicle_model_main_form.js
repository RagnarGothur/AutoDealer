var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_vehicle_model = (function () {
    /**
     * id роли системного администратора
    */
    const SYSTEM_ADMIN_ROLEID = "4c7cd3c4-9c42-eb11-bb23-000d3a49e35a";

    /**
     * Проверяет роли текущего пользователя
    */
    function checkAdminRole(ctx) {
        let currentUserRoles = Xrm.Utility.getGlobalContext().userSettings.roles;
        for (let id in currentUserRoles._collection) {
            if (id === SYSTEM_ADMIN_ROLEID) {
                return true;
            }
        }

        return false;
    }

    /**
     * Делает поля Модели недоступными для изменения.
     * АХТУНГ: дополнительная проверка в бэкенде обязательна!
    */
    function readOnly(ctx) {
        let formContext = ctx.getFormContext();

        formContext.ui.tabs.forEach(tab => {
            tab.sections.forEach(section => {
                section.controls.forEach(control => {
                    control.setDisabled(true);
                })
            });
        });
    }

    return {
        onLoad: function (ctx) {
            let formContext = ctx.getFormContext();

            switch (formContext.ui.getFormType()) {
                case 0: //Undefined
                    console.error("cannot define form type");
                //walkthrough 
                case 2: //Update
                case 3: //Read Only
                case 6: //Bulk Edit (really 6)
                    if (!checkAdminRole(ctx)) {
                        readOnly(ctx);
                    }

                    break;
                case 1: //Create
                case 4: //Disabled
                    break;
            }
        }
    };
})();
