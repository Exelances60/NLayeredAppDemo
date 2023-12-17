using Northwind.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll(Expression<Func<Product, bool>> filter = null  ); // Expression<Func<Product, bool>> filter = null
        List<Product> Get(Expression<Func<Product, bool>> filter);
        void Add(Product product);
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetProductsByName(string productName,int categoryId);
        void Update(Product product);
        void Delete(Product product);
    }
}
