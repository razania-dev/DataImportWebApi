# DataImportWebApi
SwaggerからJSON形式のデータを送信し、その内容をC#で受け取ってMySQLに保存するWeb API

# ProductImportApi

## 概要

ProductImportApi は、SwaggerからJSON形式の商品データを送信し、ASP.NET Core Web APIで受け取り、MySQLに保存するWeb APIです。

商品データの登録、一覧取得、ID指定による1件取得に対応しています。

## 作成目的

C#、ASP.NET Core Web API、MySQLを使用して、実務に近いWeb API開発の流れを学習することを目的として作成しました。

本プロジェクトでは、以下の内容を学習・実装しています。

* Swaggerを使用したAPI動作確認
* JSON形式のリクエスト受信
* DTOを使用した入力データの受け取り
* Controller / Service / Repository の責務分離
* MySQLへのデータ登録・取得
* 入力チェック
* GitHubでのソースコード管理
* 接続情報の管理

## 使用技術

* C#
* ASP.NET Core Web API
* MySQL
* MySqlConnector
* Swagger / OpenAPI
* Git / GitHub

## 主な機能

| 機能     | 内容                           |
| ------ | ---------------------------- |
| 商品登録   | JSON形式の商品データを受け取り、MySQLに保存する |
| 商品一覧取得 | 登録済みの商品データを一覧で取得する           |
| 商品1件取得 | IDを指定して商品データを1件取得する          |
| 入力チェック | 商品名、価格、在庫数の入力値を検証する          |

## API一覧

| メソッド | URL                | 内容               |
| ---- | ------------------ | ---------------- |
| POST | /api/Products      | 商品データを登録する       |
| GET  | /api/Products      | 商品一覧を取得する        |
| GET  | /api/Products/{id} | IDを指定して商品を1件取得する |

## リクエスト例

### POST /api/Products

```json
{
  "name": "Keyboard",
  "price": 3000,
  "stock": 10
}
```

## レスポンス例

```json
{
  "id": 1,
  "name": "Keyboard",
  "price": 3000,
  "stock": 10
}
```

## 入力チェック

| 項目    | 条件         |
| ----- | ---------- |
| name  | 必須、100文字以内 |
| price | 1以上        |
| stock | 0以上        |

入力値が不正な場合は、400 Bad Requestを返します。

## 構成

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
└ Program.cs
```

## 処理の流れ

```text
Swagger
↓
ProductsController
↓
ProductService
↓
ProductRepository
↓
MySQL products テーブル
```

## データベース

### データベース名

```text
product_import_db
```

### products テーブル

```sql
CREATE TABLE products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price INT NOT NULL,
    stock INT NOT NULL
);
```

## 設定ファイルについて

本プロジェクトでは、DB接続情報を以下のように管理しています。

* `appsettings.json`

  * GitHubに公開する基本設定
  * 本物のDBパスワードは記載しない

* `appsettings.Development.json`

  * ローカル開発用の接続情報
  * 本物のDBパスワードを記載する
  * GitHubには公開しない

`.gitignore` には以下を追加しています。

```gitignore
**/appsettings.Development.json
```

## 今後追加予定

* PUT /api/Products/{id} による商品更新
* DELETE /api/Products/{id} による商品削除
* エラーハンドリングの改善
* READMEへの実行手順追加
* API仕様の整理

```
```

