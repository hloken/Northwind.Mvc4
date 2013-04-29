var MyCtrl = function($scope) {
    $scope.customers = [
        {CustomerID : "IBM", CompanyName : "International Business Machines", Price : 17.80, LastPurchase : "2013-03-12" }, 
        { CustomerID: "MS", CompanyName: "Microsoft", Price: 5.90, LastPurchase: "2013-03-12" }
    ];

    $scope.pluralizer = {
        0: "No customer!",
        1: "Only one customer, work harder!",
        2: "Two customers, you're getting there",
        other: "{} customers, doing great"
    };

    $scope.addCustomer = function() {
        var newCustomer = {
            CustomerID: $scope.customerID,
            CompanyName: $scope.companyName,
            Price: $scope.stockPrice,
            LastPurchase: new Date()
        };
        $scope.customers.push(newCustomer);
    };

    $scope.removeCustomer = function(item) {
        $scope.customers.splice($scope.customers.indexOf(item), 1);
    };
    
    //$.getJSON("api/Customers", "Get",
    //    function (data) {
    //        $scope.customers = data;
    //    }
    //).fail(function (jqxhr, textStatus, error) {
    //    var err = textStatus + ', ' + error;
    //    console.log(err);
    //});
}