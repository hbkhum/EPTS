
app.controller('partnumberAddEditController', function ($scope, items, addEdit, close) {
    var currentItems = {};
    $scope.partnumber = items;
    $scope.partnumberEditmode = addEdit;
    angular.copy($scope.partnumber, currentItems);


    $scope.close = function (result) {
        if (result === false) { angular.copy( currentItems,$scope.partnumber); }
        close(result, 500);
    };
});

app.controller('partnumberDeleteController', function ($scope, item, close) {
    $scope.modeldetail = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/

app.controller('partnumberIndexController', function postController($scope, partnumberFactory, ModalService, db) {
    //var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    //var partnumberHub = connection.createHubProxy('PartNumberHub');
    $scope.modeldetails = [];
    $scope.partnumber_editmode = false;

    $scope.reverseSort = false;
    $scope.currentPage = 1;
    $scope.pageSize = 5;
    $scope.orderByField = "PartNumberName";

    /***************************************************************************
    *
    * Signalr client functions
    *
    ***************************************************************************/
    //partnumberHub.on("AddPartNumber", function (item) {
    //    $scope.modeldetails.unshift(item);
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //partnumberHub.on("UpdatePartNumber", function (item) {
    //    var index = db.searchIndex($scope.modeldetails, "PartNumberId", item.PartNumberId);
    //    $scope.modeldetails[index].PartNumberName = item.PartNumberName;
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //partnumberHub.on("DeletePartNumber", function (item) {
    //    var index = db.searchIndex($scope.modeldetails, "PartNumberId", item.PartNumberId);
    //    $scope.modeldetails.splice(index, 1);
    //    $scope.$apply(); // this is outside of angularjs, so need to apply
    //});

    //connection.start();

    $scope.$on('ModelId_click', function (e, id) {
        $scope.ModelId = id;
        $scope.GetPartNumbers($scope.ModelId);
    });

    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get partnumber
    $scope.GetPartNumber = function (currentPartNumber) {
        partnumberFactory.GetPartNumber(currentPartNumber)
            .then(function (response) {
                angular.copy(response, currentPartNumber);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading partnumber! ' + err.ExceptionInformation);
            });
    };

    //get all partnumber
    $scope.GetPartNumbers = function (id) {
        //partnumberFactory.GetPartNumbers()
        //    .then(function (reponse) {
        //        $scope.modeldetails = reponse.result;
        //        $scope.totalItems = $scope.modeldetails.length;
        //    }, function (error) {
        //        db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
        //    });
        //var orderbyfield = $scope.orderByField;
        //if ($scope.reverseSort === true) {
        //    orderbyfield = $scope.orderByField + " Desc";
        //}
        partnumberFactory.GetPartNumberByModelId(id)
          //partnumberFactory.GetPartNumberByModelId(id, $scope.pageSize, $scope.currentPage, orderbyfield, $scope.pageFilter)
              .then(function (reponse) {
                  $scope.modeldetails = reponse.result;
                  $scope.totalItems = reponse.TotalCount;
                  $scope.noOfPages = Math.ceil($scope.totalItems / $scope.pageSize);
              }, function (error) {
                  db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
              });
    };

    // add partnumber
    $scope.AddPartNumber = function (currentPartNumber) {
        if (currentPartNumber != null) {
            partnumberFactory.AddPartNumber(currentPartNumber)
                .then(function (partnumberid) {
                    currentPartNumber.PartNumberId = partnumberid;
                    partnumberFactory.AddPartNumberByModel(partnumberid, $scope.ModelId)
                        .then(function (reponse) {
                            //angular.copy(currentPartNumber, reponse.PartNumber);
                            reponse.PartNumber = currentPartNumber;
                            $scope.modeldetails.unshift(reponse);
                            $scope.partnumber = {};
                        }, function (error) {
                            db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding partnumber! ' + error.ExceptionInformation);
                        });
                    //currentPartNumber.PartNumberId = reponse;

                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> partnumber ' + currentPartNumber.PartNumberId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding partnumber! ' + error.ExceptionInformation);
                });
        }
    };

    //update partnumber
    $scope.UpdatePartNumber = function (currentPartNumber) {
        partnumberFactory.UpdatePartNumber(currentPartNumber)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> partnumber ' + currentPartNumber.PartNumberId + ' has been updated.');
                }
            }, function (error) {
                $scope.PartNumberCancel(currentPartNumber);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating partnumber! ' + error.ExceptionInformation);
            });
    };

    // delete partnumber
    $scope.DeletePartNumber = function (currentModelDetail) {
        partnumberFactory.DeletePartNumberByModel(currentModelDetail)
            .then(function (reponse) {
                if (reponse === true) {
                    partnumberFactory.DeletePartNumber(currentModelDetail)
                        .then(function (reponse) {
                            var index = db.searchIndex($scope.modeldetails, "PartNumberId", currentModelDetail.PartNumberId);
                            $scope.modeldetails.splice(index, 1);
                            db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> partnumber ' + currentModelDetail.PartNumberId + ' has been deleted.');
                        }, function (error) {
                            db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting partnumber! ' + error.ExceptionInformation);
                        });
                }


            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting partnumber! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit partnumber
    $scope.PartNumberEdit = function (childScope, currentpartnumber) {
        ModalService.showModal({
            templateUrl: "app/views/catalogs/PartNumber/PartNumberAddEdit.html",
            controller: "partnumberAddEditController",
            inputs: {
                items: currentpartnumber,
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
                    $scope.UpdatePartNumber(modal.scope.partnumber);
                } else {
                    $scope.PartNumberCancel(modal.scope.partnumber);
                }
            });
        });
    };


    $scope.PartNumberShowAdd = function () {
        var partnumber = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/PartNumber/PartNumberAddEdit.html",
            controller: "partnumberAddEditController",
            inputs: {
                items: partnumber,
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
                    $scope.AddPartNumber(modal.scope.partnumber);
                } else {
                    modal.scope.partnumber = null;
                }
            });
        });
    };

    $scope.PartNumberShowConfirm = function (currentmodeldetail) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/PartNumber/PartNumberDelete.html",
            controller: "partnumberDeleteController",
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
                    $scope.DeletePartNumber(modal.scope.modeldetail);
                }
            });
        });
    };

    $scope.PartNumberCancel = function (currentpartnumber) {
        $scope.GetPartNumber(currentpartnumber);
    }
    //var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    //menu.removeClass('active');
    //var submenu = menu.find("a:contains('Familes')");
    //submenu.click();
    //$scope.GetPartNumbers();
});





