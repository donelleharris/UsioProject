using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace UsioProject
{
    class Program
    {
        public static void Main(string[] args)
        {
            var orderString = new List<string>();
            foreach (var line in File.ReadLines(@"OrderFile.txt"))
            {
                try
                {
                    Order order = new Order();
                   
                    if (line.StartsWith("100"))
                    {
                        orderString.Add(line);
                    }
                    else if(!line.StartsWith("100") && !line.Equals(null))
                    {
                        var currentItem = orderString.LastOrDefault();
                        orderString.RemoveAt(orderString.IndexOf(currentItem));
                        orderString.Add(String.Concat(currentItem, "\t", line));
                    }
                    //use Utilities method
                    var singleOrder = Utilities.ConverListToString(orderString);
                    order.ParseOrderInfo(singleOrder);
                    
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
    class Utilities
    {
        public static string ConverListToString(List<string> orderString)
        {
            string singleOrder = "";
            for (int i = 0; i < orderString.Count; i++)
            {
                singleOrder = orderString[i];
                
            }
            return singleOrder;
        }
    }
}
