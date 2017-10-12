
app.controller('businessunitAddEditController', function ($scope,addEdit, item, close) {
    var currentbusinessunit = {};
    $scope.businessunit = item;
    $scope.businessunit_editmode = addEdit;
    angular.copy($scope.businessunit, currentbusinessunit);
    $scope.close = function (result) {
        if (result === false) { angular.copy(currentbusinessunit, $scope.businessunit); }
        close(result, 500);
    };
});


app.controller('businessunitDeleteController', function ($scope,item, close) {
    $scope.businessunit = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});


/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/
app.controller('businessunitPaginationController', function ($scope) {
    $scope.pageChangeHandler = function (num) {
        console.log('going to page ' + num);
    };
});


app.controller('businessunitIndexController', function postController($scope, businessunitFactory, ModalService, db, $window) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var businessunitHub = connection.createHubProxy('BusinessUnitHub');
    $scope.businessunits = [];
    $scope.businessunit_editmode = false;

    $scope.reverseSort = false;
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.orderByField = "BusinessUnitName";


    /***************************************************************************
    *
    * Signalr client functions
    *
    ***************************************************************************/
    businessunitHub.on("AddBusinessUnit", function (item) {
        $scope.businessunits.unshift(item);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    businessunitHub.on("UpdateBusinessUnit", function (item) {
        var index = db.searchIndex($scope.businessunits, "BusinessUnitId", item.BusinessUnitId);
        $scope.businessunits[index].BusinessUnitName = item.BusinessUnitName;
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    businessunitHub.on("DeleteBusinessUnit", function (item) {
        var index = db.searchIndex($scope.businessunits, "BusinessUnitId", item.BusinessUnitId);
        $scope.businessunits.splice(index, 1);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    connection.start();


    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get businessunit
    $scope.GetBusinessUnit = function (currentBusinessUnit) {
        businessunitFactory.GetBusinessUnit(currentBusinessUnit)
            .then(function (response) {
                angular.copy(response, currentBusinessUnit);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading businessunit! ' + err.ExceptionInformation);
            });
    };

    //get all businessunit
    $scope.GetBusinessUnits = function () {
        var orderbyfield = $scope.orderByField;
        if ($scope.reverseSort === true) {
            orderbyfield = $scope.orderByField + " Desc";
        }
        businessunitFactory.GetBusinessUnits()
        //businessunitFactory.GetBusinessUnits($scope.pageSize, $scope.currentPage, orderbyfield, $scope.pageFilter)
            .then(function(reponse) {
                $scope.businessunits = reponse.result;
                $scope.totalItems = reponse.TotalCount;
                $scope.noOfPages = Math.ceil($scope.totalItems / $scope.pageSize);
                //$('#card-table').cardtable();
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add businessunit
    $scope.AddBusinessUnit = function (currentBusinessUnit) {
        if (currentBusinessUnit != null) {
            businessunitFactory.AddBusinessUnit(currentBusinessUnit)
                .then(function (reponse) {
                    currentBusinessUnit.BusinessUnitId = reponse;
                    $scope.businessunit =
                    {
                        BusinessUnitId: null,
                        BusinessUnitName: null,
                        BusinessUnitDescription: null,
                        Open: false
                    };
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> businessunit ' + currentBusinessUnit.BusinessUnitId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding businessunit! ' + error.ExceptionInformation);
                });
        }
    };

    //update businessunit
    $scope.UpdateBusinessUnit = function (currentBusinessUnit) {
        businessunitFactory.UpdateBusinessUnit(currentBusinessUnit)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> businessunit ' + currentBusinessUnit.BusinessUnitId + ' has been updated.');
                }
            }, function(error) {
                $scope.BusinessUnitCancel(currentBusinessUnit);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating businessunit! ' + error.ExceptionInformation);
            });
    };

    // delete businessunit
    $scope.DeleteBusinessUnit = function (currentBusinessUnit) {
        businessunitFactory.DeleteBusinessUnit(currentBusinessUnit)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> businessunit ' + currentBusinessUnit.BusinessUnitId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting businessunit! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit businessunit
    $scope.BusinessUnitEdit = function (childScope,currentbusinessunit) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/BusinessUnit/BusinessUnitAddEdit.html",
            controller: "businessunitAddEditController",
            inputs: {
                item: currentbusinessunit,
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
                    $scope.UpdateBusinessUnit(modal.scope.businessunit);
                } else {
                    $scope.BusinessUnitCancel(modal.scope.businessunit);
                }
            });
        });
    };


    $scope.BusinessUnitShowAdd = function () {
        var businessunit = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/BusinessUnit/BusinessUnitAddEdit.html",
            controller: "businessunitAddEditController",
            inputs: {
                item: businessunit,
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
                    $scope.AddBusinessUnit(modal.scope.businessunit);
                } else {
                    modal.scope.businessunit = null;
                }
            });
        });
    };

    $scope.BusinessUnitShowConfirm = function (currentbusinessunit) {
        var data = currentbusinessunit;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/BusinessUnit/BusinessUnitDelete.html",
            controller: "businessunitDeleteController",
            inputs: {
                item: currentbusinessunit
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteBusinessUnit(modal.scope.businessunit);
                }
            });
        });
    };

    $scope.BusinessUnitCancel = function (currentbusinessunit) {
        $scope.GetBusinessUnit(currentbusinessunit);
    }
    var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    menu.removeClass('active');
    var submenu = menu.find("a:contains('Business Unit')");
    submenu.click();
    $scope.GetBusinessUnits();
    
});

