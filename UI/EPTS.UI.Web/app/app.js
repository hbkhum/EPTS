//var app = angular.module("app", ['ngRoute', 'angularMoment', /*'ngAnimate',*/ 'ui.bootstrap', 'angularUtils.directives.dirPagination', 'LocalStorageModule', 'angular-loading-bar']);
var app = angular.module("app", ['ngRoute', 'angularMoment',  'ui.bootstrap',  'LocalStorageModule', 'angular-loading-bar', 'angularModalService'], function () {

});

app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);

app.config(function ($routeProvider) {
    $routeProvider

        //Login
        .when('/home',
        {
            controller: "homeController",
            templateUrl: "app/views/home.html"
        })
        .when('/login',
        {
            controller: "loginController",
            templateUrl: "app/views/login.html"
        })
        .when('/signup',
        {
            controller: "signupController",
            templateUrl: "app/views/signup.html"
        })

		.when('/BusinessUnit',
        {
            controller: 'businessunitIndexController',
            templateUrl: 'app/views/catalogs/BusinessUnit/BusinessUnitIndex.html'
        })
        .when('/Family',
        {
            controller: 'familyIndexController',
            templateUrl: 'app/views/catalogs/Family/FamilyIndex.html'
        })
        .when('/Model',
        {
            controller: 'modelIndexController',
            templateUrl: 'app/views/catalogs/Model/ModelIndex.html'
        })
		.when('/TestPlan',
        {
            controller: 'testplanIndexController',
            templateUrl: 'app/views/catalogs/TestPlan/TestPlanIndex.html'
        })
    .otherwise({ redirectTo: "/home" });
});


app.factory('db', function (ModalService) {
    return {
        InformationMessageSuccess: function (html) {
            ModalService.showModal({
                templateUrl: "app/views/commun/ModalSucess.html",
                controller: "ModalSucessController"
            }).then(function (modal) {
                modal.scope.getHtml(html);
                modal.element.modal();
                modal.close.then(function (result) {
                });
            });
        },
        InformationMessageDanger: function (html) {
            ModalService.showModal({
                templateUrl: "app/views/commun/ModalDanger.html",
                controller: "ModalDangerController"
            }).then(function (modal) {
                modal.scope.getHtml(html);
                modal.element.modal();
                modal.close.then(function (result) {
                });
            });
        },

        InformationMessageWarning: function (html) {
            ModalService.showModal({
                templateUrl: "app/views/commun/ModalWarning.html",
                controller: "ModalWarningController"
            }).then(function (modal) {
                modal.scope.getHtml(html);
                modal.element.modal();
                modal.close.then(function (result) {
                });
            });
        },

        searchIndex: function (arraytosearch, key, valuetosearch) {

            for (var i = 0; i < arraytosearch.length; i++) {

                if (arraytosearch[i][key] === valuetosearch) {
                    return i;
                }
            }
            return null;
        }
    };
});


app.run(['authService', function ($rootScope) {

}]);

app.controller('ModalSucessController', function ($scope, $sce, close) {
    $scope.html = null;
    $scope.close = function (result) {
        close(result, 500);
    };
    $scope.getHtml = function (html) {
        $scope.html = $sce.trustAsHtml(html);
    };
});

app.controller('ModalDangerController', function ($scope, $sce, close) {
    $scope.html = null;
    $scope.close = function (result) {
        close(result, 500);
    };
    $scope.getHtml = function (html) {
        $scope.html = $sce.trustAsHtml(html);
    };
});


app.controller('ModalWarningController', function ($scope, $sce, close) {
    $scope.html = null;
    $scope.close = function (result) {
        close(result, 500);
    };
    $scope.getHtml = function (html) {
        $scope.html = $sce.trustAsHtml(html);
    };
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});


app.directive('families', function () {
    return {
        controller: 'familyIndexController',
        templateUrl: 'app/views/Products/Family/FamilyIndex.html',
        scope: {
            businessunitid: '='
        }
    }
});

app.directive('businessunits', function () {
    return {
        controller: 'businessunitIndexController',
        templateUrl: 'app/views/Products/BusinessUnit/BusinessUnitIndex.html'
    }
});



app.directive('models', function () {
    return {
        controller: 'modelIndexController',
        templateUrl: 'app/views/Products/Model/ModelIndex.html',
        scope: {
            familyid: '='
        }
    }
});


app.directive('partnumber', function () {
    return {
        controller: 'partnumberIndexController',
        templateUrl: 'app/views/catalogs/PartNumber/PartNumberIndex.html',
        scope: {
            modelid: '='
        }
    }
});

app.directive('testgroup', function () {
    return {
        controller: 'testgroupIndexController',
        templateUrl: 'app/views/catalogs/TestGroup/TestGroupIndex.html',
        scope: {
            modelid: '='
        }
    }
});

app.directive('test', function () {
    return {
        controller: 'testIndexController',
        templateUrl: 'app/views/catalogs/Test/TestIndex.html',
        scope: {
            modelid: '='
        }
    }
});










