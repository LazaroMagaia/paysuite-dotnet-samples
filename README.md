# PaySuite .NET SDK - Exemplo de Consola

Aplicação de consola simples que mostra como usar o [PaySuite .NET SDK](https://github.com/LazaroMagaia/paysuite-dotnet-sdk) para integrar com a API da PaySuite.

Tem um menu interativo para testar **Pagamentos**, **Contactos**, **Payouts**, **Refunds** e **Webhooks**, com código curto e fácil de ler.

> ⚠️ **Aviso:** o SDK é **não oficial** e não tem qualquer ligação à PaySuite. Para informações oficiais sobre a API, consulta <https://paysuite.tech/docs>.

---

## 📋 O que precisas

- Um **token da API** da PaySuite (no painel, em *Settings > API Access*)
- **Docker** e **Docker Compose** (forma mais fácil), ou o **.NET 10 SDK**

---

## 🐳 Correr com Docker (recomendado)

1. Clona o projeto:

```bash
git clone https://github.com/LazaroMagaia/paysuite-dotnet-sdk.git
cd paysuite-dotnet-sdk
```

2. Cria o ficheiro `.env` a partir do exemplo:

```bash
cp .env.example .env
```

3. Abre o `.env` e coloca o teu token:

```env
PAYSUITE_TOKEN=o-teu-token-aqui
```

4. Corre a aplicação:

```bash
docker compose run --rm paysuit
```

Usa `docker compose run` (e não `docker compose up`) porque a aplicação é um menu interativo e precisa do teclado. O `--rm` apaga o contentor quando saíres.

Se alterares o código, volta a construir a imagem:

```bash
docker compose build
```

---

## 💻 Correr sem Docker

Precisas do [.NET 10 SDK](https://dotnet.microsoft.com/download).

**Linux / macOS**

```bash
export PAYSUITE_TOKEN="o-teu-token-aqui"
dotnet run
```

**Windows (PowerShell)**

```powershell
$env:PAYSUITE_TOKEN="o-teu-token-aqui"
dotnet run
```

---

## 🧭 Menu

Ao arrancar, a aplicação mostra este menu:

| Opção | Ação |
| ----- | ---- |
| 1 | Criar pagamento |
| 2 | Listar pagamentos |
| 3 | Ver um pagamento |
| 4 | Criar contacto |
| 5 | Listar contactos |
| 6 | Ver um contacto |
| 7 | Atualizar contacto |
| 8 | Eliminar contacto |
| 9 | Criar payout |
| 10 | Listar payouts |
| 11 | Ver um payout |
| 12 | Criar refund |
| 13 | Listar refunds |
| 14 | Ver um refund |
| 15 | Testar webhook |
| 0 | Sair |

---

## 📁 Estrutura

```
Paysuit/
├── Program.cs        # Menu (switch case)
├── Settings.cs       # Lê o token da variável de ambiente
├── Payments.cs       # Criar, listar e consultar pagamentos
├── Contacts.cs       # Criar, listar, consultar, atualizar e eliminar contactos
├── Payouts.cs        # Criar, listar e consultar payouts
├── Refunds.cs        # Criar, listar e consultar refunds
├── Webhooks.cs       # Validar assinatura e tratar eventos
├── Paysuit.csproj
├── Dockerfile
├── compose.yaml
├── .env.example
└── .dockerignore
```

Cada recurso (`Payments`, `Contacts`, ...) é uma classe pequena com um método por operação. O `Program.cs` só decide qual método chamar.

---

## ✏️ Antes de testar

Os dados de exemplo estão **escritos diretamente no código**, para ser fácil de ler. Antes de testar, ajusta o que for preciso:

- **IDs:** `paymentId`, `contactId`, `payoutId` e `refundId` nos métodos de consultar, atualizar e eliminar. Cria primeiro um recurso (por exemplo, um contacto), copia o ID que aparece no ecrã e cola-o no método seguinte.
- **Valores:** montante, referência, número de telefone, nome do titular, etc.
- **Refund:** o `PaymentId` tem de ser de um pagamento **completed**, e o valor não pode ser maior do que ainda pode ser reembolsado.
- **Payout:** a `Reference` tem de ser alfanumérica (sem hífens nem espaços, máximo 30 caracteres). Para pagar a uma conta bancária, usa `Method = "bank"` e `Nib` (21 dígitos) em vez de `Phone`.

Depois de alterares o código, volta a correr `docker compose build` (ou `dotnet run`, se não usas Docker).

---

## 🌐 Webhooks

O SDK não recebe webhooks, porque um webhook é a PaySuite a chamar o **teu servidor**. A opção **15** do menu simula a chegada de um webhook, para veres como:

1. validar a assinatura (header `X-Signature`, HMAC-SHA256 do corpo bruto);
2. ler o evento (`payment.success`, `payout.failed`, `refund.success`, ...);
3. reagir a cada evento com um `switch`.

Num projeto real, tens de ter um endpoint HTTP (por exemplo, uma Minimal API em ASP.NET Core) que leia o corpo bruto e o header `X-Signature` e chame `ProcessWebhook`. Valida a assinatura **antes** de fazer parse do JSON.

O `Secret` está fixo em `Webhooks.cs` só para o teste. Em produção, guarda-o numa variável de ambiente.

---

## 🔒 Segurança

- **Nunca** faças commit do ficheiro `.env` (já está no `.gitignore` e no `.dockerignore`).
- **Nunca** coloques o token diretamente no código.
- Se o token for exposto por engano, gera um novo no painel da PaySuite.

---

## 🛠️ Resolução de problemas

| Problema | Solução |
| -------- | ------- |
| `Define PAYSUITE_TOKEN no ficheiro .env` | Cria o `.env` a partir do `.env.example` e coloca o token. |
| `PAYSUITE_TOKEN is not set.` | Sem Docker: define a variável de ambiente antes do `dotnet run`. |
| O menu não aceita teclado no Docker | Usa `docker compose run --rm paysuit`, não `docker compose up`. |
| Erro 401 | O token é inválido ou expirou. |
| Erro 422 | Dados inválidos (por exemplo, saldo insuficiente num payout, ou pagamento ainda não completo num refund). |
| Erro 429 | Limite de 100 pedidos por minuto excedido. Espera um pouco. |

---

## 📚 Links úteis

- SDK: <https://github.com/LazaroMagaia/paysuite-dotnet-sdk>
- Documentação oficial da API: <https://paysuite.tech/docs>

---

## 📄 Licença

Consulta o ficheiro `LICENSE` do repositório.# paysuite-dotnet-samples
