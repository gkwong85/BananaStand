namespace BananaStand.Data
{
    using System.Data.Entity;
    using BananaStand.Data.Models;

    public partial class BananaStandContext : DbContext
    {
        public BananaStandContext(): base("name=BananaStandContext") { }

        public virtual DbSet<Bananas_Purchased> Bananas_Purchased { get; set; }
        public virtual DbSet<Bananas_Sold> Bananas_Sold { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bananas_Purchased>()
                .HasMany(e => e.Bananas_Sold)
                .WithRequired(e => e.Bananas_Purchased)
                .HasForeignKey(e => e.BananasPurchasedId)
                .WillCascadeOnDelete(false);
        }
    }
}
