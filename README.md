# Văn phòng Vận chuyển Bắc Nam

Web app quản trị cho văn phòng nhận vận chuyển hàng hóa & bưu kiện nội địa Việt Nam:
danh bạ nhà xe theo tỉnh, quản lý vận đơn và in nhãn dán kiện hàng.

## Công nghệ

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core — Code First + Migrations
- SQL Server LocalDB (môi trường phát triển)
- Razor Views + Bootstrap 5

## Chạy lần đầu (Visual Studio)

1. Mở file `VanPhongVanchuyenBacNam.sln`.
2. Mở **Tools → NuGet Package Manager → Package Manager Console**, chạy:
   ```
   Update-Database
   ```
   Lệnh này tạo database trên LocalDB và nạp sẵn dữ liệu nhà xe mẫu.
3. Nhấn **F5** (hoặc **Ctrl+F5**) để chạy ứng dụng.

## Lệnh EF Core hay dùng (Package Manager Console)

| Lệnh | Ý nghĩa |
|------|---------|
| `Add-Migration TenMigration` | Tạo migration mới sau khi sửa model |
| `Update-Database` | Áp dụng các migration vào database |

## Cấu trúc chính

```
VanPhongVanchuyenBacNam/
├── Models/        # Entity + validation (Data Annotations)
├── Data/          # AppDbContext + seed data
├── Controllers/   # MVC controllers
├── Views/         # Razor views (giao diện tiếng Việt)
└── Migrations/    # EF Core migrations
```

## Lộ trình

- [x] Phase 0 — Khởi tạo project, EF Core + LocalDB, layout Bootstrap 5, seed dữ liệu mẫu
- [x] Phase 1 — Danh bạ nhà xe: CRUD + tìm kiếm/lọc theo tỉnh & tên
- [ ] Phase 2 — Vận đơn: CRUD, tự sinh mã vận đơn, chọn nhà xe
- [ ] Phase 3 — In nhãn dán kiện hàng (barcode + CSS in)
- [ ] Phase 4 — Trạng thái đơn, tra cứu nhanh, dashboard
- [ ] Phase 5 — Đăng nhập admin, khách gửi quen, báo cáo
