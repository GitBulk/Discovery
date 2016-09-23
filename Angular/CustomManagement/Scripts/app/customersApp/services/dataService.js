(function () {
    'use strict';

    angular.module('customersApp').factory('dataService', dataService);
    dataService.$inject = ['config', 'customersService', 'customersBreezeService']
    function dataService(config, customersService, customersBreezeService) {
        return (config.useBreeze) ? customersBreezeService : customersService;
    };

})();