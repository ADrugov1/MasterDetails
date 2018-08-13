using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MasterDetails.Models;

namespace MasterDetails.Controllers
{
    public class BooksController : ApiController
    {
        private Models.Database db = new Models.Database();
        

        // GET: api/Books
        public IList<Book> GetBooks()
        {
            try
            {
                var books = db.Books.ToList();
                return books;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != book.Id)
                    {
                        return BadRequest();
                    }

                    var oldBook = db.Books.FirstOrDefault(s => s.Id == id);
                    oldBook.Title = book.Title;
                    oldBook.PublishingHouse = book.PublishingHouse;
                    oldBook.PublicationYear = book.PublicationYear;
                    oldBook.Pages = book.Pages;
                    oldBook.Image = book.Image;

                    oldBook.Authors = book.Authors;


                    db.SaveChanges();
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                using (var _db = new Models.Database())
                {
                    _db.Books.Add(book);
                    foreach (var author in book.Authors)
                    {
                        author.BookId = book.Id;
                        _db.Authors.Add(author);
                    }
                    _db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}