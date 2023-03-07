using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedOrderApi.Data;
using RedOrderApi.DTOS;
using Serilog;
using Order = RedOrderApi.DTOS.Order;
using OrderType = RedOrderApi.DTOS.OrderType;
using DatabaseOrderType = RedOrderApi.Data.OrderType;
using DatabaseOrder = RedOrderApi.Data.Order;
namespace RedOrderApi.Services;

public interface IOrderService
{
    Task<ActionResult<Order>>  CreateOrder(Order order);

    Task<ActionResult<Order>> GetOrder(int id);
    
    Task<Order> UpdateOrder(int id, Order order);
    
    Task<ActionResult<IEnumerable<Order>>> SearchByOrderType(OrderType orderType);

    string DeleteOrder(int id);
}

public class OrderService : IOrderService
{
    private readonly OrderContext _orderContext;

    public OrderService(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }

    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        var newOrder = new DatabaseOrder
        {
            OrderType = (DatabaseOrderType)order.OrderType,
            CustomerName = order.CustomerName,
            CreatedDate = order.CreatedDate,
            CreatedByUsername = order.CreatedByUsername
        };
        _orderContext.Orders?.Add(newOrder);
        await _orderContext.SaveChangesAsync();
        
        Log.Information("Added new order to database.");
        
        var orderResponse = new Order()
        {
            OrderType = order.OrderType,
            CustomerName = order.CustomerName,
            CreatedDate = order.CreatedDate,
            CreatedByUsername = order.CreatedByUsername
        };
        return orderResponse;
    }

    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var orders =  _orderContext.Orders?.FindAsync(id).Result;
        
        Log.Information("Getting order ${id} from database.");
        
        var returnedOrder = new Order();

        if (orders != null)
        {
            var orderResponse = new Order()
            {
                OrderType = (OrderType)orders.OrderType,
                CustomerName = orders.CustomerName,
                CreatedDate = orders.CreatedDate,
                CreatedByUsername = orders.CreatedByUsername
            };
            returnedOrder = orderResponse;

        }

        Log.Information("Returning order ${id}");
        
        return returnedOrder;
    }

    public async Task<Order> UpdateOrder(int id, Order order)
    {
        var existingOrder = _orderContext.Orders?.FindAsync(id).Result;

        Log.Information("Getting order ${id} from database.");

        if (existingOrder!=null)
        {
            existingOrder.OrderType = (DatabaseOrderType)order.OrderType;
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.CreatedDate = order.CreatedDate;
            existingOrder.CreatedByUsername = order.CreatedByUsername;

            _orderContext.Orders?.Update(existingOrder);
            await _orderContext.SaveChangesAsync(); 
        }

        var orderResponse = new Order()
        {
            OrderType = (OrderType)existingOrder?.OrderType,
            CustomerName = existingOrder.CustomerName,
            CreatedDate = existingOrder.CreatedDate,
            CreatedByUsername = existingOrder.CreatedByUsername
        };
        
        Log.Information("Updating order ${id}");

        return orderResponse;
    }

    public async Task<ActionResult<IEnumerable<Order>>> SearchByOrderType(OrderType orderType)
    {
        var orders =  _orderContext.Orders?.ToList();
        
        Log.Information("Getting orders with ordertype ${orderType} from database.");

        var orderList = new List<Order>();

        if (orders!=null)
        {
            foreach (var order in orders)
            {
                DatabaseOrderType type;

                if (Enum.TryParse<DatabaseOrderType>(orderType.ToString(), out type))
                {
                    if (order.OrderType == type)
                    {
                        var mappedOrders = new Order
                        {
                            OrderType = (OrderType)order.OrderType,
                            CustomerName = order.CustomerName,
                            CreatedDate = order.CreatedDate,
                            CreatedByUsername = order.CreatedByUsername
                        };
                        orderList.Add(mappedOrders);                
                    }
                }
            }

        }
        Log.Information("Returning orders with ordertype ${orderType}.");

        return orderList;
    }

     public string DeleteOrder(int id)
    {
        var order =  _orderContext.Orders?.FindAsync(id).Result;
        
        Log.Information("Finding order ${id} in database.");

        if (order!=null)
        {
            _orderContext.Orders?.Remove(order); 
            _orderContext.SaveChangesAsync();
        }
        
        Log.Information("Deleted order ${id} from the database.");

        return $"Successfully deleted order number {id}";
    }
}
