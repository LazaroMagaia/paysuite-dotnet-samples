namespace Paysuit;
using PaySuite.Sdk;
using PaySuite.Sdk.Models;

class Payouts
{
    private readonly PaySuiteClient _client;

    public Payouts()
    {
        var token = Settings.PaySuiteToken;
        _client = new PaySuiteClient(token);
    }

    async public Task CreatePayout()
    {
        var request = new CreatePayoutRequest
        {
            Amount = 100m,
            Currency = "MZN",
            Method = "mpesa",
            Reference = "PO123456",
            Description = "Pagamento ao beneficiario",
            Beneficiary = new Beneficiary
            {
                Phone = "841234567",
                Holder = "John Doe"
            },
            WebhookUrl = "https://example.com/webhooks/payout" // opcional
        };

        var result = await _client.Payouts.CreateAsync(request);
        if (result.Status != "success")
        {
            Console.WriteLine("Error occurred while creating payout.");
        }
        else
        {
            Console.WriteLine("Payout created successfully.");
            Console.WriteLine($"Payout ID: {result.Data?.Id}");
            Console.WriteLine($"Amount: {result.Data?.Amount}");
            Console.WriteLine($"Status: {result.Data?.Status}");
        }
    }

    async public Task GetAllPayouts()
    {
        var payouts = await _client.Payouts.ListAsync(page: 1, limit: 15);

        if (payouts.Status != "success")
        {
            Console.WriteLine("Error occurred while fetching payouts.");
        }
        else
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine("Todos Payouts");
            Console.WriteLine("==============================================================================");
            foreach (var payout in payouts.Data)
            {
                Console.WriteLine($"Payout ID: {payout.Id}\nAmount: {payout.Amount}\nStatus: {payout.Status}");
                Console.WriteLine("==============================================================================");
            }
        }
    }

    async public Task GetPayout()
    {
        var payoutId = "ID do payout";
        var payout = await _client.Payouts.GetAsync(payoutId);
        Console.WriteLine("PAYOUT SINGULAR");
        Console.WriteLine("==============================================================================");
        Console.WriteLine($"Payout ID: {payout.Data?.Id}\nAmount: {payout.Data?.Amount}\nStatus: {payout.Data?.Status}");
        Console.WriteLine("==============================================================================");
    }
}