var AutoDealer = AutoDealer || {};

AutoDealer.autodeal_agreement = (function () {
    let globalNotificationIds = [];

    /** 
     * Невалидные сроки кредитной программы
    */
    const INVALID_CREDIT_PERIOD_MSG = "Дата договора вне периода действия кредитной программы";

    /** 
     * Список валидаторов формы
    */
    const FORM_VALIDATORS = [validateCredit];

    /**
     * Валидирует данные на форме с помощью валидаторов из FORM_VALIDATORS
     * @param ctx = контекст,
     * @param validationAccumulator = список ошибок валидации, optional
    */
    function validateForm(ctx, validationAccumulator = []) {
        return new Promise((resolve, reject) => {
            let validatorsChain = Promise.resolve(validationAccumulator);

            FORM_VALIDATORS.forEach(
                validator => {
                    validatorsChain = validatorsChain.then(
                        accum => validator(ctx, accum),
                        err => console.error(validator.name + ": " + err)
                    )
                }
            );

            validatorsChain.then(resolve, reject);
        });
    };

    /**
     * Проверяет срок действия кредитной программы относительно даты договора.
     * Если срок истек, система показывает пользователю сообщение.
     * @param ctx = контекст,
     * @param validationAccumulator = список ошибок валидации, optional
    */
    function validateCredit(ctx, validationAccumulator = []) {
        return new Promise((resolve, reject) => {
            let formContext = ctx.getFormContext();
            let creditLookup = formContext.getAttribute("autodeal_creditid").getValue();
            let agreementDate = formContext.getAttribute("autodeal_date").getValue();

            if (!creditLookup || !agreementDate) {
                resolve(validationAccumulator);
                return;
            }

            Xrm.WebApi
                .retrieveRecord("autodeal_credit", creditLookup[0].id)
                .then(
                    function (credit) {
                        let dateStart = new Date(credit.autodeal_datestart);
                        let dateEnd = new Date(credit.autodeal_dateend);

                        if (!(dateStart <= agreementDate && agreementDate <= dateEnd)) {
                            validationAccumulator.push(INVALID_CREDIT_PERIOD_MSG);
                        }

                        resolve(validationAccumulator);
                    },

                    function (err) {
                        console.error(err.message);

                        reject(err);
                    },
                );
        })
    }

    /**
     * Скрывает вкладку
     * @param ctx = контекст,
     * @param tabName = название таблицы,
    */
    function hideTab(ctx, tabName) {
        let formContext = ctx.getFormContext();

        formContext.ui.tabs.get(tabName).setVisible(false);
    };

    /**
     * изменяет состояние всех полей вкладки на переданное в disabled
     * @param ctx = контекст,
     * @param tabName = название таблицы,
     * @param enabled = включить поля - bool,
     * @param except = за исключением полей - [fieldName]. optional
    */
    function updateTabFieldsState(ctx, tabName, enabled, except = []) {
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
     * 1) Если кредитная программа не выбрана - отключить прочие поля на вкладке кредита.
    */
    function onCreditChanged(ctx) {
        let formContext = ctx.getFormContext();
        let credit = formContext.getAttribute("autodeal_creditid").getValue();

        if (!credit) {
            return;
        }

        let creditCtrl = formContext.getControl("autodeal_creditid");

        creditCtrl.clearNotification();
        formContext.getControl("autodeal_date").clearNotification();

        //Валидация
        validateCredit(ctx)
            .then(
                function (accumulator) {
                    accumulator.forEach(msg => {
                        creditCtrl.setNotification(msg);
                    });
                },

                function (err) {
                    console.error(err);
                    creditCtrl.setNotification(err);
                }
            );

        //Разблокирование кредитных вкладок
        updateTabFieldsState(ctx, "credit_tab", credit != null, ["autodeal_creditid"]);

        //Обновление сроков кредита
        updateCreditPeriod(ctx);
    }

    /**
     * Обновляет кредитный период согласно данным по выбранной кредитной программы
    */
    function updateCreditPeriod(ctx) {
        let formContext = ctx.getFormContext();
        let creditLookup = formContext.getAttribute("autodeal_creditid").getValue();

        if (!creditLookup) {
            return;
        }

        Xrm.WebApi
            .retrieveRecord("autodeal_credit", creditLookup[0].id, "?$select=autodeal_creditperiod")
            .then(
                function (credit) {
                    let creditPeriod = credit.autodeal_creditperiod;
                    if (creditPeriod) {
                        formContext.getAttribute("autodeal_creditperiod").setValue(creditPeriod);
                    }
                },

                function (err) {
                    console.error(err.message);
                },
            );
    }

    /**
     * Обновляет стоимость в договоре согласно данным по выбранному автомобилю
     * По ТЗ:
     * При выборе объекта Автомобиль в объекте Договор, стоимость должна подставляться 
     * Автоматически в соответствии с правилом:
     * Если автомобиль с пробегом, стоимость берется из поля Сумма на объекте Автомобиль.
     * Если автомобиль без пробега, стоимость берется из поля Сумма объекта Модель, указанной на Автомобиле.
    */
    function updateCost(ctx) {
        let formContext = ctx.getFormContext();
        let vehicleLookup = formContext.getAttribute("autodeal_autoid").getValue();

        if (!vehicleLookup) {
            return;
        }

        Xrm.WebApi
            .retrieveRecord(
                "autodeal_vehicle",
                vehicleLookup[0].id,
                "?$select=autodeal_used,autodeal_amount,autodeal_modelId&$expand=autodeal_modelId($select=autodeal_recommendedamount)"
            )
            .then(
                function (vehicle) {
                    let used = vehicle.autodeal_used;
                    let sumAttr = formContext.getAttribute("autodeal_sum");
                    if (used) {
                        sumAttr.setValue(vehicle.autodeal_amount);
                    }
                    else {
                        sumAttr.setValue(vehicle.autodeal_modelId.autodeal_recommendedamount);
                    }
                },

                function (err) {
                    console.error(err.message);
                },
            );
    }

    /**
     * Обработчик события изменения даты договора
    */
    function onDateChanged(ctx) {
        let formContext = ctx.getFormContext();
        let dateCtrl = formContext.getControl("autodeal_date");

        dateCtrl.clearNotification();
        formContext.getControl("autodeal_creditid").clearNotification();

        //Валидация
        validateCredit(ctx)
            .then(
                function (accumulator) {
                    accumulator.forEach(msg => {
                        dateCtrl.setNotification(msg);
                    });
                },

                function (err) {
                    console.error(err);
                    dateCtrl.setNotification(err);
                }
            );
    }

    /**
     * Делает видимой вкладку кредита.
     * По тз:
     * При выборе контакта И авто должна стать видимой кредитная вкладка
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
        let creditCtrl = formContext.getControl("autodeal_creditid");

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

            creditCtrl.addCustomView(
                creditCtrl.getDefaultView(),
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
     * Обработчик события сохранения. Предотвращает сохранение в случае невалидности формы
    */
    function onSaveEventHandler(ctx) {
        globalNotificationIds.forEach(id => Xrm.App.clearGlobalNotification(id));
        globalNotificationIds = [];

        validateForm(ctx, [])
            .then(
                validationAccumulator => {
                    validationAccumulator.forEach(
                        msg => {
                            Xrm.App
                                .addGlobalNotification({ level: 2, message: msg, type: 2 })
                                .then(id => globalNotificationIds.push(id));
                        }
                    );
                },
                err => {
                    console.error(err);
                    context.getEventArgs().preventDefault();
                }
            );
    }

    function onVehicleChanged(ctx) {
        let vehicle = ctx.getFormContext().getAttribute("autodeal_autoid").getValue();
        if (vehicle) {
            //Открываем вкладки
            showCreditTab(ctx);

            //Фильтруем кредитные программы
            filterCreditPrograms(ctx);

            updateCost(ctx);
        }
    }

    function onContactChanged(ctx) {
        showCreditTab(ctx);
    }

    /**
     * Добавляет обработчики событий
    */
    function addEventHandlers(ctx) {
        let formContext = ctx.getFormContext();

        formContext.data.entity.addOnSave(onSaveEventHandler);

        formContext.getAttribute("autodeal_contact").addOnChange(onContactChanged);
        formContext.getAttribute("autodeal_autoid").addOnChange(onVehicleChanged);
        formContext.getAttribute("autodeal_creditid").addOnChange(onCreditChanged);
        formContext.getAttribute("autodeal_date").addOnChange(onDateChanged);
        formContext.getAttribute("autodeal_name").addOnChange(clearNumber);
    };

    return {
        onLoad: function (ctx) {
            let formContext = ctx.getFormContext();

            switch (formContext.ui.getFormType()) {
                case 0: //Undefined
                    console.error("cannot define form type");
                //walkthrough 
                case 1: //Create
                    hideTab(ctx, "credit_tab");
                    updateTabFieldsState(ctx, tabName = "credit_tab", enabled = false, except = ["autodeal_creditid"]);
                    break;
            }

            addEventHandlers(ctx);
            filterCreditPrograms(ctx);
        },

        /**
         * Пересчитывает данные кредита по ТЗ:
         * Пересчитывать поле сумма кредита:
         * Сумма кредита = [Договор].[Сумма] – [Договор].[Первоначальный взнос]
         * Пересчитать поле полная стоимость кредита:
         * Полная стоимость кредита = ([Кредитная Программа].[Ставка] / 100 * [Договор].[Срок кредита] * [Договор].[Сумма кредита]) + [Договор].[Сумма кредита]
        */
        onCalculateCreditClick: function (primaryControl) {
            let formContext = primaryControl;

            let agreementSum = formContext.getControl("autodeal_sum").getValue();
            let initialFee = formContext.getControl("autodeal_initialfee").getValue();
            let creditLookup = formContext.getAttribute("autodeal_creditid").getValue();
            let creditPeriod = formContext.getControl("autodeal_creditperiod").getValue();

            if ([agreementSum, initialFee, creditLookup, creditPeriod].includes(null)) {
                console.log("Кредит не пересчитан, одно из обязательных полей не заполнено или заполнено неверно");
                return;
            }

            agreementSum = AutoDealer.convert.currencyToFloat(agreementSum);
            initialFee = AutoDealer.convert.currencyToFloat(initialFee);
            creditPeriod = creditPeriod.replaceAll(" ", "");

            let calculatedCreditSum = agreementSum - initialFee;

            Xrm.WebApi
                .retrieveRecord(
                    "autodeal_credit",
                    creditLookup[0].id,
                    "?$select=autodeal_percent"
                )
                .then(
                    function (credit) {
                        let calculatedFullCredit = (
                            (credit.autodeal_percent / 100) *
                            creditPeriod *
                            calculatedCreditSum
                        ) +
                            calculatedCreditSum;

                        formContext.getAttribute("autodeal_fullcreditamount").setValue(calculatedFullCredit);
                        formContext.getAttribute("autodeal_creditamount").setValue(calculatedCreditSum);
                    },

                    function (err) {
                        console.error(err.message);
                    },
                );
        }
    };
})();
