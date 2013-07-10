// <reference path="../../../Content/jasmine/jasmine_favicon.png"/>
// <reference path="../../../Content/jasmine/jasmine.css"/>
/// <reference path="../../jasmine/jasmine.js"/>
/// <reference path="../../jasmine/jasmine-html.js"/>

// <reference path="../../../Content/bootstrap.css.js"/>
// <reference path="../../../Content/bootstrap-responsive.css"/>
// <reference path="../../../Content/Site.css"/>
/// <reference path="../../jquery-2.0.2.js"/>
/// <reference path="../../bootstrap.js"/>
/// <reference path="../../underscore.js"/>
/// <reference path="../../angular.js"/>
/// <reference path="../../angular-resource.js"/>
/// <reference path="../../ng-bootstrap.js"/>
/// <reference path="../../CustomerAdmin/customerAdmin.js"/>

/// <reference path="SpecHelper.js"/>


describe("Northwind.CustomerAdmin", function () {
    it("isDefined", function() {
        expect(Northwind.CustomerAdmin).toBeDefined();
    });

    it ("returns the module on start", function() {
        var module = Northwind.CustomerAdmin.start("ngCustomerAdmin", { root: "/" });
        expect(module).toBeDefined();
    });

    describe("List Controller", function() {
        var testCustomerALFKI = { "CustomerId": "ALFKI", "CompanyName": "Alfreds Futterkiste", "ContactName": "Maria Anders", "Details": "Customers/ALFKI" };
        var testCustomers = [testCustomerALFKI];

        var scope, route, mediaService, ctrl;

        beforeEach(function() {
            scope = {};
            route = {};
            mediaService = {
                customers : {
                    query: function () {
                        return testCustomers;
                    }
                }
            };
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