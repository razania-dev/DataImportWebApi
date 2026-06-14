using Microsoft.AspNetCore.Mvc;
using ProductImportApi.Dtos;
using ProductImportApi.Models;

namespace ProductImportApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products = new();
    private static int _nextId = 1;

    [HttpPost]
    public ActionResult<Product> CreateProduct(CreateProductRequest request)
    {
        var product = new Product
        {
            Id = _nextId++,
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        var response = new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock
        };

        Products.Add(product);

        return Ok(product);
    }

    [HttpGet]
    public ActionResult<List<Product>> GetProducts()
    {
        var responses = Products.Select(product => new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock
        }).ToList();

        return Ok(Products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = Products.FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        var response = new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock
        };

        return Ok(product);
    }
}
