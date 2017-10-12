/*****************************************************************************************/
/******                                 Factory                                     ******/
/*****************************************************************************************/
app.service('dataService', function (businessunitFactory) {

    return {
        businessunitFactory: function () {

            return businessunitFactory;

        }

    };

});