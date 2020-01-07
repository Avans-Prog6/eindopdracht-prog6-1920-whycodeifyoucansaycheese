using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace BeestjeOpJeFeestje.Domain.Repositories
{
    public class BeastRepository : Repository<Beast>, IBeastRepository
    {

        public BeastRepository(BeesteOpJeFeestjeEntities context) : base(context)
        {
        }

        public IEnumerable<Beast> TempSelected { get; set; }

        public void UpdateBeast(Beast beast)
        {
            Context.Set<Beast>().AddOrUpdate(beast);
        }

        public IEnumerable<Beast> BeastsAvailable()
        {
            var list = GetAll().ToList();
            if(ExcludePinguin == true)
            {
                var pin = list.Where(beast => beast.Name == "Pinguin").SingleOrDefault();
                list.Remove(pin);
            }
            if(ExcludePolarLion == true)
            {
                for (int i = list.Count-1; i >= 0; i--)
                {
                    if(list[i].Name == "Leeuw" || list[i].Name == "Ijsbeer")
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            if(ExcludeSnow == true)
            {
                for (int i = list.Count-1; i >= 0; i--)
                {
                    if (list[i].Type == "Sneeuw")
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            if(ExcludeDesert == true)
            {
                for (int i = list.Count-1; i >= 0; i--)
                {
                    if (list[i].Type == "Woestijn")
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            if(ExcludeFarm == true)
            {
                for (int i = list.Count-1; i >= 0; i--)
                {
                    if (list[i].Type == "Boerderij")
                    {
                        list.RemoveAt(i);
                    }
                }
            }

            return list;

        }

        public bool ExcludePinguin { get; set; }
        public bool ExcludeDesert { get; set; }
        public bool ExcludeSnow { get; set; }
        public bool ExcludeFarm { get; set; }
        public bool ExcludePolarLion { get; set; }
    }
}
