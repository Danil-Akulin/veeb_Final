using Microsoft.AspNetCore.Mvc;
using veeb.Data;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Objekti hankimine bd-st
        [HttpGet]
        public List<CartProduct> GetCarProduct()
        {
            var carproduct = _context.CartProducts.ToList();
            return carproduct;
        }

        //Objekti sisestamine andmebaasidesse
        [HttpPost]
        public List<CartProduct> PostCartProduct([FromBody] CartProduct carproduct)
        {
            _context.CartProducts.Add(carproduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }
        /*
        //this delete from table
        [HttpDelete("/kustuta2/{id}")]
        public IActionResult DeleteCartProduct(int id)
        {
            var carproduct = _context.CartProducts.Find(id);

            if (carproduct == null)
            {
                return NotFound();
            }

            _context.CartProducts.Remove(carproduct);
            _context.SaveChanges();
            return NoContent();
        }*/

        //Just connect with id and in future you can work with it
        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCartProduct(int id)
        {
            var carproduct = _context.CartProducts.Find(id);

            if (carproduct == null)
            {
                return NotFound();
            }

            return carproduct;
        }

        //Siin saame muuta oma objekte
        [HttpPut("{id}")]
        public ActionResult<List<CartProduct>> PutCarProduct(int id, [FromBody] CartProduct updatedCarProduct)
        {
            var carproduct = _context.CartProducts.Find(id);

            if (carproduct == null)
            {
                return NotFound();
            }

            carproduct.ProductId = updatedCarProduct.ProductId;
            carproduct.Quantity = updatedCarProduct.Quantity;

            _context.CartProducts.Update(carproduct);
            _context.SaveChanges();

            return Ok(_context.CartProducts);
        }
    }
}