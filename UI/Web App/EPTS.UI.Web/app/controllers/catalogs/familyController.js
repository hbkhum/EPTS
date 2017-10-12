
app.controller('familyAddEditController', function ($scope, items, addEdit, businessunitFactory, close) {
    var currentItems = {};
    $scope.family = items;
    $scope.familyEditmode = addEdit;
    $scope.selectedItem = $scope.family.BusinessUnit;
    angular.copy($scope.family, currentItems);

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
        if (result === false) { angular.copy( currentItems,$scope.family); }
        close(result, 500);
    };
    $scope.update = function(item) {
        $scope.selectedItem = item;
        $scope.family.BusinessUnitId = item.BusinessUnitId;
        if ($scope.familyEditmode !== false) {
            angular.copy(item, $scope.family.BusinessUnit);
            //$scope.family.BusinessUnit.BusinessUnitId = item.BusinessUnitId;
            //$scope.family.BusinessUnit.BusinessUnitName = item.BusinessUnitName;
        }
    };
    $scope.GetBusinessUnits();
});

app.controller('familyDeleteController', function ($scope, item, close) {
    $scope.family = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/
app.controller('familyIndexController', function postController($scope, familyFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var familyHub = connection.createHubProxy('FamilyHub');
    $scope.families = [];
    $scope.family = {};
    $scope.family_editmode = false;

    $scope.reverseSort = false;
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.orderByField = "FamilyName";



    /***************************************************************************
    *
    * Signalr client functions
    *
    ***************************************************************************/
    familyHub.on("AddFamily", function (item) {
        $scope.families.unshift(item);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    familyHub.on("UpdateFamily", function (item) {
        var index = db.searchIndex($scope.families, "FamilyId", item.FamilyId);
        $scope.families[index].FamilyName = item.FamilyName;
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    familyHub.on("DeleteFamily", function (item) {
        var index = db.searchIndex($scope.families, "FamilyId", item.FamilyId);
        $scope.families.splice(index, 1);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    connection.start();

    $scope.pageChanged = function () {
        //var startPos = ($scope.page - 1) * 3;
    };


    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get family
    $scope.GetFamily = function (currentFamily) {
        familyFactory.GetFamily(currentFamily)
            .then(function (response) {
                angular.copy(response, currentFamily);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading family! ' + err.ExceptionInformation);
            });
    };

    //get all family
    $scope.GetFamilies = function () {
        //var orderbyfield = $scope.orderByField;
        //if ($scope.reverseSort === true) {
        //    orderbyfield = $scope.orderByField + " Desc";
        //}
        //familyFactory.GetFamilies($scope.pageSize, $scope.currentPage, orderbyfield, $scope.pageFilter)
        familyFactory.GetFamilies()
            .then(function (reponse) {
                $scope.families = reponse.result;
                $scope.totalItems = reponse.TotalCount;
                $scope.noOfPages = Math.ceil($scope.totalItems / $scope.pageSize);
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add family
    $scope.AddFamily = function (currentFamily) {
        if (currentFamily != null) {
            familyFactory.AddFamily(currentFamily)
                .then(function (reponse) {
                    currentFamily.FamilyId = reponse;
                    $scope.family = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> family ' + currentFamily.FamilyId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding family! ' + error.ExceptionInformation);
                });
        }
    };

    //update family
    $scope.UpdateFamily = function (currentFamily) {
        familyFactory.UpdateFamily(currentFamily)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> family ' + currentFamily.FamilyId + ' has been updated.');
                }
            }, function (error) {
                $scope.FamilyCancel(currentFamily);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating family! ' + error.ExceptionInformation);
            });
    };

    // delete family
    $scope.DeleteFamily = function (currentFamily) {
        familyFactory.DeleteFamily(currentFamily)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> family ' + currentFamily.FamilyId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting family! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit family
    $scope.FamilyEdit = function (childScope, currentfamily) {
        angular.copy(currentfamily, $scope.family);
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Family/FamilyAddEdit.html",
            controller: "familyAddEditController",
            inputs: {
                items: currentfamily,
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
                    $scope.UpdateFamily(modal.scope.family);
                } else {
                    $scope.FamilyCancel(modal.scope.family);
                }
            });
        });
    };


    $scope.FamilyShowAdd = function () {
        var family = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Family/FamilyAddEdit.html",
            controller: "familyAddEditController",
            inputs: {
                items: family,
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
                    $scope.AddFamily(modal.scope.family);
                } else {
                    modal.scope.family = null;
                }
            });
        });
    };

    $scope.FamilyShowConfirm = function (currentfamily) {
        var data = currentfamily;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Family/FamilyDelete.html",
            controller: "familyDeleteController",
            inputs: {
                item: currentfamily
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteFamily(modal.scope.family);
                }
            });
        });
    };

    $scope.FamilyCancel = function (currentfamily) {
        angular.copy($scope.family, currentfamily);
        $scope.family = {};
        //$scope.GetFamily(currentfamily);
    }
    var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    menu.removeClass('active');
    var submenu = menu.find("a:contains('Families')");
    submenu.click();
    $scope.GetFamilies();
});




