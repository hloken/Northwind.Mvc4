var Northwind = Northwind || {};

Northwind.CustomerAdmin = function () {
    // the players
    var customerProvider = function() {
        // config
        var resources = [];
        this.setResource = function(resourceName, url, methods) {
            var resource = { name: resourceName, url: url, methods: methods };
            resources.push(resource);
        };

        // injected
        this.$get = function($resource) {
            var result = {};
            _.each(resources, function(resource) {
                if (resource.methods == undefined) {
                    result[resource.name] = $resource(resource.url);
                } else {
                    result[resource.name] = $resource(resource.url, {}, resource.methods);
                }
            });
            return result;
        };
    };
    var customerController = function($scope, $routeParams, Media) {

        _.extend($scope, $routeParams);

        if ($routeParams.customerId) {
            // go fetch a customer
            $scope.customer = Media.customer.query({ customerId: $routeParams.customerId });
            $scope.test = "TEST";
        } else {
            $scope.customer = {
                CustomerId: "NA",
                CompanyName: "NA",
                ContactName: "NA"
            };
        }
    };
    var customerListController = function($scope, $routeParams, Media) {

        if ($routeParams.customerId) {
            // go fetch a customer
            $scope.customer = Media.customer.query({ customerId: $routeParams.customerId }, { isArray: false });
        } else {
            $scope.customers = Media.customers.query({}, { isArray: true });
        }

        $scope.pluralizer = {
            0: "No customer!",
            1: "Only one customer, work harder!",
            2: "Two customers, you're getting there!",
            other: "{} customers, doing great!"
        };

        $scope.addCustomerToDb = function() {
            if ($scope.customerID != "") {
                var newCustomer = {
                    CustomerId: $scope.customerId,
                    CompanyName: $scope.companyName,
                    ContactName: $scope.contactName,
                };

                var newDb = new Media.customers(newCustomer);
                newDb.$save();
                $scope.customers.push(newDb);
            }
        };

        $scope.removeCustomerFromDb = function(dbCustomer) {
            if (confirm("Delete this customer? There's no undo...")) {
                dbCustomer.$delete({ customerId: dbCustomer.CustomerId });
                $scope.customers.splice($scope.customers.indexOf(dbCustomer), 1);
            }
        };
    };

    // the play
    var start = function(appName, payload) {
        // initialize the app
        init(appName, payload);
        
        // startup Angular
        angular.bootstrap(document, [appName]);
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
                .when("/Customers/:customerId", {
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
                MediaProvider.setResource(key, payload[key].url, payload[key].methods);
            }
            ;
        });

        // wireup directives
        customerAdminApp.directive("breadcrumbs", Northwind.Bootstrap.BreadCrumbs);

        // wireup the controllers
        customerAdminApp.controller("CustomerListController", customerListController);
        customerAdminApp.controller("CustomerController", customerController);

        return customerAdminApp;
    };

    return {
        start: start
    };
}();

    