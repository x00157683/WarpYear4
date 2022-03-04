using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class AppDBContext : DbContext
    {

        public DbSet<Category> Categories {get; set;}

        public DbSet<Car> Cars {get; set;}

        public DbSet<Booking> Bookings {get; set;}

        public DbSet<User> Users { get; set; }

        public DbSet<License> Licenses { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { } 
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //call the base verision of this method or we get an error
            base.OnModelCreating(modelBuilder);

            #region

            Category[] categoriesToSeed = new Category[3];

            for(int i = 1; i < 4; i++)
            {
                categoriesToSeed[i-1] = new Category { CategoryId = i, Name = $"Category {i}", Description = $"Description {i}" };
            }

            modelBuilder.Entity<Category>().HasData(categoriesToSeed);

            #endregion


            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasOne(car => car.Category)
                .WithMany(category => category.Cars)
                .HasForeignKey("CategoryId");
            
            });



            #region Car Seed
 

   

        Car [] carsToSeed = new Car[6];

            for (int i = 1; i < 7; i++)
            {
                string carMake = string.Empty;
                string carModel = string.Empty;
                double range = 0;
                double rangeLeft = 100;
                int catId = 0;
                int bookingId = 0;
                bool isLocked = true;
                double pricePerUnit = 7;


                switch (i)
                {

                    case 1:
                        carMake = "Tesla";
                        carModel = "Model X";
                        catId = 1;
                        bookingId = 1;
                        range = 250;

                        break;
                    case 2:
                        carMake = "Tesla";
                        carModel = "Model S";
                        catId = 2;
                        bookingId = 2;
                        range = 200;
                        break;
                    case 3:
                        carMake = "Porsche";
                        carModel = "Taycan";
                        catId = 3;
                        bookingId = 3;
                        range = 270;
                        break;
                    case 4:
                        carMake = "Nissan";
                        carModel = "Leaf";
                        catId = 1;
                        bookingId = 1;
                        range = 150;
                        break;
                    case 5:
                        carMake = "Honda";
                        carModel = "Up!";
                        catId = 2;
                        bookingId = 2;
                        range = 220;
                        break;
                    case 6:
                        carMake = "Toyota";
                        carModel = "GT";
                        catId = 3;
                        bookingId = 3;
                        range = 200;
                        break;

                    default:
                        break;

                }

                carsToSeed[i - 1] = new Car
                {
                    CarId = i,
                    Make = carMake,
                    Model = carModel,
                    CategoryId = catId,
                    Range = range,
                    PricePerUnit = pricePerUnit,
                    isLocked = isLocked,
                    RangeLeft = rangeLeft,
              

                };

            }

            modelBuilder.Entity<Car>().HasData(carsToSeed);

            #endregion

            #region

            User[] usersToSeed = new User[3];

            for (int i = 1; i < 4; i++)
            {
                usersToSeed[i - 1] = new User { UserId = i, EmailAddress = $"Email: {i}", Password = $"Description {i}" };
            }

            modelBuilder.Entity<User>().HasData(usersToSeed);

            #endregion





            #region Booking Seed

            modelBuilder.Entity<Booking>(entity =>
            {
                //entity.HasOne(b => b.User)
                //.WithMany(u => u.Bookings)
                //.HasForeignKey("UserId");

                //entity.HasOne(c => c.Car);

            });


            Booking[] bookingsToSeed = new Booking[2];

  

            for (int i = 1; i < 3; i++)
            {
                string dNow = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm");
                DateTime dLap;
                bool isComplete = true;
                double cost = 0;
                int userId= 0;
                int carId = 0;



                switch (i)
                {

                    case 1:

                        cost = 88;
                        userId = 1;
                        carId = 1;
                        break;
                    case 2:

                        cost = 98;
                        userId = 2;
                        carId = 2;
                        break;

                    case 3:

                        cost = 998;
                        userId = 3;
                        carId = 5;

                        break;


                    default:
                        break;

                }

                bookingsToSeed[i - 1] = new Booking
                {
                    BookingId = i,
                    StartTime = dNow,
                    Cost = cost,
                    IsComplete = isComplete,
                    //UserId = userId,
                    //CarId = carId
                    
                    
                };

            }

            modelBuilder.Entity<Booking>().HasData(bookingsToSeed);

            #endregion


            //



        }
    }
}
