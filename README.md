# BookAPI

## Author Endpoints

| Endpoint                | Description                             | Request Body
| ----------------------- | --------------------------------------- | -------------- 
| Get/api/authors         | Gets all authors                        | N/A
| Get/api/authors/{id}    | Gets the author with the given id       | N/A
| Put/api/authors/{id}    | Updates the author with the given id    | Author Object
| Post/api/authors        | Creates a new author                    | Author Object
| Delete/api/authors/{id} | Deletes the author with the given id    | N/A

Author Object = { firstname, lastname }

### Author Sample Response Body

{
  statusCode,
  statusDescription,
  Authors
}

## Book Endpoints

| Endpoint                | Description                             | Request Body
| ----------------------- | --------------------------------------- | --------------
| Get/api/books           | Gets all books                          | N/A
| Get/api/books/{id}      | Gets the book with the given id         | N/A
| Put/api/books/{id}      | Updates the book with the given id      | Book Object
| Post/api/books          | Creates a new book                      | Book Object
| Delete/api/books/{id}   | Deletes the book with the given id      | N/A

Book Object = { title, authorid || new author object }

### Book Sample Response Body

{
  statusCode,
  statusDescription,
  Books
}
