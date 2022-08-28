using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<UserHomeAddress> UserHomeAddresses { get; set; }
        public DbSet<UserWorkAddress> UserWorkAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategoryProduct>()
                .HasKey(p => new { p.ProductId, p.CategoryId });

            //builder.Entity<ProductImage>()
            //    .HasOne<Product>(p => p.Product)
            //    .WithMany(pi => pi.ProductImages)
            //    .HasForeignKey(p => p.ProductId);

            //builder.Entity<Product>()
            //    .HasMany<ProductImage>(p => p.ProductImages)
            //    .WithOne(p => p.Product)
            //    .HasForeignKey(p => p.ProductId)
            //    .HasPrincipalKey(p => p.ProductId);

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = new Guid("08D68E62-6E3B-4CF4-BFB0-49D59F670A5B"),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "qwerty"),
                SecurityStamp = string.Empty
            });

            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = new Guid("5FFDEB4A2B284FC89D4130BD16984923"),
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            {
                UserId = new Guid("08D68E62-6E3B-4CF4-BFB0-49D59F670A5B"),
                RoleId = new Guid("5FFDEB4A2B284FC89D4130BD16984923")
            });

            builder.Entity<Brand>().HasData(new Brand[]
            {
                new Brand
                {
                    BrandId = new Guid("4b40b766-f054-455d-be9d-30f22a80b48c"),
                    Name = "New Balance",
                    Description = "New Balance Athletics, Inc., best known as simply New Balance, is one of the world's major sports footwear and " +
                            "apparel manufacturers. Based in Boston, Massachusetts, the multinational corporation was founded in 1906 as the New Balance " +
                            "Arch Support Company. "
                },
                new Brand
                {
                    BrandId = new Guid("47fc022b-672d-4869-835f-84eef087f735"),
                    Name ="HOKA ONE ONE",
                    Description = "Hoka One One is an athletic shoe company originating in France that designs and markets running shoes."
                },
                new Brand
                {
                    BrandId = new Guid("2c46fc6a-2b53-4ff3-bc2d-6388e82f9bdc"),
                    Name ="Under Armour",
                    Description = "Under Armour, Inc. is an American sports equipment company that manufactures footwear, sports and casual apparel."
                },
                new Brand
                {
                    BrandId = new Guid("18913488-917c-4748-b19f-37bb1b03b889"),
                    Name ="Salomon Group",
                    Description = "Salomon Group is a French sports equipment manufacturing company based in Annecy, France. It was founded in 1947 " +
                        "by François Salomon in the heart of the French Alps and is a major brand in outdoor sports equipment."
                }
            });

            builder.Entity<Product>().HasData(new Product[]
            {
                new Product
                {
                ProductId = new Guid("0966f6c6-505a-46bc-b424-d75dd3c7f85b"),
                Name = "HOKA TECTON X",
                Title = "HOKA TECTON X TRAIL RUNNING SHOES - AW22",
                Price = 174,
                Description = "Hoka Tecton X Trail Running Shoes Named after the earth's tectonic plates, which inspired " +
                                "its revoluntionary parallel carbon fiber plate technology, the new Hoka Tecton X is poised to " +
                                "unleash a seismic shift in trail running. Built for speed, with a Profly X midsole bolstered by " +
                                "a Vibram Megagrip Litebase outsole, the Tecton X is Hoka's first trail shoe to incorporate dynamically " +
                                "propulsive,dual parallel carbon fiber plates.",
                BrandId = new Guid("47fc022b-672d-4869-835f-84eef087f735")
                },
                new Product
                {
                    ProductId = new Guid("441af150-fdea-4784-9c99-057fd4a73d60"),
                    Name = "NEW BALANCE FRESH FOAM",
                    Title = "NEW BALANCE FRESH FOAM X HIERRO V7 TRAIL RUNNING SHOES - SS22",
                    Price = 129,
                    Description = "New Balance Fresh Foam X Hierro V7 Trail Running ShoesPeople love the idea of adventure in the great outdoors," +
                        "but there's more to the natural landscape than fresh air and scenic views. For those who take going off the beaten path literally, " +
                        "there's the Fresh Foam X Hierro, a dedicated,off - road application of our best running technology.",
                     BrandId = new Guid("4b40b766-f054-455d-be9d-30f22a80b48c")
                },
                new Product
                {
                    ProductId = new Guid("76024e75-ada1-448c-9dcf-5b620eb01e52"),
                    Name = "UNDER ARMOUR FLOW VELOCITI WIND",
                    Title = "UNDER ARMOUR FLOW VELOCITI WIND 2 RUNNING SHOES - SS22",
                    Price = 139,
                    Description = "Under Armour Flow Velociti Wind 2 Running ShoesThere's fast, and then there's UA Flow fast.Lightweight," +
                                    "rubberless,and durable,our newest cushioning gives a close - to - the - ground,grippy feeling of speed." +
                                    "The kind of speed that feels like you've got the wind at your back.",
                     BrandId = new Guid("2c46fc6a-2b53-4ff3-bc2d-6388e82f9bdc")
                },
                new Product
                {
                    ProductId = new Guid("435bc615-f43f-497d-a2c6-51b9ce7fd4af"),
                    Name = "HIGHER STATE RUNNING T-SHIRT",
                    Title = "HIGHER STATE SEAMFREE RUNNING T-SHIRT",
                    Price = 9,
                    Description = "Higher State Seamfree Running T-ShirtRun with confidence and no distractions with the Higher State Seamfree " +
                                    "Running T - Shirt.The Higher State Seamfree Running T - Shirt is a lightweight running tee with a seamfree " +
                                    "construction for comfort and freedom of movement without the risk of irritation or unnecessary heavy materials " +
                                    "holding you back.The lightweight material offers superior breathability allowing airflow in and out of the " +
                                    "t - shirt easily.This prevents warm air from building up within the t - shirt and is replaced with cooler, " +
                                    "fresher air.As you begin to sweat, the material works hard to wick away moisture keeping you feeling dry " +
                                    "and comfortable and the top light.The back features further ventilation panels which help to keep your " +
                                    "temperature down as your run intensifies.Lastly, the short sleeves are specially designed to allow a wide " +
                                    "range of movement in all directions and the fit of the tee gives a soft next to skin feel without feeling " +
                                    "too restrictive.",
                     BrandId = new Guid("47fc022b-672d-4869-835f-84eef087f735")
                },
                new Product
                {
                    ProductId = new Guid("6f43d008-a379-4dfe-b7e1-834c9974cefb"),
                    Name = "SALOMON S/LAB ",
                    Title = "SALOMON S/LAB NSO RUNNING T-SHIRT",
                    Price = 29,
                    Description = "Salomon S/LAB NSO Running T-ShirtA lightweight running tee with NSO Self Activated Energy " +
                                    "technology.The S / LAB NSO TEE harnesses next generation oxide mineral technology.",
                     BrandId = new Guid("18913488-917c-4748-b19f-37bb1b03b889")
                },
                new Product
                {
                    ProductId = new Guid("5c05add7-4333-4f5d-8182-04203fde6941"),
                    Name = "NEW BALANCE ACCELERATE RUNNING",
                    Title = "NEW BALANCE ACCELERATE RUNNING T-SHIRT - SS22",
                    Price = 139,
                    Description = "New Balance Accelerate T-ShirtRun with comfort and confidence in the Accelerate T - shirt from " +
                                    "New Balance.Signature NB Dry technology eradicates sweat and moisture while reflective details " +
                                    "keep you visible in low light.",
                     BrandId = new Guid("4b40b766-f054-455d-be9d-30f22a80b48c")
                }
            });


            builder.Entity<Category>().HasData(new Category[]
            {
                new Category
                {
                    CategoryId = new Guid("0e19b211-dc2a-490e-905d-a0420032cc57"),
                    Name = "Shoes",
                    Description= "A shoe is an item of footwear intended to protect and comfort the human foot. " +
                                    "Shoes are also used as an item of decoration and fashion. The design of shoes has " +
                                    "varied enormously through time and from culture to culture, with form originally being tied to function."
                },
                new Category
                {
                    CategoryId = new Guid("9da89049-b1fd-4074-8fbd-c9d62e58c59b"),
                    Name = "T-Shirts",
                    Description = "A T-shirt, or tee, is a style of fabric shirt named after the T shape of its body and sleeves. Traditionally, " +
                                    "it has short sleeves and a round neckline, known as a crew neck, which lacks a collar. T-shirts are generally " +
                                    "made of a stretchy, light, and inexpensive fabric and are easy to"
                },
                new Category
                {
                    CategoryId = new Guid("e35a55c3-63f6-40dc-becf-d980367b36a6"),
                    Name = "Shorts",
                    Description = "Shorts are a garment worn over the pelvic area, circling the waist and splitting to cover the upper " +
                                    "part of the legs, sometimes extending down to the knees but not covering the entire length of the leg."
                },
                new Category
                {
                    CategoryId = new Guid("db1c223f-de21-48ff-a7d7-1470e5587d1f"),
                    Name = "Jacket",
                    Description = "A jacket is a garment for the upper body, usually extending below the hips. A jacket typically " +
                                    "has sleeves, and fastens in the front or slightly on the side. A jacket is generally lighter, tighter-fitting, " +
                                    "and less insulating than a coat, which is outerwear."
                }
            });

            builder.Entity<CategoryProduct>().HasData(new CategoryProduct[]
            {
                new CategoryProduct
                {
                    CategoryId = new Guid("0e19b211-dc2a-490e-905d-a0420032cc57"),
                    ProductId = new Guid("0966f6c6-505a-46bc-b424-d75dd3c7f85b")
                },
                new CategoryProduct
                {
                    CategoryId = new Guid("0e19b211-dc2a-490e-905d-a0420032cc57"),
                    ProductId = new Guid("441af150-fdea-4784-9c99-057fd4a73d60")
                },
                new CategoryProduct
                {
                    CategoryId = new Guid("0e19b211-dc2a-490e-905d-a0420032cc57"),
                    ProductId = new Guid("76024e75-ada1-448c-9dcf-5b620eb01e52")
                },
                new CategoryProduct
                {
                    CategoryId = new Guid("9da89049-b1fd-4074-8fbd-c9d62e58c59b"),
                    ProductId = new Guid("435bc615-f43f-497d-a2c6-51b9ce7fd4af")
                }, new CategoryProduct
                {
                    CategoryId = new Guid("9da89049-b1fd-4074-8fbd-c9d62e58c59b"),
                    ProductId = new Guid("6f43d008-a379-4dfe-b7e1-834c9974cefb")
                }, new CategoryProduct
                {
                    CategoryId = new Guid("9da89049-b1fd-4074-8fbd-c9d62e58c59b"),
                    ProductId = new Guid("5c05add7-4333-4f5d-8182-04203fde6941")
                }
            });
        }
    }
}