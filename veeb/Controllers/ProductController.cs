using Microsoft.AspNetCore.Mvc;
using veeb.Data;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Objekti hankimine bd-st
        [HttpGet]
        public List<Product> GetProduct()
        {
            var product = _context.Products.ToList();
            return product;
        }

        //Sisesta objekt
        [HttpPost]
        public List<Product> PostProductes([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        //see kustutada tabelist
        [HttpDelete("{id}")]
        public List<Product> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return _context.Products.ToList();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        //Just connect with id and in future you can work with it
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        //Siin saame muuta oma objekte
        [HttpPut("{id}")]
        public ActionResult<List<Product>> PutProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Image = updatedProduct.Image;
            product.Active = updatedProduct.Active;
            product.Stock = updatedProduct.Stock;
            product.CategoryId = updatedProduct.CategoryId;

            _context.Products.Update(product);
            _context.SaveChanges();

            return Ok(_context.Products);
        }
    }
}