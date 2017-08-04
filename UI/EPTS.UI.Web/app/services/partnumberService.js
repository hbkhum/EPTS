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
        GetPartNumberByModelId: function (model) {
            url = baseAddress + 'PartNumber/Model/' + model;
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
        AddPartNumber: function (partnumber,modelid) {
            url = baseAddress + 'PartNumber/';
            return $http.post(url, partnumber)
                .then(function (response) {
                    var modeldetail = {
                        ModelId : modelid,
                        PartNumberId: response.data
                    };
                    url = baseAddress + 'ModelDetail/';
                    $http.post(url, modeldetail)
                        .then(function () {
                            
                        }, function (response) {
                            // something went wrong
                            return $q.reject(response.data);
                        });
                    return response.data;
                    
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        DeletePartNumber: function (partnumber) {
            url = baseAddress + 'ModelDetail/PartNumber/' + partnumber.PartNumberId;
            $http.delete(url)
                .then(function (response) {
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
            url = baseAddress + 'PartNumber/' + partnumber.PartNumberId;
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
