using Microsoft.AspNetCore.Mvc;
using RedOrderApi.Data;

namespace RedOrderApi.Controllers;


[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderContext _orderContext;

    public OrderController (OrderContext orderContext)
    {
        _orderContext = orderContext;
    }

    [Route("getsetoforders")]
    [HttpGet]
    public ActionResult Get(int take = 10, int skip = 0)
    {
        return Ok(_orderContext.Orders.OrderBy(p => p.OrderId).Skip(skip).Take(take));
    }
    
    [Route("createorder")]
    [HttpPost]
    public ActionResult CreateOrder(int take = 10, int skip = 0)
    {
        return Ok(_orderContext.Orders.OrderBy(p => p.OrderId).Skip(skip).Take(take));
    }
    
    [Route("getorder")]
    [HttpGet]
    public ActionResult GetOrder(int take = 10, int skip = 0)
    {
        return Ok(_orderContext.Orders.OrderBy(p => p.OrderId).Skip(skip).Take(take));
    }
    
    [Route("updateorder")]
    [HttpPut]
    public ActionResult UpdateOrder(int take = 10, int skip = 0)
    {
        return Ok(_orderContext.Orders.OrderBy(p => p.OrderId).Skip(skip).Take(take));
    }
    
    [Route("searchbyordertype")]
    [HttpGet]
    public ActionResult GetOrdersByType(int take = 10, int skip = 0)
    {
        return Ok(_orderContext.Orders.OrderBy(p => p.OrderId).Skip(skip).Take(take));
    }
    
    [Route("deleteorder")]
    [HttpDelete]
    public ActionResult DeleteOrder(int take = 10, int skip = 0)
    {
        return Ok(_orderContext.Orders.OrderBy(p => p.OrderId).Skip(skip).Take(take));
    }
}
