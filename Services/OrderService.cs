using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedOrderApi.Data;
using RedOrderApi.DTOS;
using Order = RedOrderApi.DTOS.Order;
using OrderType = RedOrderApi.DTOS.OrderType;
using DatabaseOrderType = RedOrderApi.Data.OrderType;
using DatabaseOrder = RedOrderApi.Data.Order;
namespace RedOrderApi.Services;

public interface IOrderService
{
    Task<ActionResult<Order>>  CreateOrder(Order order);

    Task<Order> GetOrder(int id);
    
    Task<Order> UpdateOrder(int id, Order order);
    
    Task <Order>SearchOrder(OrderType orderType);

    Task DeleteOrder(int id);
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
        
        var orderResponse = new Order()
        {
            OrderType = order.OrderType,
            CustomerName = order.CustomerName,
            CreatedDate = order.CreatedDate,
            CreatedByUsername = order.CreatedByUsername
        };
        return orderResponse;
    }

    public async Task<Order> GetOrder(int id)
    {
        var orders = _orderContext.Orders?.FindAsync(id);
        var dto = new Order()
        {

        };
        return  dto;
    }

    public async Task<Order> UpdateOrder(int id, Order order)
    {
        var existingOrder = await _orderContext.Orders.FindAsync(id);
        //existingOrder.OrderType = order.OrderType;
        await _orderContext.SaveChangesAsync();
        return new Order();
    }

    public async Task<Order> SearchOrder(OrderType orderType)
    {
        _orderContext.Orders.FindAsync(orderType);
         _orderContext.SaveChangesAsync();
        return new Order();      
    }

    public async Task DeleteOrder(int id)
    {
        var order = await _orderContext.Orders.FindAsync(id);
        _orderContext.Orders.Remove(order);
        await _orderContext.SaveChangesAsync();    
    }
}
