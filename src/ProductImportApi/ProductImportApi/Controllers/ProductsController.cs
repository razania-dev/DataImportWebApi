using Microsoft.AspNetCore.Mvc;
using ProductImportApi.Dtos;
using ProductImportApi.Models;
using ProductImportApi.Properties.Services;

namespace ProductImportApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public ActionResult<ProductResponse> CreateProduct(CreateProductRequest request)
    {
        var response = _productService.CreateProduct(request);

        return Ok(response);
    }

    [HttpGet]
    public ActionResult<List<ProductResponse>> GetProducts()
    {
        var responses = _productService.GetProducts();

        return Ok(responses);
    }

    [HttpGet("{id}")]
    public ActionResult<ProductResponse> GetProduct(int id)
    {
        var response = _productService.GetProduct(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
