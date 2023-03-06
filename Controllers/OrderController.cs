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

    [HttpGet]
    public ActionResult Get(int take = 10, int skip = 0)
    {
        return Ok(_orderContext.Orders.OrderBy(p => p.OrderId).Skip(skip).Take(take));
    }
}
