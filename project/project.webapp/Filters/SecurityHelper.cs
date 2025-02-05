using Microsoft.AspNetCore.Identity;

namespace project.webapp.Filters{
    public static class SecurityHelper
    {
        private static readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        public static string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            try
            {
                var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
                return result == PasswordVerificationResult.Success;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

