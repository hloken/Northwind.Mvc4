var ngCustomerAdmin = angular.module("ngCustomerAdmin", ['ngResource']);

ngCustomerAdmin.factory("Database", function($resource) {
    return {
        customers: $resource("api/Customers/")
    };
});

ngCustomerAdmin.directive("deleteButton", Common.Bootstrap.RemoveButton);

ngCustomerAdmin.directive("addButton", Common.Bootstrap.AddButton);

ngCustomerAdmin.controller("CustomerListController", function($scope, Database) {
    $scope.pluralizer = {
        0: "No customer!",
        1: "Only one customer, work harder!",
        2: "Two customers, you're getting there!",
        other: "{} customers, doing great!"
    };

    $scope.addCustomerToDb = function () {
        if ($scope.customerID != "") {
            var newCustomerData = {
                CustomerId: $scope.customerId,
                CompanyName: $scope.companyName,
                ContactName: $scope.contactName,
            };

            var newDb = new Database.customers(newCustomerData);
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

    $scope.customers = Database.customers.query({}, isArray = true);
});