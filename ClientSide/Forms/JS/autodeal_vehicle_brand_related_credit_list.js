var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_vehicle_brand_credit_list = (function () {
    const entityName = {
        credit: "autodeal_credit",
        vehicleModel: "autodeal_model",
    };

    function findCreditsByBrand(brandId) {
        let fetchXml = [
            "<fetch>",
            "  <entity name='autodeal_credit'>",
            "    <attribute name='autodeal_creditid' />",
            "    <attribute name='autodeal_name' />",
            "    <attribute name='autodeal_creditperiod' />",
            "    <link-entity name='autodeal_autodeal_credit_autodeal_vehicle' from='autodeal_creditid' to='autodeal_creditid' link-type='inner' intersect='true'>",
            "      <attribute name='autodeal_creditid' />",
            "      <attribute name='autodeal_vehicleid' />",
            "      <link-entity name='autodeal_vehicle' from='autodeal_vehicleid' to='autodeal_vehicleid' link-type='inner' intersect='true'>",
            "        <attribute name='autodeal_modelid' />",
            "        <filter>",
            "          <condition attribute='autodeal_brandid' operator='eq' value='", brandId, "'/>",
            "        </filter>",
            "        <link-entity name='autodeal_model' from='autodeal_modelid' to='autodeal_modelid' link-type='inner'>",
            "          <attribute name='autodeal_name' />",
            "        </link-entity>",
            "      </link-entity>",
            "    </link-entity>",
            "  </entity>",
            "</fetch>",
        ].join("");

        return Xrm.WebApi.retrieveMultipleRecords("autodeal_credit", "?fetchXml= " + fetchXml);
    }

    function addEventHandler(htmlElem, name, id) {
        const windowFeatures = "menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes";

        htmlElem.onclick = function (event) {
            window.open(
                getUrl(name, id),
                name + " " + id,
                windowFeatures
            );
        }
    }

    function getUrl(name, id) {
        return [
            "https://autodealer.crm4.dynamics.com/main.aspx?forceUCI=1",
            "pagetype=entityrecord",
            "etn=" + name,
            "id=" + id
        ].join("&")
    }

    function fillGrid(ctx) {
        let formContext = ctx.getFormContext();
        let brandId = formContext.data.entity.getId();

        findCreditsByBrand(brandId)
            .then(
                function (found) {
                    let table = document.getElementById("cl-table");

                    found.entities.forEach(creditInfo => {
                        let rowElem = document.createElement("div");
                        let nameElem = document.createElement("div");
                        let modelElem = document.createElement("div");
                        let periodElem = document.createElement("div");

                        rowElem.className = "row cl-row";
                        nameElem.className = "col-5 cl-col";
                        modelElem.className = "col-5 cl-col";
                        periodElem.className = "col-2 cl-col";

                        nameElem.innerHTML += creditInfo["autodeal_name"];
                        modelElem.innerHTML += creditInfo["autodeal_model3.autodeal_name"];
                        periodElem.innerHTML += creditInfo["autodeal_creditperiod"] ?? "-";

                        addEventHandler(nameElem, entityName.credit, creditInfo["autodeal_autodeal_credit_autodeal_vehicle1.autodeal_creditid"]);
                        addEventHandler(modelElem, entityName.vehicleModel, creditInfo["autodeal_vehicle2.autodeal_modelid"]);

                        [nameElem, modelElem, periodElem].forEach(
                            el => rowElem.appendChild(el)
                        );

                        table.appendChild(rowElem);
                    });
                },

                function (err) {
                    console.error(err.message);
                }
            );
    }

    function addEventHandlers(ctx) {
        let formContext = ctx.getFormContext();
    }

    return {
        init: function (ctx, xrm) {
            window.Xrm = xrm;
            fillGrid(ctx);
            addEventHandlers(ctx);
        }
    };
})();