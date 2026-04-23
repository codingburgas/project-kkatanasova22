using LibraryManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Data
{
    // Трябва да дефинираш: <User, Role, KeyType>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 1. Задължително извикване на base метода за Identity таблиците
            base.OnModelCreating(builder);

            // 2. Конфигурация за Книги (Books)
            builder.Entity<Book>(entity =>
            {
                // ISBN трябва да е уникален
                entity.HasIndex(b => b.ISBN).IsUnique();

                // Връзка Author -> Books (One-to-Many)
                entity.HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict); // Не трием автор, ако има книги

                // Връзка Category -> Books (One-to-Many)
                entity.HasOne(b => b.Category)
                    .WithMany(c => c.Books)
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict); // Не трием категория, ако има книги
            });

            // 3. Конфигурация за Заемания (Loans)
            builder.Entity<Loan>(entity =>
            {
                // Връзка Book -> Loans
                // Когато книга бъде изтрита, заеманията към нея се трият (Cascade)
                entity.HasOne(l => l.Book)
                    .WithMany()
                    .HasForeignKey(l => l.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Връзка ApplicationUser -> Loans
                // Промени това:
                // entity.HasOne(l => l.User).WithMany(u => u.Loans)...

                // На това:
                entity.HasOne(l => l.User)
                    .WithMany(u => u.Loans) // Сега EF ще търси свойството в ApplicationUser
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Валидация на датите (незадължително, но полезно)
                // Тук може да се добавят check constraints, ако базата поддържа
            });

            // 4. Конфигурация за Автори (Authors)
            builder.Entity<Author>(entity =>
            {
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
            });

            // 5. Конфигурация за Категории (Categories)
            builder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
                entity.HasIndex(c => c.Name).IsUnique(); // Имената на категориите да са уникални
            });
        }
    }
}