using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RedOrderApi.Data;
using RedOrderApi.Services;
using Serilog;
using Order = RedOrderApi.DTOS.Order;

namespace RedOrderApi.Controllers;


[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;


    public OrderController (IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Route("createorder")]
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        try
        {
            var results = _orderService.CreateOrder(order);
            Log.Information("Order created.");
            return Ok(results);
        }
        catch (Exception e)
        {
            Log.Error(e,"Error creating order.");
            throw;
        }
    }
    
    [Route("getorder")]
    [HttpGet]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        try
        {
            var results = _orderService.GetOrder(id);
            Log.Information("Returning order ${id}.");
            return Ok(results); 
        }
        catch (Exception e)
        {
            Log.Error(e,"Error getting order.");
            throw;
        }
            
    }
    
    [Route("updateorder")]
    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
    {
        try
        {
            var results = _orderService.UpdateOrder(id,order);
            Log.Information("Updating order ${id}.");
            return Ok(results);
        }
        catch (Exception e)
        {
            Log.Error(e,"Error updating order.");
            throw;
        }
           
    }
    
    [Route("searchbyordertype")]
    [HttpGet]
    public async Task<ActionResult<Order>> SearchByOrderType(DTOS.OrderType orderType)
    {
        try
        {
            var results = _orderService.SearchByOrderType(orderType);
            Log.Information("Searching for ordertype ${orderType}.");
            return Ok(results); 
        }
        catch (Exception e)
        {
            Log.Error(e, "Error searching database.");
            throw;
        }
            
    }
    
    [Route("deleteorder")]
    [HttpDelete]
    public OkObjectResult DeleteOrder(int id)
    {
        try
        {
            _orderService.DeleteOrder(id);
            Log.Information("Deleting order ${id}.");
            return Ok($"Record {id} deleted.");
        }
        catch (Exception e)
        {
            Log.Error(e,"Error deleting order.");
            throw;
        }
      
    }
}
