namespace ProductImportApi.Dtos;

/// <summary>
/// 商品の出力データを受け取るレスポンスDTO
/// </summary>

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public int Stock { get; set; }
}

