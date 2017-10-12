
app.controller('testgroupAddEditController', function ($scope, items, addEdit, close) {
    var currentItems = {};
    $scope.testgroup = items;
    $scope.testgroupEditmode = addEdit;
    angular.copy($scope.testgroup, currentItems);
    $scope.close = function (result) {
        if (result === false) { angular.copy( currentItems,$scope.testgroup); }
        close(result, 500);
    };
});

app.controller('testgroupDeleteController', function ($scope, item, close) {
    $scope.testgroup = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/

app.controller('testgroupIndexController', function postController($scope, testgroupFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var testgroupHub = connection.createHubProxy('TestGroupHub');
    $scope.testgroups = [];
    $scope.testgroup_editmode = false;

    $scope.reverseSort = false;
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.orderByField = "Sequence";



    /***************************************************************************
    *
    * Signalr client functions
    *
    ***************************************************************************/
    //testgroupHub.on("AddTestGroup", function (item) {
    //    $scope.testgroups.unshift(item);
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //testgroupHub.on("UpdateTestGroup", function (item) {
    //    var index = db.searchIndex($scope.testgroups, "TestGroupId", item.TestGroupId);
    //    $scope.testgroups[index].TestGroupName = item.TestGroupName;
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //testgroupHub.on("DeleteTestGroup", function (item) {
    //    var index = db.searchIndex($scope.testgroups, "TestGroupId", item.TestGroupId);
    //    $scope.testgroups.splice(index, 1);
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //connection.start();
    $scope.$on('TestPlanId_click', function (e, id) {
        $scope.TestPlanId = id;
        $scope.GetTestGroups($scope.TestPlanId);
    });
    $scope.collapse = function (event, childScope, id) {
        $(event.target).toggleClass("glyphicon-chevron-down");
        if (childScope.expanded === true) {
            childScope.$broadcast('TestGroupId_click', id);
        }
    };

    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get testgroup
    $scope.GetTestGroup = function (currentTestGroup) {
        testgroupFactory.GetTestGroup(currentTestGroup)
            .then(function (response) {
                angular.copy(response, currentTestGroup);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading testgroup! ' + err.ExceptionInformation);
            });
    };

    //get all testgroup
    $scope.GetTestGroups = function (id) {
        testgroupFactory.GetTestGroupByTestPlanId(id)
            .then(function (reponse) {
                $scope.testgroups = reponse.result;
                $scope.totalItems = reponse.TotalCount;
                $scope.noOfPages = Math.ceil($scope.totalItems / $scope.pageSize);
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add testgroup
    $scope.AddTestGroup = function (currentTestGroup) {
        if (currentTestGroup != null) {
            currentTestGroup.TestPlanId = {};
            currentTestGroup.TestPlanId = $scope.TestPlanId;
            testgroupFactory.AddTestGroup(currentTestGroup)
                .then(function (reponse) {
                    currentTestGroup.TestGroupId = reponse;
                    $scope.testgroups.unshift(currentTestGroup);
                    $scope.testgroup = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> testgroup ' + currentTestGroup.TestGroupId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding testgroup! ' + error.ExceptionInformation);
                });
        }
    };

    //update testgroup
    $scope.UpdateTestGroup = function (currentTestGroup) {
        testgroupFactory.UpdateTestGroup(currentTestGroup)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> testgroup ' + currentTestGroup.TestGroupId + ' has been updated.');
                }
            }, function (error) {
                $scope.TestGroupCancel(currentTestGroup);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating testgroup! ' + error.ExceptionInformation);
            });
    };

    // delete testgroup
    $scope.DeleteTestGroup = function (currentTestGroup) {
        testgroupFactory.DeleteTestGroup(currentTestGroup)
            .then(function (reponse) {
                var index = db.searchIndex($scope.testgroups, "TestGroupId", currentTestGroup.TestGroupId);
                $scope.testgroups.splice(index, 1);
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> testgroup ' + currentTestGroup.TestGroupId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting testgroup! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit testgroup
    $scope.TestGroupEdit = function (childScope, currenttestgroup) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/TestGroup/TestGroupAddEdit.html",
            controller: "testgroupAddEditController",
            inputs: {
                items: currenttestgroup,
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
                    $scope.UpdateTestGroup(modal.scope.testgroup);
                } else {
                    $scope.TestGroupCancel(modal.scope.testgroup);
                }
            });
        });
    };


    $scope.TestGroupShowAdd = function () {
        var testgroup = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/TestGroup/TestGroupAddEdit.html",
            controller: "testgroupAddEditController",
            inputs: {
                items: testgroup,
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
                    $scope.AddTestGroup(modal.scope.testgroup);
                } else {
                    modal.scope.testgroup = null;
                }
            });
        });
    };

    $scope.TestGroupShowConfirm = function (currenttestgroup) {
        var data = currenttestgroup;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/TestGroup/TestGroupDelete.html",
            controller: "testgroupDeleteController",
            inputs: {
                item: currenttestgroup
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteTestGroup(modal.scope.testgroup);
                }
            });
        });
    };

    $scope.TestGroupCancel = function (currenttestgroup) {
        $scope.GetTestGroup(currenttestgroup);
    }
    //var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    //menu.removeClass('active');
    //var submenu = menu.find("a:contains('Familes')");
    //submenu.click();
    //$scope.GetTestGroups();
});





