﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;

namespace BeestjeOpJeFeestje.Domain.Repositories
{
    public class ContactpersonRepository : Repository<ContactPerson>, IContactpersonRepository
    {
        public ContactpersonRepository(BeesteOpJeFeestjeEntities context) : base(context)
        {
        }

        public ContactPerson TempPerson { get; set; }
    }
}
