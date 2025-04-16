using System;

namespace PlaywrightTests.Utilities
{
    public static class DataGenerator
    {
        public static string GenerateRandomEmail()
        {
            return $"testuser_{Guid.NewGuid().ToString().Substring(0, 8)}@mail.com";
        }

        public static string GenerateRandomPassword()
        {
            return $"P@ssw0rd{new Random().Next(1000, 9999)}";
        }

        public static string GenerateRandomName()
        {
            return $"TestUser{new Random().Next(1000, 9999)}";
        }
    }
}
