using Paysuit;

var payments = new Payments();
var contacts = new Contacts();
var payouts = new Payouts();
var refunds = new Refunds();
var webhooks = new Webhooks();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("========== PAYSUITE ==========");
    Console.WriteLine("-- Pagamentos --");
    Console.WriteLine("1  - Criar pagamento");
    Console.WriteLine("2  - Listar pagamentos");
    Console.WriteLine("3  - Ver um pagamento");
    Console.WriteLine("-- Contactos --");
    Console.WriteLine("4  - Criar contacto");
    Console.WriteLine("5  - Listar contactos");
    Console.WriteLine("6  - Ver um contacto");
    Console.WriteLine("7  - Atualizar contacto");
    Console.WriteLine("8  - Eliminar contacto");
    Console.WriteLine("-- Payouts --");
    Console.WriteLine("9  - Criar payout");
    Console.WriteLine("10 - Listar payouts");
    Console.WriteLine("11 - Ver um payout");
    Console.WriteLine("-- Refunds --");
    Console.WriteLine("12 - Criar refund");
    Console.WriteLine("13 - Listar refunds");
    Console.WriteLine("14 - Ver um refund");
    Console.WriteLine("-- Webhooks --");
    Console.WriteLine("15 - Testar webhook");
    Console.WriteLine("0  - Sair");
    Console.Write("Escolha: ");

    var option = Console.ReadLine();

    try
    {
        switch (option)
        {
            case "1":
                await payments.ProcessPayment();
                break;
            case "2":
                await payments.GetAllPayment();
                break;
            case "3":
                await payments.GetPayment();
                break;
            case "4":
                await contacts.CreateContact();
                break;
            case "5":
                await contacts.GetAllContacts();
                break;
            case "6":
                await contacts.GetContact();
                break;
            case "7":
                await contacts.UpdateContact();
                break;
            case "8":
                await contacts.DeleteContact();
                break;
            case "9":
                await payouts.CreatePayout();
                break;
            case "10":
                await payouts.GetAllPayouts();
                break;
            case "11":
                await payouts.GetPayout();
                break;
            case "12":
                await refunds.CreateRefund();
                break;
            case "13":
                await refunds.GetAllRefunds();
                break;
            case "14":
                await refunds.GetRefund();
                break;
            case "15":
                webhooks.Test();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}