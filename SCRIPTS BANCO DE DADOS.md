# Database Setup — MarketCheckout

Script de criação do banco de dados e tabelas do projeto **MarketCheckoutApi**.  
---

## 1. Criar o Banco de Dados

```sql
CREATE DATABASE "MarketCheckout";
```

---

## 2. Tabela `Produto`

Armazena os produtos buscados da API externa (DummyJSON) e persistidos localmente.

```sql
CREATE TABLE "Produto" (
    "Id"    INTEGER         NOT NULL,
    "Nome"  VARCHAR(200)    NOT NULL,
    "Valor" NUMERIC(18, 2)  NOT NULL,

    CONSTRAINT "PK_Produto" PRIMARY KEY ("Id")
);
```

---

## 3. Tabela `Carrinho`

Representa o carrinho de compras de um comprador.

```sql
CREATE TABLE "Carrinho" (
    "Id"            SERIAL          NOT NULL,
    "NomeComprador" VARCHAR(200)    NOT NULL,
    "CpfComprador"  VARCHAR(11)     NOT NULL,
    "CriadoPor"     VARCHAR(200)    NOT NULL,
    "DataCriacao"   TIMESTAMP       NOT NULL,

    CONSTRAINT "PK_Carrinho" PRIMARY KEY ("Id")
);
```

---

## 4. Tabela `ItemCarrinho`

Representa os itens vinculados a um carrinho, com referência ao produto.

```sql
CREATE TABLE "ItemCarrinho" (
    "Id"         SERIAL   NOT NULL,
    "CarrinhoId" INTEGER  NOT NULL,
    "ProdutoId"  INTEGER  NOT NULL,
    "Quantidade" INTEGER  NOT NULL,

    CONSTRAINT "PK_ItemCarrinho" PRIMARY KEY ("Id"),

    CONSTRAINT "FK_ItemCarrinho_Carrinho" FOREIGN KEY ("CarrinhoId")
        REFERENCES "Carrinho" ("Id")
        ON DELETE CASCADE,

    CONSTRAINT "FK_ItemCarrinho_Produto" FOREIGN KEY ("ProdutoId")
        REFERENCES "Produto" ("Id")
        ON DELETE RESTRICT
);
```