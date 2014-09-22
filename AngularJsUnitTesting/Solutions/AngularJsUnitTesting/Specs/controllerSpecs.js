///<reference path="~/Scripts/jasmine/jasmine.js"/>
///<reference path="~/Scripts/angular.js"/>
///<reference path="~/Scripts/angular-mocks.js"/>

///<reference path="~/App/app.js"/>
///<reference path="~/App/controllers.js"/>
///<reference path="~/App/Controllers/dogController.js"/>

describe("Controllers", function() {

    beforeEach(module("controllers"));

    describe("Dog controller", function() {

        var scope,
            dog,
            controller;

        beforeEach(inject(function ($rootScope, $controller) {
            scope = $rootScope.$new();

            dog = {
                query: function() {
                    return [{ name: 'TEST', type: 'TEST' }];
                }
            }

            controller = $controller('DogController', { $scope: scope, Dog: dog });
        }));

        it('should set the page title as "Dogs"', function() {
            expect(scope.pageTitle).toBe('Dogs');
        });

        it('should have 1 dog', function () {
            expect(scope.dogs.length).toBe(1);
        });
    });
});
