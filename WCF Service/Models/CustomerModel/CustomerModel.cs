namespace WCF_Service.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CustomerModel : DbContext
    {
        public CustomerModel()
            : base("name=dxAzureDB")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.PostalCode)
                .IsFixedLength();

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Customers)
                .WithMany(e => e.Addresses)
                .Map(m => m.ToTable("CustomerAddress").MapLeftKey("AddressID").MapRightKey("CustomerID"));

            modelBuilder.Entity<AddressType>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.AddressType1)
                .HasForeignKey(e => e.AddressType)
                .WillCascadeOnDelete(false);
        }
    }
}
