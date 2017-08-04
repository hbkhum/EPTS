/*****************************************************************************************/
/******                                 Factory                                     ******/
/*****************************************************************************************/
app.factory('businessunitFactory', function ($http,$q) {
    var baseAddress = 'http://humbertopedraza.dynu.com/epts/webapi/api/';
    var url = "";

    return {
        GetBusinessUnits: function () {
            url = baseAddress + 'BusinessUnit/';
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
        GetBusinessUnit: function (businessunit) {
            url = baseAddress + 'BusinessUnit/' + businessunit.BusinessUnitId;
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
        AddBusinessUnit: function (businessunit) {
            url = baseAddress + 'BusinessUnit/';
            return $http.post(url, businessunit)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        DeleteBusinessUnit: function (businessunit) {
            url = baseAddress + 'BusinessUnit/' + businessunit.BusinessUnitId;
            return $http.delete(url)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        UpdateBusinessUnit: function (businessunit) {
            url = baseAddress + 'BusinessUnit/' + businessunit.BusinessUnitId;
            return $http.put(url, businessunit).then(function (response) {
                return response.data;
            }, function (response) {
                // something went wrong
                return $q.reject(response.data);
            });
        }
    };
});

