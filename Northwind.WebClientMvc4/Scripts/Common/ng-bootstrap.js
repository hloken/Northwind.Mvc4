var Common = Common || {};
Common.Bootstrap = Common.bootstrap || {};

Common.Bootstrap.AddButton = function () {
    return {
        restrict: "E",
        replace: true,
        scope: {
            text: "@",
            action: "&",
        },
        template: "<button class='btn btn-success' ng-click='action()'><i class='icon icon-white icon-plus-sign'></i> {{text}}</button>"
    };
};

Common.Bootstrap.RemoveButton = function () {
    return {
        restrict: "E",
        replace: true,
        transclude: true,
        scope: {
            text: "@",
            action: "&"
        },
        template: "<button class='btn btn-danger' ng-click='action()'><i class='icon icon-remove icon-white'></i> {{text}}</button>"
    };
};