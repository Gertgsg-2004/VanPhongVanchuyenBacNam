using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VanPhongVanchuyenBacNam.Models;

namespace VanPhongVanchuyenBacNam.Data;

// IdentityDbContext adds the user/role tables for admin login on top of our own entities.
public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TransportCompany> TransportCompanies => Set<TransportCompany>();

    public DbSet<Shipment> Shipments => Set<Shipment>();

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // One saved customer per phone number, so autofill is never ambiguous.
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Phone)
            .IsUnique();

        modelBuilder.Entity<Shipment>(entity =>
        {
            // Tracking codes must never repeat; the unique index is the safety net
            // even if the generator ever produced a duplicate.
            entity.HasIndex(s => s.TrackingCode).IsUnique();

            // VND amounts are whole numbers; weight keeps 2 decimals (kg).
            entity.Property(s => s.ShippingFee).HasColumnType("decimal(18,0)");
            entity.Property(s => s.CODAmount).HasColumnType("decimal(18,0)");
            entity.Property(s => s.Weight).HasColumnType("decimal(8,2)");

            // Deleting a transport company must not delete its shipments.
            entity.HasOne(s => s.TransportCompany)
                .WithMany()
                .HasForeignKey(s => s.TransportCompanyId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Seed sample companies so the app has data right after the first Update-Database.
        modelBuilder.Entity<TransportCompany>().HasData(
            new TransportCompany
            {
                Id = 1,
                Name = "Nhà xe Hoàng Long",
                Province = "Hải Phòng",
                PhoneNumber = "0225 392 0920",
                BackupPhone = "0912 245 245",
                Address = "Bến xe Vĩnh Niệm, Hải Phòng",
                ContactPerson = "Anh Long",
                Note = "Nhận hàng cồng kềnh",
                IsActive = true
            },
            new TransportCompany
            {
                Id = 2,
                Name = "Nhà xe Văn Minh",
                Province = "Nghệ An",
                PhoneNumber = "0238 383 8383",
                Address = "Bến xe Vinh, Nghệ An",
                ContactPerson = "Chị Hoa",
                IsActive = true
            },
            new TransportCompany
            {
                Id = 3,
                Name = "Nhà xe Phương Trang",
                Province = "TP. Hồ Chí Minh",
                PhoneNumber = "0283 838 6852",
                Address = "Bến xe Miền Đông, TP. Hồ Chí Minh",
                IsActive = true
            },
            new TransportCompany
            {
                Id = 4,
                Name = "Nhà xe Hải Âu",
                Province = "Hải Phòng",
                PhoneNumber = "0225 374 7474",
                ContactPerson = "Anh Tuấn",
                Note = "Tuyến Hà Nội - Hải Phòng, nhiều chuyến mỗi ngày",
                IsActive = true
            },
            new TransportCompany
            {
                Id = 5,
                Name = "Nhà xe Quang Hạnh",
                Province = "Khánh Hòa",
                PhoneNumber = "0258 381 2812",
                Address = "Bến xe phía Nam Nha Trang",
                IsActive = true
            },
            new TransportCompany
            {
                Id = 6,
                Name = "Nhà xe Ba Miền",
                Province = "Đà Nẵng",
                PhoneNumber = "0236 362 6262",
                Note = "Tạm ngừng hợp tác từ 5/2026",
                IsActive = false
            }
        );
    }
}
