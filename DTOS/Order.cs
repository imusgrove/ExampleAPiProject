namespace RedOrderApi.DTOS;

public class Order
{ 
    public OrderType OrderType { get; set; }
    public string CustomerName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedByUsername { get; set; }
}