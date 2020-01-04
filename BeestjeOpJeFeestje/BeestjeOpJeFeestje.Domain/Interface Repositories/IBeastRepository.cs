using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Interface_Repositories
{
    public interface IBeastRepository : IRepository<Beast>
    {
        IEnumerable<Beast> TempSelected { get; set; }

        IEnumerable<Beast> BeastsAvailable();

        bool ExcludePinguin { get; set; }
        bool ExcludeDesert { get; set; }
        bool ExcludeSnow { get; set; }
        bool ExcludeFarm { get; set; }
        bool ExcludePolarLion { get; set; }

    }
}
