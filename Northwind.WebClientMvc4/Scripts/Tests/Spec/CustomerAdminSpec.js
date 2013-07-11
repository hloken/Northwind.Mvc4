/// <reference path="../../jasmine/jasmine.js"/>
/// <reference path="../../jasmine/jasmine-html.js"/>

/// <reference path="../../jquery-2.0.2.js"/>
/// <reference path="../../bootstrap.js"/>
/// <reference path="../../underscore.js"/>
/// <reference path="../../angular.js"/>
/// <reference path="../../angular-resource.js"/>
/// <reference path="../../../Assets/js/ng-bootstrap.js"/>
/// <reference path="../../../Assets/js/CustomerAdmin/app.js"/>
/// <reference path="../../../Assets/js/CustomerAdmin/config.js"/>
/// <reference path="../../../Assets/js/CustomerAdmin/controllers.js"/>
/// <reference path="../../../Assets/js/CustomerAdmin/filters.js"/>
/// <reference path="../../../Assets/js/CustomerAdmin/services.js"/>

/// <reference path="SpecHelper.js"/>


describe("Northwind.CustomerAdmin", function () {
    it("isDefined", function() {
        expect(Northwind.CustomerAdmin).toBeDefined();
    });

    it ("returns the module on start", function() {
        var module = Northwind.CustomerAdmin.start("ngCustomerAdmin", { root: "/" });
        expect(module).toBeDefined();
    });

    describe("List Controller", function () {
        // test data
        var testCustomerALFKI = { "CustomerId": "ALFKI", "CompanyName": "Alfreds Futterkiste", "ContactName": "Maria Anders", "Details": "Customers/ALFKI" };
        var testCustomers = [testCustomerALFKI];

        // stubs
        var scope, route, mediaService, ctrl;

        // setup step
        beforeEach(function () {
            // stub out dependencies
            scope = {};
            route = {};
            
            // stubs out service provider
            mediaService = { 
                customers : {
                    query: function () {
                        return testCustomers;
                    }
                }
            };
            
            // create controller with stubs
            ctrl = new Northwind.CustomerListController(scope, route, mediaService);
        });

        it("exists", function() {
            expect(ctrl).toBeDefined();
        });

        it ("returns a list of customers by default", function() {
            expect(scope.customers).toEqual(testCustomers);
        }) ;
    });
    
})