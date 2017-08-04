/*****************************************************************************************/
/******                                 Factory                                     ******/
/*****************************************************************************************/
app.factory('familyFactory', function ($http, $q) {
    var baseAddress = 'http://humbertopedraza.dynu.com/epts/webapi/api/';
    var url = "";

    return {
        GetFamilies: function () {
            url = baseAddress + 'Family/';
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
        GetFamily: function (family) {
            url = baseAddress + 'Family/' + family.FamilyId;
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
        AddFamily: function (family) {
            url = baseAddress + 'Family/';
            return $http.post(url, family)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        DeleteFamily: function (family) {
            url = baseAddress + 'Family/' + family.FamilyId;
            return $http.delete(url)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        UpdateFamily: function (family) {
            url = baseAddress + 'Family/' + family.FamilyId;
            return $http.put(url, family).then(function (response) {
                return response.data;
            }, function (response) {
                // something went wrong
                return $q.reject(response.data);
            });
        }
    };
});