
app.controller('modelAddEditController', function ($scope, items, addEdit, familyFactory, close) {
    var currentItems = {};
    $scope.model = items;
    $scope.modelEditmode = addEdit;
    $scope.selectedItem = $scope.model.Family;
    angular.copy($scope.model, currentItems);

    //get all families
    $scope.GetFamilies = function () {
        familyFactory.GetFamilies()
            .then(function (reponse) {
                $scope.families = reponse.result;
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Family! ' + error.ExceptionInformation);
            });
    };
    $scope.close = function (result) {
        if (result === false) { angular.copy( currentItems,$scope.model); }
        close(result, 500);
    };
    $scope.update = function(item) {
        $scope.selectedItem = item;
        $scope.model.FamilyId = item.FamilyId;
        if ($scope.modelEditmode !== false) {
            angular.copy(item, $scope.model.Family);
        }
    };
    $scope.GetFamilies();
});

app.controller('modelDeleteController', function ($scope, item, close) {
    $scope.model = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/

app.controller('modelIndexController', function postController($scope, modelFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var modelHub = connection.createHubProxy('ModelHub');
    $scope.models = [];
    $scope.model = {};
    $scope.model_editmode = false;

    $scope.reverseSort = false;
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.orderByField = "ModelName";
    $scope.search = {};
    $scope.search.FamilyName = "";
    $scope.search.ModelName = "";

    /***************************************************************************
    *
    * Signalr client functions
    *
    ***************************************************************************/
    modelHub.on("AddModel", function (item) {
        $scope.models.unshift(item);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    modelHub.on("UpdateModel", function (item) {
        var index = db.searchIndex($scope.models, "ModelId", item.ModelId);
        $scope.models[index].ModelName = item.ModelName;
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    modelHub.on("DeleteModel", function (item) {
        var index = db.searchIndex($scope.models, "ModelId", item.ModelId);
        $scope.models.splice(index, 1);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    connection.start();

    $scope.collapse = function (event, childScope, id) {
        $(event.target).toggleClass("glyphicon-chevron-down");
        childScope.$broadcast('ModelId_click', id);
    };

    $scope.pageChanged = function () {
        $scope.GetModels();
    };

    $scope.Change = function () {
        $scope.pageFilter = "ModelName.Contains(\"" + $scope.search.ModelName + "\") and Family.FamilyName.Contains(\"" + $scope.search.FamilyName + "\")";
        $scope.GetModels();
    }
    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get model
    $scope.GetModel = function (currentModel) {
        modelFactory.GetModel(currentModel)
            .then(function (response) {
                angular.copy(response, currentModel);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading model! ' + err.ExceptionInformation);
            });
    };

    //get all model
    $scope.GetModels = function () {
        var orderbyfield = $scope.orderByField;
        if ($scope.reverseSort === true) {
            orderbyfield = $scope.orderByField + " Desc";
        }
        modelFactory.GetModels($scope.pageSize, $scope.currentPage, orderbyfield, $scope.pageFilter)
            .then(function (reponse) {
                $scope.models = reponse.result;
                $scope.totalItems = reponse.TotalCount;
                $scope.noOfPages = Math.ceil($scope.totalItems / $scope.pageSize);
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add model
    $scope.AddModel = function (currentModel) {
        if (currentModel != null) {
            modelFactory.AddModel(currentModel)
                .then(function (reponse) {
                    currentModel.ModelId = reponse;
                    $scope.model = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> model ' + currentModel.ModelId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding model! ' + error.ExceptionInformation);
                });
        }
    };

    //update model
    $scope.UpdateModel = function (currentModel) {
        modelFactory.UpdateModel(currentModel)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> model ' + currentModel.ModelId + ' has been updated.');
                }
            }, function (error) {
                $scope.ModelCancel(currentModel);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating model! ' + error.ExceptionInformation);
            });
    };

    // delete model
    $scope.DeleteModel = function (currentModel) {
        modelFactory.DeleteModel(currentModel)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> model ' + currentModel.ModelId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting model! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit model
    $scope.ModelEdit = function (childScope, currentmodel) {
        angular.copy(currentmodel, $scope.model);
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Model/ModelAddEdit.html",
            controller: "modelAddEditController",
            inputs: {
                items: currentmodel,
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
                    $scope.UpdateModel(modal.scope.model);
                } else {
                    $scope.ModelCancel(modal.scope.model);
                }
            });
        });
    };


    $scope.ModelShowAdd = function () {
        var model = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Model/ModelAddEdit.html",
            controller: "modelAddEditController",
            inputs: {
                items: model,
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
                    $scope.AddModel(modal.scope.model);
                } else {
                    modal.scope.model = null;
                }
            });
        });
    };

    $scope.ModelShowConfirm = function (currentmodel) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Model/ModelDelete.html",
            controller: "modelDeleteController",
            inputs: {
                item: currentmodel
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteModel(modal.scope.model);
                }
            });
        });
    };

    $scope.ModelCancel = function (currentmodel) {
        angular.copy($scope.model, currentmodel);
        $scope.model = {};
        //$scope.GetModel(currentmodel);
    }

    var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    menu.removeClass('active');
    var submenu = menu.find("a:contains('Models')");
    submenu.click();
    
    $scope.GetModels();
    
});





