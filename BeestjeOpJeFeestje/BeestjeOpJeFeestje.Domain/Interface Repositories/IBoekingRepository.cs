using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Interface_Repositories
{
    public interface IBoekingRepository : IRepository<Booking>
    {
        Booking TempBooking { get; set; }
        IEnumerable<Beast> AnimalsBooked();
        IEnumerable<Accessory> AccessoriesBooked();
        void AddBookedAccessory(Accessory acc);
        void RemoveBookAccessory(Accessory acc);
    }

    
}
