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


        public DiscountCalculator()
        {
            _discounts = new List<Discount>();
        }


        private int CalculateTotalDiscount(string name, DateTime date)
        {
            CalculateCharacterDiscount(name);
            DuckDiscount(name);
            DateDiscount(date);
            if (_totaldiscount > 60)
                _totaldiscount = 60;
            return _totaldiscount;
        }

        private void CalculateCharacterDiscount(string name)
        {
            int discount = 0;
            name = name.ToLower();
            for (var c = 'a'; c <= 'z'; c++)
                if (name.Contains(c))
                {
                    _totaldiscount += 2;
                    discount += 2;
                }
                else
                {
                    _discounts.Add(new Discount("Letter korting: ", discount));
                    break;
                }
                    
        }

        private void DuckDiscount(string name)
        {
            if (!name.Equals("Eend") && _totaldiscount < 60) return;
            if (new Random().Next(6) == 1)
            {
                _totaldiscount += 50;
                const int discount = 50;
                _discounts.Add(new Discount("Eend: ", discount));
            }
        }

        private void DateDiscount(DateTime date)
        {
            if (date.DayOfWeek != DayOfWeek.Monday && date.DayOfWeek != DayOfWeek.Tuesday) return;
            _totaldiscount += 15;
            const int discount = 15;
            _discounts.Add(new Discount("Dag van de week korting: ", discount));
        }
    }
}
