using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelecomProject.API.Handlers
{
    public interface IJwtAuthHandler
    {
        string Authenticate(string username, string password);
    }
}
