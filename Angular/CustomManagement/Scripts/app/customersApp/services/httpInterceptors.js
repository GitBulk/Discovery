


(function () {
    'use strict';

    angular.module('customersApp').config(['$httpProvider', function ($httpProvider) {

        var httpInterceptor401 = function ($q, $rootScope) {
            function sucess(response) {
                return response;
            }

            function error(res) {
                if (res.status === 401) {
                    //Raise event so listener (navbarController) can act on it
                    $rootScope.$broadcase('redirectToLogin', null);
                    return $q.reject(res);
                }
                return $q.reject(res);
            }

            return function (promise) {
                return promise.then(sucess, error);
            }
        }
        httpInterceptor401.$inject = ['$q', '$rootScope'];

        $httpProvider.interceptors.push(httpInterceptor401);
    }]);

})();