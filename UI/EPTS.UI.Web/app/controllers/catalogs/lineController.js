
app.controller('lineAddEditController', function ($scope, items, addEdit, businessunitFactory, close) {
    var currentItems = {};
    $scope.line = items;
    $scope.lineEditmode = addEdit;
    $scope.selectedItem = $scope.line.BusinessUnit;
    angular.copy($scope.line, currentItems);

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
        if (result === false) { angular.copy( currentItems,$scope.line); }
        close(result, 500);
    };
    $scope.update = function(item) {
        $scope.selectedItem = item;
        $scope.line.BusinessUnitId = item.BusinessUnitId;
        if ($scope.lineEditmode !== false) {
            angular.copy(item, $scope.line.BusinessUnit);
            //$scope.line.BusinessUnit.BusinessUnitId = item.BusinessUnitId;
            //$scope.line.BusinessUnit.BusinessUnitName = item.BusinessUnitName;
        }
    };
    $scope.GetBusinessUnits();
});

app.controller('lineDeleteController', function ($scope, item, close) {
    $scope.line = item;
    $scope.close = function (result) {
        close(result, 500);
    };
});

/*****************************************************************************************/
/******                                 CONTROLLER                                  ******/
/*****************************************************************************************/
app.controller('linePaginationController', function ($scope) {
    $scope.pageChangeHandler = function (num) {
        console.log('going to page ' + num);
    };
});


app.controller('lineIndexController', function postController($scope, lineFactory, ModalService, db) {
    var connection = $.hubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
    var lineHub = connection.createHubProxy('LineHub');
    $scope.lines = [];
    $scope.line_editmode = false;
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
    lineHub.on("AddLine", function (item) {
        $scope.lines.unshift(item);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    lineHub.on("UpdateLine", function (item) {
        var index = db.searchIndex($scope.lines, "LineId", item.LineId);
        $scope.lines[index].LineName = item.LineName;
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    lineHub.on("DeleteLine", function (item) {
        var index = db.searchIndex($scope.lines, "LineId", item.LineId);
        $scope.lines.splice(index, 1);
        $scope.$apply(); // this is outside of angularjs, so need to apply
    });

    connection.start();

    /***************************************************************************
    *
    * CRUD 
    *
    ***************************************************************************/

    //get line
    $scope.GetLine = function (currentLine) {
        lineFactory.GetLine(currentLine)
            .then(function (response) {
                angular.copy(response, currentLine);
            }).catch(function (err) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading line! ' + err.ExceptionInformation);
            });
    };

    //get all line
    $scope.GetLines = function () {
        lineFactory.GetLines()
            .then(function (reponse) {
                $scope.lines = reponse.result;
                $scope.totalItems = $scope.lines.length;
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Loading Business Unit! ' + error.ExceptionInformation);
            });
    };

    // add line
    $scope.AddLine = function (currentLine) {
        if (currentLine != null) {
            lineFactory.AddLine(currentLine)
                .then(function (reponse) {
                    currentLine.LineId = reponse;
                    $scope.line = {};
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> line ' + currentLine.LineId + ' has been added.');
                }, function (error) {
                    db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Adding line! ' + error.ExceptionInformation);
                });
        }
    };

    //update line
    $scope.UpdateLine = function (currentLine) {
        lineFactory.UpdateLine(currentLine)
            .then(function (response) {
                if (response === true) {
                    db.InformationMessageSuccess('<i class="fa fa-check-square-o fa-3x" aria-hidden="true"></i> <strong>Success!</strong> line ' + currentLine.LineId + ' has been updated.');
                }
            }, function (error) {
                $scope.LineCancel(currentLine);
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Updating line! ' + error.ExceptionInformation);
            });
    };

    // delete line
    $scope.DeleteLine = function (currentLine) {
        lineFactory.DeleteLine(currentLine)
            .then(function (reponse) {
                db.InformationMessageWarning('<i class="fa fa-exclamation-triangle fa-3x" aria-hidden="true"></i> line ' + currentLine.LineId + ' has been deleted.');
            }, function (error) {
                db.InformationMessageDanger('<i class="fa fa-times fa-3x" aria-hidden="true"></i> An Error has occured while Deleting line! ' + error.ExceptionInformation);
            });
    };

    /***************************************************************************
    *
    * Model popup events 
    *
    ***************************************************************************/

    //edit line
    $scope.LineEdit = function (childScope, currentline) {
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Line/LineAddEdit.html",
            controller: "lineAddEditController",
            inputs: {
                items: currentline,
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
                    $scope.UpdateLine(modal.scope.line);
                } else {
                    $scope.LineCancel(modal.scope.line);
                }
            });
        });
    };


    $scope.LineShowAdd = function () {
        var line = {};
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Line/LineAddEdit.html",
            controller: "lineAddEditController",
            inputs: {
                items: line,
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
                    $scope.AddLine(modal.scope.line);
                } else {
                    modal.scope.line = null;
                }
            });
        });
    };

    $scope.LineShowConfirm = function (currentline) {
        var data = currentline;
        ModalService.showModal({
            templateUrl: "app/views/Catalogs/Line/LineDelete.html",
            controller: "lineDeleteController",
            inputs: {
                item: currentline
            }
        }).then(function (modal) {
            modal.element.modal({
                backdrop: 'static',
                keyboard: false
            });
            modal.element.modal();
            modal.close.then(function (result) {
                if (result === true) {
                    $scope.DeleteLine(modal.scope.line);
                }
            });
        });
    };

    $scope.LineCancel = function (currentline) {
        $scope.GetLine(currentline);
    }
    var menu = $(".main-sidebar").find('.sidebar-menu').find('.treeview');
    menu.removeClass('active');
    var submenu = menu.find("a:contains('Familes')");
    submenu.click();
    $scope.GetLines();
    $scope.orderByField = 'LineName';
    $scope.reverseSort = false;
});





