/*****************************************************************************************/
/******                                 Factory                                     ******/
/*****************************************************************************************/
app.factory('testgroupFactory', function ($http, $q) {
    var baseAddress = 'http://humbertopedraza.dynu.com/epts/webapi/api/';
    var url = "";

    return {
        GetTestGroups: function () {
            url = baseAddress + 'TestGroup/';
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
        GetTestGroup: function (testgroup) {
            url = baseAddress + 'TestGroup/' + testgroup.TestGroupId;
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
        AddTestGroup: function (testgroup) {
            url = baseAddress + 'TestGroup/';
            return $http.post(url, testgroup)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        DeleteTestGroup: function (testgroup) {
            url = baseAddress + 'TestGroup/' + testgroup.TestGroupId;
            return $http.delete(url)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        UpdateTestGroup: function (testgroup) {
            url = baseAddress + 'TestGroup/' + testgroup.TestGroupId;
            return $http.put(url, testgroup).then(function (response) {
                return response.data;
            }, function (response) {
                // something went wrong
                return $q.reject(response.data);
            });
        }
    };
});
