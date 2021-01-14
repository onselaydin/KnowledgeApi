using KnowledgeApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace KnowledgeApi.Services
{
    public class AuthenticateService: IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly IMongoCollection<User> _user;
        public AuthenticateService(IOptions<AppSettings> appSettings, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.MongoConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>("users");

            _appSettings = appSettings.Value;
        }

        //private List<User> users = new List<User>()
        //{
        //    new User{UserId = 1, FirstName ="Önsel", LastName = "Aydın",UserName="onselaydin",Password="onsel123"}
        //};

        public bool CheckUser(string UserName)
        {
            var usr = _user.AsQueryable().Where(x => x.UserName == UserName).ToList();
            if (usr.Count > 0)
                return true;
            return false;
        }

        public User CreateUser(User usr)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usr.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //HmacSha256Signature //HmacSha256
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var model = new User
            {
                UserId = usr.UserId,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                UserName = usr.UserName,
                Password = usr.Password,
                Token = tokenHandler.WriteToken(token)
            };

            _user.InsertOne(model);
            return usr;
        }

        public User Authenticate(string userName, string password)
        {
            var user = _user.AsQueryable().SingleOrDefault(x => x.UserName == userName && x.Password == password);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //HmacSha256Signature //HmacSha256
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }
    }
}
