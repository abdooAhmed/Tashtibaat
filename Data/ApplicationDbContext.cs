
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tashtibaat.Models;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Duende.IdentityServer.EntityFramework.Extensions;
using OperationalStoreOptions = Duende.IdentityServer.EntityFramework.Options.OperationalStoreOptions;

namespace Tashtibaat.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>, IPersistedGrantDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;
        public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductsCategory> ProductCategories { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Assay> Assays { get; set; }
        public DbSet<AssayOrder> AssayOrders { get; set; }
        public DbSet<Preview> Previews { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Designs> Designs { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<DesignOrder> DesignOrders { get; set; }
        public DbSet<DesignToMeters> DesignToMeters { get; set; }
        public DbSet<AssayToMeters> AssayToMeters { get; set; }
        public DbSet<ProductToMeters> ProductToMeters { get; set; }
        public DbSet<SecuritySurveillance> SecuritySurveillances { get; set; }
        public DbSet<ArmoredDoor> ArmoredDoors {  get; set; }
        public DbSet<Kitchen> Kitchens { get; set; }
        public DbSet<DressingRoom> DressingRooms { get; set; }

        public DbSet<ServerSideSession> ServerSideSessions { get; set; }
        public DbSet<SecuritySystemToOrder> SecuritySystemToOrders { get; set; }
        public DbSet<ArmoredDoorTOOrder> ArmoredDoorTOOrders { get; set; }
        public DbSet <KitchenToOrder> KitchenToOrders { get; set; }
        public DbSet <DressingRoomToOrder> DressingRoomToOrders { get; set; }
        public DbSet<ServicesOrder> ServicesOrders { get; set; }
        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);


            //    builder.Entity<Item>()
            //        .HasKey(nameof(Item.Id), nameof(Item.Number));

            //    builder.Entity<Category>()
            //        .HasKey(nameof(Category.Id), nameof(Category.Number));

            //    builder.Entity<Addon>()
            //        .HasKey(nameof(Addon.Id), nameof(Addon.Number));

            //    builder.Entity<CookingState>()
            //        .HasKey(nameof(CookingState.Id), nameof(CookingState.Number));

            //    builder.Entity<Recipe>()
            //        .HasKey(nameof(Recipe.Id), nameof(Recipe.Number));

            //    builder.Entity<Ingredient>()
            //        .HasKey(nameof(Ingredient.Id), nameof(Ingredient.Number));

            //    builder.Entity<Discount>()
            //        .HasKey(nameof(Discount.Id), nameof(Discount.Number));

            //    builder.Entity<SKU>()
            //        .HasKey(nameof(SKU.Id), nameof(SKU.Number));

            //    builder.Entity<ItemSize>()
            //        .HasKey(nameof(ItemSize.Id), nameof(ItemSize.Number));

            //    builder.Entity<Order>()
            //        .HasKey(nameof(Order.Id), nameof(Order.Number));
        }
        
       

    }
}