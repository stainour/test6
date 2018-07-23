using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryStorage
{
    internal class AuthorTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(author => author.Id);
            builder.Metadata.FindNavigation(nameof(Author.AuthorBooks)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(author => author.AuthorBooks).WithOne(authorBook => authorBook.Author).IsRequired();
        }
    }
}