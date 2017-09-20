/*****************************************************************************************/
/******                                 Factory                                     ******/
/*****************************************************************************************/
app.factory('modelFactory', function ($http, $q) {
    var baseAddress = 'http://humbertopedraza.dynu.com/epts/webapi/api/';
    var url = "";

    return {
        GetModels: function (pageSize, pageNumber, orderby, wherevalue) {
            url = baseAddress + 'Model/';
            return $http.get(url, { params: { "pageSize": pageSize, "pageNumber": pageNumber, "orderby": orderby, "wherevalue": wherevalue } })
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
        GetModel: function (model) {
            url = baseAddress + 'Model/' + model.ModelId;
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
        AddModel: function (model) {
            url = baseAddress + 'Model/';
            return $http.post(url, model)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        DeleteModel: function (model) {
            url = baseAddress + 'Model/' + model.ModelId;
            return $http.delete(url)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        UpdateModel: function (model) {
            url = baseAddress + 'Model/' + model.ModelId;
            return $http.put(url, model).then(function (response) {
                return response.data;
            }, function (response) {
                // something went wrong
                return $q.reject(response.data);
            });
        }
    };
});
