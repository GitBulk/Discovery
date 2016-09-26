(function () {
    'use strict'
    angular.module('customersApp').filter('nameProductFilter', nameProductFilter);
    function nameProductFilter() {
        function matchesProduct(customer, filterValue) {
            if (customer.orders) {
                for (var i = 0; i < customer.orders.length; i++) {
                    if (customer.orders[i].product.toLowerCase().indexOf(filterValue) > -1) {
                        return true;
                    }
                }
            }
            return false;
        }

        return function (customers, filterValue) {
            if (!customers || !filterValue) {
                return customers;
            }
            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < customers.length; i++) {
                var customer = customers[i];
                if (customer.firstName.toLowerCase().indexOf(filterValue) > -1 ||
                    customer.lastName.toLowerCase().indexOf(filterValue) > -1 ||
                    matchesProduct(customer, filterValue)) {
                    matches.push(customer);
                }
            }
            return matches;
        }
    }
})();