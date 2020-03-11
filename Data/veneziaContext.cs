using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using venezia.Models;

namespace venezia.Data
{
    public partial class veneziaContext : DbContext
    {
        public veneziaContext()
        {
        }

        public veneziaContext(DbContextOptions<veneziaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Menuitem> Menuitem { get; set; }
        public virtual DbSet<Section> Section { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=venezia;password=StArmands1966;database=venezia", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logins>(entity =>
            {
                entity.ToTable("logins");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasColumnType("varchar(25)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(25)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Menuitem>(entity =>
            {
                entity.ToTable("menuitem");

                entity.HasIndex(e => e.MenuId)
                    .HasName("menuId");

                entity.HasIndex(e => e.SectionId)
                    .HasName("sectionId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MenuId).HasColumnName("menuId");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SectionId).HasColumnName("sectionId");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Menuitem)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("menuitem_ibfk_1");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Menuitem)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("menuitem_ibfk_2");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("section");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Tagline)
                    .HasColumnName("tagline")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
