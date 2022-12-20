using NZWalks.API.Models.Domain;
using System.Collections.Generic;

namespace NZWalks.API.Repository
{
   
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>()
        {
        new User()
        {
         FirstName="Readonly",LastName="User",EmailID="readonly@user.com",
         Id=Guid.NewGuid(),UserName="readonly@user.com",Password="Readonly@user",
         Roles=new List<string>{"reader"}
        },
        new User()
        {
         FirstName="Read Write",LastName="User",EmailID="readwrite@user.com",
         Id=Guid.NewGuid(),UserName="readwrite@user.com",Password="ReadWrite@user",
         Roles=new List<string>{"reader","writer"}
        }
        };
        public async Task<User> AuthenticateAsync(string Username, string Password)
        {
            var user = users.Find(x => x.UserName.Equals(Username, StringComparison.InvariantCultureIgnoreCase)
            && x.Password == Password);
            if(user!=null)
            {
                return user;
            }
            return null;
        }
    }
}
