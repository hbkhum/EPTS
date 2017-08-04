/*****************************************************************************************/
/******                                 Factory                                     ******/
/*****************************************************************************************/
app.factory('testplanFactory', function ($http, $q) {
    var baseAddress = 'http://humbertopedraza.dynu.com/epts/webapi/api/';
    var url = "";

    return {
        GetTestPlans: function () {
            url = baseAddress + 'TestPlan/';
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
        GetTestPlan: function (testplan) {
            url = baseAddress + 'TestPlan/' + testplan.TestPlanId;
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
        AddTestPlan: function (testplan) {
            url = baseAddress + 'TestPlan/';
            return $http.post(url, testplan)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        DeleteTestPlan: function (testplan) {
            url = baseAddress + 'TestPlan/' + testplan.TestPlanId;
            return $http.delete(url)
                .then(function (response) {
                    return response.data;
                }, function (response) {
                    // something went wrong
                    return $q.reject(response.data);
                });
        },
        UpdateTestPlan: function (testplan) {
            url = baseAddress + 'TestPlan/' + testplan.TestPlanId;
            return $http.put(url, testplan).then(function (response) {
                return response.data;
            }, function (response) {
                // something went wrong
                return $q.reject(response.data);
            });
        }
    };
});
