'use strict';
app.controller('SignalRHubController', ['$scope', 'signalRHubService', 'notify', function ($scope, signalRHubService, notify) {

      console.log('trying to connect to service')
      var SignalRHub = signalRHubService();
      console.log('connected to service')
      
  }
]);