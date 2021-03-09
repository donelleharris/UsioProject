using System.Collections.Generic;

namespace UsioProject
{
    public class LineItem
    {
        private int LineNumber { get; set; }
        private int Quantity { get; set; }
        private float Cost { get; set; }
        private float TotalCost { get; set; }
        private string Description { get; set; }

        public LineItem() { }

        public static void ParseLineItems(List<string> lineItems)
        {
            LineItem lineItem = new LineItem();
            foreach(var lineNumber in lineItems)
            {
                lineItem.LineNumber = int.Parse(lineNumber.Substring(3, 2));
                lineItem.Quantity = int.Parse(lineNumber.Substring(5, 5));
                lineItem.Cost = float.Parse(lineNumber.Substring(10, 10));
                lineItem.TotalCost = float.Parse(lineNumber.Substring(20, 10));
                lineItem.Description = lineNumber.Substring(30);
            }

        }
        

        
    }
}
