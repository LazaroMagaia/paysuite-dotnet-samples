namespace Paysuit;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

class Webhooks
{
    // O secret fica nas configuracoes da conta PaySuite (merchant settings)
    private const string Secret = "your_webhook_secret";

    // Valida se o webhook veio mesmo da PaySuite
    public bool VerifySignature(string payload, string signature)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Secret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        var expected = Convert.ToHexString(hash).ToLowerInvariant();

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(expected),
            Encoding.UTF8.GetBytes(signature));
    }

    // Isto e o que farias no teu endpoint: recebe o corpo e o header X-Signature
    public void ProcessWebhook(string payload, string signature)
    {
        if (!VerifySignature(payload, signature))
        {
            Console.WriteLine("Assinatura invalida. Webhook ignorado.");
            return;
        }

        using var doc = JsonDocument.Parse(payload);
        var root = doc.RootElement;

        var evento = root.GetProperty("event").GetString();
        var data = root.GetProperty("data");

        var id = data.GetProperty("id").GetString();
        var amount = data.GetProperty("amount").GetDecimal();
        var reference = data.GetProperty("reference").GetString();

        switch (evento)
        {
            case "payment.success":
                Console.WriteLine($"Pagamento {reference} pago com sucesso ({amount} MZN).");
                break;
            case "payment.failed":
                Console.WriteLine($"Pagamento {reference} falhou.");
                break;
            case "payout.success":
                Console.WriteLine($"Payout {reference} concluido ({amount} MZN).");
                break;
            case "payout.failed":
                Console.WriteLine($"Payout {reference} falhou.");
                break;
            case "refund.success":
                Console.WriteLine($"Refund {reference} concluido ({amount} MZN).");
                break;
            case "refund.failed":
                Console.WriteLine($"Refund {reference} falhou.");
                break;
            default:
                Console.WriteLine($"Evento desconhecido: {evento}");
                break;
        }

        // Dica da documentacao: usa "id + evento" para nao processar o mesmo webhook duas vezes
        Console.WriteLine($"ID do recurso: {id}");
    }

    // Simula um webhook a chegar
    public void Test()
    {
        var payload = """
        {
          "event": "payment.success",
          "data": {
            "id": "01H8X9V8X9Y8Z9A8B8C8D8E8F8",
            "amount": 100.50,
            "reference": "INV2024001"
          }
        }
        """;

        // A PaySuite calcularia isto por ti. Aqui calculamos para simular
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Secret));
        var signature = Convert.ToHexString(
            hmac.ComputeHash(Encoding.UTF8.GetBytes(payload))).ToLowerInvariant();

        Console.WriteLine("--- Assinatura correta ---");
        ProcessWebhook(payload, signature);

        Console.WriteLine("--- Assinatura errada ---");
        ProcessWebhook(payload, "assinatura-falsa");
    }
}