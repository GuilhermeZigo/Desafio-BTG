# PedidoProcessor

Sistema simples de processamento assíncrono de pedidos, usando .NET + RabbitMQ via Docker.

---

## O que esse projeto faz

- Exponibiliza uma API com dois endpoints:
  - `POST /pedidos` → cria um pedido
  - `GET /pedidos/{id}` → consulta o status
- Os pedidos são colocados em uma fila RabbitMQ
- Um consumidor pega os pedidos da fila, espera 2 segundos e marca como "processado"
- O status dos pedidos é mantido em memória (sem banco de dados)

---

## Tecnologias usadas

- .NET 8
- RabbitMQ
- MassTransit
- Swagger (para testar a API)
- Docker Compose (para subir o RabbitMQ facilmente)

---

## 💻 Como rodar localmente

### Pré-requisitos:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Passos:

1. Clone o repositório:
   ```bash
   git clone <URL_DO_SEU_REPO>
   cd PedidoProcessor
Suba o RabbitMQ com Docker:

bash
Copiar
Editar
docker compose up -d
Rode a aplicação:

bash
Copiar
Editar
dotnet run
Acesse a documentação da API no navegador:

bash
Copiar
Editar
http://localhost:5126/swagger
📦 Testando a API
Criar um pedido
Endpoint: POST /pedidos

Corpo JSON de exemplo:

json
Copiar
Editar
{
  "clienteId": "123",
  "itens": [
    { "nome": "Teclado", "quantidade": 1 },
    { "nome": "Mouse", "quantidade": 2 }
  ]
}
Consultar status
Endpoint: GET /pedidos/{id}

O status será "pendente" no início e mudará para "processado" após ~2 segundos

Observações:
Os pedidos são armazenados em memória. Ao reiniciar a aplicação, tudo é perdido (intencional).

Não há banco de dados, autenticação nem interface gráfica — foco na lógica assíncrona.

A fila precisa estar rodando. Se quiser resetar o RabbitMQ:

bash
Copiar
Editar
docker compose restart