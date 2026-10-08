namespace Paysuit;
using PaySuite.Sdk;
using PaySuite.Sdk.Models;

class Refunds
{
    private readonly PaySuiteClient _client;

    public Refunds()
    {
        var token = Settings.PaySuiteToken;
        _client = new PaySuiteClient(token);
    }

    async public Task CreateRefund()
    {
        var request = new CreateRefundRequest
        {
            PaymentId = "01H8X9V8X9Y8Z9A8B8C8D8E8F8", // tem de ser um pagamento completed
            Amount = 50m,
            Reason = "Customer requested refund",
            WebhookUrl = "https://example.com/webhooks/refund" // opcional
        };

        var result = await _client.Refunds.CreateAsync(request);
        if (result.Status != "success")
        {
            Console.WriteLine("Error occurred while creating refund.");
        }
        else
        {
            Console.WriteLine("Refund created successfully.");
            Console.WriteLine($"Refund ID: {result.Data?.Id}");
            Console.WriteLine($"Amount: {result.Data?.Amount}");
            Console.WriteLine($"Status: {result.Data?.Status}");
        }
    }

    async public Task GetAllRefunds()
    {
        var refunds = await _client.Refunds.ListAsync(page: 1, limit: 20);

        if (refunds.Status != "success")
        {
            Console.WriteLine("Error occurred while fetching refunds.");
        }
        else
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine("Todos Refunds");
            Console.WriteLine("==============================================================================");
            foreach (var refund in refunds.Data)
            {
                Console.WriteLine($"Refund ID: {refund.Id}\nAmount: {refund.Amount}\nStatus: {refund.Status}");
                Console.WriteLine("==============================================================================");
            }
        }
    }

    async public Task GetRefund()
    {
        var refundId = "Id do refund";
        var refund = await _client.Refunds.GetAsync(refundId);
        Console.WriteLine("REFUND SINGULAR");
        Console.WriteLine("==============================================================================");
        Console.WriteLine($"Refund ID: {refund.Data?.Id}\nAmount: {refund.Data?.Amount}\nStatus: {refund.Data?.Status}");
        Console.WriteLine("==============================================================================");
    }
}