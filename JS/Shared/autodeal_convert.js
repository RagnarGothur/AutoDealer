var AutoDealer = AutoDealer || {};

AutoDealer.convert = (function () {
    return {
        currencyToFloat: function (currency) {
            return parseFloat(currency.replace(" ", "").replaceAll(",", "."));
        }
    };
})();
