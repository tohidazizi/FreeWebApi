using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Thrita.Web.Api.FreeWebApi.Models;
using Thrita.Web.Api.FreeWebApi.Models.Entities;

namespace Thrita.Web.Api.FreeWebApi.Controllers
{
    public class ProductsController : ApiController
    {
        IRepository _repository;

        public ProductsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Products
        public IEnumerable<Product> Get()
        {
            return _repository.GetProducts();
        }

        // GET: api/Products/5
        public Product Get(int id)
        {
            Product product = _repository.GetProduct(id);

            if (product == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found!"));
            }

            return product;
        }

        // POST: api/Products
        public Product Post([FromBody]Product product)
        {
            return _repository.AddProduct(product);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]Product product)
        {
            bool notFound;
            product.Id = id;

            _repository.UpdateProduct(product, out notFound);

            if (notFound)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found!"));
            }
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
            bool notFound;

            _repository.DeleteProduct(id, out notFound);

            if (notFound)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found!"));
            }
        }

        public HttpResponseMessage Post(string id)
        {
            if ("UPLOAD".Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                return ProductBulkUpload();
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Command is not supported!");
            }
        }

        private HttpResponseMessage ProductBulkUpload()
        {
            List<Product> products = new List<Product>();

            Task<Stream> taskReadStream = this.Request.Content.ReadAsStreamAsync();
            taskReadStream.Wait();

            using (Stream requestStream = taskReadStream.Result)
            {
                using (StreamReader streamReader = new StreamReader(requestStream))
                {
                    string stringLine;

                    while ((stringLine = streamReader.ReadLine()) != null)
                    {
                        Product product = Product.Generate(stringLine);

                        if (product != null)
                        {
                            Post(product);
                            products.Add(product);
                        }
                    }
                }
            }

            return Request.CreateResponse<IEnumerable<Product>>(products.AsEnumerable());
        }
    }
}
