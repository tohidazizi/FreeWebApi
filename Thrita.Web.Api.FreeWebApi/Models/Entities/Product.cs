using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Thrita.Web.Api.FreeWebApi.Models.Entities
{
    [DataContract]
    public class Product
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [IgnoreDataMember] // Note: Not necessary to explicitly say it.        
        public int InStock { get; set; }

        [DataMember(Name = "isInStock")]
        public bool IsInStock
        {
            get { return this.InStock > 0; }
            private set { /* To prevent XML Media Formatter to raise InvalidDataContractException! */ }
        }

        public static Product Generate(string str)
        {
            Product product = null;

            if (string.IsNullOrWhiteSpace(str))
            {
                return product;
            }

            string[] fields = str.Split(new char[] { ',' });
            int numberOfFields = fields.Count();

            int inStock;

            product = new Product
            {
                Name = fields[0],
                InStock = (numberOfFields > 1) ? (int.TryParse(fields[1], out inStock) ? inStock : 0) : 0
            };

            return product;
        }
    }
}