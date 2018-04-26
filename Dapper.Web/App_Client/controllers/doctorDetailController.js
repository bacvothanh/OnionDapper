app.controller('doctorDetailController',['$scope','$http',function($scope, $http) {
    $scope.doctor = null;
    var doctorId = window.config.doctorId;
    //$scope.doc = null;
    $scope.showDoctorDetail = function (doctorId) {
        $http.get('/doctor/GetDoctorDetail?doctorId=' + doctorId).then(function(response) {
            if (response.data.isSuccess) {
                $scope.doctor = response.data.value;
                $scope.showModal('#myModal');
            }
        });
    }

    $scope.showModal =function(id) {
        $(id).addClass('show');
    }

    $scope.closeModal=function(id) {
        $(id).removeClass('show');
    }

    function init() {
        
    }

    init();
}])