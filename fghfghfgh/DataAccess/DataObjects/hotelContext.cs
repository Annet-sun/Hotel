using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hotel.DataAccess.DataObjects
{
    public partial class hotelContext : DbContext
    {
        public hotelContext()
        {
        }

        public hotelContext(DbContextOptions<hotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Room> Room { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=hotel");
            }
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.IdAdministrator)
                    .HasName("PRIMARY");

                entity.ToTable("administrator");

                entity.Property(e => e.IdAdministrator).HasColumnName("id_administrator");

                entity.Property(e => e.AdministratorFam)
                    .IsRequired()
                    .HasColumnName("administrator_fam")
                    .HasMaxLength(45);

                entity.Property(e => e.AdministratorIo)
                    .IsRequired()
                    .HasColumnName("administrator_io")
                    .HasMaxLength(45);

                entity.Property(e => e.AdministratorPassword)
                    .HasColumnName("administrator_password")
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("PRIMARY");

                entity.ToTable("client");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.ClientFam)
                    .IsRequired()
                    .HasColumnName("client_fam")
                    .HasMaxLength(45);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasColumnName("client_name")
                    .HasMaxLength(45);

                entity.Property(e => e.ClientOtch)
                    .HasColumnName("client_otch")
                    .HasMaxLength(45);

                entity.Property(e => e.ClientPassport)
                    .IsRequired()
                    .HasColumnName("client_passport")
                    .HasMaxLength(11);

                entity.Property(e => e.ClientTelephone)
                    .HasColumnName("client_telephone")
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.IdReservation)
                    .HasName("PRIMARY");

                entity.ToTable("reservation");

                entity.HasIndex(e => e.AdministratorIdAdministrator)
                    .HasName("fk_reservation_administrator1_idx");

                entity.HasIndex(e => e.ClientIdClient)
                    .HasName("fk_reservation_client1_idx");

                entity.HasIndex(e => e.RoomIdRoom)
                    .HasName("fk_reservation_room1_idx");

                entity.Property(e => e.IdReservation).HasColumnName("id_reservation");

                entity.Property(e => e.AdministratorIdAdministrator).HasColumnName("administrator_id_administrator");

                entity.Property(e => e.ClientIdClient).HasColumnName("client_id_client");

                entity.Property(e => e.ReservationDateIn)
                    .HasColumnName("reservation_date_in")
                    .HasColumnType("date");

                entity.Property(e => e.ReservationDateOut)
                    .HasColumnName("reservation_date_out")
                    .HasColumnType("date");

                entity.Property(e => e.RoomIdRoom).HasColumnName("room_id_room");

                entity.HasOne(d => d.AdministratorIdAdministratorNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.AdministratorIdAdministrator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservation_administrator1");

                entity.HasOne(d => d.ClientIdClientNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.ClientIdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservation_client1");

                entity.HasOne(d => d.RoomIdRoomNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.RoomIdRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservation_room1");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdRoom)
                    .HasName("PRIMARY");

                entity.ToTable("room");

                entity.HasIndex(e => e.CategoryIdCategory)
                    .HasName("fk_room_category_idx");

                entity.Property(e => e.IdRoom).HasColumnName("id_room");

                entity.Property(e => e.CategoryIdCategory).HasColumnName("category_id_category");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.RoomKolmest).HasColumnName("room_kolmest");

                entity.HasOne(d => d.CategoryIdCategoryNavigation)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.CategoryIdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_room_category");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
