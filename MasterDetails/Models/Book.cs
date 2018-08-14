namespace MasterDetails.Models
{
    using System.Collections.Generic;

    public class Book
    {
        public virtual int Id { get; set; }
        
        public virtual string Title { get; set; }

        public virtual int Pages { get; set; }

        public virtual string PublishingHouse { get; set; }

        public virtual int? PublicationYear { get; set; }

        public virtual byte[] Image { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual void Add(Author author)
        {
            if (Authors == null)
            {
                Authors = new List<Author>();
            }

            author.Book = this;
            Authors.Add(author);
        }
    }
}
