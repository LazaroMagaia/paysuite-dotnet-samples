namespace Paysuit;
using PaySuite.Sdk;
using PaySuite.Sdk.Models;
class Payments
{
    private readonly PaySuiteClient _client;

    public Payments()
    {
        var token = Settings.PaySuiteToken;
        _client = new PaySuiteClient(token);
    }

    async public Task ProcessPayment()
    {
        var request = new CreatePaymentRequest
        {
            Amount = 100.50m,
            Method = "mpesa",
            Reference = "INV123456",
            Description = "Pagamento da fatura",
            ReturnUrl = "https://seudominio.com/success",
        };

        var result = await _client.Payments.CreateAsync(request);
        if (result.Status != "success")
        {
            Console.WriteLine("Error occurred while processing payment.");
        }
        else
        {
            Console.WriteLine("Payment processed successfully.");
            Console.WriteLine($"Payment ID: {result.Data?.Id}");
            Console.WriteLine($"Amount: {result.Data?.Amount}");
            Console.WriteLine($"Status: {result?.Status}");
            Console.WriteLine($"Payment URL: https://paysuite.tech/checkout/{result?.Data?.Id}");
        }
    }
    async public Task GetAllPayment()
    {
        var payments =await _client.Payments.ListAsync();

        if(payments.Status != "success")
        {
            Console.WriteLine("Error occurred while fetching payments.");
        }else
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine($"Todos Pagamentos");
            Console.WriteLine("==============================================================================");
            foreach(var payment in payments.Data)
            {
                Console.WriteLine($"Payment ID: {payment.Id}\nAmount: {payment.Amount}\nStatus: {payment.Status}");
                Console.WriteLine("==============================================================================");
            }
        }
    }
    async public Task GetPayment()
    {
        var paymentId = "ID do pagamento";
        var payment = await _client.Payments.GetAsync(paymentId);
        Console.WriteLine("PAGAMENTO SINGULAR");
        Console.WriteLine("==============================================================================");
        Console.WriteLine($"Payment ID: {payment.Data?.Id}\nAmount: {payment.Data?.Amount}\nStatus: {payment?.Status}");
        Console.WriteLine("==============================================================================");
    }
}