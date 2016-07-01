'use strict';
app.controller('usersController', ['$scope', '$location', '$route', 'usersService', function ($scope, $location, $route, usersService) {

    $scope.users = [];

    usersService.getUserList().then(function (results) {

        $scope.users = results.data;

    }, function (error) {
        alert(error.data.message);
    });

    $scope.addToFriends = function (user) {

        usersService.addToFriends(user).then(function (results) {

            alert(user.lastName + " add to friends");
            $route.reload();

        }, function (error) {
            alert(error.data.message);
        })
    };

}]);