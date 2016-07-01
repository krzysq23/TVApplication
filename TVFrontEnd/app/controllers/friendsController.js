'use strict';
app.controller('friendsController', ['$scope', '$location', 'usersService', function ($scope, $location, usersService) {

    $scope.users = [];

    usersService.getUserFriends().then(function (results) {

        $scope.users = results.data;

    }, function (error) {
        alert(error.data.message);
    });

    $scope.deleteFriend = function (user) {

        usersService.deleteFriend(user).then(function (results) {

            alert(user.lastName + " remove");

        }, function (error) {
            alert(error.data.message);
        })
    };

}]);