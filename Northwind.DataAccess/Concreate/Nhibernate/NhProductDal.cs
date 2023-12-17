using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concreate.Nhibernate
{
    public class NhProductDal : IProductDal
    {
        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

   

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

      
        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            List<Product> products = new List<Product>
           {
                new Product{ProductID=1, CategoryID=1, ProductName="Laptop",QuantityPerUnit="1 in a box",UnitPrice=3000,UnitsInStock=2000},
                new Product{ProductID=2, CategoryID=1, ProductName="Mouse",QuantityPerUnit="1 in a box",UnitPrice=50,UnitsInStock=1000},
                new Product{ProductID=3, CategoryID=1, ProductName="Keyboard",QuantityPerUnit="1 in a box",UnitPrice=100,UnitsInStock=500},
                new Product{ProductID=4, CategoryID=1, ProductName="Monitor",QuantityPerUnit="1 in a box",UnitPrice=500,UnitsInStock=200},
                new Product{ProductID=5, CategoryID=1, ProductName="Headphone",QuantityPerUnit="1 in a box",UnitPrice=150,UnitsInStock=100},

            };
            return products;
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
