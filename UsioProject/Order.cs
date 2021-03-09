using System;
using System.Collections.Generic;
using System.Linq;

namespace UsioProject
{
    public class Order
    {
        private int OrderNumber { get; set; } // line 100
        private int TotalItems { get; set; } // line 100
        private float OrderTotal { get; set; }  // line 100
        private string OrderDate { get; set; } // line 100

        private string CustomerName { get; set; }
        private string CustomerPhone { get; set; }
        private string CustomerEmail { get; set; }
        private string AddressLine1 { get; set; }
        private string AddressLine2 { get; set; }
        private string City { get; set; }
        private string State { get; set; }
        private string Zip { get; set; }

        private Boolean Paid { get; set; } // line 100
        private Boolean Shipped { get; set; } // line 100
        private Boolean Completed { get; set; } // line 100

        private List<LineItem> lineItems = new List<LineItem>();

        private Boolean Success = false;
        private List<string> ErrorMsg { get; set; } = new List<string>();

       
        public void ParseOrderInfo(string lines)
        {
            var orderLines = lines.Split('\t');

            var orderInfo = orderLines.Take(1).ToList().FirstOrDefault();
            var orderAddress = orderLines.Skip(1).Take(1).ToList().FirstOrDefault();
            var orderItems = orderLines.Skip(2).ToList();

            ParseLineOne(orderInfo);
            ParseLineTwo(orderAddress);
            LineItem.ParseLineItems(orderItems);

        }

        private void ParseLineOne(string line)
        {
            try
            {
                OrderNumber = int.Parse(line.Substring(3, 10).Trim());
                TotalItems = int.Parse(line.Substring(13, 5).Trim());
                OrderTotal = float.Parse(line.Substring(18, 10).Trim());
                OrderDate = line.Substring(28, 19);
                CustomerName = line.Substring(47, 50).Trim();
                CustomerPhone = line.Substring(97, 30).Trim();
                CustomerEmail = line.Substring(127, 50).Trim();
                Paid = line.Substring(177, 1).Equals('1');
                Shipped = line.Substring(178, 1).Equals('1');
                Completed = line.Substring(179).Equals('1');

            }
            catch (ArgumentNullException e)
            {
                ErrorMsg.Add(e.Message);
            }
            catch (FormatException e)
            {
                ErrorMsg.Add(e.Message);
            }
            catch (OverflowException e)
            {
                ErrorMsg.Add(e.Message);
            }

            if (ErrorMsg.Count.Equals(0))
                Success = true;

        }

        private void ParseLineTwo(string line)
        {
            try
            {
                    AddressLine1 = line.Substring(3, 50).Trim();
                    AddressLine2 = line.Substring(53, 50);
                    City = line.Substring(103, 50);
                    State = line.Substring(153, 2);
                    Zip = line.Substring(155);
            }
            catch (ArgumentNullException e)
            {
                ErrorMsg.Add(e.Message);
            }
            catch (FormatException e)
            {
                ErrorMsg.Add(e.Message);
            }
            catch (OverflowException e)
            {
                ErrorMsg.Add(e.Message);
            }

            if (ErrorMsg.Count.Equals(0))
                Success = true;
        }
    }
}
