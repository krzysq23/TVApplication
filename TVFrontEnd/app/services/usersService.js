'use strict';
app.factory('usersService', ['$http', 'localStorageService', function ($http, localStorageService) {

    var serviceBase = 'http://localhost:7620/';
    var friendsServiceFactory = {};

    var _getUserList = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            return $http.get(serviceBase + "api/users/list?userName=" + authData.userName).then(function (results) {
                return results;
            });
        }
    };

    var _getUserFriends = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            return $http.get(serviceBase + "api/users/userfriends?userName=" + authData.userName).then(function (results) {
                return results;
            });
        }
    };

    var _addToFriends = function (user) {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            var data = {
                userName: authData.userName,
                userFriendId: user.id
            };
            return $http.post(serviceBase + "api/users/addtofirends", data).then(function (results) {
                return results;
            });
        }
    };

    var _deleteFriend = function (user) {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            var data = {
                userName: authData.userName,
                userFriendId: user.id
            };
            return $http.post(serviceBase + "api/users/deleteFriend", data).then(function (results) {
                return results;
            });
        }
    };

    friendsServiceFactory.getUserList = _getUserList;
    friendsServiceFactory.getUserFriends = _getUserFriends;
    friendsServiceFactory.addToFriends = _addToFriends; 
    friendsServiceFactory.deleteFriend = _deleteFriend;

    return friendsServiceFactory;

}]);