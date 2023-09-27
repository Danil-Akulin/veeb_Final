using Microsoft.AspNetCore.Mvc;
using veeb.Data;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Objekti hankimine bd-st
        [HttpGet]
        public List<Category> GetCategorys()
        {
            var categorys = _context.Categorys.ToList();
            return categorys;
        }

        //Objekti sisestamine andmebaasi
        [HttpPost]
        public List<Category> PostCategorys([FromBody] Category categorys)
        {
            _context.Categorys.Add(categorys);
            _context.SaveChanges();
            return _context.Categorys.ToList();
        }

        //see kustutatakse tabelist id
        [HttpDelete("{id}")]
        public List<Category> DeleteCategory(int id)
        {
            var category = _context.Categorys.Find(id);

            if (category == null)
            {
                return _context.Categorys.ToList();
            }

            _context.Categorys.Remove(category);
            _context.SaveChanges();
            return _context.Categorys.ToList();
        }

        //Siin saame muuta meie objekte andmebaasides
        [HttpPut("{id}")]
        public ActionResult<List<Category>> PutArtikkel(int id, [FromBody] Category updatedCategory)
        {
            var category = _context.Categorys.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = updatedCategory.Name;

            _context.Categorys.Update(category);
            _context.SaveChanges();

            return Ok(_context.Articles);
        }
    }
}