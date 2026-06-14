namespace ProductImportApi.Dtos
{
    /// <summary>
    /// 商品の入力データを受け取るリクエストDTO 
    /// </summary>
    public class CreateProductRequest
    {
        public string Name { get; set; } = "";
        public int Price { get; set; }
        public int Stock { get; set; }
    }
    /// IDがない理由は入力データから勝手に決められないようにするため 
}
