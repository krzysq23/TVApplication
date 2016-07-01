var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'cgNotify']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/main", {
        controller: "mainController",
        templateUrl: "/app/views/main.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    $routeProvider.when("/movies", {
        controller: "moviesController",
        templateUrl: "/app/views/movies.html"
    });

    $routeProvider.when("/users", {
        controller: "usersController",
        templateUrl: "/app/views/users.html"
    });

    $routeProvider.when("/friends", {
        controller: "friendsController",
        templateUrl: "/app/views/friends.html"
    });

    $routeProvider.when("/editprofile", {
        controller: "editprofileController",
        templateUrl: "/app/views/editprofile.html"
    });

    $routeProvider.when("/favoritemovies", {
        controller: "favoritemoviesController",
        templateUrl: "/app/views/favoritemovies.html"
    });

    $routeProvider.when("/notifications", {
        controller: "notificationsController",
        templateUrl: "/app/views/notifications.html"
    });
    
    $routeProvider.when("/comments/:title", {
        controller: "commentsController",
        templateUrl: "/app/views/comments.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

var serviceBase = 'http://localhost:7620/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

app.config(function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];

    $httpProvider.interceptors.push('authInterceptorService');
});