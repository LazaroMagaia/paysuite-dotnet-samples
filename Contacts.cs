namespace Paysuit;
using PaySuite.Sdk;
using PaySuite.Sdk.Models;

class Contacts
{
    private readonly PaySuiteClient _client;

    public Contacts()
    {
        var token = Settings.PaySuiteToken;
        _client = new PaySuiteClient(token);
    }

    async public Task CreateContact()
    {
        var request = new CreateContactRequest
        {
            Name = "João Silva",
            Email = "joao@example.com",
            Phone = "+258841234567"
        };

        var result = await _client.Contacts.CreateAsync(request);
        if (result.Status != "success")
        {
            Console.WriteLine("Error occurred while creating contact.");
        }
        else
        {
            Console.WriteLine("Contact created successfully.");
            Console.WriteLine($"Contact ID: {result.Data?.Id}");
            Console.WriteLine($"Name: {result.Data?.Name}");
            Console.WriteLine($"Email: {result.Data?.Email}");
            Console.WriteLine($"Phone: {result.Data?.Phone}");
        }
    }

       async public Task GetAllContacts()
    {
        var contacts = await _client.Contacts.ListAsync(page: 1, limit: 20);

        if (contacts.Status != "success")
        {
            Console.WriteLine("Error occurred while fetching contacts.");
        }
        else
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine("Todos Contactos");
            Console.WriteLine("==============================================================================");
            foreach (var contact in contacts.Data)
            {
                Console.WriteLine($"Contact ID: {contact.Id}\nName: {contact.Name}\nEmail: {contact.Email}\nPhone: {contact.Phone}");
                Console.WriteLine("==============================================================================");
            }
        }
    }

    async public Task GetContact()
    {
        var contactId = "01H8X9V8X9Y8Z9A8B8C8D8E8F8";
        var contact = await _client.Contacts.GetAsync(contactId);
        Console.WriteLine("CONTACTO SINGULAR");
        Console.WriteLine("==============================================================================");
        Console.WriteLine($"Contact ID: {contact.Data?.Id}\nName: {contact.Data?.Name}\nEmail: {contact.Data?.Email}\nPhone: {contact.Data?.Phone}");
        Console.WriteLine("==============================================================================");
    }

    async public Task UpdateContact()
    {
        var contactId = "01H8X9V8X9Y8Z9A8B8C8D8E8F8";
        var request = new UpdateContactRequest
        {
            Name = "João Silva Atualizado",
            Email = "novo@example.com"
        };

        var result = await _client.Contacts.UpdateAsync(contactId, request);
        if (result.Status != "success")
        {
            Console.WriteLine("Error occurred while updating contact.");
        }
        else
        {
            Console.WriteLine("Contact updated successfully.");
            Console.WriteLine($"Name: {result.Data?.Name}");
            Console.WriteLine($"Email: {result.Data?.Email}");
        }
    }

       async public Task DeleteContact()
    {
        var contactId = "01H8X9V8X9Y8Z9A8B8C8D8E8F8";
        var result = await _client.Contacts.DeleteAsync(contactId);

        if (result.Status != "success")
        {
            Console.WriteLine("Error occurred while deleting contact.");
        }
        else
        {
            Console.WriteLine("Contact deleted successfully.");
            Console.WriteLine($"Message: {result.Message}");
        }
    }
}