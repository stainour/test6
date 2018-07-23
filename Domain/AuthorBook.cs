using System;

namespace Domain
{
    public class AuthorBook : IEquatable<AuthorBook>
    {
        public AuthorBook(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }

        internal AuthorBook()
        {
        }

        public Author Author { get; private set; }
        public int AuthorId { get; private set; }
        public Book Book { get; private set; }
        public int BookId { get; private set; }

        public bool Equals(AuthorBook other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AuthorId == other.AuthorId && BookId == other.BookId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AuthorBook)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (AuthorId * 397) ^ BookId;
            }
        }
    }
}