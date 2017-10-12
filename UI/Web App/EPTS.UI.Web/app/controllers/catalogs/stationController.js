
app.controller('stationAddEditController', function ($scope, items, addEdit, businessunitFactory, close) {
    var currentItems = {};
    $scope.station = items;
    $scope.stationEditmode = addEdit;
    $scope.selectedItem = $scope.station.BusinessUnit;
    angular.copy($scope.station, currentItems);

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
        if (result === false) { angular.copy( currentItems,$scope.station); }
        close(result, 500);
    };
    $scope.update = function(item) {
        $scope.selectedItem = item;
        $scope.station.BusinessUnitId = item.BusinessUnitId;
        if ($scope.stationEditmode !== false) {
            angular.copy(item, $scope.station.BusinessUnit);
            //$scope.station.BusinessUnit.BusinessUnitId = item.BusinessUnitId;
            //$scope.station.BusinessUnit.BusinessUnitName = item.BusinessUnitName;
        }
    };
    $scope.GetBusinessUnits();
});

app.controller('stationDeleteController', function ($scope, item, close) {
    $scope.station = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/
app.controller('stationPaginationController', function ($scope) {
    $scope.pageChangeHandler = function (num) {
        console.log('going to page ' + num);
    };
});


app.controller('stationIndexController', function postController($scope, stationFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var stationHub = connection.createHubProxy('StationHub');
    $scope.stations = [];
    $scope.station_editmode = false;
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
    stationHub.on("AddStation", function (item) {
        $scope.stations.unshift(item);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    stationHub.on("UpdateStation", function (item) {
        var index = db.searchIndex($scope.stations, "StationId", item.StationId);
        $scope.stations[index].StationName = item.StationName;
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    stationHub.on("DeleteStation", function (item) {
        var index = db.searchIndex($scope.stations, "StationId", item.StationId);
        $scope.stations.splice(index, 1);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    connection.start();

    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get station
    $scope.GetStation = function (currentStation) {
        stationFactory.GetStation(currentStation)
            .then(function (response) {
                angular.copy(response, currentStation);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading station! ' + err.ExceptionInformation);
            });
    };

    //get all station
    $scope.GetStations = function () {
        stationFactory.GetStations()
            .then(function (reponse) {
                $scope.stations = reponse.result;
                $scope.totalItems = $scope.stations.length;
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add station
    $scope.AddStation = function (currentStation) {
        if (currentStation != null) {
            stationFactory.AddStation(currentStation)
                .then(function (reponse) {
                    currentStation.StationId = reponse;
                    $scope.station = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> station ' + currentStation.StationId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding station! ' + error.ExceptionInformation);
                });
        }
    };

    //update station
    $scope.UpdateStation = function (currentStation) {
        stationFactory.UpdateStation(currentStation)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> station ' + currentStation.StationId + ' has been updated.');
                }
            }, function (error) {
                $scope.StationCancel(currentStation);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating station! ' + error.ExceptionInformation);
            });
    };

    // delete station
    $scope.DeleteStation = function (currentStation) {
        stationFactory.DeleteStation(currentStation)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> station ' + currentStation.StationId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting station! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit station
    $scope.StationEdit = function (childScope, currentstation) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Station/StationAddEdit.html",
            controller: "stationAddEditController",
            inputs: {
                items: currentstation,
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
                    $scope.UpdateStation(modal.scope.station);
                } else {
                    $scope.StationCancel(modal.scope.station);
                }
            });
        });
    };


    $scope.StationShowAdd = function () {
        var station = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Station/StationAddEdit.html",
            controller: "stationAddEditController",
            inputs: {
                items: station,
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
                    $scope.AddStation(modal.scope.station);
                } else {
                    modal.scope.station = null;
                }
            });
        });
    };

    $scope.StationShowConfirm = function (currentstation) {
        var data = currentstation;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Station/StationDelete.html",
            controller: "stationDeleteController",
            inputs: {
                item: currentstation
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteStation(modal.scope.station);
                }
            });
        });
    };

    $scope.StationCancel = function (currentstation) {
        $scope.GetStation(currentstation);
    }
    var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    menu.removeClass('active');
    var submenu = menu.find("a:contains('Familes')");
    submenu.click();
    $scope.GetStations();
    $scope.orderByField = 'StationName';
    $scope.reverseSort = false;
});





