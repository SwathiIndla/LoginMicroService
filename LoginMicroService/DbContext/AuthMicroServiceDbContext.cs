using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroService.DbContext
{
    public class AuthMicroServiceDbContext : IdentityDbContext
    {
        public AuthMicroServiceDbContext(DbContextOptions<AuthMicroServiceDbContext> dbOptions) : base(dbOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var customerRoleId = "0c9f500b-e8fb-4fb1-abef-6072a354674f";
            var sellerRoleId = "b8880864-ce0d-4e80-adb6-05ad1d9ca5d7";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = customerRoleId,
                    ConcurrencyStamp = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "Customer".ToUpper()
                },
                new IdentityRole
                {
                    Id = sellerRoleId,
                    ConcurrencyStamp = sellerRoleId,
                    Name = "Seller",
                    NormalizedName = "Seller".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
