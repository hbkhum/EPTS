
app.controller('testAddEditController', function ($scope, items, addEdit, businessunitFactory, close) {
    var currentItems = {};
    $scope.test = items;
    $scope.testEditmode = addEdit;
    $scope.selectedItem = $scope.test.BusinessUnit;
    angular.copy($scope.test, currentItems);

    //get all businessunit
    $scope.GetBusinessUnits = function () {
        businessunitFactory.GetBusinessUnits()
            .then(function (reponse) {
                $scope.businessunits = reponse.result;
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };
    $scope.close = function (result) {
        if (result === false) { angular.copy( currentItems,$scope.test); }
        close(result, 500);
    };
    $scope.update = function(item) {
        $scope.selectedItem = item;
        $scope.test.BusinessUnitId = item.BusinessUnitId;
        if ($scope.testEditmode !== false) {
            angular.copy(item, $scope.test.BusinessUnit);
            //$scope.test.BusinessUnit.BusinessUnitId = item.BusinessUnitId;
            //$scope.test.BusinessUnit.BusinessUnitName = item.BusinessUnitName;
        }
    };
    $scope.GetBusinessUnits();
});

app.controller('testDeleteController', function ($scope, item, close) {
    $scope.test = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/
app.controller('testPaginationController', function ($scope) {
    $scope.pageChangeHandler = function (num) {
        console.log('going to page ' + num);
    };
});


app.controller('testIndexController', function postController($scope, testFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var testHub = connection.createHubProxy('TestHub');
    $scope.tests = [];
    $scope.test_editmode = false;
    $scope.currentPage = 1;
    $scope.pageSize = 10;

    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };


    /***************************************************************************
    *
    * Signalr client functions
    *
    ***************************************************************************/
    //testHub.on("AddTest", function (item) {
    //    $scope.tests.unshift(item);
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //testHub.on("UpdateTest", function (item) {
    //    var index = db.searchIndex($scope.tests, "TestId", item.TestId);
    //    $scope.tests[index].TestName = item.TestName;
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //testHub.on("DeleteTest", function (item) {
    //    var index = db.searchIndex($scope.tests, "TestId", item.TestId);
    //    $scope.tests.splice(index, 1);
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //connection.start();
    $scope.$on('TestGroupId_click', function (e, id) {
        $scope.isCollapsed = $scope.isCollapsed === 0 ? true : false;
        $scope.TestPlanId = id;
        if ($scope.isCollapsed === false) {
            testFactory.GetTestGroupByTestPlanId(id)
                .then(function (reponse) {
                    $scope.tests = reponse.result;
                    $scope.totalItems = $scope.tests.length;

                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
                });
        }
    });

    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get test
    $scope.GetTest = function (currentTest) {
        testFactory.GetTest(currentTest)
            .then(function (response) {
                angular.copy(response, currentTest);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading test! ' + err.ExceptionInformation);
            });
    };

    //get all test
    $scope.GetTests = function () {
        testFactory.GetTests()
            .then(function (reponse) {
                $scope.tests = reponse.result;
                $scope.totalItems = $scope.tests.length;
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add test
    $scope.AddTest = function (currentTest) {
        if (currentTest != null) {
            testFactory.AddTest(currentTest)
                .then(function (reponse) {
                    currentTest.TestId = reponse;
                    $scope.test = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> test ' + currentTest.TestId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding test! ' + error.ExceptionInformation);
                });
        }
    };

    //update test
    $scope.UpdateTest = function (currentTest) {
        testFactory.UpdateTest(currentTest)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> test ' + currentTest.TestId + ' has been updated.');
                }
            }, function (error) {
                $scope.TestCancel(currentTest);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating test! ' + error.ExceptionInformation);
            });
    };

    // delete test
    $scope.DeleteTest = function (currentTest) {
        testFactory.DeleteTest(currentTest)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> test ' + currentTest.TestId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting test! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit test
    $scope.TestEdit = function (childScope, currenttest) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Test/TestAddEdit.html",
            controller: "testAddEditController",
            inputs: {
                items: currenttest,
                addEdit: true
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.UpdateTest(modal.scope.test);
                } else {
                    $scope.TestCancel(modal.scope.test);
                }
            });
        });
    };


    $scope.TestShowAdd = function () {
        var test = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Test/TestAddEdit.html",
            controller: "testAddEditController",
            inputs: {
                items: test,
                addEdit: false
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.AddTest(modal.scope.test);
                } else {
                    modal.scope.test = null;
                }
            });
        });
    };

    $scope.TestShowConfirm = function (currenttest) {
        var data = currenttest;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Test/TestDelete.html",
            controller: "testDeleteController",
            inputs: {
                item: currenttest
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteTest(modal.scope.test);
                }
            });
        });
    };

    $scope.TestCancel = function (currenttest) {
        $scope.GetTest(currenttest);
    }
    //var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    //menu.removeClass('active');
    //var submenu = menu.find("a:contains('Familes')");
    //submenu.click();
    //$scope.GetTests();
    //$scope.orderByField = 'TestName';
    //$scope.reverseSort = false;
});





