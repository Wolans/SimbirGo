using Microsoft.EntityFrameworkCore;

using SimbirGo.Contracts.Models;

namespace SimbirGo.Bll.DbConfiguration;

public sealed class SimbirGoDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transport> Transports { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public SimbirGoDbContext(DbContextOptions<SimbirGoDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("user_pkey");

            entity.ToTable(nameof(Users));

            entity.Property(e => e.UserID).IsRequired().HasColumnName("UserID");
            entity.Property(e => e.UserName).IsRequired().HasColumnName("Username");
            entity.Property(e => e.PasswordHash).IsRequired().HasColumnName("PasswordHash");
            entity.Property(e => e.Email).IsRequired().HasColumnName("Email");
            entity.Property(e => e.Role).IsRequired().HasColumnName("Role");
            entity.Property(e => e.CreatedAt).IsRequired().HasColumnName("CreatedAt");
            entity.Property(e => e.UpdatedAt).IsRequired().HasColumnName("UpdatedAt");

            entity.Ignore(e => e.Rentals);
            entity.Ignore(e => e.Payments);
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.TransportID).HasName("transport_pkey");

            entity.ToTable(nameof(Transports));

            entity.Property(e => e.TransportID).IsRequired().HasColumnName("TransportID");
            entity.Property(e => e.TransportType).IsRequired().HasColumnName("TransportType");
            entity.Property(e => e.Model).IsRequired().HasColumnName("Model");
            entity.Property(e => e.RegistrationNumber).IsRequired().HasColumnName("RegistrationNumber");
            entity.Property(e => e.Availability).IsRequired().HasColumnName("Availability");
            entity.Property(e => e.PricePerMinute).IsRequired().HasColumnName("PricePerMinute");
            entity.Property(e => e.PricePerDay).IsRequired().HasColumnName("PricePerDay");

            entity.Ignore(e => e.Rentals);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalID).HasName("rental_pkey");

            entity.ToTable(nameof(Rentals));

            entity.Property(e => e.RentalID).IsRequired().HasColumnName("RentalID");
            entity.Property(e => e.UserID).IsRequired().HasColumnName("UserID");
            entity.Property(e => e.TransportID).IsRequired().HasColumnName("TransportID");
            entity.Property(e => e.TimeStart).IsRequired().HasColumnName("TimeStart");
            entity.Property(e => e.TimeEnd).IsRequired().HasColumnName("TimeEnd");
            entity.Property(e => e.PriceOfUnit).IsRequired().HasColumnName("PriceOfUnit");
            entity.Property(e => e.PriceType).IsRequired().HasColumnName("PriceType");
            entity.Property(e => e.FinalPrice).IsRequired().HasColumnName("FinalPrice");

            entity.Ignore(e => e.User);
            entity.Ignore(e => e.Transport);
            entity.Ignore(e => e.Payment);


            entity.HasOne(d => d.User).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("rental_id_user_fkey");

            entity.HasOne(d => d.Transport).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.TransportID)
                .HasConstraintName("rental_id_transport_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentID).HasName("payment_pkey");

            entity.ToTable(nameof(Payments));

            entity.Property(e => e.PaymentID).IsRequired().HasColumnName("PaymentID");
            entity.Property(e => e.UserID).IsRequired().HasColumnName("UserID");
            entity.Property(e => e.RentalID).IsRequired().HasColumnName("RentalID");
            entity.Property(e => e.Amount).IsRequired().HasColumnName("Amount");
            entity.Property(e => e.PaymentDate).IsRequired().HasColumnName("PaymentDate");
            entity.Property(e => e.Status).IsRequired().HasColumnName("Status");

            entity.Ignore(e => e.User);
            entity.Ignore(e => e.Rental);

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("payment_id_user_fkey");

            entity.HasOne(d => d.Rental).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.RentalID)
                .IsRequired()
                .HasConstraintName("payment_id_rental_fkey");
        });
    }
}