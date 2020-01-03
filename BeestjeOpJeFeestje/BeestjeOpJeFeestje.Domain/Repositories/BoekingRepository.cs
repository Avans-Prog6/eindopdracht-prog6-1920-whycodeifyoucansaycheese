﻿using System;
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
    }
}
