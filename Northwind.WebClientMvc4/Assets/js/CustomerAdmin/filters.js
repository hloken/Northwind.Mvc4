var Northwind = Northwind || {};

// filters
Northwind.CustomerLink = function () {

    // Filters return a function
    return function (customer) {
        var currentLocation = location.href;
        if (currentLocation[currentLocation.length - 1] !== "/") {
            currentLocation += "/";
        }

        return currentLocation + customer.CustomerId;
    };
};