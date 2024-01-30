using Azure;
using Azure.Data.Tables;
using Microsoft.VisualBasic;

namespace FunctionVolumeTest.Function;

public class VolumeMessage : ITableEntity
{
    public string RowKey {get;set;}
    public string PartitionKey {get;set;}
    public string Name {get;set;}
    public ETag ETag {get;set;}
    public DateTimeOffset? Timestamp { get; set; }
}
