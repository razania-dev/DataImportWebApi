using ProductImportApi.Dtos;
using ProductImportApi.Models;

namespace ProductImportApi.Properties.Services
{
    public class ProductService
    {
        private static readonly List<Product> Products = new();
        private static int _nextId = 1;

        public ProductResponse CreateProduct(CreateProductRequest request)
        {
            var product = new Product
            {
                Id = _nextId++,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            Products.Add(product);

            return ToResponse(product);

        }

        public List<ProductResponse> GetProducts()
        {
            return Products
                .Select(product => ToResponse(product))
                .ToList();
        }

        public ProductResponse? GetProduct(int id)
        {
            var product = Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return null;
            }

            return ToResponse(product);
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
}
