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
2. Nhấn **F5** (hoặc **Ctrl+F5**) để chạy. Ứng dụng tự áp dụng migration vào LocalDB
   và nạp dữ liệu mẫu khi khởi động, nên không cần chạy lệnh gì trước.
3. Đăng nhập với tài khoản mặc định:
   - Tên đăng nhập: `admin`
   - Mật khẩu: `Admin@123`

   **Đổi mật khẩu ngay sau lần đăng nhập đầu** (menu góc phải → Đổi mật khẩu).

## Lệnh EF Core hay dùng (Package Manager Console)

| Lệnh | Ý nghĩa |
|------|---------|
| `Add-Migration TenMigration` | Tạo migration mới sau khi sửa model |
| `Update-Database` | Áp dụng các migration vào database (tùy chọn — app cũng tự chạy khi khởi động) |

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
- [x] Phase 2 — Vận đơn: CRUD, tự sinh mã vận đơn (VPyyyyMMdd-NNNN), chọn nhà xe
- [x] Phase 3 — In nhãn 100×100mm: barcode CODE128 (JsBarcode), in 1 hoặc nhiều nhãn
- [x] Phase 4 — Trạng thái đơn, tra cứu nhanh, dashboard tổng quan
- [x] Phase 5 — Đăng nhập admin (Identity), khách gửi quen + tự điền, đối soát COD theo nhà xe
