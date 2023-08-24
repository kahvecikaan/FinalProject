using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory;

public class InMemoryProductDal : IProductDal
{
    private List<Product> _products;

    public InMemoryProductDal()
    {
        // for simulation purposes
        _products = new List<Product>()
        {
            new Product()
            {
                ProductId = 1,
                CategoryId = 1,
                ProductName = "Glass",
                UnitPrice = 15,
                UnitsInStock = 15
            },
            new Product()
            {
                ProductId = 2,
                CategoryId = 1,
                ProductName = "Camera",
                UnitPrice = 500,
                UnitsInStock = 3
            },
            new Product()
            {
                ProductId = 3,
                CategoryId = 2,
                ProductName = "Phone",
                UnitPrice = 1500,
                UnitsInStock = 2
            },
            new Product()
            {
                ProductId = 4,
                CategoryId = 2,
                ProductName = "Keyboard",
                UnitPrice = 150,
                UnitsInStock = 65
            },
            new Product()
            {
                ProductId = 5,
                CategoryId = 2,
                ProductName = "Mouse",
                UnitPrice = 85,
                UnitsInStock = 1
            }
        };
    }
    public List<Product> GetAll()
    {
        return _products;
    }

    public List<Product> GetAll(Expression<Func<Product, bool>>? filter = null)
    {
        throw new NotImplementedException();
    }

    public Product Get(Expression<Func<Product, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public void Update(Product product)
    {
        var productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
        if (productToUpdate != null)
        {
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

    }

    public void Delete(Product product) // a parameter like "productId" would be better
    {
        var productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
        if (productToDelete != null)
        {
            _products.Remove(productToDelete);   
        }
    }

    public List<Product> GetAllByCategory(int categoryId)
    {
        return _products.Where(p => p.CategoryId == categoryId).ToList();
    }
}