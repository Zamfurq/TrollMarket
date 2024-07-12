using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrollMarket.DataAccess.Models
{
    public partial class TrollMarketContext : DbContext
    {
        public TrollMarketContext()
        {
        }

        public TrollMarketContext(DbContextOptions<TrollMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Merchandise> Merchandises { get; set; } = null!;
        public virtual DbSet<Shipment> Shipments { get; set; } = null!;
        public virtual DbSet<TransactionHistory> TransactionHistories { get; set; } = null!;
        public virtual DbSet<UserDetail> UserDetails { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Account__536C85E5F0EE3645");

                entity.ToTable("Account");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.BuyerName, e.MerchaniseId })
                    .HasName("PK__Cart__DF66A4DCB71DBD3C");

                entity.ToTable("Cart");

                entity.Property(e => e.BuyerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MerchaniseId).HasColumnName("MerchaniseID");

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.HasOne(d => d.BuyerNameNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.BuyerName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__BuyerName__619B8048");

                entity.HasOne(d => d.Merchanise)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.MerchaniseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__Merchanise__628FA481");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ShipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__ShipmentID__6383C8BA");
            });

            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.ToTable("Merchandise");

                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseID");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SellerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SellerNameNavigation)
                    .WithMany(p => p.Merchandises)
                    .HasForeignKey(d => d.SellerName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Merchandi__Selle__5070F446");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ShipperName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__55433A4BFF75C993");

                entity.ToTable("TransactionHistory");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.BuyerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseID");

                entity.Property(e => e.SellerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.Property(e => e.ShipmentPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.BuyerNameNavigation)
                    .WithMany(p => p.TransactionHistoryBuyerNameNavigations)
                    .HasForeignKey(d => d.BuyerName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Buyer__6FE99F9F");

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.TransactionHistories)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Merch__70DDC3D8");

                entity.HasOne(d => d.SellerNameNavigation)
                    .WithMany(p => p.TransactionHistorySellerNameNavigations)
                    .HasForeignKey(d => d.SellerName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Selle__6EF57B66");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.TransactionHistories)
                    .HasForeignKey(d => d.ShipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Shipm__71D1E811");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__UserDeta__536C85E5720E74E7");

                entity.ToTable("UserDetail");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithOne(p => p.UserDetail)
                    .HasForeignKey<UserDetail>(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDetai__Usern__4D94879B");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Role })
                    .HasName("PK__UserRole__BECDD1F660B5EE8A");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRoles__Usern__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
