﻿using Business.Concrete;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI;

class Program
{
    static void Main(string[] args)
    {
        var productManager = new ProductManager(new InMemoryProductDal());

        foreach (var product in productManager.GetAll())
        {
            Console.WriteLine(product.ProductName);
        }
    }
}