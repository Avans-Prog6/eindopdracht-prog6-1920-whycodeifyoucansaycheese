using BeestjeOpJeFeestje.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Domain.Models
{
    public partial class BeastVM : IValidatableObject
    {
        private Beast _beast;
        public BeastVM()
        {
            _beast = new Beast();
            Selected = "Selecteren";
            //Accessory = new List<AccessoryVM>();
            //Booking = new List<BookingVM>();
        }

        public BeastVM(Beast beast)
        {
            _beast = beast;
            Selected = "Selecteren";
            //Accessory = new List<AccessoryVM>(beast.Accessory.Select(a => new AccessoryVM(a)));
            //Booking = new List<BookingVM>(beast.Booking.Select(b => new BookingVM(b)));
        }
        [Key]
        public int ID
        {
            get
            {
                return _beast.ID;
            }
            set
            {
                _beast.ID = value;
            }
        }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Naam beest")]
        public string Name
        {
            get => _beast.Name;

            set
            {
                _beast.Name = value;
            }
        }

        [Required]
        [DisplayName("Type")]
        public string Type { get => _beast.Type; set { _beast.Type = value; } }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "De geldprijs moet positief zijn.")]
        [DisplayName("Prijs beest")]
        public decimal Price { get => _beast.Price; set { _beast.Price = value; } }

        public string Selected { get; set; } = "Selected";
        public string ImagePath
        {
            get
            {
                return Name + ".png";
            }
        }

        public Beast Beast { get => _beast; }

        public virtual List<AccessoryVM> Accessory { get => _beast.Accessory.Select(a => new AccessoryVM(a)).ToList(); set { _beast.Accessory = value.Select(a => a.Accessory).ToList(); } }
        public virtual List<BookingVM> Booking { get => _beast.Booking.Select(a => new BookingVM(a)).ToList(); set { _beast.Booking = value.Select(a => a.Booking).ToList(); } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}