namespace BananaStand.Data.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Bananas_Purchased
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bananas_Purchased()
        {
            Bananas_Sold = new HashSet<Bananas_Sold>();
        }

        public int Id { get; set; }

        public int Bananas { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Sold { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bananas_Sold> Bananas_Sold { get; set; }
    }
}
