using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VanPhongVanchuyenBacNam.Helpers;

public static class FormatExtensions
{
    private static readonly CultureInfo Vietnamese = CultureInfo.GetCultureInfo("vi-VN");

    // VND is always displayed as whole numbers with Vietnamese separators: 1.500.000 đ
    public static string ToVnd(this decimal value)
    {
        return value.ToString("N0", Vietnamese) + " đ";
    }

    // Reads the [Display(Name = ...)] attribute of an enum member (e.g. Vietnamese status names).
    public static string GetDisplayName(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        var display = member?.GetCustomAttribute<DisplayAttribute>();
        return display?.Name ?? value.ToString();
    }
}
