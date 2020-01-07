using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Interface_Repositories
{
    public interface IAccessoryRepository : IRepository<Accessory>
    {
        void UpdateAccessory(Accessory acc);

       
    }

    
}
