namespace ZaminEducation.Api.Helpers;

#pragma warning disable
public static class CustomRoles
{
    private const string Admin = "Admin";
    private const string Mentor = "Mentor";
    private const string User = "User";

    public const string AllRoles = UserRole + "," + Mentor;
    public const string UserRole = User + "," + AdminRole;
    public const string MentorRole = Mentor + "," + AdminRole;
    public const string AdminRole = Admin + "," + SuperAdminRole;
    public const string SuperAdminRole = "SuperAdmin";
}
