# Mottu Bracelet – Sprint 4

## 📌 Descrição do Projeto

O **Mottu Bracelet** é um projeto desenvolvido para a empresa **Mottu**, com o objetivo de otimizar o gerenciamento das motos presentes nos pátios de manutenção. Cada moto recebe um bracelete inteligente capaz de:

- Ajudar na localização rápida dentro do pátio.
- Emitir sinal sonoro e infravermelho para facilitar identificação.
- Integrar informações entre moto, dispositivo, pátio e histórico de movimentações.

Nesta Sprint 4, o projeto evoluiu e passou a incluir:

- Versionamento de API (v1 e v2)  
- Health Check (`/health`)  
- Middleware de autenticação por API Key  
- Endpoint de predição com ML.NET  
- Testes unitários e de integração com xUnit  
- Swagger com suporte a autenticação  
- HATEOAS nos endpoints principais  

---

## 👨‍💻 Integrantes

- Pedro Abrantes Andrade | RM558186  
- Ricardo Tavares de Oliveira Filho | RM556092  
- Victor Alves Carmona | RM555726  

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8.0 Web API  
- C#  
- Entity Framework Core  
- Oracle EF Core  
- ML.NET  
- Swagger / OpenAPI  
- xUnit  
- WebApplicationFactory  
- Visual Studio 2022  

---

## ✅ Funcionalidades da Sprint 4

### ✅ 1. Health Check
Endpoint simples que retorna **200 OK** caso a API esteja funcionando.

```GET /health```

### ✅ 2. Versionamento de API (v1 e v2)
Agora a API possui:

- **v1** → controladores principais (Moto, Patio, Dispositivo, HistoricoPatio)  
- **v2** → controlador de predição (Machine Learning)  

Controllers usam:

```
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/moto")]
```

E para ML:

```
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/ml")]
```


### ✅ 3. Middleware de API Key
Todos os endpoints (exceto `/health`) exigem:

```
X-API-KEY: mottu-bracelet-2025
```

### ✅ 4. Endpoint de Predição com ML.NET (v2)

O endpoint estima a ocupação futura de um pátio com base em:

- motos atuais
- entrada média
- saída média
- capacidade máxima

Endpoint:

```
POST /api/v2/ml/prever-ocupacao
```

### ✅ 5. Testes Automatizados

- Testes unitários de todos os Services
- Testes de integração
- Banco InMemory para ambiente de teste

## 📂 Instalação e Execução

### ✅ Pré-requisitos

- .NET 8.0
- Visual Studio 2022
- Banco Oracle ativo

### ✅ Passos para executar

1. Clone o repositório:

```
git clone https://github.com/pdroandrad/Sprint-4-Mottu-Bracelet-CSharp
```

2. Abra no Visual Studio.
3. Configure o banco no appsettings.json:

```
"ConnectionStrings": {
  "DefaultConnection": "Data Source=oracle.fiap.com.br:1521/orcl;User Id=SEU_USER;Password=SUA_SENHA;"
},
"ApiKey": "mottu-bracelet-2025"
```

4. Execute o projeto em HTTPS.
5. O Swagger abrirá automaticamente.

### Autenticação no Swagger
1. Clique em Authorize.
2. Digite:
```
mottu-bracelet-2025
```
3. Os cadeados ficarão fechados.
4. Teste qualquer endpoint normalmente.

## 📡 Endpoints da API

---

## ✅ Versão 1 (v1)

### 🔧 MotoController

| Método | Endpoint               | Descrição                 |
|--------|-------------------------|---------------------------|
| GET    | `/api/v1/moto`          | Lista motos               |
| GET    | `/api/v1/moto/{id}`     | Moto com HATEOAS          |
| POST   | `/api/v1/moto`          | Cria moto                 |
| PUT    | `/api/v1/moto/{id}`     | Atualiza moto             |
| DELETE | `/api/v1/moto/{id}`     | Remove moto               |

---

### 🔧 DispositivoController

| Método | Endpoint                      | Descrição                    |
|--------|--------------------------------|------------------------------|
| GET    | `/api/v1/dispositivo`          | Lista dispositivos           |
| GET    | `/api/v1/dispositivo/{id}`     | Dispositivo com HATEOAS      |
| POST   | `/api/v1/dispositivo`          | Cria dispositivo             |
| PUT    | `/api/v1/dispositivo/{id}`     | Atualiza dispositivo         |
| DELETE | `/api/v1/dispositivo/{id}`     | Remove dispositivo           |

---

### 🔧 PatioController

| Método | Endpoint               | Descrição                |
|--------|-------------------------|--------------------------|
| GET    | `/api/v1/patio`         | Lista pátios             |
| GET    | `/api/v1/patio/{id}`    | Pátio com HATEOAS        |
| POST   | `/api/v1/patio`         | Cria pátio               |
| PUT    | `/api/v1/patio/{id}`    | Atualiza pátio           |
| DELETE | `/api/v1/patio/{id}`    | Remove pátio             |

---

### 🔧 HistoricoPatioController

| Método | Endpoint                        | Descrição                   |
|--------|----------------------------------|-----------------------------|
| GET    | `/api/v1/historicopatio`         | Lista registros             |
| GET    | `/api/v1/historicopatio/{id}`    | Histórico com HATEOAS       |
| POST   | `/api/v1/historicopatio`         | Cria registro               |

---

## ✅ Versão 2 (v2)

### 🔧 MlController (Predição)

| Método | Endpoint                          | Descrição                   |
|--------|------------------------------------|-----------------------------|
| POST   | `/api/v2/ml/prever-ocupacao`        | Predição de ocupação        |

---

## 📦 Exemplos de Payloads

---

### ✅ Exemplo de Payload (v1)

🔹 Patio
```json
{
  "nome": "Pátio Central",
  "capacidadeMaxima": 50,
  "administradorResponsavel": "João",
  "endereco": {
    "logradouro": "Rua A",
    "numero": 100,
    "cidade": "São Paulo",
    "cep": "00000-000",
    "pais": "Brasil"
  }
}
```

🔹 Dispositivo
```json
{
  "statusDispositivo": "Ativo",
  "motoId": null,
  "patioId": 1
}
```

🔹 Moto
```json
{
  "imei": "123456789012345",
  "placa": "ABC-1234"
}
```

🔹 HistoricoPatio
```json
{
  "motoId": 1,
  "patioId": 2,
  "dataEntrada": "2025-09-18T10:00:00"
}
```

### ✅ Exemplo de Payload (v2)

🔹 Predição
```json
{
  "motosAtuais": 20,
  "entradaMedia": 4,
  "saidaMedia": 2,
  "capacidadeMaxima": 60
}
```

---

## ✅ Testes Automatizados

---

### ✅ Testes Unitários  
**Local:** `MottuBracelet.Tests/Services`

Os testes unitários cobrem os principais serviços da aplicação:

- `ServicoMotos`
- `ServicoPatios`
- `ServicoDispositivos`
- `ServicoHistoricoPatios`

Cada teste valida:
- Criação de registros  
- Consulta por ID  
- Remoção  
- Funcionamento isolado usando **EF Core InMemory**

---

### ✅ Testes de Integração  
**Local:** `MottuBracelet.Tests/Integrations`

Tecnologias utilizadas:
- `WebApplicationFactory`
- `HttpClient`
- Banco **InMemory** para isolar o ambiente real

Cobertura:
- Health Check  
- Endpoint `/api/v1/moto`  
- Middleware de API Key funcionando  

---

### ✅ Como rodar os testes

No terminal, dentro da solução:

```bash
dotnet test
