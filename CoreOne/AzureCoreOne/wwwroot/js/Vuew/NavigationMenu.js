// define a custom filter called currency
Vue.filter('currency', function (value) {
    return '$' + value.toFixed(2);
});

var orderFormApp = new Vue({
    el: '#orderFormApp',
    data: {
        services: [
            { name: "Web Developement", price: 300, active: true },
            { name: "Desgin", price: 400, active: false },
            { name: "Integration", price: 250, active: false },
            { name: "Training", price: 220, active: false }
        ]
    },
    methods: {
        toggleActive: function (item) {
            item.active = !item.active;
        },
        total: function () {
            var total = 0;
            this.services.forEach(function (item) {
                if (item.active) {
                    total += item.price;
                }
            });
            return total;
        }
    }
});