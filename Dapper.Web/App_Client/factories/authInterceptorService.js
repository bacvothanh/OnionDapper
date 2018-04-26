'use strict';
app.factory('authInterceptorService', ['$q', function ($q) {
    var authInterceptorServiceFactory = {};

    var response = function (rejection) {
        var deferred = $q.defer();
        if (rejection.data.isSuccess === false) {
            if (rejection.data.errors) {
                $.each(rejection.data.errors,
                    function (index, error) {
                        $.toast({
                            heading: 'Lỗi',
                            text: error,
                            position: 'top-right',
                            loaderBg: '#ff6849',
                            icon: 'error',
                            hideAfter: 3500
                        });
                    });
            }
        }

        deferred.resolve(rejection);
        return deferred.promise;
    }

    authInterceptorServiceFactory.response = response;

    return authInterceptorServiceFactory;
}]);