using LongKhanhMobile.DAL;
using System.Data.Entity.Migrations;


namespace LongKhanhMobile.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LongKhanhMobile.DAL.ShopDbContext";
        }

        protected override void Seed(ShopDbContext context)
        {
            AccountSeeder.Seed(context);
        }
    }
}
