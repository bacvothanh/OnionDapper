var app;
app = angular.module('appModule', ['angular-loading-bar']);

app.constant('appSettings', {

});

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);