using System;
using System.Collections.Generic;
using System.Text;

namespace MMABooksBusinessClasses
{
    public class Product
    {
        public Product() { }

        public Product(string productCode, string description, decimal unitPrice, int onHandQuantity)
        {
            ProductCode = productCode;
            Description = description;
            UnitPrice = unitPrice;
            OnHandQuantity = onHandQuantity;
        }

        private string productCode;
        private string description;
        private decimal unitPrice;
        private int onHandQuantity;
        
        public string ProductCode
        {
            get
            {
                return productCode;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 10)
                    productCode = value;
                else
                    throw new ArgumentOutOfRangeException("ProductCode have at least one char but no more than 10.");
            }

        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 50)
                    description = value;
                else
                    throw new ArgumentOutOfRangeException("description must be at least 1 character and no more than 50 characters");
            }

        }

        public decimal UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                if (value > 0m)
                    unitPrice = value;
                else
                    throw new ArgumentOutOfRangeException("unitPrice must be at Positive value");
            }

        }

        public int OnHandQuantity
        {
            get
            {
                return onHandQuantity;
            }
            set
            {
                if (value >= 0)
                    onHandQuantity = value;
                else
                    throw new ArgumentOutOfRangeException("onHandQuantity must be 0 or a positve integer");
            }

        }
        public override string ToString()
        {
            return productCode + ", " + description;
        }
    }
}
