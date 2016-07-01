'use strict';
app.factory('signalRHubService', ['$rootScope', 'localStorageService', '$filter', 'notify', function ($rootScope, localStorageService, $filter, notify) {

    if (!String.prototype.supplant) {
        String.prototype.supplant = function (o) {
            return this.replace(/{([^{}]*)}/g,
                function (a, b) {
                    var r = o[b];
                    return typeof r === 'string' || typeof r === 'number' ? r : a;
                }
            );
        };
    }


    function signalRHubService() {

        var conn = $.connection.observableNotifier;

        var connection = $.hubConnection("http://localhost:7620");
        $.connection.hub.url = "http://localhost:7620/signalr";
        
        var proxy = connection.createHubProxy("observableNotifier");

        $.extend(conn.client, {
            addCommentNotif: function (notif) {
                console.log(notif);
                var date = $filter('date')(notif.DateCreated, "yyyy-MM-dd");
                notify({
                    message: notif.Description + " " + date,
                    //url: "/app/views/notifications.html",
                    position: 'right',
                    duration: 10000
                });
            },
        });

        $.connection.hub.start().done();

        return {
            on: function (eventName, callback) {
                connection.start().done(function () {
                    proxy.on(eventName, function (result) {
                            $rootScope.$apply(function () {
                                if (callback) {
                                    callback(result);
                                }
                            });
                        });
                })
            },
            off: function (eventName, callback) {
                proxy.off(eventName, function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            },
            invoke: function (methodName, callback) {
                proxy.invoke(methodName)
                    .done(function (result) {
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
            },
            connection: connection
        };
    }
    

    return signalRHubService;

}]);