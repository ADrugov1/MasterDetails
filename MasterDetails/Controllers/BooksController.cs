using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MasterDetails.Models;
using NHibernate;

namespace MasterDetails.Controllers
{
    public class BooksController : ApiController
    {    
        // GET: api/Books
        public IList<Book> GetBooks()
        {
            try
            {
                using (ISession session = HibernateHelper.OpenSession())
                {
                    var queryResult = session.QueryOver<Book>().List();
                    return queryResult;
                }
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
            using (ISession session = HibernateHelper.OpenSession())
            {
                var book = session.Query<Book>().FirstOrDefault(x => x.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }          
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ISession session = HibernateHelper.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Update(book);
                            transaction.Commit();
                        }
                    }
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
                using (ISession session = HibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        foreach (var author in book.Authors)
                        {
                            author.Book = book;
                        }
                        session.Save(book);
                        transaction.Commit();
                    }
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
            using (ISession session = HibernateHelper.OpenSession())
            {
                var book = session.QueryOver<Book>().Where(x => x.Id == id).SingleOrDefault();

                if (book == null)
                {
                    return NotFound();
                }

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(book);
                    transaction.Commit();
                }

                return Ok(book);
            }
        }
    }
}