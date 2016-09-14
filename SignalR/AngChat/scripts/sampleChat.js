(function () {
    var app = angular.module('chatApp', []);
    app.controller('chatController', chatController);
    chatController.$inject = ['$scope'];
    function chatController($scope) {
        $scope.name = 'default name';
        $scope.message = '';
        $scope.messages = [];
        $scope.chatHub = null;

        $scope.chatHub = $.connection.SimpleChatHub; // init hub
        $.connection.hub.start(); // start hub

        // register a client method on hub to be invoked by the server
        $scope.chatHub.client.broadcastMessage = function (name, message) {
            var newMessage = name + ' says: ' + message;

            $scope.messages.push(newMessage);
            $scope.$apply();
        };

        // click event
        $scope.send = function () {
            // sends a new message to the server
            $scope.chatHub.server.sendMessage($scope.name, $scope.message);
            $scope.message = '';
        };
    }
})();