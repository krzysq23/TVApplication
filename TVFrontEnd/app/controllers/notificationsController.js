'use strict';
app.controller('notificationsController', ['$scope', '$location', 'notificationsService', function ($scope, $location, notificationsService) {

    $scope.notifications = [];

    notificationsService.getNotifications().then(function (results) {

        $scope.notifications = results.data;

    }, function (error) {
        alert(error.data.message);
    });
}]);