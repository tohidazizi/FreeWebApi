using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrita.Web.Api.FreeWebApi.Models;
using Thrita.Web.Api.FreeWebApi.Models.Entities;

namespace Thrita.Web.Api.FreeWebApi.Repositories.InMemoryDb
{
    public class MemoryDb : IRepository
    {
        private static readonly object _productsLock = new Object();

        //protected readonly string _ipAddress = GetIPAddress();

        public List<Product> GetProducts()
        {
            return MemoryDataContainer.Products;
        }

        public Product GetProduct(int id)
        {
            return MemoryDataContainer.Products.SingleOrDefault(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            lock (_productsLock)
            {
                int newId = MemoryDataContainer.Products.Max(p => p.Id) + 1;
                product.Id = newId;
                MemoryDataContainer.Products.Add(product);
            }

            return product;
        }

        public void UpdateProduct(Product product, out bool notFound)
        {
            Product currentProduct = MemoryDataContainer.Products.SingleOrDefault(p => p.Id == product.Id);

            notFound = currentProduct == null;

            if (!notFound)
            {
                currentProduct.Name = product.Name;
            }
        }


        public void DeleteProduct(int id, out bool notFound)
        {
            lock (_productsLock)
            {
                notFound = MemoryDataContainer.Products.RemoveAll(p => p.Id == id) == 0;
            }
        }


        protected static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

    }
}
