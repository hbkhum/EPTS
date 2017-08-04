
app.controller('modeldetailAddEditController', function ($scope, items, addEdit, close) {
    //var currentItems = {};
    $scope.partnumber = items;
    $scope.partnumberEditmode = addEdit;
    //angular.copy($scope.partnumber, currentItems);


    $scope.close = function (result) {
        if (result === false) { angular.copy(currentItems, $scope.partnumber); }
        close(result, 500);
    };
});

app.controller('modeldetailDeleteController', function ($scope, item, close) {
    $scope.modeldetail = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/
app.controller('modeldetailPaginationController', function ($scope) {
    $scope.pageChangeHandler = function (num) {
        console.log('going to page ' + num);
    };
});


app.controller('modeldetailIndexController', function postController($scope, modeldetailFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var modeldetailHub = connection.createHubProxy('ModelDetailHub');
    $scope.modeldetails = [];
    $scope.modeldetail_editmode = false;
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
    modeldetailHub.on("AddModelDetail", function (item) {
        $scope.modeldetails.unshift(item);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    modeldetailHub.on("UpdateModelDetail", function (item) {
        angular.copy(item, modeldetails);
        //var index = db.searchIndex($scope.modeldetails, "ModelDetailId", item.ModelDetailId);
        //$scope.modeldetails[index].ModelDetailName = item.ModelDetailName;
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    modeldetailHub.on("DeleteModelDetail", function (item) {
        var index = db.searchIndex($scope.modeldetails, "ModelDetailId", item.ModelDetailId);
        $scope.modeldetails.splice(index, 1);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    connection.start();

    $scope.$on('ModelId_click', function (e, id) {
        $scope.isCollapsed = $scope.isCollapsed === 0 ? true : false;
        if ($scope.isCollapsed === false) {
            modeldetailFactory.GetModelDetails('wherevalue=ModelId="' + id  +'"')
                .then(function (reponse) {
                    $scope.modeldetails = reponse.result;
                    $scope.totalItems = $scope.modeldetails.length;
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

    //get modeldetail
    $scope.GetModelDetail = function (currentModelDetail) {
        modeldetailFactory.GetModelDetail(currentModelDetail)
            .then(function (response) {
                angular.copy(response, currentModelDetail);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading modeldetail! ' + err.ExceptionInformation);
            });
    };

    //get all modeldetail
    $scope.GetModelDetails = function () {
        modeldetailFactory.GetModelDetails()
            .then(function (reponse) {
                $scope.modeldetails = reponse.result;
                $scope.totalItems = $scope.modeldetails.length;
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add modeldetail
    $scope.AddModelDetail = function (currentModelDetail) {
        if (currentModelDetail != null) {
            modeldetailFactory.AddModelDetail(currentModelDetail)
                .then(function (reponse) {
                    currentModelDetail.ModelDetailId = reponse;
                    $scope.modeldetail = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> modeldetail ' + currentModelDetail.ModelDetailId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding modeldetail! ' + error.ExceptionInformation);
                });
        }
    };

    //update modeldetail
    $scope.UpdateModelDetail = function (currentModelDetail) {
        modeldetailFactory.UpdateModelDetail(currentModelDetail)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> modeldetail ' + currentModelDetail.ModelDetailId + ' has been updated.');
                }
            }, function (error) {
                $scope.ModelDetailCancel(currentModelDetail);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating modeldetail! ' + error.ExceptionInformation);
            });
    };

    // delete modeldetail
    $scope.DeleteModelDetail = function (currentModelDetail) {
        modeldetailFactory.DeleteModelDetail(currentModelDetail)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> modeldetail ' + currentModelDetail.ModelDetailId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting modeldetail! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit modeldetail
    $scope.ModelDetailEdit = function (childScope, currentmodeldetail,index) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/ModelDetail/ModelDetailAddEdit.html",
            controller: "modeldetailAddEditController",
            inputs: {
                items: currentmodeldetail.PartNumber[index],
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
                    
                    $scope.UpdateModelDetail(currentmodeldetail);
                } else {
                    $scope.ModelDetailCancel(currentmodeldetail);
                }
            });
        });
    };


    $scope.ModelDetailShowAdd = function () {
        var modeldetail = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/ModelDetail/ModelDetailAddEdit.html",
            controller: "modeldetailAddEditController",
            inputs: {
                items: modeldetail,
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
                    $scope.AddModelDetail(modal.scope.modeldetail);
                } else {
                    modal.scope.modeldetail = null;
                }
            });
        });
    };

    $scope.ModelDetailShowConfirm = function (currentmodeldetail) {
        var data = currentmodeldetail;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/ModelDetail/ModelDetailDelete.html",
            controller: "modeldetailDeleteController",
            inputs: {
                item: currentmodeldetail
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteModelDetail(modal.scope.modeldetail);
                }
            });
        });
    };

    $scope.ModelDetailCancel = function (currentmodeldetail) {
        $scope.GetModelDetail(currentmodeldetail);
    }
    //var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    //menu.removeClass('active');
    //var submenu = menu.find("a:contains('Familes')");
    //submenu.click();
    //$scope.GetModelDetails();
    //$scope.orderByField = 'PartNumber.PartName';
    //$scope.reverseSort = false;
});





