
app.controller('flowAddEditController', function ($scope, items, addEdit, businessunitFactory, close) {
    var currentItems = {};
    $scope.flow = items;
    $scope.flowEditmode = addEdit;
    $scope.selectedItem = $scope.flow.BusinessUnit;
    angular.copy($scope.flow, currentItems);

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
        if (result === false) { angular.copy( currentItems,$scope.flow); }
        close(result, 500);
    };
    $scope.update = function(item) {
        $scope.selectedItem = item;
        $scope.flow.BusinessUnitId = item.BusinessUnitId;
        if ($scope.flowEditmode !== false) {
            angular.copy(item, $scope.flow.BusinessUnit);
            //$scope.flow.BusinessUnit.BusinessUnitId = item.BusinessUnitId;
            //$scope.flow.BusinessUnit.BusinessUnitName = item.BusinessUnitName;
        }
    };
    $scope.GetBusinessUnits();
});

app.controller('flowDeleteController', function ($scope, item, close) {
    $scope.flow = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/
app.controller('flowPaginationController', function ($scope) {
    $scope.pageChangeHandler = function (num) {
        console.log('going to page ' + num);
    };
});


app.controller('flowIndexController', function postController($scope, flowFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var flowHub = connection.createHubProxy('FlowHub');
    $scope.flows = [];
    $scope.flow_editmode = false;
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
    flowHub.on("AddFlow", function (item) {
        $scope.flows.unshift(item);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    flowHub.on("UpdateFlow", function (item) {
        var index = db.searchIndex($scope.flows, "FlowId", item.FlowId);
        $scope.flows[index].FlowName = item.FlowName;
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    flowHub.on("DeleteFlow", function (item) {
        var index = db.searchIndex($scope.flows, "FlowId", item.FlowId);
        $scope.flows.splice(index, 1);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    connection.start();

    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get flow
    $scope.GetFlow = function (currentFlow) {
        flowFactory.GetFlow(currentFlow)
            .then(function (response) {
                angular.copy(response, currentFlow);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading flow! ' + err.ExceptionInformation);
            });
    };

    //get all flow
    $scope.GetFlows = function () {
        flowFactory.GetFlows()
            .then(function (reponse) {
                $scope.flows = reponse.result;
                $scope.totalItems = $scope.flows.length;
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add flow
    $scope.AddFlow = function (currentFlow) {
        if (currentFlow != null) {
            flowFactory.AddFlow(currentFlow)
                .then(function (reponse) {
                    currentFlow.FlowId = reponse;
                    $scope.flow = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> flow ' + currentFlow.FlowId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding flow! ' + error.ExceptionInformation);
                });
        }
    };

    //update flow
    $scope.UpdateFlow = function (currentFlow) {
        flowFactory.UpdateFlow(currentFlow)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> flow ' + currentFlow.FlowId + ' has been updated.');
                }
            }, function (error) {
                $scope.FlowCancel(currentFlow);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating flow! ' + error.ExceptionInformation);
            });
    };

    // delete flow
    $scope.DeleteFlow = function (currentFlow) {
        flowFactory.DeleteFlow(currentFlow)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> flow ' + currentFlow.FlowId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting flow! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit flow
    $scope.FlowEdit = function (childScope, currentflow) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Flow/FlowAddEdit.html",
            controller: "flowAddEditController",
            inputs: {
                items: currentflow,
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
                    $scope.UpdateFlow(modal.scope.flow);
                } else {
                    $scope.FlowCancel(modal.scope.flow);
                }
            });
        });
    };


    $scope.FlowShowAdd = function () {
        var flow = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Flow/FlowAddEdit.html",
            controller: "flowAddEditController",
            inputs: {
                items: flow,
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
                    $scope.AddFlow(modal.scope.flow);
                } else {
                    modal.scope.flow = null;
                }
            });
        });
    };

    $scope.FlowShowConfirm = function (currentflow) {
        var data = currentflow;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Flow/FlowDelete.html",
            controller: "flowDeleteController",
            inputs: {
                item: currentflow
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteFlow(modal.scope.flow);
                }
            });
        });
    };

    $scope.FlowCancel = function (currentflow) {
        $scope.GetFlow(currentflow);
    }
    var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    menu.removeClass('active');
    var submenu = menu.find("a:contains('Familes')");
    submenu.click();
    $scope.GetFlows();
    $scope.orderByField = 'FlowName';
    $scope.reverseSort = false;
});





