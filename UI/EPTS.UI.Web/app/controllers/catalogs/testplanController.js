
app.controller('testplanAddEditController', function ($scope, items, addEdit, businessunitFactory, close) {
    var currentItems = {};
    $scope.testplan = items;
    $scope.testplanEditmode = addEdit;
    $scope.selectedItem = $scope.testplan.BusinessUnit;
    angular.copy($scope.testplan, currentItems);

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
        if (result === false) { angular.copy( currentItems,$scope.testplan); }
        close(result, 500);
    };
    $scope.update = function(item) {
        $scope.selectedItem = item;
        $scope.testplan.BusinessUnitId = item.BusinessUnitId;
        if ($scope.testplanEditmode !== false) {
            angular.copy(item, $scope.testplan.BusinessUnit);
            //$scope.testplan.BusinessUnit.BusinessUnitId = item.BusinessUnitId;
            //$scope.testplan.BusinessUnit.BusinessUnitName = item.BusinessUnitName;
        }
    };
    $scope.GetBusinessUnits();
});

app.controller('testplanDeleteController', function ($scope, item, close) {
    $scope.testplan = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/
app.controller('testplanPaginationController', function ($scope) {
    $scope.pageChangeHandler = function (num) {
        console.log('going to page ' + num);
    };
});


app.controller('testplanIndexController', function postController($scope, testplanFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var testplanHub = connection.createHubProxy('TestPlanHub');
    $scope.testplans = [];
    $scope.testplan_editmode = false;
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
    testplanHub.on("AddTestPlan", function (item) {
        $scope.testplans.unshift(item);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    testplanHub.on("UpdateTestPlan", function (item) {
        var index = db.searchIndex($scope.testplans, "TestPlanId", item.TestPlanId);
        $scope.testplans[index].TestPlanName = item.TestPlanName;
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    testplanHub.on("DeleteTestPlan", function (item) {
        var index = db.searchIndex($scope.testplans, "TestPlanId", item.TestPlanId);
        $scope.testplans.splice(index, 1);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    connection.start();
    $scope.collapse = function (event, childScope, id) {
        $(event.target).toggleClass("glyphicon-chevron-down");
        childScope.$broadcast('TestPlanId_click', id);
    };

    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get testplan
    $scope.GetTestPlan = function (currentTestPlan) {
        testplanFactory.GetTestPlan(currentTestPlan)
            .then(function (response) {
                angular.copy(response, currentTestPlan);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading testplan! ' + err.ExceptionInformation);
            });
    };

    //get all testplan
    $scope.GetTestPlans = function () {
        testplanFactory.GetTestPlans()
            .then(function (reponse) {
                $scope.testplans = reponse.result;
                $scope.totalItems = $scope.testplans.length;
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add testplan
    $scope.AddTestPlan = function (currentTestPlan) {
        if (currentTestPlan != null) {
            testplanFactory.AddTestPlan(currentTestPlan)
                .then(function (reponse) {
                    currentTestPlan.TestPlanId = reponse;
                    $scope.testplan = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> testplan ' + currentTestPlan.TestPlanId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding testplan! ' + error.ExceptionInformation);
                });
        }
    };

    //update testplan
    $scope.UpdateTestPlan = function (currentTestPlan) {
        testplanFactory.UpdateTestPlan(currentTestPlan)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> testplan ' + currentTestPlan.TestPlanId + ' has been updated.');
                }
            }, function (error) {
                $scope.TestPlanCancel(currentTestPlan);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating testplan! ' + error.ExceptionInformation);
            });
    };

    // delete testplan
    $scope.DeleteTestPlan = function (currentTestPlan) {
        testplanFactory.DeleteTestPlan(currentTestPlan)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> testplan ' + currentTestPlan.TestPlanId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting testplan! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit testplan
    $scope.TestPlanEdit = function (childScope, currenttestplan) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/TestPlan/TestPlanAddEdit.html",
            controller: "testplanAddEditController",
            inputs: {
                items: currenttestplan,
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
                    $scope.UpdateTestPlan(modal.scope.testplan);
                } else {
                    $scope.TestPlanCancel(modal.scope.testplan);
                }
            });
        });
    };


    $scope.TestPlanShowAdd = function () {
        var testplan = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/TestPlan/TestPlanAddEdit.html",
            controller: "testplanAddEditController",
            inputs: {
                items: testplan,
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
                    $scope.AddTestPlan(modal.scope.testplan);
                } else {
                    modal.scope.testplan = null;
                }
            });
        });
    };

    $scope.TestPlanShowConfirm = function (currenttestplan) {
        var data = currenttestplan;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/TestPlan/TestPlanDelete.html",
            controller: "testplanDeleteController",
            inputs: {
                item: currenttestplan
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteTestPlan(modal.scope.testplan);
                }
            });
        });
    };

    $scope.TestPlanCancel = function (currenttestplan) {
        $scope.GetTestPlan(currenttestplan);
    }
    var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    menu.removeClass('active');
    var submenu = menu.find("a:contains('Test Plan')");
    submenu.click();
    $scope.GetTestPlans();
    $scope.orderByField = 'TestPlanName';
    $scope.reverseSort = false;
});





