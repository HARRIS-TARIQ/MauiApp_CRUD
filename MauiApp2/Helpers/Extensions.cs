namespace MauiApp2.Helpers
{
    public static class Extensions
    {
        public static string ToCurrency(this decimal value)
        {
            return value.ToString("C2");
        }

        public static string ToShortDate(this DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }

        public static bool IsNullOrEmptyList<T>(this List<T>? list)
        {
            return list == null || list.Count == 0;
        }
    }
}
