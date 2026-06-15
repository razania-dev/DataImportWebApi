# DB設計書

## 概要

本APIでは、Swaggerから送信された商品データをMySQLのproductsテーブルに保存します。

## データベース名

```text
product_import_db
```

## テーブル一覧

| テーブル名    | 内容             |
| -------- | -------------- |
| products | 商品データを管理するテーブル |

## products テーブル

| カラム名  | 型            | 制約                          | 内容   |
| ----- | ------------ | --------------------------- | ---- |
| id    | INT          | PRIMARY KEY, AUTO_INCREMENT | 商品ID |
| name  | VARCHAR(100) | NOT NULL                    | 商品名  |
| price | INT          | NOT NULL                    | 価格   |
| stock | INT          | NOT NULL                    | 在庫数  |

## CREATE TABLE文

```sql
CREATE TABLE products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price INT NOT NULL,
    stock INT NOT NULL
);
```

## C#のProductモデルとの対応

| productsテーブル | Productクラス | 内容   |
| ------------ | ---------- | ---- |
| id           | Id         | 商品ID |
| name         | Name       | 商品名  |
| price        | Price      | 価格   |
| stock        | Stock      | 在庫数  |

## 登録データ例

```sql
INSERT INTO products (name, price, stock)
VALUES ('Keyboard', 3000, 10);
```

## 確認用SQL

```sql
SELECT * FROM products;
```

## 備考

idはMySQLのAUTO_INCREMENTによって自動で番号を振ります。
そのため、APIから商品登録を行う際にはidを送信せず、name、price、stockのみをJSON形式で送信します。
