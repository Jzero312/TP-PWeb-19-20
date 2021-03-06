using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PWebTudoDeNovo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public string LoginUserRole { get; set; }

        public float Saldo { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MINHA_BD", throwIfV1Schema: false)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCar> ShoppingCars { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


        
        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
    }
}