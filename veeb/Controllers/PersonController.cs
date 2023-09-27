using Microsoft.AspNetCore.Mvc;
using veeb.Data;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Objekti hankimine bd-st
        [HttpGet]
        public List<Person> GetArticles()
        {
            var person = _context.Persons.ToList();
            return person;
        }

        //Sisesta objekt
        [HttpPost]
        public List<Person> PostPerson([FromBody] Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
            return _context.Persons.ToList();
        }

        //see funktsioon kustutab objekti tabelist id-le
        [HttpDelete("{id}")]
        public List<Person> DeletePerson(int id)
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return _context.Persons.ToList();
            }

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return _context.Persons.ToList();
        }

        //Just connect with id and in future you can work with it
        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        //Siin saame muuta oma objekte
        [HttpPut("{id}")]
        public ActionResult<List<Person>> PutPerson(int id, [FromBody] Person updatedPerson)
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            person.PersonCode = updatedPerson.PersonCode;
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Phone = updatedPerson.Phone;
            person.Address = updatedPerson.Address;
            person.Password = updatedPerson.Password;

            _context.Persons.Update(person);
            _context.SaveChanges();

            return Ok(_context.Articles);
        }

    }
}