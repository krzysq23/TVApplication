'use strict';
app.controller('editprofileController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.user = [];

    authService.getAppUser().then(function (results) {

        $scope.user = results.data;

    }, function (error) {
        alert(error.data.message);
    });

    $scope.update = function () {

        authService.updateAppUser($scope.user).then(function (results) {

            $scope.savedSuccessfully = true;
            $scope.message = "User has been update successfully";

        }, function (error) {
            $scope.message = "Failed to update user : " + err.error_description;;
        })
    };

}]);