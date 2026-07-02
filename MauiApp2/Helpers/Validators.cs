using System.Text.RegularExpressions;

namespace MauiApp2.Helpers
{
    public static class Validators
    {
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPassword(string? password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 4;
        }

        public static bool IsValidProductName(string? name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public static bool IsValidPrice(decimal price)
        {
            return price > 0;
        }

        public static bool IsValidQuantity(int quantity)
        {
            return quantity >= 0;
        }

        public static bool IsValidCategory(string? category)
        {
            return !string.IsNullOrWhiteSpace(category);
        }
    }
}
