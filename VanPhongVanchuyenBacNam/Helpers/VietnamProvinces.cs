namespace VanPhongVanchuyenBacNam.Helpers;

// The 63 provinces/cities of Vietnam (pre-2025-merger list), in the
// conventional Vietnamese order. Used by the transport company and
// shipment forms so province names stay consistent in the database.
public static class VietnamProvinces
{
    public static readonly IReadOnlyList<string> All = new[]
    {
        "An Giang",
        "Bà Rịa - Vũng Tàu",
        "Bạc Liêu",
        "Bắc Giang",
        "Bắc Kạn",
        "Bắc Ninh",
        "Bến Tre",
        "Bình Dương",
        "Bình Định",
        "Bình Phước",
        "Bình Thuận",
        "Cà Mau",
        "Cao Bằng",
        "Cần Thơ",
        "Đà Nẵng",
        "Đắk Lắk",
        "Đắk Nông",
        "Điện Biên",
        "Đồng Nai",
        "Đồng Tháp",
        "Gia Lai",
        "Hà Giang",
        "Hà Nam",
        "Hà Nội",
        "Hà Tĩnh",
        "Hải Dương",
        "Hải Phòng",
        "Hậu Giang",
        "Hòa Bình",
        "Hưng Yên",
        "Khánh Hòa",
        "Kiên Giang",
        "Kon Tum",
        "Lai Châu",
        "Lạng Sơn",
        "Lào Cai",
        "Lâm Đồng",
        "Long An",
        "Nam Định",
        "Nghệ An",
        "Ninh Bình",
        "Ninh Thuận",
        "Phú Thọ",
        "Phú Yên",
        "Quảng Bình",
        "Quảng Nam",
        "Quảng Ngãi",
        "Quảng Ninh",
        "Quảng Trị",
        "Sóc Trăng",
        "Sơn La",
        "Tây Ninh",
        "Thái Bình",
        "Thái Nguyên",
        "Thanh Hóa",
        "Thừa Thiên Huế",
        "Tiền Giang",
        "TP. Hồ Chí Minh",
        "Trà Vinh",
        "Tuyên Quang",
        "Vĩnh Long",
        "Vĩnh Phúc",
        "Yên Bái"
    };

    // Options for a form dropdown. If the record being edited holds a value
    // outside the official list (e.g. typed before this list existed),
    // prepend it so the form still shows and keeps that value.
    public static List<string> WithCurrent(string? current)
    {
        var provinces = All.ToList();
        if (!string.IsNullOrEmpty(current) && !provinces.Contains(current))
        {
            provinces.Insert(0, current);
        }
        return provinces;
    }
}
