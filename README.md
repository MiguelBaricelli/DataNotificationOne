# 📊 DataNotificationOne API (Em desenvolvimento)

API desenvolvida em **.NET**, seguindo os princípios da **Clean Architecture**, com o objetivo de **enviar notificações e disponibilizar dados do mercado financeiro**.

A aplicação integra-se com **APIs externas** para consumo, processamento e análise de dados financeiros, oferecendo endpoints claros e bem definidos para consulta semanal e análise estatística de ativos.

---

## 🧱 Arquitetura

A API segue os princípios da **Clean Architecture**, garantindo:

- Separação de responsabilidades  
- Facilidade de manutenção  
- Testabilidade  
- Baixo acoplamento  
- Escalabilidade  

---

## 📚 Documentação da API

### 🔹 Controller: `DataDailyController`

Responsável por expor endpoints relacionados a:
- Consulta de dados financeiros diários
- Análise estatística de ativos
- Cálculo de variância e tendência de mercado

---

## 🔸 GET `api/FinanceData/GetVariationAsset/{ativo}/{date}`

### 📌 Descrição

Calcula e retorna a **variância dos preços** de um ativo financeiro em determinada data com base nos valores:

- Open
- High
- Low
- Close

Além disso, informa se o ativo está **em alta** com base nos dados analisados.

---

### 📥 Parâmetros

| Nome  | Tipo   | Obrigatório | Descrição                           |
|------|--------|-------------|------------------------------------|
| ativo | string | ✅ Sim       | Símbolo do ativo (ex.: MSFT, AAPL) |

---

### 📤 Respostas

| Código | Descrição |
|------|----------|
| 200 OK | Retorna um objeto `FinanceSummaryDto` |
| 400 Bad Request | Ativo não informado |
| 404 Not Found | Dados não encontrados |

---

```json
{
  "open": 308.98,
  "high": 311.36,
  "low": 295.70,
  "close": 300.98,
  "volume": 26507574,
  "IsAlta": True or False
}
```

### 🔹 Controller: `DataWeeklyController`

Responsável por expor endpoints relacionados a:
- Consulta de dados financeiros semanais
- Recuperação dos últimos registros de mercado
- Obtenção de dados de uma semana específica para determinado ativo
- **OBS: Consultas sempre feitas com datas de sexta-feiras**

## 🔸 GET `/api/FinanceData/Last10Weeks/{ativo}`

### 📌 Descrição

Retorna os **dados semanais** mais recentes de um ativo financeiro, limitando-se às **últimas 10 semanas** disponíveis.

---

### 📥 Parâmetros

| Nome  | Tipo   | Obrigatório | Descrição         |
|------|--------|-------------|------------------|
| ativo | string | ✅ Sim       | Símbolo do ativo |

---

### 📤 Respostas

| Código | Descrição |
|------|----------|
| 200 OK | Retorna um objeto `FinanceDataModel` |
| 400 Bad Request | Ativo não informado |
| 404 Not Found | Nenhum dado encontrado |

---

### 🧾 Exemplo de Resposta

```json
[
  {
    "date": "2025-12-05",
    "open": 308.98,
    "high": 311.36,
    "low": 295.70,
    "close": 300.98,
    "volume": 26507574
  },
  {
    "date": "2025-11-28",
    "open": 310.10,
    "high": 315.00,
    "low": 305.00,
    "close": 312.50,
    "volume": 19876543
  }
]


```

### 📌 GET `/api/FinanceData/DataSpecificWeekly/{ativo}/{date}`

### 📖 Descrição
Retorna os **dados financeiros de uma semana específica** para o ativo informado.

---

### 📅 Regras importantes
- A data deve ser um **dia útil**
- Datas em **sábado ou domingo não são aceitas**

---

### 📥 Parâmetros de Rota

| Nome  | Tipo     | Obrigatório | Descrição |
|------|----------|-------------|-----------|
| ativo | string | ✅ Sim | Símbolo do ativo (ex.: MSFT, AAPL) |
| date  | DateTime | ✅ Sim | Data da semana desejada (`yyyy-MM-dd`) |

---

### 📤 Respostas

| Código | Descrição |
|------|-----------|
| **200 OK** | Retorna um objeto `FinanceDataModel` |
| **400 Bad Request** | Ativo inválido ou data em final de semana |
| **404 Not Found** | Nenhum dado retornado pelo serviço |

---

### 🧾 Exemplo de Resposta — **200 OK**

```json
"2025-12-05" : {
  "open": 308.98,
  "high": 311.36,
  "low": 295.70,
  "close": 300.98,
  "volume": 26507574
}

