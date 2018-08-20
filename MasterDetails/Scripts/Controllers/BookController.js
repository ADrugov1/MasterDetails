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

    $routeProvider.when('/add', {
        templateUrl: '/Home/AddBook',
        controller: 'BookController'
    });

    $routeProvider.when('/edit', {
        templateUrl: '/Home/EditBook',
        controller: 'BookController'
    });

    $routeProvider.when('/delete', {
        templateUrl: '/Home/DeleteBook',
        controller: 'BookController'
    });

    $routeProvider.otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});

app.controller('BookController', function ($scope, $location, BookService, ShareData, $window) {
    $scope.add = function () {
        $location.path("/add");
    };
    $scope.edit = function (id) {
        ShareData.value = id;
        $location.path('/edit');
    }

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

    // Add Book function.
    $scope.addBook = function () {
        BookService.addBook($scope.Book)
            .then(function (book) {
                $scope.Book = book;
                console.log($scope.Book);
                $location.url('/');
            },
                function (error) {
                    $scope.error = 'Unable to add book data: ' + error.message;
                    console.log($scope.error);
                });
    };

    // Add Author.
    $scope.addAuthor = function () {
        $scope.Book.Authors.push({ "BookId": $scope.Book.Id, "FirstName": $scope.FirstName, "LastName": $scope.LastName });
        $scope.FirstName = '';
        $scope.LastName = '';
    };

    // Delete Comment.
    $scope.deleteAuthor = function (index) {
        $scope.Book.Authors.splice(index, 1);
    };

    // Get Book by Id.
    $scope.getBook = function () {
        BookService.getBook(ShareData.value)
            .then(function (book) {
                    $scope.Book = book.data;
                    console.log($scope.Book);
            },
                function (error) {
                    $scope.error = 'Unable to load book data: ' + error.message;
                    console.log($scope.error);
                });
    };

    // Edit Book Function.
    $scope.editBook = function () {
        var Book = {
            Id: $scope.Book.Id,
            Title: $scope.Book.Title,
            Pages: $scope.Book.Pages,
            PublishingHouse: $scope.Book.PublishingHouse,
            PublicationYear: $scope.Book.PublicationYear,
            Image: $scope.Book.Image
        };

        BookService.updateBook($scope.Book.Id, $scope.Book)
            .then(function (book) {
                $scope.Book = book;
                console.log($scope.Book);
                $location.url('/');
            },
                function (error) {
                    $scope.error = 'Unable to load book data: ' + error.message;
                    console.log($scope.error);
                });
    };

    // Set the route to navigate.
    $scope.delete = function (id) {
        ShareData.value = id;
        $location.path('/delete');
    }

    // Delete Book Function
    $scope.deleteBook = function () {
        BookService.deleteBook($scope.Book.Id)
            .then(function (book) {
                $scope.Book = book.data;
                console.log($scope.Book);
                    $location.url('/');
                },
                function (error) {
                    $scope.error = 'Unable to delete book data: ' + error.message;
                    console.log($scope.error);
                });
    };

});
