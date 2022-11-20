using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using LibraryAPI.Models;

namespace LibraryAPI.Models
{
    public class LibraryAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public LibraryAPIDBContext(DbContextOptions<LibraryAPIDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("library");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
    }
}
