using KnowledgeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeApi.Services
{
    public interface IAuthenticateService
    {
        User Authenticate(string userName, string password);
        bool CheckUser(string UserName);
        User CreateUser(User usr);
    }
}
