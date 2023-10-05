using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Performance;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly ICategoryService _categoryService;

    public ProductManager(IProductDal productDal, ICategoryService categoryService)
    {
        _productDal = productDal;
        _categoryService = categoryService;
    }

    [CacheAspect]
    public IDataResult<List<Product>> GetAll()
    {
        if (DateTime.Now.Hour == 22)
        {
            return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        }
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
    }

    public IDataResult<List<Product>> GetAllByCategoryId(int id)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
    }

    public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
    {
        return new SuccessDataResult<List<Product>>
            (_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductsListed);
    }
    
    [SecuredOperation("product.add,admin")]
    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    public IResult Add(Product product)
    {
        IResult? result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
            CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExceeded());
        if (result != null)
        {
            return result;
        }
        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    [CacheAspect]
    [PerformanceAspect(10)]
    public IDataResult<Product> GetById(int productId)
    {
        return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
    }

    
    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    public IResult Update(Product product)
    {
        IResult? result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
            CheckIfProductCountOfCategoryCorrect(product.CategoryId));
        if (result != null)
        {
            return result;
        }
        
        _productDal.Update(product);
        return new SuccessResult(Messages.ProductUpdated);
    }

    [TransactionScopeAspect]
    public IResult? AddTransactionalTest(Product product)
    {
        Add(product);
        if (product.UnitPrice < 10)
        {
            throw new Exception("");
        }
        Add(product);
        return null;
    }

    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        // e.g., select count(*) from products where categoryId = 1 (this what goes to db as a LINQ query)
        var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
        if (result >= 10)
        {
            return new ErrorResult(Messages.ProductCountOfCategoryError);
        }
        return new SuccessResult();
    }
    
    private IResult CheckIfProductNameExists(string productName)
    {
        // select count(*) from products where productName = "productName"
        var result = _productDal.GetAll(p => p.ProductName == productName).Any();
        if (result)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }
        return new SuccessResult();
    }
    
    private IResult CheckIfCategoryLimitExceeded()
    {
        var result = _categoryService.GetAll();
        if (result.Data != null && result.Data.Count > 15)
        {
            return new ErrorResult(Messages.CategoryLimitExceeded);
        }
        return new SuccessResult();
    }
}