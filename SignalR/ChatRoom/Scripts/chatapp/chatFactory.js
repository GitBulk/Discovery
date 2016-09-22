(function () {
    'user strict'
    app.factory('chatFactory', chatFactory);
    chatFactory.$inject = ['$rootScope'];
    function chatFactory($rootScope) {
        var $hub = $.connection.chatHub;
        var connection = null;

        function startHub() {
            console.log('start hub');
            connection = $.connection.hub.start();
        }


        // SERVER METHODS --------------------------------------------
        function login(username) {
            console.log("login with: " + username);
            connection.done(function () {
                $hub.server.login(username);
            });
        }

        function sendPrivateMessage(toUser, message) {
            //connection.server.sendPrivateMessage(toUser, message);
            connection.done(function () {
                $hub.server.sendPrivateMessage(toUser, message);
            });
        }

        function updateStatus(status) {
            connection.done(function () {
                $hub.server.updateStatus(status);
            })
        }

        function userTyping(connectionId, message) {
            console.log('Connection id: ' + connectionId);
            connection.done(function () {
                $hub.server.userTyping(connectionId, message);
            });
        }

        // CLIENT METHODS
        function joinRoom(callback) {
            $hub.client.joinRoom = callback;
        }

        function userEntered(callback) {
            $hub.client.userEntered = callback;
        }

        function userLoggedOut(callback) {
            $hub.client.userLoggedOut = callback;
        }

        function recievingPrivateMessage(callback) {
            $hub.client.recievingPrivateMessage = callback;
        }

        function getOnlineUsers(callback) {
            $hub.client.getOnlineUsers = callback;
        }

        function newOnlineUser(callback) {
            $hub.client.newOnlineUser = callback;
        }

        function newOfflineUser(callback) {
            $hub.client.newOfflineUser = callback;
        }

        function statusChanged(callback) {
            $hub.client.statusChanged = callback;
        }

        function isTyping(callback) {
            $hub.client.isTyping = callback;
        }


        var factory = {
            startHub: startHub,
            login: login,
            sendPrivateMessage: sendPrivateMessage,
            updateStatus: updateStatus,
            userTyping: userTyping,
            joinRoom: joinRoom,
            userEntered: userEntered,
            userLoggedOut: userLoggedOut,
            recievingPrivateMessage: recievingPrivateMessage,
            getOnlineUsers: getOnlineUsers,
            newOnlineUser: newOnlineUser,
            newOfflineUser: newOfflineUser,
            statusChanged: statusChanged,
            isTyping: isTyping
        }

        return factory;
    }

})();