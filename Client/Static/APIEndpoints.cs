namespace Client.Static
{
    internal static class APIEndpoints
    {

#if DEBUG
      
        internal const string SererBaseUrl = "https://localhost:5003";

#else
        
        internal const string SererBaseUrl = "https://year4pipev5.azurewebsites.net/";

#endif

        internal static readonly string s_categories = $"{SererBaseUrl}/api/categories";
        internal static readonly string s_cars = $"{SererBaseUrl}/api/cars";

    }
}
