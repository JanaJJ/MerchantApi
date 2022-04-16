using MerchantApi.Dto;
using MerchantApi.Models;
using MerchantApi.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace MerchantApi.Database
{
    public class Merchant_StoreDbContext : DbContext
    {
        public Merchant_StoreDbContext(DbContextOptions<Merchant_StoreDbContext> options):base(options)
        {

        }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}
