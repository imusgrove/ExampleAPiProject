using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RedOrderApi.Data;
using RedOrderApi.Services;
using Order = RedOrderApi.DTOS.Order;

namespace RedOrderApi.Controllers;


[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderContext _orderContext;
    private readonly IOrderService _orderService;


    public OrderController (OrderContext orderContext, IOrderService orderService)
    {
        _orderContext = orderContext;
        _orderService = orderService;
    }

    [Route("createorder")]
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    { 
        var results = _orderService.CreateOrder(order);
        return Ok(results);
    }
    
    [Route("getorder")]
    [HttpGet]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var results = _orderService.GetOrder(id);
        return Ok(results);     
    }
    
    [Route("updateorder")]
    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
    {
        var results = _orderService.UpdateOrder(id,order);
        return Ok(results);    
    }
    
    [Route("searchbyordertype")]
    [HttpGet]
    public async Task<ActionResult<Order>> SearchByOrderType(DTOS.OrderType orderType)
    {
        var results = _orderService.SearchByOrderType(orderType);
        return Ok(results);     
    }
    
    [Route("deleteorder")]
    [HttpDelete]
    public OkObjectResult DeleteOrder(int id)
    {
        _orderService.DeleteOrder(id);
        return Ok($"Record {id} deleted");
    }
}
