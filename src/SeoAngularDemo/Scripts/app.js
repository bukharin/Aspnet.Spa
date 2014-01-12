var app = angular.module('app', ['ngRoute']);
var _routeTable = {
    defaultRoutePath: '/',
    rootPath: '/views/',
    routes: {
        '/': {
            templateUrl: 'home.html'
        },
        '/about': {
            templateUrl: 'about.html'
        }
    }
};

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true).hashPrefix('!');;

    //Route config

    angular.forEach(_routeTable.routes, function (route, path) {
        var routeConf = { template: route.template };
        if (route.templateUrl)
            routeConf['templateUrl'] = _routeTable.rootPath + route.templateUrl;
        $routeProvider.when(path, routeConf);
    });

    $routeProvider.otherwise({ redirectTo: _routeTable.defaultRoutePath });


}]);

/* CONTROLLERS  */

var HomeCtrl = function($scope, $http) {
    $scope.vm = this;

    this.items = [];

    $http.get('/api/test').then(function(response) {
        $scope.vm.items = response.data;
    });
};

var AboutCtrl = function($scope) {

};