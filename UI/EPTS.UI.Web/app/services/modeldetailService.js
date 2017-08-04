/*****************************************************************************************/
/******                                 Factory                                     ******/
/*****************************************************************************************/
app.factory('modeldetailFactory', function ($http, $q) {
    var baseAddress = 'http://humbertopedraza.dynu.com/epts/webapi/api/';
    var url = "";

    return {
        GetModelDetails: function (where) {
            url = baseAddress + 'ModelDetails/?' + where;
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
        GetModelDetail: function (modeldetail) {
            url = baseAddress + 'ModelDetails/' + modeldetail.ModelDetailId;
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
        AddModelDetail: function (modeldetail) {
            url = baseAddress + 'ModelDetails/';
            return $http.post(url, modeldetail)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        DeleteModelDetail: function (modeldetail) {
            url = baseAddress + 'ModelDetails/' + modeldetail.ModelDetailId;
            return $http.delete(url)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        UpdateModelDetail: function (modeldetail) {
            url = baseAddress + 'ModelDetails/' + modeldetail.ModelDetailId;
            return $http.put(url, modeldetail).then(function (response) {
                return response.data;
            }, function (response) {
                // something went wrong
                return $q.reject(response.data);
            });
        }
    };
});
