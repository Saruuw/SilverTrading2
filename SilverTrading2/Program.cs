using System;
using System.Collections.Generic;

namespace SilverTrading2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> startListDays = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> startListPrices = new List<int>() { 7, 4, 12, 5, 8, 3, 11, 2, 9 };

            var mergedList = MergeValues(startListDays, startListPrices);
            var buyDay = BuyDay(mergedList);
            var sellDay = SellDay(buyDay, mergedList);

            Console.WriteLine("I buy silver on day " + buyDay + " and I sell on day " + sellDay);
        }

        public static int BuyDay(Dictionary<int, int> mergedList)
        {
            var lowestPrice = double.MaxValue;
            var dayWithLowestPrice = 0;

            for (int i = 0; i < mergedList.Count; i++)
            {
                lowestPrice = Math.Min(mergedList[i], lowestPrice);
            }
            foreach(var day in mergedList)
            {
                if(day.Value == lowestPrice)
                {
                    dayWithLowestPrice = day.Key + 1;
                }
            }

            Dictionary<int, int> highestPricesAfterLowest = new Dictionary<int, int>();

            foreach(var day in mergedList)
            {
                if(day.Key >= dayWithLowestPrice && day.Value > lowestPrice)
                {
                    highestPricesAfterLowest.Add(day.Key, day.Value);
                }
            }

            if (highestPricesAfterLowest != null)
            {
                return dayWithLowestPrice;
            }

            return 0;
        }

        public static int SellDay(int buyDay, Dictionary<int, int> mergedList)
        {
            var sellDay = 0;
            var theHighestAfterLowest = double.MinValue;

            for (int i = buyDay - 1; i < mergedList.Count; i++)
            {
                theHighestAfterLowest = Math.Max(mergedList[i], theHighestAfterLowest);
            }
            foreach(var day in mergedList)
            {
                if(day.Value == theHighestAfterLowest)
                {
                    return day.Key + 1;
                }
            }
            return 0;
        }

        public static Dictionary<int, int> MergeValues(List<int> startListDays, List<int> startListPrices)
        {
            Dictionary<int, int> mergedList = new Dictionary<int, int>();
            for (int i = 0; i < startListDays.Count; i++)
            {
                   mergedList.Add(startListDays[i], startListPrices[i]);
            }

            return mergedList;
        }
    }
}
