using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrita.Web.Api.FreeWebApi.Models.Entities;

namespace Thrita.Web.Api.FreeWebApi.Models
{
    public interface IRepository
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);

        void UpdateProduct(Product product, out bool notFound);

        void DeleteProduct(int id, out bool notFound);
    }
}
