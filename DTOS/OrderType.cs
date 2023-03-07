using System.Text.Json.Serialization;

namespace RedOrderApi.DTOS;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderType
{
    Standard = 1,
    SaleOrder = 2,
    PurchaseOrder = 3,
    TransferOrder = 4,
    ReturnOrder = 5
}