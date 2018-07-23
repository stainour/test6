using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryStorage
{
    internal class BookTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.Id);

            builder.Metadata.FindNavigation(nameof(Book.AuthorBooks)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(book => book.AuthorBooks).WithOne(authorBook => authorBook.Book).IsRequired();
        }
    }
}