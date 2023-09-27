using Microsoft.AspNetCore.Mvc;
using veeb.Data;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Objekti hankimine andmebaasidest
        [HttpGet]
        public List<Order> GetOrder()
        {
            var order = _context.Orders.ToList();
            return order;
        }

        //Sisesta objekt
        [HttpPost]
        public List<Order> PostOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }
        /*
        //this delete from table
        [HttpDelete("/kustuta2/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }*/

        //Just connect with id and in future you can work with it
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        //Siin saame muuta oma objekte
        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrder(int id, [FromBody] Order updatedOrder)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            order.created = updatedOrder.created;
            order.TotalSum = updatedOrder.TotalSum;
            order.Paid = updatedOrder.Paid;
            order.Person = updatedOrder.Person;

            _context.Orders.Update(order);
            _context.SaveChanges();

            return Ok(_context.Orders);
        }
    }
}