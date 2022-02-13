
using Client.Static;
using Shared.Models;
using System.Net.Http.Json;

namespace Client.Services
{
    internal sealed class InMemoryDatabaseCache
    {
        private HttpClient _httpClient;

        public InMemoryDatabaseCache(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private List<Category> _categories = null;
        private List<Car> _cars = null;

        internal List<Category> Categories
        {
            get { return _categories; }

            set
            {
                _categories = value;

                NotifyChange();
            }

        }

        internal List<Car> Cars
        {
            get { return _cars; }

            set
            {
                _cars = value;

                NotifyChange();
            }

        }

        private bool _GetCatCache = false;   //to allow only one get req
        private bool _GetCarCache = false;


        internal async Task GetCatCache()
        {
            if (_GetCatCache == false)
            {
                _GetCatCache = true;  //lock
                _categories = await _httpClient.GetFromJsonAsync<List<Category>>(APIEndpoints.s_categories);
                _GetCatCache = false;  //unlock
            }


        }

        internal async Task GetCarCache()
        {

            if (_GetCarCache == false)
            {
                _GetCarCache = true;  //lock
                _cars = await _httpClient.GetFromJsonAsync<List<Car>>(APIEndpoints.s_cars);
                _GetCarCache = false;  //unlock
                Console.WriteLine("in herere" + _cars.Count);
            }


        }



        internal event Action onCatChange;
        internal event Action onCarChange;
        private void NotifyChange()
        {
            onCatChange?.Invoke();
            onCarChange?.Invoke();
        }
    }
}
