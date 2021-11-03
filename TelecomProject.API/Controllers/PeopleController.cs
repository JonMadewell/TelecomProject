using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telecom.Domain;
using TelecomProject.API.Handlers;
using TelecomProject.API.Services;
using TelecomProject.Data;

namespace TelecomProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly TelecomProjectContext _context;
        private readonly ILoginService _loginService;
        public PeopleController(TelecomProjectContext context, ILoginService loginService)
        {
            _context = context;
            _loginService = loginService;
            
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        // GET: api/People/5
        
        [HttpGet("ViewInfo")]
        public async Task<ActionResult<Person>> GetPerson()
        {
            string userName = HttpContext.User.Identity.Name;

            
            var person = await _context.People.Include(p => p.Login).Include(p => p.Account.plans).Include(p => p.Devices).FirstOrDefaultAsync(person => person.Login.Username == userName);

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
        [AllowAnonymous]
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
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            var person = await _loginService.Authenticate(login.Username, login.Password);

            if(person == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

           

            return Ok(person);
        }
        [HttpPut("AddDevice")]
        public async Task<IActionResult> AddDevice([FromQuery]int DeviceId, int planId)
        {
            string userName = HttpContext.User.Identity.Name;

            var person = await _context.People.Include(p => p.Devices).Include(p => p.Login).Include(p => p.Account.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);
            var plans =  person.Account.plans.ToList();
            var plan = plans.FirstOrDefault(p => p.PlanId == planId);
            var device = await _context.Devices.Include(d => d.People).FirstOrDefaultAsync(d => d.DeviceId == DeviceId);

            if(person.Devices.Count < plan.DeviceLimit)
            {
                person.Devices.Add(device);
                device.People.Add(person);                
                await _context.SaveChangesAsync();

                //var p_d = await _context.Set<PersonDevice>().SingleOrDefaultAsync(pd => pd.PersonId == person.PersonId && pd.DeviceId == device.DeviceId);
                //if (p_d != null)
                //{
                //    p_d.PhoneNumber = new Random().Next(1000000000, 2147483647).ToString();
                //}
                //_context.Entry(p_d).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

                return NoContent();
            }

            return BadRequest(new { message = "Exceeded Device Limit"});
        }
        [HttpPut("AddPlan")]
        public async Task<IActionResult> AddPlan([FromQuery]int planId)
        {
            string userName = HttpContext.User.Identity.Name;
            var person = await _context.People.Include(p => p.Devices).Include(p => p.Login).Include(p => p.Account).FirstOrDefaultAsync(p => p.Login.Username == userName);
            var plan = person.Account.plans.FirstOrDefault(p => p.PlanId == planId);


            person.Account.plans.Add(plan);

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("DeleteDevice")]
        public async Task<IActionResult> DeleteDevice([FromQuery]int deviceId)
        {
            string userName = HttpContext.User.Identity.Name;
            var person = await _context.People.Include(p => p.Login).Include(p => p.Devices).Include(p => p.Account.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);
            var device = person.Devices.FirstOrDefault(d => d.DeviceId == deviceId);
            if (person == null)
            {
                return NotFound();
            }

            person.Devices.Remove(device);
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("DeletePlan")]
        public async Task<IActionResult> DeletePlan([FromQuery] int planId)
        {
            string userName = HttpContext.User.Identity.Name;
            var person = await _context.People.Include(p => p.Login).Include(p => p.Devices).Include(p => p.Account.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);
            var plan = person.Account.plans.FirstOrDefault(p => p.PlanId == planId);
            if (person == null)
            {
                return NotFound();
            }

            person.Account.plans.Remove(plan);
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("GetDevicesForPerson")]
        public async Task<IEnumerable<Device>> ViewDevices()
        {
            string userName = HttpContext.User.Identity.Name;
            var person = await _context.People.Include(p => p.Login).Include(p => p.Devices).Include(p => p.Account.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);
            var devices = person.Devices.ToList();
            //var devices = await _context.Devices.Include(d => d.People).ToListAsync();

            return devices;
        }
        [HttpGet("GetPlansForPerson")]
        public async Task<IEnumerable<Plan>> ViewPlans()
        {
            string userName = HttpContext.User.Identity.Name;
            var person = await _context.People.Include(p => p.Login).Include(p => p.Account.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);
            var plans = person.Account.plans.ToList();

            return plans;
        }
    }
}