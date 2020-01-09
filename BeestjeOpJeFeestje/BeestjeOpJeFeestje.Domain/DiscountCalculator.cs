using System;
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
        private int _randomNumber;

        public DiscountCalculator()
        {
            Discounts = new List<Discount>();
            GetRandomNumber();
        }

        public List<Discount> CalculateTotalDiscount(Booking booking)
        {
            foreach (var beast in booking.Beast)
            {
                CalculateCharacterDiscount(beast.Name);
                Discounts.Add(DuckDiscount(beast.Name, _randomNumber));
                if (Discounts[Discounts.Count - 1] == null)
                {
                    Discounts.RemoveAt(Discounts.Count - 1);
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

                    _characterdiscount = CalculateHalvedDiscount(_characterdiscount);

                }
                else
                {
                    return _characterdiscount;
                }
            return -1;
        }

        public Discount DuckDiscount(string name, int random)
        {
            if (!name.Equals("Eend") || _totaldiscount >= 60 || random != 1) return null;
            _totaldiscount += 50;
            var discount = 50;

            discount = CalculateHalvedDiscount(discount);


            return new Discount("Eend: ", discount);
        }

        private int GetRandomNumber()
        {
            _randomNumber = new Random().Next(6);
            return _randomNumber;
        }

        public Discount DateDiscount(DateTime date)
        {
            if ((date.DayOfWeek != DayOfWeek.Monday && date.DayOfWeek != DayOfWeek.Tuesday) || _totaldiscount >= 60) return null;
            _totaldiscount += 15;
            var discount = 15;

            discount = CalculateHalvedDiscount(discount);

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
            discount = CalculateHalvedDiscount(discount);

            return new Discount("Type korting:", discount);
        }

        public int CalculateHalvedDiscount(int discount)
        {
            if (discount < 60) return discount;
            var temp = discount - 60;
            discount -= temp;
            _totaldiscount = 60;
            return discount;
        }
    }
}
