using Microsoft.AspNetCore.Identity;

namespace VanPhongVanchuyenBacNam.Data;

public static class SeedData
{
    public const string AdminUserName = "admin";

    // First-login password only — change it right after the first sign-in.
    private const string DefaultAdminPassword = "Admin@123";

    public static async Task EnsureAdminAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        if (await userManager.FindByNameAsync(AdminUserName) != null)
        {
            return;
        }

        var admin = new IdentityUser { UserName = AdminUserName };
        var result = await userManager.CreateAsync(admin, DefaultAdminPassword);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                "Cannot create the default admin account: " +
                string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
}
