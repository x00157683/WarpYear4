
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

    

        #region Categories        

        private List<Category> _categories = null;
        internal List<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                NotifyCategoriesDataChanged();
            }
        }

        internal async Task<Category> GetCategoryByCategoryId(int categoryId, bool withCars)
        {
            if (_categories == null)
            {
                await GetCategoriesFromDatabaseAndCache(withCars);
            }

            Category categoryToReturn = _categories.First(category => category.CategoryId == categoryId);

            if (categoryToReturn.Cars == null && withCars == true)
            {
                categoryToReturn = await _httpClient.GetFromJsonAsync<Category>($"{APIEndpoints.s_categoriesWithCars}/{categoryToReturn.CategoryId}");
            }

            return categoryToReturn;
        }

        internal async Task<Category> GetCategoryByCategoryName(string categoryName, bool withCars, bool nameToLowerFromUrl)
        {
            if (_categories == null)
            {
                await GetCategoriesFromDatabaseAndCache(withCars);
            }

            Category categoryToReturn = null;

            if (nameToLowerFromUrl == true)
            {
                categoryToReturn = _categories.First(category => category.Name.ToLowerInvariant() == categoryName);
            }
            else
            {
                categoryToReturn = _categories.First(category => category.Name == categoryName);
            }

            if (categoryToReturn.Cars == null && withCars == true)
            {
                categoryToReturn = await _httpClient.GetFromJsonAsync<Category>($"{APIEndpoints.s_categoriesWithCars}/{categoryToReturn.CategoryId}");
            }

            return categoryToReturn;
        }

        private bool _gettingCategoriesFromDatabaseAndCaching = false;
        internal async Task GetCategoriesFromDatabaseAndCache(bool withCars)
        {
            // Only allow one Get request to run at a time.
            if (_gettingCategoriesFromDatabaseAndCaching == false)
            {
                _gettingCategoriesFromDatabaseAndCaching = true;

                List<Category> categoriesFromDatabase = null;

                if (_categories != null)
                {
                    _categories = null;
                }

                if (withCars == true)
                {
                    categoriesFromDatabase = await _httpClient.GetFromJsonAsync<List<Category>>(APIEndpoints.s_categoriesWithCars);
                }
                else
                {
                    categoriesFromDatabase = await _httpClient.GetFromJsonAsync<List<Category>>(APIEndpoints.s_categories);
                }

                _categories = categoriesFromDatabase.OrderByDescending(category => category.CategoryId).ToList();

                if (withCars == true)
                {
                    List<Car> postsFromCategories = new List<Car>();

                    foreach (var category in _categories)
                    {
                        if (category.Cars.Count != 0)
                        {
                            foreach (var post in category.Cars)
                            {
                                Category postCategoryWithoutCars = new Category
                                {
                                    CategoryId = category.CategoryId,
                                  
                                    Name = category.Name,
                                    Description = category.Description,
                                    Cars = null
                                };

                                post.Category = postCategoryWithoutCars;

                                postsFromCategories.Add(post);
                            }
                        }
                    }

                    _cars = postsFromCategories.OrderByDescending(post => post.CarId).ToList();
                }

                _gettingCategoriesFromDatabaseAndCaching = false;
            }
        }

        internal event Action OnCategoriesDataChanged;
        private void NotifyCategoriesDataChanged() => OnCategoriesDataChanged?.Invoke();

        #endregion

        #region Cars

        private List<Car> _cars = null;
        internal List<Car> Cars
        {
            get
            {
                return _cars;
            }
            set
            {
                _cars = value;
                NotifyCarsDataChanged();
            }
        }

        internal async Task<Car> GetCarByCarId(int carId)
        {
            if (_cars == null)
            {
                await GetCarsFromDatabaseAndCache();
            }

            return _cars.First(car => car.CarId == carId);
        }

        internal async Task<CarDTO> GetCarDTOByCarId(int carId) => await _httpClient.GetFromJsonAsync<CarDTO>($"{APIEndpoints.s_carsDTO}/{carId}");

        private bool _gettingCarsFromDatabaseAndCaching = false;
        internal async Task GetCarsFromDatabaseAndCache()
        {
            // Only allow one Get to run at a time
            if (_gettingCarsFromDatabaseAndCaching == false)
            {
                _gettingCarsFromDatabaseAndCaching = true;

                if (_cars != null)
                {
                    _cars = null;
                }

                List<Car> carsFromDatabase = await _httpClient.GetFromJsonAsync<List<Car>>(APIEndpoints.s_cars);

                _cars = carsFromDatabase.OrderByDescending(car => car.CarId).ToList();

                _gettingCarsFromDatabaseAndCaching = false;
            }
        }

        internal event Action OnCarsDataChanged;
        private void NotifyCarsDataChanged() => OnCarsDataChanged?.Invoke();

        #endregion

        #region Bookings

        private List<Booking> _bookings = null;
        internal List<Booking> Bookings
        {
            get
            {
                return _bookings;
            }
            set
            {
                _bookings = value;
                NotifyBookingsDataChanged();
            }
        }

        internal async Task<Booking> GetBookingByBookingId(string bookingId)
        {
            if (_bookings == null)
            {
                await GetBookingsFromDatabaseAndCache();
            }

            return _bookings.First(booking => booking.BookingId == bookingId);
        }

        internal async Task<BookingDTO> GetBookingDTOByBookingId(int bookingId) => await _httpClient.GetFromJsonAsync<BookingDTO>($"{APIEndpoints.s_bookingsDTO}/{bookingId}");

        private bool _gettingBookingsFromDatabaseAndCaching = false;
        internal async Task GetBookingsFromDatabaseAndCache()
        {
            // Only allow one Get to run at a time
            if (_gettingBookingsFromDatabaseAndCaching == false)
            {
                _gettingBookingsFromDatabaseAndCaching = true;

                if (_bookings != null)
                {
                    _bookings = null;
                }

                List<Booking> bookingsFromDatabase = await _httpClient.GetFromJsonAsync<List<Booking>>(APIEndpoints.s_bookings);

                _bookings = bookingsFromDatabase.OrderByDescending(booking => booking.BookingId).ToList();

                _gettingBookingsFromDatabaseAndCaching = false;
            }
        }

        internal event Action OnBookingsDataChanged;
        private void NotifyBookingsDataChanged() => OnBookingsDataChanged?.Invoke();

        #endregion

        #region Users

        private List<AppUser> _users = null;
        internal List<AppUser> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                NotifyUsersDataChanged();
            }
        }

        internal async Task<AppUser> GetUserByUserId(string email)
        {
            if (_users == null)
            {
                await GetUsersFromDatabaseAndCache();
            }

            return _users.First(user => user.NormalizedUserName == email);
        }

        internal async Task<UserDTO> GetUserDTOByUserId(string email) => await _httpClient.GetFromJsonAsync<UserDTO>($"{APIEndpoints.s_users}/{email}");

        private bool _gettingUsersFromDatabaseAndCaching = false;
        internal async Task GetUsersFromDatabaseAndCache()
        {

         
            // Only allow one Get to run at a time
            if (_gettingUsersFromDatabaseAndCaching == false)
            {
                _gettingUsersFromDatabaseAndCaching = true;

                if (_users != null)
                {
                    _users = null;
                }

                List<AppUser> usersFromDatabase = await _httpClient.GetFromJsonAsync<List<AppUser>>(APIEndpoints.s_users);

                _users = usersFromDatabase.OrderByDescending(user => user.NormalizedUserName).ToList();

          


                _gettingUsersFromDatabaseAndCaching = false;
            }

            
        }

        internal event Action OnUsersDataChanged;
        private void NotifyUsersDataChanged() => OnUsersDataChanged?.Invoke();

        #endregion


    }
}
