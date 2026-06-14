# 基本設計書

## 概要

このAPIは、SwaggerからJSON形式の商品データを送信し、C#のWeb APIで受け取り、MySQLに保存するためのAPIです。

## API一覧

| メソッド | URL | 内容 |
|---|---|---|
| POST | /api/products | 商品データを登録する |
| GET | /api/products | 商品一覧を取得する |
| GET | /api/products/{id} | IDを指定して商品を1件取得する |

## 登録JSON例

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

## 処理の流れ

1. SwaggerからJSON形式の商品データを送信する
2. ASP.NET Core Web APIのControllerで受け取る
3. 受け取ったデータをC#のクラスに変換する
4. MySQLのproductsテーブルに保存する
5. 登録結果をJSON形式で返す
