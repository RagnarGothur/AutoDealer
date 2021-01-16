var AutoDealer = AutoDealer || {};

AutoDealer.roles = (function () {
    return {
        SYSTEM_ADMIN_ROLEID: "4c7cd3c4-9c42-eb11-bb23-000d3a49e35a",

        checkAdminRole: function (ctx) {
            let currentUserRoles = Xrm.Utility.getGlobalContext().userSettings.roles;
            for (let id in currentUserRoles._collection) {
                if (id === this.SYSTEM_ADMIN_ROLEID) {
                    return true;
                }
            }

            return false;
        }
    };
})();
