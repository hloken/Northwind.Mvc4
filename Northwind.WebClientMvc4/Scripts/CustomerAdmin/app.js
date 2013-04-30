var CustomerAdminController = function ($scope, $http) {
    $scope.pluralizer = {
        0: "No customer!",
        1: "Only one customer, work harder!",
        2: "Two customers, you're getting there!",
        other: "{} customers, doing great!"
    };
   
    //$scope.addCustomer = function() {
    //    var newCustomer = {
    //        CustomerID: $scope.customerID,
    //        CompanyName: $scope.companyName,
    //        Price: $scope.stockPrice,
    //        LastPurchase: new Date()
    //    };
    //    $scope.customers.push(newCustomer);
    //};

    //$scope.removeCustomer = function(item) {
    //    $scope.customers.splice($scope.customers.indexOf(item), 1);
    //};

    $scope.customers = [];
    $http({
        url: "api/Customers",
        dataType: "json",
        method: "GET"
    }).success(function(response) {
        $scope.customers = response;
    }).error(function (error) {
        $scope.error = error;
        console.log(error);
    });
        
}