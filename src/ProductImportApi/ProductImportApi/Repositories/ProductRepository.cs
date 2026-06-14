using ProductImportApi.Models;

namespace ProductImportApi.Repositories;

public class ProductRepository
{
    private static readonly List<Product> Products = new();
    private static int _nextId = 1;

    public Product Add(Product product)
    {
        product.Id = _nextId++;
        Products.Add(product);

        return product;
    }

    public List<Product> GetAll()
    {
        return Products;
    }

    public Product? GetById(int id)
    {
        return Products.FirstOrDefault(x => x.Id == id);
    }
}