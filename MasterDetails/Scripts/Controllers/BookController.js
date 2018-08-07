// Share Data.
app.factory("ShareData", function () {
    return { value: 0 }
});

// Defining Routing.
app.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/', {
        templateUrl: '/Home/ShowBooks',
        controller: 'BookController'
    });

    $routeProvider.otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});

app.controller('BookController', function ($scope, $location, BookService, ShareData, $window) {
    $scope.error = null;
    $scope.Book = {
        Title: '',
        Pages: '',
        PublishingHouse: '',
        PublicationYear: '',
        Image: '',
        Authors: []
    };

    // Call to "getBooks" function.
    getBooks();

    // getBooks function.
    function getBooks() {
        BookService.getBooks()
            .then(function (book) {
                $scope.Books = book.data;
                console.log($scope.Books);
            }
                , function (error) {
                    $scope.error = 'Unable to load book data: ' + error.message;
                    console.log($scope.error);
                });
    };
});
