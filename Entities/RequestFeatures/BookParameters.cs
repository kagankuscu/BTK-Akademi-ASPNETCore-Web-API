using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters
    {
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set;} = 1000;
        public bool ValidPriceRange => MaxPrice > MinPrice;

        public string? SearchTerm { get; set; }

        public BookParameters()
        {
            OrderBy = "title";
        }
    }
}