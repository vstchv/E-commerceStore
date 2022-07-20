using API.Entities;

using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public static class SampleData
    {
        public static async Task Initialize( Contexts context, UserManager<User> userManager )
        {
            if ( !userManager.Users.Any() )
            {
                var user = new User
                {
                    UserName = "vasilena",
                    Email = "vasilena@test.com"
                };

                await userManager.CreateAsync( user, "Pa$$w0rd" );
                await userManager.AddToRoleAsync( user, "Member" );

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };

                await userManager.CreateAsync( admin, "Pa$$w0rd" );
                await userManager.AddToRolesAsync( admin, new[] { "Member", "Admin" } );
            }

            if ( context.Products.Any() ) return;

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Jones Snowboard",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 2000,
                    PictureUrl = "/images/products/SnowJones1.jpg",
                    Brand = "Jones",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Jones Snowboard Mountain",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 1500,
                    PictureUrl = "/images/products/SnowJones2.jpeg",
                    Brand = "Jones",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Jones Snowboard Dark",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 1800,
                    PictureUrl = "/images/products/SnowJones3.jpg",
                    Brand = "Jones",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Roxy Snowboard Blue",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 3000,
                    PictureUrl = "/images/products/SnowRoxy1.png",
                    Brand = "Roxy",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Roxy Snowboard Pink",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 25000,
                    PictureUrl = "/images/products/SnowRoxy2.jpg",
                    Brand = "Roxy",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Roxy Snowboard Purple",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1200,
                    PictureUrl = "/images/products/SnowRoxy3.jpg",
                    Brand = "Roxy",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Drake Snowboard",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1000,
                    PictureUrl = "/images/products/SnowDrake1.jpg",
                    Brand = "Drake",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Drake Snowboard Purple",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 800,
                    PictureUrl = "/images/products/SnowDrake2.jpg",
                    Brand = "Drake",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Drake Snowboard Dark Blue",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1500,
                    PictureUrl = "/images/products/SnowDrake3.jpg",
                    Brand = "Drake",
                    Type = "Snowboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Element Skateboard Skull",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1800,
                    PictureUrl = "/images/products/SkateElement1.jpg",
                    Brand = "Element",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Element Skateboard Mummy",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1500,
                    PictureUrl = "/images/products/SkateElement2.jpg",
                    Brand = "Element",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Element Skateboard Black Logo",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1600,
                    PictureUrl = "/images/products/SkateElement3.jpg",
                    Brand = "Element",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Girl Skateboard Black",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1400,
                    PictureUrl = "/images/products/SkateGirl1.jpg",
                    Brand = "Girl",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Girl Skateboard Blue",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 2500,
                    PictureUrl = "/images/products/SkateGirl2.png",
                    Brand = "Girl",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Girl Skateboard Red",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1899,
                    PictureUrl = "/images/products/SkateGirl3.jpg",
                    Brand = "Girl",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Plan B Skateboard Yellow",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 19999,
                    PictureUrl = "/images/products/SkatePlanb1.jpg",
                    Brand = "Plan B",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Plan B Skateboard Red",
                    Description = "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.",
                    Price = 1500,
                    PictureUrl = "/images/products/SkatePlanb2.jpg",
                    Brand = "Plan B",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Plan B Skateboard Animals",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 1800,
                    PictureUrl = "/images/products/SkatePlanb3.jpg",
                    Brand = "Plan B",
                    Type = "Skateboard",
                    QuantityInStock = 100
                },
            };

            foreach ( var product in products )
            {
                context.Products.Add( product );
            }

            context.SaveChanges();
        }
    }
}




