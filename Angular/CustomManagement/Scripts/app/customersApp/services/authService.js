(function () {
    'use strict';
    angular.module('customersApp').factory('authService', authService);
    authService.$inject = ['$http', '$rootScope'];
    function authService($http, $rootScope) {
        var baseUrl = 'api/dataservice';
        var factory = {
            loginPath: '/login',
            user: {
                isAuthenticated: false,
                roles: null
            }
        };

        factory.login = function (email, password) {

        }

        factory.logout = function () {
            //return $http.post


        }

        return factory;
    }

})()