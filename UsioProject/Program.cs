using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UsioProject
{
    class Program
    {
        public static void Main(string[] args)
        {

            foreach (string line in File.ReadLines(@"OrderFile.txt"))
            {
                try
                {
                    Order order = new Order();
                   
                    if (line.StartsWith("100"))
                    {
                        order.parseLine100(line);
                    }
                    else
                    {
                        order.parseOrderDetails(line, order.Customer);
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                

            }
        }
    }
    public class Order
    {
        public int OrderNumber { get; set; } // line 100
        public int TotalItems { get; set; } // line 100
        public float TotalCost { get; set; }  // line 100
        public string OrderDate { get; set; } // line 100
        public Customer Customer; // line 100
        public Boolean Paid { get; set; }
        public Boolean Shipped { get; set; }
        public Boolean Completed { get; set; }
        public List<OrderDetail> OrderDetail;
        public Boolean Success = false;
        public List<string> ErrorMsg { get; set; } = new List<string>();

        public Order() { }

        public void parseLine100(string line)
        {

            Customer customer = new Customer();
            try
            {
                OrderNumber = int.Parse(line.Substring(3, 10));
                TotalItems = int.Parse(line.Substring(13, 5));
                TotalCost = float.Parse(line.Substring(18, 10));
                OrderDate = line.Substring(28, 19);
                customer.CustomerName = line.Substring(47, 50);
                customer.CustomerPhone = line.Substring(97, 30);
                customer.CustomerEmail = line.Substring(127, 50);
                Paid = line.Substring(177, 1).Equals('1');
                Shipped = line.Substring(178, 1).Equals('1');
                Completed = line.Substring(179, 1).Equals('1');
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
                

    

        public void parseOrderDetails(string line, Customer customer)
        {
            try
            {
                if (line.StartsWith("200"))
                {
                    customer.Address.AddressLine1 = line.Substring(3, 50);
                    customer.Address.AddressLine2 = line.Substring(53, 50);
                    customer.Address.City = line.Substring(103, 50);
                    customer.Address.State = line.Substring(153, 2);
                    customer.Address.Zip = line.Substring(155, 10);
                }
                else if (line.StartsWith("300"))
                {
                    OrderDetail orderDetails = new OrderDetail();
                    orderDetails.LineNumber = int.Parse(line.Substring(3, 2));
                    orderDetails.Quantity = int.Parse(line.Substring(5, 5));
                    orderDetails.Cost = float.Parse(line.Substring(10, 10));
                    orderDetails.TotalCost = float.Parse(line.Substring(20, 10));
                    orderDetails.Description = line.Substring(30, 50);
                    OrderDetail.Add(orderDetails);
                }
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

    public class Customer
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public Address Address;

        public Customer() { }
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Address() { }
    }

    public class OrderDetail
    {
        public int LineNumber { get; set; }
        public int Quantity { get; set; }
        public float Cost { get; set; }
        public float TotalCost { get; set; }
        public string Description { get; set; }
        public Order Order;

        public OrderDetail(){ }
    }
}
