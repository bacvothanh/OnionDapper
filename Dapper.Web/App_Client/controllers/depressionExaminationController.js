app.controller('depressionExaminationController',['$scope','$http',function($scope,$http) {
    $scope.examResult = null;
    $scope.submitExam = submitExamination;
    $scope.questions = window.config.questions;
    $scope.indexOfSickTime = 11;
    $scope.totalCheckedQuestion = 0;
    $scope.mainCheckedQuestion = 0;
    $scope.$watch('questions',
        function(newValue, oldValue) {
            $scope.totalCheckedQuestion = 0;
            $scope.mainCheckedQuestion = 0;
            $.each($scope.questions, function(index, question) {
                if (question.isChecked && question.isChecked === true) {
                    $scope.totalCheckedQuestion += 1;
                    if (question.level >= 10) {
                        $scope.mainCheckedQuestion += 1;
                    }
                }
            });
        },
        true);

    $scope.bookAppointment = function () {
        var data = {
            questions: $scope.questions,
            result: $scope.examResult.message
        };

        $http.post(window.config.submitExamResult, data).then(function (response) {
            if (response.data.isSuccess) {
                window.location.href = window.config.bookAppointmentUrl;
            }
        });
    }

    function submitExamination() {
        var values = [];
        $.each($scope.questions, function(index, question) {
            if (question.isChecked && question.isChecked === true) {
                values.push(question.id);
            }
        });

        $http.post('/patient/DepressionExamination', { result: values }).then(function(response) {
            if (response.data.isSuccess) {
                $scope.examResult = response.data.value;
            }
        });

    }
}])