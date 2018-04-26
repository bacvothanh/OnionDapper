app.controller('depressionTreatmentController', ['$scope', '$http', '$timeout', function ($scope, $http, $timeout) {
    $scope.exam = window.config.exam;
    $scope.questions = window.config.questions;
    $scope.pivots = window.config.pivots;
    $scope.specialistId = window.config.specialistId;
    $scope.numberOfSymptom = 0;
    $scope.resultSymptom = '';
    $scope.numberOfDepression = 0;
    $scope.numberOfPsychotic = 0;
    $scope.tabIndex = 0;
    $scope.handleResultTreatment = function() {
        $scope.numberOfSymptom = 0;
        $.each($scope.questions, function(index, question) {
            if (question.isChecked === true) {
                $scope.numberOfSymptom += 1;
            }
        });

        if ($scope.numberOfSymptom === 0) {
            $scope.resultSymptom = 'Thuyên giảm';
            $scope.numberOfPsychotic = 0;
            $scope.numberOfDepression = 0;
        }
        else if ($scope.numberOfSymptom < 4) {
            $scope.resultSymptom = 'Thuyên giảm một phần';
            $scope.numberOfDepression = 1;
            $scope.numberOfPsychotic = 0;
        } else if ($scope.numberOfSymptom >= 4) {
            $scope.resultSymptom = 'Không thuyên giảm';
            $scope.numberOfDepression = 2;
            $scope.numberOfPsychotic = 1;
        }
    }

    $scope.next = function() {
        $scope.tabIndex += 1;
    }

    $scope.previous= function() {
        $scope.tabIndex -= 1;
    }

    $scope.setTabIndex = function(index) {
        $scope.tabIndex = index;
    }

    $scope.updateResult =function() {
        var url = '/doctor/GetTreatmentResult';
        var model = {
            questions: $scope.questions,
            specialistId: $scope.specialistId
        };

        $http.post(url, model).then(function(response) {
            if (response.data.isSuccess) {
                $scope.pivots = response.data.value;
                $scope.handleResultTreatment();
            }
        });
    }

    function init() {
        $scope.handleResultTreatment();
        console.log($scope.pivots);
    }

    $timeout(init);

}])