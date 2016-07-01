'use strict';
app.controller('commentsController', ['$scope', '$routeParams', 'moviesService', function ($scope, $routeParams, moviesService) {

    $scope.comments = [];
    var title = $routeParams.title;
    moviesService.getComments(title).then(function (results) {

        $scope.comments = results.data;

    }, function (error) {
        alert(error.data.message);
    });
}]);