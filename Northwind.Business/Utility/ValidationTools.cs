using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Utility
{
    public static class ValidationTools
    {
        public static void Validate<T>(IValidator<T> validator,T entity)
        {
           var result=validator.Validate(entity);
            if (result.Errors.Count>0)
            {
                throw new ValidationException(result.Errors);
            }
        }
       
    }
}
