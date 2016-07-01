'use strict';
app.controller('favoritemoviesController', ['$scope', '$location', '$route', 'moviesService', function ($scope, $location, $route, moviesService) {

    $scope.movies = [];

    moviesService.getUserFavoritMovies().then(function (results) {

        $scope.movies = results.data;
        
    }, function (error) {
        alert(error.data.message);
    });

    $scope.Delete = function (movie) {

        moviesService.deleteMovie(movie).then(function (results) {

            alert(movie.title + " is delated");
            $route.reload();

        }, function (error) {
            alert(error.data.message);
        });
    };

}]);