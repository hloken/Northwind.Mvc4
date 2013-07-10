var Northwind = Northwind || {};

Northwind.CustomerAdmin = function () {
    
    // services
    var customerProvider = function() {
        // config
        var resources = [];
        this.setResource = function(resourceName, url) {
            var resource = { name: resourceName, url: url };
            resources.push(resource);
        };

        // injected
        this.$get = function($resource) {
            var result = {};
            _.each(resources, function(resource) {
                result[resource.name] = $resource(resource.url, {}, { update: { method: 'PUT' } });
            });
            return result;
        };
    };
    
    // controllers
    var customerController = function($scope, $routeParams, Media, $location) {

        _.extend($scope, $routeParams);

        if ($routeParams.customerId) {
            // go fetch a customer
            $scope.customer = Media.customer.get({ customerId: $routeParams.customerId });
        } else {
            $scope.customer = {
                CustomerId: "NA",
                CompanyName: "NA",
                ContactName: "NA"
            };
        }

        var backToList = function() {
            $location.path("/");
        };

        $scope.saveCustomer = function () {
            if ($scope.customer.CustomerId !== "") {
                
                var newDb = new Media.customers($scope.customer);
                newDb.$update({ customerId: $scope.customer.CustomerId }, backToList);
            }
        };

        $scope.deleteCustomer = function() {
            if (confirm("Delete this customer? There's no undo...")) {
                $scope.customer.$delete({ customerId: $scope.customer.CustomerId }, backToList);
            }
        };
    };
    
    Northwind.CustomerListController = function($scope, $routeParams, Media) {

        $scope.customers = Media.customers.query({}, { isArray: true });

        $scope.pluralizer = {
            0: "No customer!",
            1: "Only one customer, work harder!",
            2: "Two customers, you're getting there!",
            other: "{} customers, doing great!"
        };

        $scope.addCustomer = function() {
            if ($scope.customerID !== "") {
                var newCustomer = {
                    CustomerId: $scope.customerId,
                    CompanyName: $scope.companyName,
                    ContactName: $scope.contactName
                };

                var newDb = new Media.customers(newCustomer);
                newDb.$save();
                $scope.customers.push(newDb);
            }
        };

        $scope.removeCustomer = function(dbCustomer) {
            if (confirm("Delete this customer? There's no undo...")) {
                dbCustomer.$delete({ customerId: dbCustomer.CustomerId });
                $scope.customers.splice($scope.customers.indexOf(dbCustomer), 1);
            }
        };
    };

    // filters
    var customerLink = function() {

        // Filters return a function
        return function(customer) {
            var currentLocation = location.href;
            if (currentLocation[currentLocation.length - 1] !== "/") {
                currentLocation += "/";
            }

            return currentLocation + customer.CustomerId;
        };
    };

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
                    templateUrl: "Scripts/CustomerAdmin/templates/list_template.html",
                    controller: "CustomerListController"
                })
                .when("/:customerId", {
                    templateUrl: "Scripts/CustomerAdmin/templates/customer_template.html",
                    controller: "CustomerController"
                });
        });

        // set the provider for Media
        customerAdminApp.provider("Media", customerProvider);

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
        customerAdminApp.filter("customerlink", customerLink);

        // wireup the controllers
        customerAdminApp.controller("CustomerListController", Northwind.CustomerListController);
        customerAdminApp.controller("CustomerController", customerController);

        return customerAdminApp;
    };

    return {
        start: start
    };
}();

    