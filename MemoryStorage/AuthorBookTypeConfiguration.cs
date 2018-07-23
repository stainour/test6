using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoryStorage
{
    internal class AuthorBookTypeConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            builder.HasKey(authorBook => new { authorBook.BookId, authorBook.AuthorId });

            builder
                .HasOne(sc => sc.Author)
                .WithMany(s => s.AuthorBooks)
                .HasForeignKey(sc => sc.AuthorId);

            builder
                .HasOne(sc => sc.Book)
                .WithMany(s => s.AuthorBooks)
                .HasForeignKey(sc => sc.BookId);
        }
    }
}