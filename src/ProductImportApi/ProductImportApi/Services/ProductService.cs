using ProductImportApi.Dtos;
using ProductImportApi.Models;
using ProductImportApi.Repositories;

namespace ProductImportApi.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ProductResponse CreateProduct(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        var createdProduct = _productRepository.Add(product);

        return ToResponse(createdProduct);
    }

    public List<ProductResponse> GetProducts()
    {
        var products = _productRepository.GetAll();

        return products
            .Select(product => ToResponse(product))
            .ToList();
    }

    public ProductResponse? GetProduct(int id)
    {
        var product = _productRepository.GetById(id);

        if (product == null)
        {
            return null;
        }

        return ToResponse(product);
    }

    public ProductResponse? UpdateProduct(int id, UpdateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        var updatedProduct = _productRepository.Update(id, product);

        if (updatedProduct == null)
        {
            return null;
        }

        return ToResponse(updatedProduct);
    }

    private ProductResponse ToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock
        };
    }
}