using Foodie.Services.CouponAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {
            
        }
        public DbSet<Coupon> Coupons { get; set; }
    }
}
