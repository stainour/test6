using System;
using System.Collections.Generic;

namespace Domain
{
    public class Book
    {
        private List<AuthorBook> _authorBooks = new List<AuthorBook>();

        public Book(string title, int pageCount, string isbn = default, DateTime? issueDate = default, string publisher = default, string image = null)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            PageCount = pageCount;
            ISBN = isbn;
            IssueDate = issueDate;
            Publisher = publisher;
            Image = image;
        }

        internal Book()
        {
        }

        public IReadOnlyCollection<AuthorBook> AuthorBooks => _authorBooks.AsReadOnly();
        public int Id { get; private set; }
        public string Image { get; private set; }
        public string ISBN { get; private set; }
        public DateTime? IssueDate { get; private set; }
        public int PageCount { get; private set; }
        public string Publisher { get; private set; }
        public string Title { get; private set; }

        public void AddAuthor(Author author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));
            _authorBooks.Add(new AuthorBook(Id, author.Id));
        }
    }
}