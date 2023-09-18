using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;

    public CategoryManager(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }

    public IDataResult<List<Category>> GetAll()
    {
       // business logic...
       return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
    }

    public IDataResult<Category> GetById(int categoryId)
    {
        // business logic...
        return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
    }
}