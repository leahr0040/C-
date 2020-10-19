//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            this.Rentals = new HashSet<Rental>();
            this.SubProperties = new HashSet<SubProperty>();
            this.Tasks = new HashSet<Task>();
        }
    
        public int PropertyID { get; set; }
        public int OwnerID { get; set; }
        public int CityID { get; set; }
        public int StreetID { get; set; }
        public string Number { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<int> Floor { get; set; }
        public bool IsDivided { get; set; }
        public Nullable<double> ManagmentPayment { get; set; }
        public bool IsPaid { get; set; }
        public bool IsExclusivity { get; set; }
        public Nullable<int> ExclusivityID { get; set; }
        public bool IsWarranty { get; set; }
        public Nullable<bool> IsRented { get; set; }
        public Nullable<double> RoomsNum { get; set; }
        public Nullable<int> ApartmentNum { get; set; }
        public Nullable<bool> status { get; set; }
    
        public virtual City City { get; set; }
        public virtual Exclusivity Exclusivity { get; set; }
        public virtual PropertiesOwner PropertiesOwner { get; set; }
        public virtual Street Street { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rental> Rentals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubProperty> SubProperties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
