using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {

        public DbSet<Category> Categories { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        //public DbSet<User> Users { get; set; }

        public DbSet<License> Licenses { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //call the base verision of this method or we get an error
            base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<AppUser>()
                .Property(e => e.Name)
                .HasMaxLength(250);

     

            modelBuilder.Entity<AppUser>()
                .Property(e => e.PhoneNumber)
                .HasMaxLength(250);




            #region

            Category[] categoriesToSeed = new Category[3];

          

            categoriesToSeed[0] = new Category { CategoryId = 1, Name = $"Standard Tier", Description = $"Insurance Covered and up to 10 hours free driving" };
            
            categoriesToSeed[1] = new Category { CategoryId = 2, Name = $"Gold Tier", Description = $"Insurance Covered and up to 20 hours free driving" };
            
            categoriesToSeed[2] = new Category { CategoryId = 3, Name = $"Premium Tier", Description = $"Insurance Covered and up to 30 hours free driving" };
            

            modelBuilder.Entity<Category>().HasData(categoriesToSeed);

            #endregion


            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasOne(car => car.Category)
                .WithMany(category => category.Cars)
                .HasForeignKey("CategoryId");

            });

          //  modelBuilder.Entity<Booking>()
          //.HasOne(p => p.AppUser)
          //.WithMany(b => b.Bookings)
          //.HasForeignKey(p => p.UserEmail).OnDelete(DeleteBehavior.Cascade);


            #region Car Seed




            Car[] carsToSeed = new Car[6];

            for (int i = 1; i < 7; i++)
            {
                string carMake = string.Empty;
                string carModel = string.Empty;
                double range = 0;
                double rangeLeft = 100;
                int catId = 0;
                int bookingId = 0;
                double lat = 0;
                double lng = 0;
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
                        lat = 53.150140;
                        lng = -6.566155;

                        break;
                    case 2:
                        carMake = "Tesla";
                        carModel = "Model S";
                        catId = 2;
                        bookingId = 2;
                        range = 200;
                        lat = 53.250140;
                        lng = -6.166155;
                        break;
                    case 3:
                        carMake = "Porsche";
                        carModel = "Taycan";
                        catId = 3;
                        bookingId = 3;
                        range = 270;
                        lat = 53.150140;
                        lng = -6.666155;
                        break;
                    case 4:
                        carMake = "Nissan";
                        carModel = "Leaf";
                        catId = 1;
                        bookingId = 1;
                        range = 150;
                        lat = 53.150140;
                        lng = -6.666155;
                        break;
                    case 5:
                        carMake = "Honda";
                        carModel = "Up!";
                        catId = 2;
                        bookingId = 2;
                        range = 220;
                        lat = 53.750140;
                        lng = -6.866155;
                        break;
                    case 6:
                        carMake = "Toyota";
                        carModel = "GT";
                        catId = 3;
                        bookingId = 3;
                        range = 200;
                        lat = 53.450140;
                        lng = -6.366155;
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
                    Lat = lat,
                    Lng =lng,

                };

            }

            modelBuilder.Entity<Car>().HasData(carsToSeed);

            #endregion

            #region

            /*
            User[] usersToSeed = new User[3];

            for (int i = 1; i < 4; i++)
            {
                usersToSeed[i - 1] = new User { EmailAddress = $"Email: {i}", Password = $"Description {i}" };
            }

            modelBuilder.Entity<User>().HasData(usersToSeed);

            */
            #endregion


        }


    }
}
