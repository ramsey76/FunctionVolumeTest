using Azure.Core;
using FunctionVolumeTest.MessageProducer.ResourceAccess.ServiceBus;
using FunctionVolumeTest.Models;
using Bogus;
using System.Security.Authentication.ExtendedProtection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace FunctionVolumeTest.MessageProducer.Service;

public class Producer
{
    public readonly ServiceBus _serviceBus;

    public Producer(ServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Produce()
    {
        var numberOfMessages = 1000000;
        var requestMessageFaker = CreateFaker();
        
        var requests = requestMessageFaker.Generate(numberOfMessages);
        foreach(var chunks in requests.Chunk(10000))
        {
            var listOfMessages = new List<string>();
            chunks.ToList().ForEach(m => listOfMessages.Add(JsonSerializer.Serialize(m)));

            await _serviceBus.SendAsync(listOfMessages, "application/json");
        }
    }
    
    private static Faker<RequestMessage> CreateFaker()
    {
        var faker = new Faker<RequestMessage>()
        .RuleFor(r => r.Id, f => Guid.NewGuid())
        .RuleFor(r => r.Name, faker => faker.Name.FullName());

        return faker;
    }
}
