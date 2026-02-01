using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA.Domain.Exceptions
{
    public class InsufficientStockException : DomainException
    {
        public InsufficientStockException(string sku)
            : base($"Insufficient stock for SKU '{sku}'") { }
    }
}
