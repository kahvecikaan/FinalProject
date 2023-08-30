using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal 
    // note that we need IProductDal for business layer and flexibility of it
{
    
}