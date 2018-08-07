// BookService.
app.service("BookService", function ($http) {

    // Get Books.
    this.getBooks = function (d) {
        //return $http.get('/api/BooksAPI');
        return $http({
            method: 'GET',
            url: '/api/Books',
            headers: { 'content-type': 'application/json' },
            dataType: 'json',
        })
    };

    // Add Book.
    this.addBook = function (book) {
        var request = $http({
            method: 'POST',
            url: '/api/Books',
            data: book
        });

        return request;
    }

    // Get Book.
    this.getBook = function (id) {
        return $http({
            method: 'GET',
            url: '/api/Books/' + id
            //data: { 'Id': d.Id },
        });
    };

    // Update Book.
    this.updateBook = function (id, Book) {
        return $http({
            method: 'PUT',
            url: '/api/Books/' + id,
            data: Book
        });
    };

    // Delete Book.
    this.deleteBook = function (id) {
        return $http({
            method: 'DELETE',
            url: '/api/Books/' + id
        });
    };

});