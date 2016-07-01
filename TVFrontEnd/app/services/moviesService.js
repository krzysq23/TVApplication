'use strict';
app.factory('moviesService', ['$http', 'localStorageService', function ($http, localStorageService) {

    var serviceBase = 'http://localhost:7620/';
    var moviesServiceFactory = {};
    var movieRec = {
        title: "",
        year: "",
        userName: ""
    };

    var _getPopularMovies = function () {
        return $http.get(serviceBase + "api/movies/popular").then(function (results) {
            return results;
        });
    }; 

    var _getUserFavoritMovies = function () {
        
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            return $http.get(serviceBase + "api/movies/favorit?userName=" + authData.userName).then(function (results) {
                return results;
            });
        }
    };

    var _getComments = function (title) {

        return $http.get(serviceBase + "api/movies/comments?title=" + title).then(function (results) {
            return results;
        });
    }; 

    var _addToFavourit = function (movie) {

        var authData = localStorageService.get('authorizationData');
        if (authData) {

            movieRec.title = movie.title;
            movieRec.year = movie.year;
            movieRec.userName = authData.userName;
        }

        return $http.post(serviceBase + "api/movies/add", movieRec).then(function (results) {
            return results;
        });
    };

    var _deleteMovie = function (movie) {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            var data = {
                title : movie.title,
                userName : authData.userName
            };
            return $http.post(serviceBase + "api/movies/deleteMovie", data).then(function (results) {
                return results;
            });
        }
    };

    moviesServiceFactory.getPopularMovies = _getPopularMovies;
    moviesServiceFactory.getComments = _getComments;
    moviesServiceFactory.addToFavourit = _addToFavourit; 
    moviesServiceFactory.getUserFavoritMovies = _getUserFavoritMovies;
    moviesServiceFactory.deleteMovie = _deleteMovie;

    return moviesServiceFactory;

}]);