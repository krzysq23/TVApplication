'use strict';
app.factory('notificationsService', ['$http', 'localStorageService', function ($http, localStorageService) {

    var serviceBase = 'http://localhost:7620/';
    var moviesServiceFactory = {};

    var _getNotifications = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            return $http.get(serviceBase + "api/notifications/get?userName=" + authData.userName).then(function (results) {
                return results;
            });
        }
    }; 

    moviesServiceFactory.getNotifications = _getNotifications;

    return moviesServiceFactory;

}]);