# 詳細設計書

## 概要

本APIは、Swaggerから送信されたJSON形式の商品データをASP.NET Core Web APIで受け取り、MySQLのproductsテーブルに保存する。

処理は以下の構成で分ける。

- Controller
- Service
- Repository
- Model
- DTO

## フォルダ構成

```text
ProductImportApi
├ Controllers
│  └ ProductsController.cs
├ Services
│  └ ProductService.cs
├ Repositories
│  └ ProductRepository.cs
├ Models
│  └ Product.cs
├ Dtos
│  ├ CreateProductRequest.cs
│  └ ProductResponse.cs
└ Program.cs
```

## 各クラスの役割

| クラス名 | 役割 |
|---|---|
| ProductsController | APIの入口。Swaggerや画面から送信されたリクエストを受け取る |
| ProductService | 商品登録や取得などの業務処理を行う |
| ProductRepository | MySQLとのやり取りを行う |
| Product | productsテーブルのデータを表す |
| CreateProductRequest | 商品登録時に受け取るJSONデータを表す |
| ProductResponse | APIから返す商品データを表す |

## 処理の流れ

### 商品登録

```text
Swagger
↓
POST /api/products
↓
ProductsController
↓
ProductService
↓
ProductRepository
↓
MySQL products テーブル
```

## 登録時のリクエスト

```json
{
  "name": "Keyboard",
  "price": 3000,
  "stock": 10
}
```

## 登録時のレスポンス

```json
{
  "id": 1,
  "name": "Keyboard",
  "price": 3000,
  "stock": 10
}
```

## Product モデル

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
}
```

## CreateProductRequest DTO

```csharp
public class CreateProductRequest
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
}
```

## ProductResponse DTO

```csharp
public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
}
```

## API設計

| メソッド | URL | 処理 |
|---|---|---|
| POST | /api/products | 商品データを登録する |
| GET | /api/products | 商品一覧を取得する |
| GET | /api/products/{id} | IDを指定して商品を1件取得する |

## 備考

最初は登録、一覧取得、1件取得のみを実装する。  
更新と削除は、基本機能が完成したあとに追加する。
