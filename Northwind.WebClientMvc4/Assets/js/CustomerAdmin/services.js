var Northwind = Northwind || {};

// services
Northwind.CustomerProvider = function () {
    // config
    var resources = [];
    this.setResource = function (resourceName, url) {
        var resource = { name: resourceName, url: url };
        resources.push(resource);
    };

    // injected
    this.$get = function ($resource) {
        var result = {};
        _.each(resources, function (resource) {
            result[resource.name] = $resource(resource.url, {}, { update: { method: 'PUT' } });
        });
        return result;
    };
};