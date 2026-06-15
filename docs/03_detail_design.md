# 詳細設計書

## 概要

本APIは、Swaggerから送信されたJSON形式の商品データをASP.NET Core Web APIで受け取り、MySQLのproductsテーブルに保存する。

内部処理は以下の層に分ける。

* Controller
* Service
* Repository
* DTO
* Model

## フォルダ構成

```text
ProductImportApi
├ Controllers
│  └ ProductsController.cs
├ Dtos
│  ├ CreateProductRequest.cs
│  └ ProductResponse.cs
├ Models
│  └ Product.cs
├ Repositories
│  └ ProductRepository.cs
├ Services
│  └ ProductService.cs
├ appsettings.json
├ appsettings.Development.json
└ Program.cs
```

## 各クラスの役割

| クラス名                         | 役割                                        |
| ---------------------------- | ----------------------------------------- |
| ProductsController           | APIの入口。HTTPリクエストを受け取り、Serviceを呼び出す        |
| ProductService               | 商品登録・取得などの処理を行う                           |
| ProductRepository            | MySQLとのデータ送受信を行う                          |
| Product                      | productsテーブルのデータを表すモデル                    |
| CreateProductRequest         | 商品登録時にSwaggerから受け取るJSONデータ                |
| ProductResponse              | APIから返す商品データ                              |
| Program.cs                   | ServiceやRepositoryをDIコンテナに登録し、アプリの起動設定を行う |
| appsettings.json             | GitHubに公開可能な基本設定を管理する                     |
| appsettings.Development.json | ローカル開発用の接続情報を管理する                         |

## 処理の流れ

### 商品登録

```text
Swagger
↓
POST /api/Products
↓
ProductsController
↓
ProductService
↓
ProductRepository
↓
MySQL products テーブル
↓
ProductResponse
↓
SwaggerへJSON形式で返却
```

## 商品登録の詳細

1. Swaggerから商品データをJSON形式で送信する
2. ProductsControllerがCreateProductRequestとして受け取る
3. ProductServiceがCreateProductRequestをProductモデルに変換する
4. ProductRepositoryがMySQLのproductsテーブルへINSERTする
5. MySQLで自動採番されたidをProductに設定する
6. ProductServiceがProductをProductResponseに変換する
7. ProductsControllerがProductResponseをJSON形式で返す

## 商品一覧取得の流れ

```text
Swagger
↓
GET /api/Products
↓
ProductsController
↓
ProductService
↓
ProductRepository
↓
MySQL products テーブル
↓
ProductResponseのリスト
↓
SwaggerへJSON形式で返却
```

## 商品1件取得の流れ

```text
Swagger
↓
GET /api/Products/{id}
↓
ProductsController
↓
ProductService
↓
ProductRepository
↓
MySQL products テーブル
↓
ProductResponse
↓
SwaggerへJSON形式で返却
```

指定したIDの商品が存在しない場合は、404 Not Foundを返す。

## DTO設計

### CreateProductRequest

商品登録時に外部から受け取るデータを表す。

```csharp
public class CreateProductRequest
{
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public int Stock { get; set; }
}
```

入力チェックは以下の通り。

| 項目    | 条件         |
| ----- | ---------- |
| Name  | 必須、100文字以内 |
| Price | 1以上        |
| Stock | 0以上        |

### ProductResponse

APIから外部へ返す商品データを表す。

```csharp
public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public int Stock { get; set; }
}
```

## Model設計

### Product

MySQLのproductsテーブルに対応するモデル。

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public int Stock { get; set; }
}
```

## Repository設計

ProductRepositoryは、MySQLへの接続とSQL実行を担当する。

主な処理は以下の通り。

| メソッド    | 内容                      |
| ------- | ----------------------- |
| Add     | 商品データをproductsテーブルへ登録する |
| GetAll  | productsテーブルから商品一覧を取得する |
| GetById | 指定したIDの商品データを1件取得する     |

## Service設計

ProductServiceは、ControllerとRepositoryの間に入り、商品に関する処理を担当する。

主な処理は以下の通り。

| メソッド          | 内容          |
| ------------- | ----------- |
| CreateProduct | 商品登録処理を行う   |
| GetProducts   | 商品一覧取得処理を行う |
| GetProduct    | 商品1件取得処理を行う |

## DI設定

Program.csでServiceとRepositoryを登録する。

```csharp
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();
```

これにより、ProductsControllerのコンストラクタでProductServiceを受け取れるようにする。

```csharp
public ProductsController(ProductService productService)
{
    _productService = productService;
}
```

## 設定ファイル

### appsettings.json

GitHubに公開する基本設定を記載する。
本物のDBパスワードは記載しない。

### appsettings.Development.json

ローカル開発環境で使用する接続文字列を記載する。
本物のDBパスワードを含むため、GitHubには公開しない。

`.gitignore` に以下を追加する。

```gitignore
**/appsettings.Development.json
```

## 今後追加予定

* PUT /api/Products/{id} による商品更新
* DELETE /api/Products/{id} による商品削除
* 例外処理の改善
* READMEの整備

