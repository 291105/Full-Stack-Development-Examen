using System;
using System.Collections.Generic;
using FlightProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightProject.Domain.Data;

public partial class FullStackDbContext : DbContext
{
    public FullStackDbContext()
    {
    }

    public FullStackDbContext(DbContextOptions<FullStackDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircraft { get; set; }

    public virtual DbSet<ArrivalPlace> ArrivalPlaces { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<DeparturePlace> DeparturePlaces { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightStop> FlightStops { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TransferPlace> TransferPlaces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL22_VIVES; Database=FullStackExamen; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.AircraftId).HasName("PK__Aircraft__F75CBC0B1830EC4C");

            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<ArrivalPlace>(entity =>
        {
            entity.HasKey(e => e.ArrivalId).HasName("PK__ArrivalP__2B1A513B85150E27");

            entity.ToTable("ArrivalPlace");

            entity.Property(e => e.ArrivalId).HasColumnName("ArrivalID");
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

            entity.HasOne(d => d.Place).WithMany(p => p.ArrivalPlaces)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKArrivalPla487541");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD12AAC51B");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TicketId).HasColumnName("TicketID");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking698796");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927A01634CCDF");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DeparturePlace>(entity =>
        {
            entity.HasKey(e => e.DepartureId).HasName("PK__Departur__8F8A7899925B7B0C");

            entity.ToTable("DeparturePlace");

            entity.Property(e => e.DepartureId).HasColumnName("DepartureID");
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

            entity.HasOne(d => d.Place).WithMany(p => p.DeparturePlaces)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKDepartureP945915");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flight__8A9E148EB74B7D40");

            entity.ToTable("Flight");

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.ArrivalId).HasColumnName("ArrivalID");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureId).HasColumnName("DepartureID");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");

            entity.HasOne(d => d.Arrival).WithMany(p => p.Flights)
                .HasForeignKey(d => d.ArrivalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFlight838934");

            entity.HasOne(d => d.Departure).WithMany(p => p.Flights)
                .HasForeignKey(d => d.DepartureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFlight14000");
        });

        modelBuilder.Entity<FlightStop>(entity =>
        {
            entity.HasKey(e => new { e.StopOrder, e.FlightId }).HasName("PK__FlightSt__88848E58E7198168");

            entity.ToTable("FlightStop");

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.TransferId).HasColumnName("TransferID");

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightStops)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFlightStop22395");

            entity.HasOne(d => d.Transfer).WithMany(p => p.FlightStops)
                .HasForeignKey(d => d.TransferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFlightStop288184");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.MealId).HasName("PK__Meal__ACF6A65DF25E4429");

            entity.ToTable("Meal");

            entity.Property(e => e.MealId).HasColumnName("MealID");
            entity.Property(e => e.Kind)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__Place__D5222B4E5FB5E60E");

            entity.ToTable("Place");

            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.SeasonId).HasName("PK__Season__C1814E18B1973222");

            entity.ToTable("Season");

            entity.Property(e => e.SeasonId).HasColumnName("SeasonID");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC627FA1790F8");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MealId).HasColumnName("MealID");
            entity.Property(e => e.NationalRegisterNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SeasonId).HasColumnName("SeasonID");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Aircraft).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.AircraftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTicket227166");

            entity.HasOne(d => d.Class).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTicket325293");

            entity.HasOne(d => d.Flight).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTicket680783");

            entity.HasOne(d => d.Meal).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTicket236030");

            entity.HasOne(d => d.Season).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTicket412156");
        });

        modelBuilder.Entity<TransferPlace>(entity =>
        {
            entity.HasKey(e => e.TransferId).HasName("PK__Transfer__9549017151D25586");

            entity.ToTable("TransferPlace");

            entity.Property(e => e.TransferId).HasColumnName("TransferID");
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

            entity.HasOne(d => d.Place).WithMany(p => p.TransferPlaces)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTransferPl966859");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
