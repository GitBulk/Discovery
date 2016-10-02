(function () {
    'use strict'
    angular.module('customersApp').filter('nameCityStateFilter', nameCityStateFilter);
    function nameCityStateFilter() {

        return function (customers, filterValue) {
            if (!customers || !filterValue) {
                return customers;
            }
            var matches = [];
            for (var i = 0; i < customers.length; i++) {
                var customer = customers[i];
                if (customer.firstName.toLowerCase().indexOf(filterValue) > -1 ||
                    customer.lastName.toLowerCase().indexOf(filterValue) > -1 ||
                    customer.city.toLowerCase().indexOf(filterValue) > -1 ||
                    customer.state.name.toLowerCase().indexOf(filterValue) > -1) {
                    matches.push(customer);
                }
            }
            return matches;
        }
    }
})();