(function () {
    'user strict'
    app.controller('chatController', chatController);
    chatController.$inject = ['$scope', '$rootScope', 'chatFactory'];
    function chatController($scope, $rootScope, chatFactory) {
        $scope.$parent.userName = '';
        $scope.rooms = [];
        $scope.$parent.userName = $('h4#userName').text();
        chatFactory.startHub();
        $scope.activeRoom = '';
        $scope.chatHistory = [];
        $scope.users = [];
        $scope.roomsLoggedIn = [];
        $scope.typemsgdiable = true;
        $scope.onlineUsers = [];
        $scope.userTyping = '';
        $scope.userInPrivateChat = null;
        $scope.showPrivateWindow = false;
        $scope.privateMessages = [];

        $scope.keyPress = function (e) {
            if (e.which == 13) {
                $scope.sendPrivateMessage();
                $scope.userTyping = ''
            } else if (e.which == 46 || e.which == 8) {
                chatFactory.userTyping($scope.userInPrivateChat.connectionId, 'Deleting...');
                window.setTimeout(function () {
                    $scope.userTyping = '';
                }, 500)
            } else {
                chatFactory.userTyping($scope.userInPrivateChat.connectionId, 'Typing...');
                window.setTimeout(function () {
                    $scope.userTyping = '';
                }, 500);
            }
        }

        $scope.privateMessage = function (user) {
            $scope.showPrivateWindow = true;
            $scope.userInPrivateChat = user;
            console.log($scope.onlineUsers);
            //$scope.$apply();
        }

        $scope.changeStatus = function (status) {
            chatFactory.updateStatus(status);
        }

        $scope.sendPrivateMessage = function () {
            chatFactory.sendPrivateMessage($scope.userInPrivateChat.connectionId, $scope.pvtmessage)
            $scope.pvtmessage = '';
        }

        $scope.closePrivateWindow = function () {
            $scope.showPrivateWindow = false;
        }

        chatFactory.userEntered(function (room, user, cid) {
            if ($scope.activeRoom == room && user !== '') {
                var result = $.grep($scope.users, function (e) {
                    return e.name == user;
                });
                console.log('------------');
                console.log(result);
                if (result != undefined || result != null) {
                    $scope.users.push({ name: user, connectionId: cid });
                    $scope.$apply();
                }
            }
        });

        chatFactory.userLoggedOut(function (room, user) {
            if ($scope.activeRoom == room && user != '') {
                $scope.users == $scope.users.filter(function (themObjects) {
                    return themObjects != user;
                });
                $scope.$apply();
            }
        });

        chatFactory.login($scope.$parent.userName);

        chatFactory.getOnlineUsers(function (onlineUsers) {
            $scope.onlineUsers = $.parseJSON(onlineUsers);
            console.log($scope.onlineUsers);
            $scope.$apply();
        });

        chatFactory.newOfflineUser(function (user) {
            $.each($scope.onlineUsers, function (index) {
                if ($scope.onlineUsers[index].name === user.name
                    && $scope.onlineUsers[index].connectionId === user.connectionId) {
                    $scope.onlineUsers.splice(index, 1);
                    var message = '<strong>' + user.name + '</strong> left the chat';
                    console.log(message);
                    //alert(message);
                }
            });
            $scope.$apply();
        });

        chatFactory.newOnlineUser(function (user) {
            $scope.onlineUsers.push(user);
            $scope.$apply();
            var message = '<strong>' + user.name + '</strong> is online.';
            console.log(message);
            //alert(message);
        });

        chatFactory.statusChanged(function (connectionId, status) {
            $.each($scope.onlineUsers, function (index) {
                if ($scope.onlineUsers[index].connectionId === connectionId) {
                    $scope.onlineUsers[index].status = status;
                }
            });
            $scope.$apply();
        });

        chatFactory.isTyping(function (connectionId, message) {
            if ($scope.userInPrivateChat.connectionId && $scope.userInPrivateChat.connectionId == connectionId) {
                $scope.userTyping = message;
            } else {
                $scope.userTyping = '';
            }
            $scope.$apply();
            window.setTimeout(function () {
                $scope.userTyping = '';
                $scope.$apply();
            }, 500);
        });

        chatFactory.recievingPrivateMessage(function (connectionId, name, message) {
            if ($scope.showPrivateWindow == false) {
                $scope.showPrivateWindow = true;
            }


            $scope.privateMessages.push({ to: connectionId, from: name, message: message });

            if ($scope.$parent.userName !== name) {
                if ($scope.userInPrivateChat == null) {
                    $scope.userInPrivateChat = { name: name, connectionId: connectionId };
                }
            }

            // to scroll the message window
            if ($('#PrivateChatArea div.panel-body').length == 1) {
                var $container = $('#PrivateChatArea div.panel-body');
                $container[0].scroolTop = $container[0].scrollHeight;
                $container.animate({ scrollTop: $container[0].scrollHeight }, 'fast');
            }

            $scope.$evalAsync();
        });
    }


})();