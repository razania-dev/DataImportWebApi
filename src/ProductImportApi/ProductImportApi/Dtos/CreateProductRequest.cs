using System.ComponentModel.DataAnnotations;

namespace ProductImportApi.Dtos
{
    /// <summary>
    /// 商品の入力データを受け取るリクエストDTO 
    /// </summary>
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "商品名は必須です。")]
        [StringLength(100, ErrorMessage = "商品名は100文字以内で入力してください。")]
        public string Name { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = "価格は1以上で入力してください。")]
        public int Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "在庫数は0以上で入力してください。")]
        public int Stock { get; set; }
    }

    /// IDがない理由は入力データから勝手に決められないようにするため 
}
