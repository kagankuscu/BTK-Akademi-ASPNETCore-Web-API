using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Dtos;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Contracts;

namespace Services
{
    public class BookLinks : IBookLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<BookDto> _dataShaper;

        public BookLinks(IDataShaper<BookDto> dataShaper, LinkGenerator linkGenerator)
        {
            _dataShaper = dataShaper;
            _linkGenerator = linkGenerator;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<BookDto> booksDto, string fields, HttpContext httpContext)
        {
            var shapedBooks = ShapedData(booksDto, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedbooks(booksDto, fields, httpContext, shapedBooks);
            
            return ReturnShapedBooks(shapedBooks);
        }

        private LinkResponse ReturnLinkedbooks(IEnumerable<BookDto> booksDto, string fields, HttpContext httpContext, List<Entity> shapedBooks)
        {
            var bookDtoList = booksDto.ToList();

            for(int i = 0; i < bookDtoList.Count(); i++) 
            {
                var bookLinks = CreateForBook(httpContext, bookDtoList[i], fields);
                shapedBooks[i].Add("Links", bookLinks);
            }

            var bookCollection = new LinkCollectionWrapper<Entity>(shapedBooks);
            CreateForBooks(httpContext, bookCollection);
            return new LinkResponse
            {
                HasLinks = true,
                LinkedEntities = bookCollection
            };
        }

        private LinkCollectionWrapper<Entity> CreateForBooks(HttpContext httpContext, LinkCollectionWrapper<Entity> bookCollectionWrapper)
        {
            bookCollectionWrapper.Links.AddRange
            (
                new List<Link>
                {
                    new Link
                    {
                        Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString()?.ToLower()}",
                        Rel = "self",
                        Method = "GET"
                    },
                    new Link
                    {
                        Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString()?.ToLower()}",
                        Rel = "create",
                        Method = "POST"
                    }
                }
            );

            return bookCollectionWrapper;
        }

        private List<Link> CreateForBook(HttpContext httpContext, BookDto bookDto, string fields)
        {
            var links = new List<Link>
            {
                new Link
                {
                    Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString()?.ToLower()}/{bookDto.Id}",
                    Rel = "self",
                    Method = "GET"
                },
                new Link
                {
                    Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString()?.ToLower()}/{bookDto.Id}",
                    Rel = "self",
                    Method = "PUT"
                },
                new Link
                {
                    Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString()?.ToLower()}/{bookDto.Id}",
                    Rel = "self",
                    Method = "DELETE"
                },
            };
            return links;
        }

        private LinkResponse ReturnShapedBooks(List<Entity> shapedBooks)
        {
            return new LinkResponse 
            {
                ShapedEntities = shapedBooks
            };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

            return mediaType
                .SubTypeWithoutSuffix
                .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapedData(IEnumerable<BookDto> booksDto, string fields)
        {
            return _dataShaper.ShapeData(booksDto, fields).Select(b => b.Entity).ToList();
        }
    }
}