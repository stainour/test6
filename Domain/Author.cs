using System;
using System.Collections.Generic;

namespace Domain
{
    public class Author
    {
        private List<AuthorBook> _authorBooks = new List<AuthorBook>();

        public Author(string firstName, string lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        internal Author()
        {
        }

        public IReadOnlyCollection<AuthorBook> AuthorBooks => _authorBooks.AsReadOnly();
        public string FirstName { get; private set; }
        public int Id { get; private set; }
        public string LastName { get; private set; }

        public void UpdateNames(string firstName, string lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }
    }
}