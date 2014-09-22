///<reference path="~/Scripts/jasmine/jasmine.js"/>
///<reference path="~/Scripts/angular.js"/>
///<reference path="~/Scripts/angular-mocks.js"/>

///<reference path="~/App/app.js"/>
///<reference path="~/App/services.js"/>
///<reference path="~/App/Services/dogService.js"/>

describe("Services", function() {

    beforeEach(module("services"));

    describe("Dog service", function() {

        var dog;

        beforeEach(inject(function($injector) {
            dog = $injector.get('Dog');
        }));

        it('should return 3 dogs when querying', function() {
            expect(dog.query().length).toBe(3);
        });

        it('should return 4 dogs when querying after adding a dog', function() {
            dog.add({ name: 'Fido', type: 'German Shepherd' });
            expect(dog.query().length).toBe(4);
        });
    });
});