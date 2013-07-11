var Northwind = Northwind || {};

// controllers
Northwind.CustomerController = function($scope, $routeParams, Media, $location) {

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