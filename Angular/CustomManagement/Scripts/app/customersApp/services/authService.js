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
            return $http.post(baseUrl + 'login', { userLogin: { userName: email, password: password } }).then(function (response) {
                var loggedIn = response.data.status;
                changeAuth(loggedIn);
                return loggedIn;
            });
        }

        factory.logout = function () {
            //return $http.post
            return $http.post(baseUrl + 'logout').then(function (response) {
                var loggedIn = !response.data.status;
                changeAuth(loggedIn);
                return loggedIn;
            });
        }

        factory.redirectToLogin = function () {
            $rootScope.$broadcast('redirectToLogin', null);
        }

        function changeAuth(loggedIn) {
            factory.user.isAuthenticated = loggedIn;
            $rootScope.$broadcast('loginStatusChanged', loggedIn);
        }

        return factory;
    }

})()