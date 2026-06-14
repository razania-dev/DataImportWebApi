using MySqlConnector;
using ProductImportApi.Models;

namespace ProductImportApi.Repositories;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    public Product Add(Product product)
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        var sql = @"
            INSERT INTO products (name, price, stock)
            VALUES (@name, @price, @stock);
            SELECT LAST_INSERT_ID();
        ";

        using var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@stock", product.Stock);

        var newId = Convert.ToInt32(command.ExecuteScalar());

        product.Id = newId;

        return product;
    }

    public List<Product> GetAll()
    {
        var products = new List<Product>();

        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        var sql = @"
            SELECT id, name, price, stock
            FROM products;
        ";

        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var product = new Product
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                Price = reader.GetInt32("price"),
                Stock = reader.GetInt32("stock")
            };

            products.Add(product);
        }

        return products;
    }

    public Product? GetById(int id)
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        var sql = @"
            SELECT id, name, price, stock
            FROM products
            WHERE id = @id;
        ";

        using var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read() == false)
        {
            return null;
        }

        return new Product
        {
            Id = reader.GetInt32("id"),
            Name = reader.GetString("name"),
            Price = reader.GetInt32("price"),
            Stock = reader.GetInt32("stock")
        };
    }
}