using FluentValidation;
using Northwind.Business.Abstract;
using Northwind.Business.Utility;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concreate.EntityFramework;
using Northwind.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concreate
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

 
        public void Add(Product product)
        {
            ProductValidator validations = new ProductValidator();
            ValidationTools.Validate(validations, product);

            _productDal.Add(product);
        }
      

        public List<Product> Get(Expression<Func<Product, bool>> filter)
        {
            return _productDal.GetAll(filter);
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
           return _productDal.GetAll(filter);
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryID == categoryId);
        }

        public List<Product> GetProductsByName(string productName,int categoryId)
        {
            if (categoryId == 0)
            {
                return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
            }
            else
            {
                return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()) && p.CategoryID == categoryId);
            }
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public void Delete(Product product)
        {
          try
            {
                _productDal.Delete(product);
            }
            catch 
            {
               throw new Exception("Ürün Silme Başarısız Oldu");
            }
        }
    }
}
