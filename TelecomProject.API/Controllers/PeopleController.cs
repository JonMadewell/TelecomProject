using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        [HttpGet("GetPersonByLogin")]
        public async Task<ActionResult<Person>> GetPerson([FromBody]Login login)
        {
            var login1 = new Login();

            login1.Username = login.Username;

            var person = await _context.People.Include(p => p.Login).Include(p => p.Account.plans).Include(p => p.Devices).FirstOrDefaultAsync(person => person.Login.Username == login1.Username);

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

        public async Task<ActionResult<Person>> PostPerson([FromBody] Person person)
        {
            var person1 = new Person();
            var account = new Account();
            var login = new Login();
            person1.FirstName = person.FirstName;
            person1.LastName = person.LastName;
            person1.Email = person.Email;
            login.Username = person.Login.Username;
            login.Password = person.Login.Password;
            person1.Login = login;
            person1.Account = account;

            
            _context.People.Add(person1);
            await _context.SaveChangesAsync();






            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeletePerson([FromBody] string email)
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

            Response.Headers.Add("Authorization", JsonConvert.SerializeObject(login));
            Response.Headers.Add("Access-Control-Allow-Headers", "Authorization");

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
            string phoneNumber = new Random().Next(1000000000, 2147483647).ToString();

            if(person.Devices.Count < plan.DeviceLimit)
            {
                _context.Database.ExecuteSqlInterpolated($"INSERT into dbo.PersonDevice (PersonId, DeviceId, PhoneNumber) VALUES ({person.PersonId}, {device.DeviceId}, {phoneNumber})");

                await _context.SaveChangesAsync();


                return NoContent();
            }

            return BadRequest(new { message = "Exceeded Device Limit"});
        }
        [HttpPut("AddPlan")]
        public async Task<IActionResult> AddPlan([FromQuery]int planId)
        {
            string userName = HttpContext.User.Identity.Name;
            var person = await _context.People.Include(p => p.Devices).Include(p => p.Login).Include(p => p.Account).ThenInclude(a => a.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);
            var plan = await _context.Plans.FirstOrDefaultAsync(p => p.PlanId == planId);

            _context.Database.ExecuteSqlInterpolated($"INSERT into dbo.AccountPlans (AccountsAccountId, PlanId, AccountNumber) VALUES ({person.Account.AccountId}, {plan.PlanId}, {person.Account.AccountId})");

            
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


            _context.Database.ExecuteSqlInterpolated($"DELETE from dbo.PersonDevice WHERE PersonId = {person.PersonId} AND DeviceID = {device.DeviceId}");
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("DeletePlan")]
        public async Task<IActionResult> DeletePlan([FromQuery] int planId)
        {
            string userName = HttpContext.User.Identity.Name;
            var person = await _context.People.Include(p => p.Login).Include(p => p.Devices).Include(p => p.Account).ThenInclude(a => a.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);
            var plan = person.Account.plans.FirstOrDefault(p => p.PlanId == planId);
            if (person == null)
            {
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"DELETE from dbo.AccountPlans WHERE PlanId = {plan.PlanId} AND AccountsAccountID = {person.Account.AccountId}");
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("GetDevicesForPerson")]
        public async Task<IEnumerable<Device>> ViewDevices()
        {
            string userName = HttpContext.User.Identity.Name;
            var person = await _context.People.Include(p => p.Login).Include(p => p.Devices).Include(p => p.Account).ThenInclude(a => a.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);
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
        [HttpGet("GetPhoneNumber")]
        public async Task<ActionResult<string>> ViewPhoneNumber([FromQuery] int deviceId)
        {
            string userName = HttpContext.User.Identity.Name;

            var person = await _context.People.Include(p => p.Login).Include(p => p.Devices).Include(p => p.Account).ThenInclude(a => a.plans).FirstOrDefaultAsync(p => p.Login.Username == userName);

            var p_d = await _context.Set<PersonDevice>().SingleOrDefaultAsync(pd => pd.PersonId == person.PersonId && pd.DeviceId == deviceId);

            string phoneNumber = p_d.PhoneNumber;

            return phoneNumber;
        }
    }
}