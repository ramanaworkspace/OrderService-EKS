using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly List<Order> Orders = new()
    {
        new Order { Id = 1, CustomerName = "Ram", Product = "Laptop", Quantity = 1, Price = 75000 },
        new Order { Id = 2, CustomerName = "Kumar", Product = "Mobile", Quantity = 2, Price = 50000 }
    };

    // GET: api/orders
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Orders);
    }

    // GET: api/orders/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    // POST: api/orders
    [HttpPost]
    public IActionResult Create(Order order)
    {
        order.Id = Orders.Max(o => o.Id) + 1;
        Orders.Add(order);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }
}
