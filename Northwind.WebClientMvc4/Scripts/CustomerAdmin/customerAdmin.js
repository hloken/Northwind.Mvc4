var ngCustomerAdmin = angular.module("ngCustomerAdmin", ['ngResource']);

ngCustomerAdmin.config(function($routeProvider) {
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

ngCustomerAdmin.factory("Database", function($resource) {
    return {
        customers: $resource("api/Customers/"),
        customer: $resource("api/Customers/:customerId", {}, {
            query: { method: 'GET', isArray: false }
        })
    };
});

ngCustomerAdmin.directive("breadcrumbs", Northwind.Bootstrap.BreadCrumbs);

ngCustomerAdmin.controller("CustomerListController", function ($scope, $routeParams, Database) {

    if ($routeParams.customerId) {
        // go fetch a customer
        $scope.customer = Database.customer.query({ customerId: $routeParams.customerId }, { isArray: false });
    } else {
        $scope.customers = Database.customers.query({}, { isArray: true });
    }
    
    $scope.pluralizer = {
        0: "No customer!",
        1: "Only one customer, work harder!",
        2: "Two customers, you're getting there!",
        other: "{} customers, doing great!"
    };

    $scope.addCustomerToDb = function () {
        if ($scope.customerID != "") {
            var newCustomer = {
                CustomerId: $scope.customerId,
                CompanyName: $scope.companyName,
                ContactName: $scope.contactName,
            };

            var newDb = new Database.customers(newCustomer);
            newDb.$save();
            $scope.customers.push(newDb);
        }
    };

    $scope.removeCustomerFromDb = function (dbCustomer) {
        if (confirm("Delete this customer? There's no undo...")) {
            dbCustomer.$delete( {customerId: dbCustomer.CustomerId} );
            $scope.customers.splice($scope.customers.indexOf(dbCustomer), 1);
        }
    };
});

ngCustomerAdmin.controller("CustomerController", function ($scope, $routeParams, Database) {
    
    _.extend($scope, $routeParams);

    if ($routeParams.customerId) {
        // go fetch a customer
        $scope.customer = Database.customer.query({ customerId: $routeParams.customerId });
        $scope.test = "TEST";
    } else {
        $scope.customer = {
            CustomerId: "NA",
            CompanyName: "NA",
            ContactName: "NA"
        };
    }
});