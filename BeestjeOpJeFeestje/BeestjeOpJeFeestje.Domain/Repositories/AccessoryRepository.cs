using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Repositories
{
    public class AccessoryRepository : Repository<Accessory>, IAccessoryRepository
    {

        public AccessoryRepository(BeesteOpJeFeestjeEntities context) : base(context)
        {

        }

    }
}
