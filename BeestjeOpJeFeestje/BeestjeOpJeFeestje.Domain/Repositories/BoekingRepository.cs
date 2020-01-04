using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;

namespace BeestjeOpJeFeestje.Domain.Repositories
{
    public class BoekingRepository : Repository<Booking>, IBoekingRepository 
    {
        public BoekingRepository(BeesteOpJeFeestjeEntities context) : base(context)
        {
            TempBooking = new Booking();
        }

        public Booking TempBooking { get; set; }
        public IEnumerable<Beast> AnimalsBooked()
        {
            return TempBooking.Beast;
        }

        public IEnumerable<Accessory> AccessoriesBooked()
        {
            return TempBooking.Accessory;
        }

        public void AddBookedAccessory(Accessory acc)
        {
           var b = AnimalsBooked().SingleOrDefault(beast => beast.Accessory.Contains(acc));
           var a = b.Accessory.First(accs => accs.ID == acc.ID);
            a.IsSelected = true;
            a.Selected = "Deselecteren";
        }

        public void RemoveBookAccessory(Accessory acc)
        {
            var b = AnimalsBooked().SingleOrDefault(beast => beast.Accessory.Contains(acc));
            var a = b.Accessory.First(accs => accs.ID == acc.ID);
            a.IsSelected = false;
            a.Selected = "Selecteren";
        }

        public bool SnowExists()
        {
            var list = AnimalsBooked().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == "Sneeuw")
                {
                    return true;
                }
            }
            return false;
        }

        public bool FarmExists()
        {
            var list = AnimalsBooked().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == "Boerderij")
                {
                    return true;
                }
            }
            return false;
        }

        public bool DesertExists()
        {
            var list = AnimalsBooked().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == "Woestijn")
                {
                    return true;
                }
            }
            return false;
        }

        public bool PolarLionExists()
        {
            var list = AnimalsBooked().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == "Leeuw" || list[i].Name == "Ijsbeer")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
