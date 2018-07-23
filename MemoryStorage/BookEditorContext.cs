using Domain;
using Microsoft.EntityFrameworkCore;

namespace MemoryStorage
{
    public class BookEditorContext : DbContext
    {
        public BookEditorContext()
        {
        }

        public BookEditorContext(DbContextOptions<BookEditorContext> options) : base(options)
        {
        }

        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorBookTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}