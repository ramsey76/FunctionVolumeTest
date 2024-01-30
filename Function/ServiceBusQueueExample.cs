using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FunctionVolumeTest.Function.Services;

namespace FunctionVolumeTest.Function
{
    public class ServiceBusQueueExample
    {
        private readonly ILogger<ServiceBusQueueExample> _logger;
        private readonly MessageHandler _messageHandler;

        public ServiceBusQueueExample(ILogger<ServiceBusQueueExample> logger, MessageHandler messageHandler)
        {
            _logger = logger;
            _messageHandler = messageHandler;
        }

        [Function(nameof(ServiceBusQueueExample))]
        public async Task Run([ServiceBusTrigger("%QueueName%", Connection = "Connection")] ServiceBusReceivedMessage message)
        {
            if(message.ContentType != "application/json") throw new FormatException("message is not a valid message");

            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
            await _messageHandler.Hanlde(message.Body.ToString());
        }
    }
}
