using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Repositories
{
    public class BeastRepository : Repository<Beast>, IBeastRepository
    {

        public BeastRepository(BeesteOpJeFeestjeEntities context) : base(context)
        {

        }

        public IEnumerable<Beast> TempSelected { get; set; }
    }
}
