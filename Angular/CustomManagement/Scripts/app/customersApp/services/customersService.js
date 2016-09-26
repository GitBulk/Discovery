(function () {
    'use strict'
    angular.module('customersApp').factory('customersService', customersService);
    customersService.$inject = ['$http', '$q'];
    function customersService($http, $q) {
        var factory = {};
        var serviceBase = '/api/dataservice/';

        factory.getCustomers = function (pageIndex, pageSize) {
            return getPagedResource('customers', pageIndex, pageSize);
        }

        factory.getCustomersSummary = function(pageIndex, pageSize) {
            return getPagedResource('customersSummary', pageIndex, pageSize);
        }

        factory.getStates = function () {
            return $http.get(serviceBase + 'states').then(function (response) {
                return response.data;
            });
        }

        factory.checkUniqueValue = function (id, property, value) {
            if (!id) {
                id = 0;
            }
            return $http.get(serviceBase + 'checkUnique/' + id + '?property=' + property + '&value=' + escape(value)).then(function (response) {
                return response.data.status;
            });
        }

        factory.insertCustomer = function (customer) {
            return $http.post(serviceBase + 'postCustomer', customer).then(function (response) {
                customer.id = response.data.id;
                return response.data;
            });
        }

        factory.newCustomer = function () {
            return $q.when({ id: 0 });
        }

        factory.updateCustomer = function (customer) {
            return $http.put(serviceBase + 'putCustomer/' + customer.id, customer).then(function (response) {
                return response.data;
            });
        }

        factory.deleteCustomer = function (id) {
            return $http.delete(serviceBase + 'deleteCustomer' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCustomer = function (id) {
            return $http.get(serviceBase + 'customerById/' + id).then(function (response) {
                extendCustomers(response.data);
                return response.data;
            });
        }

        function extendCustomers(customers) {
            for (var i = 0; i < customers.length; i++) {
                var cust = customers[i];
                if (!cust.orders) {
                    continue;
                }
                for (var j = 0; j < cust.orders.length; j++) {
                    var order = cust.orders[j];
                    order.orderTotal = order.quantity * order.price;
                }
                cust.ordersTotal = ordersTotal(cust);
            }
        }

        function getPagedResource(baseResource, pageIndex, pageSize) {
            var resource = baseResource;
            resource += (arguments.length == 3) ? buildPagingUrl(pageIndex, pageSize) : '';
            return $http.get(serviceBase + resource).then(function (response) {
                var custs = response.data;
                extendCustomers(custs);
                return {
                    totalRecoreds: parseInt(response.headers('X-InlineCount')),
                    results: custs
                };
            });
        }

        function buildPagingUrl(pageIndex, pageSize) {
            var uri = '?$top=' + pageSize + '&$skip=' + (pageIndex * pageSize);
            return uri;
        }

        function ordersTotal(order) {
            return order.quantity * order.price;
        }

        function ordersTotal() {
            var total = 0;
            var orders = customer.orders;
            var count = orders.length;
            for (var i = 0; i < count; i++) {
                total += orders[i].orderTotal;
            }
        }

        return factory;
    }
})();