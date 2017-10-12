/*****************************************************************************************/
/******                                 Factory                                     ******/
/*****************************************************************************************/
app.factory('partnumberFactory', function ($http, $q) {
    var baseAddress = 'http://humbertopedraza.dynu.com/epts/webapi/api/';
    var url = "";

    return {
        GetPartNumbers: function () {
            url = baseAddress + 'PartNumber/';
            return $http.get(url)
                .then(function (response) {
                    if (typeof response.data === 'object') {
                        return response.data;
                    } else {
                        return q.reject(response.data);
                    }
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        //GetPartNumberByModelId: function (model, pageSize, pageNumber, orderby, wherevalue) {
        GetPartNumberByModelId: function (model) {
            url = baseAddress + 'PartNumber/Model/' + model;
            return $http.get(url)
            //return $http.get(url, { params: { "pageSize": pageSize, "pageNumber": pageNumber, "orderby": orderby, "wherevalue": wherevalue } })
                .then(function (response) {
                    if (typeof response.data === 'object') {
                        return response.data;
                    } else {
                        return q.reject(response.data);
                    }
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },

        GetPartNumber: function (partnumber) {
            url = baseAddress + 'PartNumber/' + partnumber.PartNumberId;
            return $http.get(url)
                .then(function (response) {
                    if (typeof response.data === 'object') {
                        return response.data;
                    } else {
                        return q.reject(response.data);
                    }
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        AddPartNumber: function (partnumber) {
            url = baseAddress + 'PartNumber/';
            return $http.post(url, partnumber)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        AddPartNumberByModel: function (partnumber, modelid) {
            url = baseAddress + 'ModelDetail/';
            var modeldetail = {
                ModelId: modelid,
                PartNumberId: partnumber
            };
            return $http.post(url, modeldetail)
                        .then(function (response) {
                            return response.data;
                        }, function (response) {
                            // something went wrong
                            return $q.reject(response.data);
                        });
        },
        DeletePartNumber: function (partnumber) {
             url = baseAddress + 'PartNumber/' + partnumber.PartNumberId;
            return $http.delete(url)
                    .then(function (response) {
                        return response.data;
                    }, function (response) {
                        // something went wrong
                        return $q.reject(response.data);
                    });
            
        },
        DeletePartNumberByModel: function (modeldetail) {
            url = baseAddress + 'ModelDetail/' + modeldetail.ModelDetailId;
            return $http.delete(url)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });

        },
        UpdatePartNumber: function (partnumber) {
            url = baseAddress + 'PartNumber/' + partnumber.PartNumberId;
            return $http.put(url, partnumber).then(function (response) {
                return response.data;
            }, function (response) {
                // something went wrong
                return $q.reject(response.data);
            });
        }
    };
});
