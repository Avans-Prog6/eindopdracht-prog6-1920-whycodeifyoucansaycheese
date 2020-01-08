using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Domain.Models
{
    public partial class AccessoryVM
    {
        private Accessory _accessory;
        private BeastVM _accessoryBeast;
        public AccessoryVM()
        {
            _accessory = new Accessory();
            _accessoryBeast = new BeastVM();
            Booking = new List<BookingVM>();
    }

        public AccessoryVM(Accessory accessory)
        {
            _accessory = accessory;
            _accessoryBeast = new BeastVM(accessory.Beast);
            //Booking = new List<BookingVM>(accessory.Booking.Select(b => new BookingVM(b)));
        }
        [Key]
        public int ID { get => _accessory.ID; set { _accessory.ID = value; } }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Naam accessoire")]
        public string Name { get => _accessory.Name; set { _accessory.Name = value; } }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Prijs accessoire")]
        public decimal Price { get => _accessory.Price; set { _accessory.Price = value; } }

        [Required]
        [DisplayName("Beest")]
        public int BeastID { get => _accessory.BeastID; set { _accessory.BeastID = value; } }

        public bool IsSelected { get; set; }

        public string Selected { get; set; } = "Selecteren";

        public Accessory Accessory { get => _accessory; }

        public BeastVM Beast { get => _accessoryBeast; set { _accessoryBeast = value; } }
        public virtual List<BookingVM> Booking { get => _accessory.Booking.Select(b => new BookingVM(b)).ToList(); set {_accessory.Booking = value.Select(b => b.Booking).ToList(); } }
    }
}