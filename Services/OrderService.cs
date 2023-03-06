using Microsoft.EntityFrameworkCore;
using RedOrderApi.Data;
using RedOrderApi.DTOS;
using Order = RedOrderApi.DTOS.Order;

namespace RedOrderApi.Services;

public interface IOrderService
{
    Task<Order> CreateOrder(Order order);

    Task<Order> GetOrder(int id);
    
    Task<Order> UpdateOrder(int id, Order order);
    
    Task <Order>SearchOrder(OrderType orderType);

    Task DeleteOrder(int id);
}

public class OrderService : IOrderService
{
    private readonly OrderContext _orderContext;

    public OrderService(DbContext orderContext)
    {
        _orderContext = orderContext;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        _orderContext.Orders.Add(order);
        await _orderContext.SaveChangesAsync();
        return Order;    
    }

    public async Task<Order> GetOrder(int id)
    {
        return await _orderContext.Orders.FindAsync(id);
    }

    public async Task<Order> UpdateOrder(int id, Order order)
    {
        var existingOrder = await _orderContext.Orders.FindAsync(id);
        existingOrder.OrderType = order.OrderType;
        await _orderContext.SaveChangesAsync();
        return Order;
    }

    public Task<Order> SearchOrder(OrderType orderType)
    {
        _orderContext.Orders.FindAsync(orderType);
        await _orderContext.SaveChangesAsync();
        return Order;      
    }

    public async Task DeleteOrder(int id)
    {
        var order = await _orderContext.Orders.FindAsync(id);
        _orderContext.Orders.Remove(order);
        await _orderContext.SaveChangesAsync();    
    }
}
