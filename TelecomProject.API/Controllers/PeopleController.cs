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
    
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly TelecomProjectContext _context;
        private readonly TelecomProjectContext newContext;

        public PeopleController(TelecomProjectContext context)
        {
            _context = context;
            newContext = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        // GET: api/People/5
        [Authorize]
        [HttpGet("GetPerson")]
        public async Task<ActionResult<Person>> GetPerson()
        {
            string userName = HttpContext.User.Identity.Name;

            var login = await _context.logins.FirstOrDefaultAsync(login => login.Username == userName);
            var person = await _context.People.FirstOrDefaultAsync(person => person.LoginId == login.LoginId);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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
        
        public async Task<ActionResult<Person>> PostPerson([FromQuery]string firstName, string lastName, string email, string userName, string password)
        {
            var person = new Person();
            var login = new Login();
            var account = new Account();

            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = email;
            login.Username = userName;
            login.Password = password;


                _context.People.Add(person);
                await _context.SaveChangesAsync();
            


                Person person1 = await _context.People.OrderBy(p => p.PersonId).LastOrDefaultAsync();
                login.PersonId = person1.PersonId;

                account.PersonId = person1.PersonId;
                _context.logins.Add(login);

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
            

            using (newContext)
            {
                Person person2 = await _context.People.OrderBy(p => p.PersonId).LastOrDefaultAsync();
                Login login1 = await _context.logins.FirstOrDefaultAsync(l => l.PersonId == person2.PersonId);

                person2.LoginId = login1.LoginId;

                Account account1 = await _context.Accounts.FirstOrDefaultAsync(a => a.PersonId == person2.PersonId);

                person2.AccountId = account1.AccountId;

                newContext.Update(person2);
                await newContext.SaveChangesAsync();
            }


            


            

            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
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
    }
}
