# MarketCheckout API

API REST desenvolvida em ASP.NET Core (.NET 10) para gerenciamento de carrinho de compras. A solução segue arquitetura em camadas (Clean Architecture), com separação entre Domain, Application, Persistence e API.

---

## Tecnologias Utilizadas

- **.NET 10** — plataforma de execução
- **ASP.NET Core** — framework web
- **Entity Framework Core** com **Npgsql** — ORM e acesso ao banco de dados PostgreSQL
- **Swashbuckle (Swagger)** — documentação interativa da API
- **xUnit + Moq** — testes unitários
- **DummyJSON** (`https://dummyjson.com/products/`) — fonte externa de produtos

---

## Estrutura da Solução

```
MarketCheckoutApi/
├── Domain/                         # Entidades e interfaces de repositório
│   ├── Entities/
│   │   ├── Cart.cs
│   │   ├── ItemCart.cs
│   │   └── Product.cs
│   └── Interfaces/Repository/
│       ├── IBaseRepository.cs
│       └── ICartRepository.cs
│
├── MarketCheckout.Application/     # Serviços, DTOs de request/response
│   ├── Request/
│   ├── Response/
│   └── Services/
│
├── MarketCheckout.Persistence/     # DbContext, repositórios, configurações EF
│   └── Data/
│
├── MarketCheckout.Test/            # Testes unitários (xUnit + Moq)
│   └── CartServiceTests.cs
│
└── MarketCheckoutApi/              # Projeto principal (API)
    ├── Controllers/
    │   ├── CartController.cs
    │   └── ProductController.cs
    ├── Program.cs
    └── appsettings.json
```

---

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL](https://www.postgresql.org/download/) (versão 13 ou superior)
- Ferramenta de linha de comando `dotnet` disponível no PATH

---

## Configuração do Banco de Dados

1. Certifique-se de que o PostgreSQL está rodando localmente na porta `5432`.

2. Crie o banco de dados:
   "MarketCheckout";

3. Edite o arquivo `MarketCheckoutApi/appsettings.json` caso suas credenciais sejam diferentes:
   ```json
   "ConnectionStrings": {
     "Default": "Host=localhost;Port=5432;Database=MarketCheckout;Username=postgres;Password=1234"
   }
   ```

## Como Executar

Acesse o Swagger para explorar e testar os endpoints:
   ```
   https://localhost:7156/swagger
   ```
   ou via HTTP:
   ```
   http://localhost:5245/swagger
   ```

---

## Endpoints Disponíveis

### Produto

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/Product/{id}` | Busca produto por ID (local ou via DummyJSON) |

O endpoint de produto verifica primeiro no banco de dados local. Se não encontrado, consulta a API externa `https://dummyjson.com/products/{id}` e persiste o resultado.

---

### Carrinho

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/Cart` | Cria um novo carrinho com itens |
| POST | `/api/Cart/{id}/items` | Adiciona itens a um carrinho existente |
| GET | `/api/Cart/{id}` | Retorna o carrinho com valor total calculado |

#### Exemplo — Criar carrinho (`POST /api/Cart`):
```json
{
  "buyerName": "João Silva",
  "buyerCpf": "123.456.789-01",
  "createdBy": "sistema",
  "items": [
    { "productId": 1, "quantity": 2 },
    { "productId": 3, "quantity": 1 }
  ]
}
```

> **Atenção:** o campo `buyerCpf` deve conter exatamente **14 dígitos, considerando pontuações do CPF **.

#### Exemplo — Resposta de `GET /api/Cart/{id}`:
```json
{
  "id": 1,
  "buyerCpf": "123.456.789-12",
  "createdBy": "sistema",
  "createdAt": "2026-06-25T01:00:00Z",
  "totalValue": 299.97,
  "items": [
    {
      "id": 1,
      "quantity": 2,
      "product": { "id": 1, "title": "Essence Mascara Lash Princess", "price": 99.99 }
    }
  ]
}
```

---

## Executar os Testes

```bash
cd MarketCheckoutApi
dotnet test
```

Os testes cobrem os cenários:
- `ProcessCart` deve lançar `ArgumentException` quando um item possui `ProductId` inválido (zero)
- `AddItemCartAsync` deve lançar `OperationCanceledException` quando o carrinho não é encontrado

---

## Observações

- Os produtos são buscados automaticamente na API externa [DummyJSON](https://dummyjson.com/products/) caso não existam no banco local, e então persistidos.
- O cálculo do valor total do carrinho é feito dinamicamente com base no preço de cada produto e na quantidade de itens..
