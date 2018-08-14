namespace MasterDetails.Models
{
    public class Author
    {
        public virtual int Id { get; set; }

        public virtual int BookId { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual Book Book { get; set; }
    }
}
