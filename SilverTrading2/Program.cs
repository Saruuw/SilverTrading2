using System;
using System.Collections.Generic;

namespace SilverTrading2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Startvärden
            List<int> startListDays = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> startListPrices = new List<int>() { 7, 4, 12, 5, 8, 3, 11, 2, 9 };

            //Skickar in värdena för att skapa en dictionary så värdena hänger ihop
            var mergedList = MergeValues(startListDays, startListPrices);
            //Skickar in dictionary i metoden BuyDay
            var buyDay = BuyDay(mergedList);
            //Skickar in buyDay och dictionary i metoden SellDay
            var sellDay = SellDay(buyDay, mergedList);

            //Skriver ut resultatet i consolen
            Console.WriteLine("Jag köper silver på dag " + buyDay + " och säljer det på dag " + sellDay);
        }

        //Metoden BuyDay letar fram dagen med lägst pris
        public static int BuyDay(Dictionary<int, int> mergedList)
        {
            var lowestPrice = double.MaxValue;
            var dayWithLowestPrice = 0;

            //Kollar igenom listan och sätter det lägsta värdet till lowestPrice och jämför varje tal med det, ser dock till så att det inte är det sista talet i listan
            for (int i = 0; i < (mergedList.Count - 1); i++)
            {
                lowestPrice = Math.Min(mergedList[i], lowestPrice);
            }
            //Letar fram dagen med det lägsta värdet
            foreach (var day in mergedList)
            {
                if (day.Value == lowestPrice)
                {
                    //Plus ett eftersom listan börjar på 0
                    dayWithLowestPrice = day.Key + 1;
                    break;
                }
            }

            Dictionary<int, int> highestPricesAfterLowest = new Dictionary<int, int>();
            //Kollar om det finns värden i listan efter det lägsta värdet, där priset är högre
            foreach (var day in mergedList)
            {
                if (day.Key > (dayWithLowestPrice - 1) && day.Value > lowestPrice)
                {
                    highestPricesAfterLowest.Add(day.Key, day.Value);
                }
            }

            //Om listan inte är lika med noll, skicka dayWithLowestPrice
            if (highestPricesAfterLowest.Count != 0)
            {
                return dayWithLowestPrice;
            }
            else
            {
                //Om listan är lika med noll, skicka in listan och dagen med lägsta priset i metoden FilterList
                var newList = FilterList(dayWithLowestPrice, mergedList);
                //Starta om metoden med nya listan
                return BuyDay(newList);
            }
        }

        //SellDay tar fram dagen så silvret ska säljas
        public static int SellDay(int buyDay, Dictionary<int, int> mergedList)
        {
            var theHighestAfterLowest = double.MinValue;
            //Tar fram högsta priset efter det lägsta
            for (int i = buyDay - 1; i < mergedList.Count; i++)
            {
                theHighestAfterLowest = Math.Max(mergedList[i], theHighestAfterLowest);
            }
            //Returnerar dagen som har det högsta priset efter det lägsta
            foreach (var day in mergedList)
            {
                if (day.Key > (buyDay - 1) && day.Value == theHighestAfterLowest)
                {
                    return day.Key + 1;
                }
            }
            //Behövs logik för vad som ska hända om något strular
            return 0;
        }

        //Metoden MergeValues lägger ihop startlistorna till en gemensam dictionary
        public static Dictionary<int, int> MergeValues(List<int> startListDays, List<int> startListPrices)
        {
            Dictionary<int, int> mergedList = new Dictionary<int, int>();
            //Sätter dag till key och pris till value
            for (int i = 0; i < startListDays.Count; i++)
            {
                mergedList.Add(startListDays[i], startListPrices[i]);
            }

            return mergedList;
        }

        //FilterList tar bort resten av listan från (och med) det lägsta numret
        public static Dictionary<int, int> FilterList(int dayWithLowestPrice, Dictionary<int, int> mergedList)
        {
            Dictionary<int, int> newList = new Dictionary<int, int>();

            //Lägger till varje par i den nya listan, men kortar ner den genom att sluta vid lägsta numret. Detta gör att ett nytt lägsta nummer kommer väljas i BuyDay
            foreach (var x in mergedList)
            {
                if (x.Key < dayWithLowestPrice - 1)
                {
                    newList.Add(x.Key, x.Value);
                }
                else
                {
                    break;
                }
            }
            return newList;
        }
    }
}
