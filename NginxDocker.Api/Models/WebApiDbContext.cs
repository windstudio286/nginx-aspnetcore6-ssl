    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    namespace NginxDocker.Api.Models
    {
        public partial class WebApiDbContext : DbContext
        {
            public WebApiDbContext()
            {
            }

            public WebApiDbContext(DbContextOptions<WebApiDbContext> options)
                : base(options)
            {
            }
            public virtual DbSet<Product> Products { get; set; } = null!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=congtt;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NumericExample>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("numeric_example");

                entity.Property(e => e.DecimalColumn)
                    .HasPrecision(10, 2)
                    .HasColumnName("decimal_column");

                entity.Property(e => e.RealColumn).HasColumnName("real_column");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("product");

                entity.Property(e => e.Currency).HasColumnName("currency");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .HasColumnType("character varying")
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("product_price");
            });

            OnModelCreatingPartial(modelBuilder);
        }*/

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
