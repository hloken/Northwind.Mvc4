var Northwind = Northwind || {};

Northwind.CustomerAdmin = function () {
    // start 'em up
    var start = function(appName, payload) {
        // initialize the app
        var module = init(appName, payload);
        
        // startup Angular
        angular.bootstrap(document, [appName]);

        return module;
    };
    
    var init = function (appName, payload) {
        // create the module
        var customerAdminApp = angular.module(appName, ['ngResource']);

        // configure routes
        customerAdminApp.config(function ($routeProvider) {
            $routeProvider
                .when("/", {
                    templateUrl: "Assets/CustomerAdmin/templates/list_template.html",
                    controller: "CustomerListController"
                })
                .when("/:customerId", {
                    templateUrl: "Assets/CustomerAdmin/templates/customer_template.html",
                    controller: "CustomerController"
                });
        });

        // set the provider for Media
        customerAdminApp.provider("Media", Northwind.CustomerProvider);

        // configure the provider
        customerAdminApp.config(function (MediaProvider) {
            if (!payload)
                throw "Need to have CustomerApiServiceConfiguration set please";
            
            for (var key in payload) {
                MediaProvider.setResource(key, payload[key]);
            }
        });

        // wireup directives
        customerAdminApp.directive("breadcrumbs", Northwind.Bootstrap.BreadCrumbs);
        customerAdminApp.directive("saveButton", Northwind.Bootstrap.SaveButton);
        customerAdminApp.directive("deleteButton", Northwind.Bootstrap.DeleteButton);
        
        // wireup the filters
        customerAdminApp.filter("customerlink", Northwind.CustomerLink);

        // wireup the controllers
        customerAdminApp.controller("CustomerListController", Northwind.CustomerListController);
        customerAdminApp.controller("CustomerController", Northwind.CustomerController);

        return customerAdminApp;
    };

    return {
        start: start
    };
}(); 