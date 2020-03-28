using FileBagWebApi.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FileBagWebApi.DataAccess.Context
{
    public class FileBagContext : DbContext
    {

        public DbSet<Application> Applications { get; set; }

        public DbSet<FileDetail> FileDetails { get; set; }

        public DbSet<FileElement> FileElements { get; set; }

        public FileBagContext() : base()
        {
        }

        public FileBagContext(DbContextOptions<FileBagContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=FileBagWebApi.Dev;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileDetail>()
                .ToTable("FileDetail", "filebag");

            modelBuilder.Entity<FileElement>()
                .ToTable("FileElement", "filebag");

            modelBuilder.Entity<Application>()
                .ToTable("Application", "filebag");

           /*modelBuilder.Entity<FileElement>()
                .Property(e => e.Application)
                .IsRequired();*/

            modelBuilder.Entity<FileElement>()
                .Property(e => e.EntityType)
                .HasMaxLength(96)
                .IsRequired();

            modelBuilder.Entity<FileElement>()
                .Property(e => e.Name)
                .HasMaxLength(96)
                .IsRequired();

            modelBuilder.Entity<FileElement>()
                .Property(e => e.MimeType)
                .HasMaxLength(96)
                .IsRequired();

            modelBuilder.Entity<FileElement>()
                .Property(e => e.Creator)
                .HasMaxLength(32)
                .IsRequired();

            modelBuilder.Entity<FileElement>()
                .Property(e => e.CreationDate)
                .IsRequired();

            modelBuilder.Entity<FileElement>()
                .Property(e => e.Modifier)
                .HasMaxLength(32);

            modelBuilder.Entity<FileElement>()
                .Property(e => e.ModificationDate);

            modelBuilder.Entity<FileElement>()
                .Property(e => e.Status)
                .HasColumnType("tinyint");

            modelBuilder.Entity<Application>()
                .Property(e => e.Name)
                .HasMaxLength(96)
                .IsRequired();

            modelBuilder.Entity<Application>()
                .Property(e => e.URI)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Application>()
                .HasAlternateKey(a => a.URI);

        }
    }
}
