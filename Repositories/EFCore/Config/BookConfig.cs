using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Karagöz ve Hacivat", Price = 75, CategoryId = 1},
                new Book { Id = 2, Title = "Mesnevi", Price = 175, CategoryId = 2},
                new Book { Id = 3, Title = "Devlet", Price = 255, CategoryId = 1}
            );
        }
    }
}