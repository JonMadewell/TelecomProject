using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Telecom.Domain;
using TelecomProject.API.Services;
using TelecomProject.Data;

namespace TelecomProject.API.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly TelecomProjectContext _context;
        private readonly ILoginService _loginService;
        public BasicAuthenticationHandler(
                IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger,
                UrlEncoder encoder,
                ISystemClock clock,
                TelecomProjectContext context,
                ILoginService loginService) 
                : base(options, logger, encoder, clock)
        {
            _context = context;
            _loginService = loginService;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header was not found");
            }
            try
            {
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                var credentials = Encoding.UTF8.GetString(bytes).Split(":");
                string userName = credentials[0];
                string password = credentials[1];

                Person Person = await _loginService.Authenticate(userName, password);
                //Login login = await _context.logins.FirstOrDefaultAsync(login => login.Username == userName && login.Password == password);
                //Person person = await _context.People.FirstOrDefaultAsync(person => person.LoginId == login.LoginId);

                if(Person == null)
                {
                    AuthenticateResult.Fail("Invalid Username");
                }
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name,Person.Login.Username) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }
            catch(Exception)
            {
                return AuthenticateResult.Fail("Error");
            }

            return AuthenticateResult.Fail("");

        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
            return base.HandleChallengeAsync(properties);
        }
    }
}
