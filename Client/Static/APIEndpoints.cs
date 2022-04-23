namespace Client.Static
{
    public static class APIEndpoints
    {

#if DEBUG
      
        internal const string SererBaseUrl = "https://localhost:5003";

#else
        
        internal const string SererBaseUrl = "https://year4pipev5.azurewebsites.net";

#endif

        internal static readonly string s_categories = $"{SererBaseUrl}/api/categories";
        internal static readonly string s_cars = $"{SererBaseUrl}/api/cars";
        internal static readonly string s_bookings = $"{SererBaseUrl}/api/bookings";
        internal static readonly string s_bookingsDTO = $"{SererBaseUrl}/api/bookings/dto";
        internal readonly static string s_categoriesWithCars = $"{SererBaseUrl}/api/categories/withcars";
        internal readonly static string s_carsDTO = $"{SererBaseUrl}/api/cars/dto";
        internal readonly static string s_signIn = $"{SererBaseUrl}/api/signin";
        internal readonly static string s_userRegister = $"{SererBaseUrl}/api/user/register";
        internal static readonly string s_book = $"{SererBaseUrl}/api/bookings/book";
        internal static readonly string s_bookEmail = $"{SererBaseUrl}/api/bookings/email";
        internal readonly static string s_users = $"{SererBaseUrl}/api/user/";
        internal readonly static string s_userupdate = $"{SererBaseUrl}/api/user/update";
        internal readonly static string s_email = $"{SererBaseUrl}/api/email/password";
    }
}
