
# 123Sales API

## Prerequisites
- [Docker](https://www.docker.com/products/docker-desktop)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup) or [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio)


## Como executar

Para rodar a aplicação localmente utilizando Docker, siga os passos abaixo:

1. Certifique-se de que o Docker e o Docker Compose estão instalados no seu sistema.

2. Na raiz do projeto, execute o seguinte comando para subir os containers:

```bash
docker-compose up --build
```

Esse comando irá:

- Construir a imagem da aplicação API (`sales-api`) e a imagem do SQL Server.
- Subir os containers conectados em uma rede Docker configurada no `docker-compose.yml`.

## Acessando a API

Após a execução, você poderá acessar o Swagger da API para testar os endpoints. Acesse o seguinte link:

[http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

## Exemplo de Requisição via cURL

Aqui está um exemplo de como criar uma venda usando o cURL:

```bash
curl -X POST "http://localhost:5000/api/sales" -H "Content-Type: application/json" -d '{
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "items": [
    {
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 1,
      "unitPrice": 1500
    }
  ],
  "saleDate": "2024-09-21T04:33:32.753Z"
}'
```

## Volumes e Redes

O projeto Docker utiliza volumes e redes para persistir dados e conectar os containers da aplicação e do banco de dados:

- O volume `salesdata` é usado para persistir os dados do banco de dados SQL Server.
- A rede `sales-network` é utilizada para conectar os serviços.

## Estrutura do `docker-compose.yml`

O arquivo `docker-compose.yml` contém as configurações para subir os serviços da API e do banco de dados.

### Estrutura simplificada:

```yaml
version: '3.8'

services:
  sales-api:
    build:
      context: .
      dockerfile: Dockerfile
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=SalesDb;User=sa;Password=YourSecurePassword123;TrustServerCertificate=True;
    ports:
      - "5000:5000"
    depends_on:
      - sqlserver
    networks:
      - sales-network

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=Peregrino11@@
      - MSSQL_PID=Developer
    ports:
      - "1433:1433"
    networks:
      - sales-network
    volumes:
      - salesdata:/var/opt/mssql

networks:
  sales-network:

volumes:
  salesdata:
```

Com isso, basta fazer o `up` do projeto e acessar os endpoints no Swagger via `http://localhost:5000/swagger/index.html`.


## Encerrando os containers

To stop the containers, run the following command:

```bash
docker-compose down
```

---

### Notas adicionais:
- Instructions for **Docker Compose** to run both SQL Server and the API.
- Detailed steps for applying **Entity Framework** migrations directly from the command line.
- Information on how to connect to SQL Server using **SSMS** or **Azure Data Studio**.
- Added troubleshooting section to help resolve common issues. 
