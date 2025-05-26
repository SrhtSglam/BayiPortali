public static class PermissionService
{
    // Bu örnekte role ID'lerine göre yetkiler atanıyor
    private static readonly Dictionary<int, List<string>> rolePermissions = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "siparisgec", "siparisonayla", "rapormizan" } },
        { 2, new List<string> { "siparisgec" } },
        { 3, new List<string> { "garanti", "raporekstre" } }
        // vb.
    };

    public static bool UserHasPermission(int role, string permission)
    {
        if (rolePermissions.ContainsKey(role))
        {
            return rolePermissions[role].Contains(permission);
        }

        return false;
    }

    public static List<string> GetPermissionsForUser(int role)
    {
        return rolePermissions.ContainsKey(role) ? rolePermissions[role] : new List<string>();
    }
}
