using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class AppDBContext : DbContext
    {

        public DbSet<Category> Categories {get; set;}

        public DbSet<Car> Cars {get; set;}

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { } 
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //call the base verision of this method or we get an error
            base.OnModelCreating(modelBuilder);

            #region

            Category[] categoriesToSeed = new Category[3];

            for(int i = 1; i < 4; i++)
            {
                categoriesToSeed[i-1] = new Category { CategoryID = i, Name = $"Category {i}", Description = $"Description {i}" };
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
                int catId = 0;


                switch (i)
                {

                    case 1:
                        carMake = "Tesla";
                        carModel = "Model X";
                        catId = 1;
                        break;
                    case 2:
                        carMake = "Tesla";
                        carModel = "Model S";
                        catId = 2;
                        break;
                    case 3:
                        carMake = "Porsche";
                        carModel = "Taycan";
                        catId = 3;
                        break;
                    case 4:
                        carMake = "Nissan";
                        carModel = "Leaf";
                        catId = 1;
                        break;
                    case 5:
                        carMake = "Honda";
                        carModel = "Up!";
                        catId = 2;
                        break;
                    case 6:
                        carMake = "Toyota";
                        carModel = "GT";
                        catId = 3;
                        break;

                    default:
                        break;

                }

                carsToSeed[i - 1] = new Car
                {
                    Id = i,
                    Make = carMake,
                    Model = carModel,
                    CategoryId = catId,
                
                };

            }

            modelBuilder.Entity<Car>().HasData(carsToSeed);

            #endregion

        }
    }
}
