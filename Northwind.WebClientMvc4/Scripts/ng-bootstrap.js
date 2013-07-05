﻿var Northwind = Northwind || {};
Northwind.Bootstrap = Northwind.Bootstrap || {};

Northwind.Bootstrap.BreadCrumbs = function ($routeParams) {
    return {
        restrict: "E",
        controller: function ($scope) {
            var rootUrl = "#/";
            $scope.crumbs = [{ url: rootUrl, text: "Customers" }];
            var runningUrl = rootUrl + "Customers/";
            for (var param in $routeParams) {
                runningUrl += $routeParams[param];
                $scope.crumbs.push({ url: runningUrl, text: $routeParams[param] });
            }

            $scope.notLast = function (crumb) {
                return crumb !== _.last($scope.crumbs);
            };
        },
        template: "<div class='row-fluid'>"
            +       "<div class='span12'>"
            +           "<ul class='breadcrumb'>"
            +               "<li ng-repeat='crumb in crumbs'>"
            +                   "<h3>"
            +                       "<a href='{{crumb.url}}'>{{crumb.text}}</a>"
            +                       "<span class='divider' ng-show='notLast(crumb)'> / </span>"
            +                   "</h3>"
            +               "</li>"
            +           "</ul>"
            +       "</div>"
            +   "</div>"
    };
};