using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CategoryDtoForUpdate
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}