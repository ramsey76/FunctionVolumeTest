using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Text.Json;
using FunctionVolumeTest.Models;
using Azure.Core;
using Azure;
using Microsoft.Extensions.Configuration;

namespace FunctionVolumeTest.Function.Services;


public class MessageHandler
{
    public readonly ILogger<MessageHandler> _logger;
    public readonly TableClient _client;
    public MessageHandler(ILogger<MessageHandler> logger)
    {
        _logger = logger;
        _client = new TableClient("BlobEndpoint=https://stcloudnative.blob.core.windows.net/;QueueEndpoint=https://stcloudnative.queue.core.windows.net/;FileEndpoint=https://stcloudnative.file.core.windows.net/;TableEndpoint=https://stcloudnative.table.core.windows.net/;SharedAccessSignature=sv=2022-11-02&ss=t&srt=sco&sp=rwdlacu&se=2024-03-01T02:22:01Z&st=2024-01-28T18:22:01Z&spr=https&sig=zzdgXwFTLY9ppn9we2eCsa5slpzPlZxrui742mvZ%2FAI%3D","VolumeData");
        _client.CreateIfNotExists();
    }

    public async Task Hanlde(string message)
    {
        var requestMessage = JsonSerializer.Deserialize<RequestMessage>(message);

        var volumeMessage = new VolumeMessage {
            Name = requestMessage.Name,
            RowKey = requestMessage.Id.ToString(),
            PartitionKey = "vol1"
        };

        await _client.UpsertEntityAsync<VolumeMessage>(volumeMessage);
    }


}
