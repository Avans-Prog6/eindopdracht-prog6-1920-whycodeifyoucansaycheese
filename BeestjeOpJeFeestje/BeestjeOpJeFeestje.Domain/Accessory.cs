//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;

namespace BeestjeOpJeFeestje.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Accessory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accessory()
        {
            this.Booking = new HashSet<Booking>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Naam accessoire")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Prijs accessoire")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Beest")]
        public int BeastID { get; set; }

        public bool IsSelected { get; set; }

        public string Selected { get; set; } = "Selecteren";

        public virtual Beast Beast { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
