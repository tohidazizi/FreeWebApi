using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrita.Web.Api.FreeWebApi.Models.Entities;

namespace Thrita.Web.Api.FreeWebApi.Repositories.InMemoryDb
{
    internal static class MemoryDataContainer
    {
        private static List<Product> _products;

        public static List<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = GenerateFakeProducts();
                }
                return _products;
            }
        }

        private static List<Product> GenerateFakeProducts()
        {
            List<Product> products = new List<Product>();

            products.Add(new Product { Id = 1, Name = "Product 01", InStock = 23 });
            products.Add(new Product { Id = 2, Name = "Product 02", InStock = 10 });
            products.Add(new Product { Id = 3, Name = "Product 03", InStock = 2 });
            products.Add(new Product { Id = 4, Name = "Product 04", InStock = 192 });
            products.Add(new Product { Id = 5, Name = "Product 05", InStock = 63 });

            return products;
        }
    }
}
