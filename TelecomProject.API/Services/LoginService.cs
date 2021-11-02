using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telecom.Domain;
using TelecomProject.Data;

namespace TelecomProject.API.Services
{
    
    public interface ILoginService 
    {
        Task<Person> Authenticate(string username, string password);
        
    }

    public class LoginService : ILoginService
    {
        private readonly TelecomProjectContext _context;
        public LoginService(TelecomProjectContext context)
        {
            _context = context;
        }
        public async Task<Person> Authenticate(string username, string password)
        {
            var person = await Task.Run(() => _context.People.Include(p => p.Login).Include(p => p.Devices).Include(p => p.Account.plans).FirstOrDefaultAsync(p => p.Login.Username == username && p.Login.Password == password));

            if(person == null)
            {
                return null;
            }

            return person;
        }
    }
}
