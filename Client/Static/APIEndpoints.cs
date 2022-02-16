﻿namespace Client.Static
{
    internal static class APIEndpoints
    {

#if DEBUG
        // do 
        internal const string SererBaseUrl = "https://localhost:5003";

#else
        live
        internal const string SererBaseUrl = "https://year4pipev5.azurewebsites.net/index.html";

#endif

        internal static readonly string s_categories = $"{SererBaseUrl}/api/categories";
        internal static readonly string s_cars = $"{SererBaseUrl}/api/cars";

    }
}
