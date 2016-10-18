(function () {
    'use strict';
    var app = angular.module('notesApp', ['ngAnimate', 'ngResource', 'ngCookies', 'ui.router', 'ui.bootstrap']);
    app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        // for any unmatched url redirect to main url
        $urlRouterProvider.otherwise('/');
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
        $stateProvider.state('initial', {
            url: '/',
            views: {
                'main': {
                    templateUrl: 'app/notes/notes.view.html',
                    controller: 'notesController as vm'
                }
            }
        });
    });
})();