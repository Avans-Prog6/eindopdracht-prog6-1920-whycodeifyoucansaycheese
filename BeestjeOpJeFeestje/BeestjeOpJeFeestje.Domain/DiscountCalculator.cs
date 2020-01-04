using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain
{
    public class DiscountCalculator
    {
        private List<Discount> _discounts;
        private int _totaldiscount;
        private int characterdiscount = 0;

        public DiscountCalculator()
        {
            _discounts = new List<Discount>();
        }

        public List<Discount> CalculateTotalDiscount(Booking booking)
        {
            
            foreach(var beast in booking.Beast)
            {
                CalculateCharacterDiscount(beast.Name);
                DuckDiscount(beast.Name);
            }
            if(characterdiscount > 0)
            {
                _discounts.Add(new Discount("Letter korting: ", characterdiscount));
            }
            
            DateDiscount(booking.Date);
            TypeDiscount(booking.Beast.ToList());
            return _discounts;
        }

        public decimal CalculateTotalPrice(Booking booking)
        {
            decimal totalprice = 0;

            foreach(var beast in booking.Beast)
            {
                totalprice += beast.Price;
            }
            foreach(var acc in booking.Accessory)
            {
                totalprice += acc.Price;
            }

            totalprice = totalprice / 100 * (100 - _totaldiscount);
            return totalprice;
        }

        private void CalculateCharacterDiscount(string name)
        {
            name = name.ToLower();
            for (var c = 'a'; c <= 'z'; c++)
                if (name.Contains(c) && _totaldiscount < 60)
                {
                    _totaldiscount += 2;
                    characterdiscount += 2;
                    if (_totaldiscount > 60)
                    {
                        characterdiscount = CalculateHalvedDiscount(characterdiscount);
                    }
                }
                else
                {
                    
                    break;
                }
                    
        }

        private void DuckDiscount(string name)
        {
            if (!name.Equals("Eend") || _totaldiscount >= 60) return;
            if (new Random().Next(6) == 1)
            {
                _totaldiscount += 50;
                int discount = 50;
                if (_totaldiscount > 60)
                {
                    discount = CalculateHalvedDiscount(discount);
                }
                
                _discounts.Add(new Discount("Eend: ", discount));
            }
        }

        private void DateDiscount(DateTime date)
        {
            if ((date.DayOfWeek != DayOfWeek.Monday && date.DayOfWeek != DayOfWeek.Tuesday) || _totaldiscount >= 60) return;
            _totaldiscount += 15;
            int discount = 15;
            if (_totaldiscount > 60)
            {
                discount = CalculateHalvedDiscount(discount);
            }
            _discounts.Add(new Discount("Dag van de week korting: ", discount));
        }

        private void TypeDiscount(List<Beast> beasts)
        {
            if (beasts.Count < 3 || _totaldiscount >= 60) return;
            var jungleAmount = 0;
            var desertAmount = 0;
            var farmAmount = 0;
            var SnowAmount = 0;
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
                        SnowAmount++;
                        break;
                    case "Woestijn":
                        desertAmount++;
                        break;
                }
            }

            if(jungleAmount >= 3 || desertAmount >= 3 || farmAmount >= 3 || SnowAmount >= 3)
            {
                _totaldiscount += 10;
                var discount = 10;
                if (_totaldiscount > 60)
                {
                    discount = CalculateHalvedDiscount(discount);
                }
                _discounts.Add(new Discount("3 Types: ", discount));
            }
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
