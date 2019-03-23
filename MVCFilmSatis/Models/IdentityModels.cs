using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCFilmSatis.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser :Customer  
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			Claim c = new Claim("TC", this.TC.ToString());
			userIdentity.AddClaim(c);
			Claim c2 = new Claim("NameLastname", NameSurname);
			userIdentity.AddClaim(c2);

			if (this.Age < 25)
			{
				Claim c3 = new Claim("AgeGroup", "Young");
				userIdentity.AddClaim(c3);
			}

			return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<Slider> Sliders { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movies>()
				.HasKey(x => x.MovieId);

            modelBuilder.Entity<ShoppingCart>()
				.HasKey(x => x.ShoppingCartID);

			modelBuilder.Entity<Order>()
				.HasKey(x => x.OrderID);

			modelBuilder.Entity<OrderItem>()
				.HasKey(x => x.OrderItemID);

			modelBuilder.Entity<Slider>()
				.HasKey(x => x.SliderID);

			modelBuilder.Entity<Order>()
				.HasMany(x => x.OrderItems)
				.WithRequired(x => x.Order);

			modelBuilder.Entity<Order>()
				.HasRequired(x => x.Customer)
				.WithMany(x => x.Orders);

			modelBuilder.Entity<OrderItem>()
				.HasRequired(x => x.Movies)
				.WithMany(x => x.OrderItems);

            modelBuilder.Entity<ShoppingCart>()
				.HasMany(x => x.Movies)
				.WithMany(x => x.ShoppingCarts);

            modelBuilder.Entity<Customer>()
				.HasOptional(x => x.ShoppingCart)
				.WithRequired(x => x.Customer);


            base.OnModelCreating(modelBuilder);
        }
    }
}