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
/// <summary>
/// Creates new order
/// </summary>
/// <param name="order"></param>
/// <remarks>
///OrderType
///{
/// Standard = 1,
///SaleOrder = 2,
///PurchaseOrder = 3,
///TransferOrder = 4,
//ReturnOrder = 5
///}
/// </remarks>
/// <returns>Newly created order</returns>
    [Route("createorder")]
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        try
        {
            var results = await _orderService.CreateOrder(order);
            Log.Information("Order created.");
            return  Ok(results);
        }
        catch (Exception e)
        {
            Log.Error(e,"Error creating order.");
            throw;
        }
    }
    /// <summary>
    /// Gets an order based on the id passed in
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns order</returns>
    [Route("getorder")]
    [HttpGet]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        try
        {
            var results = await _orderService.GetOrder(id);
            Log.Information("Returning order ${id}.");
            return Ok(results); 
        }
        catch (Exception e)
        {
            Log.Error(e,"Error getting order.");
            throw;
        }
            
    }
    /// <summary>
    /// Updates order based on id passed in
    /// </summary>
    /// <param name="id"></param>
    /// <param name="order"></param>
    /// <remarks>
    ///OrderType
    ///{
    /// Standard = 1,
    ///SaleOrder = 2,
    ///PurchaseOrder = 3,
    ///TransferOrder = 4,
   //ReturnOrder = 5
    ///}
    /// </remarks>
    /// <returns>Returns updated order</returns>
    [Route("updateorder")]
    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
    {
        try
        {
            var results = await _orderService.UpdateOrder(id,order);
            Log.Information("Updating order ${id}.");
            return Ok(results);
        }
        catch (Exception e)
        {
            Log.Error(e,"Error updating order.");
            throw;
        }
           
    }
    /// <summary>
    /// Search orders based on order type
    /// </summary>
    /// <param name="orderType"></param>
    /// <remarks>
    ///OrderType
    ///{
    /// Standard = 1,
    ///SaleOrder = 2,
    ///PurchaseOrder = 3,
    ///TransferOrder = 4,
    //ReturnOrder = 5
    ///}
    /// </remarks>
    /// <returns>Returns list of orders with specified order type</returns>
    [Route("searchbyordertype")]
    [HttpGet]
    public async Task<ActionResult<Order>> SearchByOrderType(DTOS.OrderType orderType)
    {
        try
        {
            var results = await _orderService.SearchByOrderType(orderType);
            Log.Information("Searching for ordertype ${orderType}.");
            return Ok(results); 
        }
        catch (Exception e)
        {
            Log.Error(e, "Error searching database.");
            throw;
        }
            
    }
    /// <summary>
    /// Deletes order
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Deletes order based on id passed in</returns>
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
