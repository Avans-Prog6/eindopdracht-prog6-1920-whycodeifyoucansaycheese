﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain
{
    public class DiscountCalculator
    {
        public List<Discount> Discounts;
        private int _totaldiscount;
        private int _characterdiscount;
        public bool DuckDiscountBool { get; set; }

        public DiscountCalculator()
        {
            Discounts = new List<Discount>();
        }

        public List<Discount> CalculateTotalDiscount(Booking booking)
        {
            foreach (var beast in booking.Beast)
            {
                CalculateCharacterDiscount(beast.Name);
                if (DuckDiscount(beast.Name) != null)
                {
                    Discounts.Add(DuckDiscount(beast.Name));
                }
            }
            if (_characterdiscount > 0)
            {
                Discounts.Add(new Discount("Letter korting: ", _characterdiscount));
            }

            if (DateDiscount(booking.Date) != null)
            {
                Discounts.Add(DateDiscount(booking.Date));
            }

            if (TypeDiscount(booking.Beast.ToList()) != null)
            {
                Discounts.Add(TypeDiscount(booking.Beast.ToList()));
            }
            return Discounts;
        }

        public decimal CalculateTotalPrice(Booking booking)
        {
            var totalprice = booking.Beast.Sum(beast => beast.Price) + booking.Accessory.Sum(acc => acc.Price);

            totalprice = totalprice / 100 * (100 - _totaldiscount);
            return totalprice;
        }

        public int CalculateCharacterDiscount(string name)
        {
            name = name.ToLower();
            for (var c = 'a'; c <= 'z'; c++)
                if (name.Contains(c) && _totaldiscount < 60)
                {
                    _totaldiscount += 2;
                    _characterdiscount += 2;
                    if (_totaldiscount > 60)
                    {
                        _characterdiscount = CalculateHalvedDiscount(_characterdiscount);
                    }
                }
                else
                {
                    return _characterdiscount;
                }
            return -1;
        }

        public Discount DuckDiscount(string name)
        {
            if (!name.Equals("Eend") || _totaldiscount >= 60) return null;
            if (new Random().Next(6) == 1)
            {
                DuckDiscountBool = true;
                _totaldiscount += 50;
                var discount = 50;
                if (_totaldiscount > 60)
                {
                    discount = CalculateHalvedDiscount(discount);
                }

                return new Discount("Eend: ", discount);
            }
            DuckDiscountBool = false;
            return null;
        }

        public Discount DateDiscount(DateTime date)
        {
            if ((date.DayOfWeek != DayOfWeek.Monday && date.DayOfWeek != DayOfWeek.Tuesday) || _totaldiscount >= 60) return null;
            _totaldiscount += 15;
            var discount = 15;
            if (_totaldiscount > 60)
            {
                discount = CalculateHalvedDiscount(discount);
            }
            return new Discount("Dag van de week korting: ", discount);
        }

        public Discount TypeDiscount(List<Beast> beasts)
        {
            if (beasts.Count < 3 || _totaldiscount >= 60) return null;
            var jungleAmount = 0;
            var desertAmount = 0;
            var farmAmount = 0;
            var snowAmount = 0;
            foreach (var beast in beasts)
            {
                switch (beast.Type)
                {
                    case "Boerderij":
                        farmAmount++;
                        break;
                    case "Jungle":
                        jungleAmount++;
                        break;
                    case "Sneeuw":
                        snowAmount++;
                        break;
                    case "Woestijn":
                        desertAmount++;
                        break;
                }
            }

            if (jungleAmount < 3 && desertAmount < 3 && farmAmount < 3 && snowAmount < 3) return null;
            _totaldiscount += 10;
            var discount = 10;
            if (_totaldiscount > 60)
            {
                discount = CalculateHalvedDiscount(discount);
            }
            return new Discount("Type korting:", discount);
        }

        private int CalculateHalvedDiscount(int discount)
        {
            var temp = _totaldiscount - 60;
            discount -= temp;
            _totaldiscount = 60;
            return discount;
        }
    }
}
