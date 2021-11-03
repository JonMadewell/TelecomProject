using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telecom.Domain;
using TelecomProject.Data;

namespace TelecomProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly TelecomProjectContext _context;

        public PeopleController(TelecomProjectContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => p.PersonId == id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdatePerson")]
        public async Task<IActionResult> PutPerson(string firstName, string lastName, string email)
        {
            Person person = await _context.People.Include(p => p.Login).FirstOrDefaultAsync(p => p.FirstName == firstName && p.LastName == lastName);
            if (person == null)
            {
                return BadRequest();
            }

            person.Email = email;
            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.PersonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<ActionResult<Person>> PostPerson([FromQuery] string firstName, string lastName, string email, string userName, string password)
        {
            var person = new Person();
            var login = new Login();
            var account = new Account();

            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = email;
            login.Username = userName;
            login.Password = password;
            person.Login = login;
            person.Account = account;
            _context.People.Add(person);
            await _context.SaveChangesAsync();






            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeletePerson([FromQuery] string email)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => p.Email == email);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<IActionResult> Authenticate([FromQuery] string username, string password)
        {
            string userName = HttpContext.User.Identity.Name;

            var login = await _context.logins.FirstOrDefaultAsync(login => login.Username == userName);
            var person = await _context.People.FirstOrDefaultAsync(person => person.LoginId == login.LoginId);

            return Ok(person);
        }
    }
}