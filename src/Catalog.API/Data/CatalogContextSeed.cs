using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct =  productCollection.Find(p => true).Any();
            if(!existProduct)
            {
                productCollection.InsertManyAsync(GetSampleProducts());
            }
        }

        public static IEnumerable<Product> GetSampleProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id ="5de491c069ee2123b48daae8",
                    Name = "Laptop 1",
                    Summary = "Laptop summary 1",
                    Description = "Laptop Description 1",
                    ImageFile = "no-image.jpg",
                    Price = 900.00M,
                    Category = "Laptop"
                },
                new Product()
                {
                    Id ="5de491c069ee2123b48daae1",
                    Name = "Mobile 1",
                    Summary = "Mobile summary 1",
                    Description = "Mobile Description 1",
                    ImageFile = "no-image.jpg",
                    Price = 800.00M,
                    Category = "Mobile"
                },
                new Product()
                {
                    Id ="5de491c069ee2123b48daae2",
                    Name = "Desktop 1",
                    Summary = "Desktop summary 1",
                    Description = "Desktop Description 1",
                    ImageFile = "no-image.jpg",
                    Price = 600.00M,
                    Category = "Desktop"
                },
            };
        }
   
    }
}
