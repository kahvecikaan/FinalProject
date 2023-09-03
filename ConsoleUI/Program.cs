﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

namespace ConsoleUI;

class Program
{
    static void Main(string[] args)
    {
        ProductTest();
        // CategoryTest();
    }

    private static void CategoryTest()
    {
        var categoryManager = new CategoryManager(new EfCategoryDal());
        foreach (var category in categoryManager.GetAll())
        {
            Console.WriteLine(category.CategoryName);
        }
    }

    private static void ProductTest()
    {
        var productManager = new ProductManager(new EfProductDal());
        var result = productManager.GetProductDetails();

        if (result.Success)
        {
            if (result.Data != null)
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " / " + product.CategoryName);
                }
        }
        else
        {
            Console.WriteLine(result.Message);
        }
    }
}