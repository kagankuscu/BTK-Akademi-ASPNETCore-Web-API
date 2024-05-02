using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CategoryNotFoundException : BadRequestException
    {
        public CategoryNotFoundException(int id) : base($"The category with id: {id} could not found.")
        {
        }
    }
}