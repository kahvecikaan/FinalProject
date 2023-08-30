using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI;

class Program
{
    static void Main(string[] args)
    {
        // ProductTest();
        var categoryManager = new CategoryManager(new EfCategoryDal());
        foreach (var category in categoryManager.GetAll())
        {
            Console.WriteLine(category.CategoryName);
        }
    }

    private static void ProductTest()
    {
        var productManager = new ProductManager(new EfProductDal());

        foreach (var product in productManager.GetAllByCategoryId(2))
        {
            Console.WriteLine(product.ProductName);
        }
    }
}