namespace BananaStand.Data.Models
{
    using System;

    public partial class Bananas_Sold
    {
        public int Id { get; set; }

        public int BananasPurchasedId { get; set; }

        public int Bananas { get; set; }

        public DateTime SellDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual Bananas_Purchased Bananas_Purchased { get; set; }
    }
}
