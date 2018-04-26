app.controller('patientDetailController',['$scope','$http',function($scope, $http) {
    $scope.patient = null;
    var patientId = window.config.patientId;
    $scope.exam = null;
    $scope.showExaminationResult = function(examId) {
        $http.get('/doctor/GetExaminationDetail?examId=' + examId).then(function(response) {
            if (response.data.isSuccess) {
                $scope.exam = response.data.value;
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