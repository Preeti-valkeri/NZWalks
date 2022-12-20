using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Collections.Generic;

namespace NZWalks.API.Repository
{
    
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDBContext _dbContext;
        public UserRepository(NZWalksDBContext nZWalksDBContext)
        {
            this._dbContext = nZWalksDBContext;

        }
        private List<User> users = new List<User>()
        {
        //new User()
        //{
        // FirstName="Readonly",LastName="User",EmailID="readonly@user.com",
        // Id=Guid.NewGuid(),UserName="readonly@user.com",Password="Readonly@user",
        // Roles=new List<string>{"reader"}
        //},
        //new User()
        //{
        // FirstName="Read Write",LastName="User",EmailID="readwrite@user.com",
        // Id=Guid.NewGuid(),UserName="readwrite@user.com",Password="ReadWrite@user",
        // Roles=new List<string>{"reader","writer"}
        //}
        };
        public async Task<User> AuthenticateAsync(string username, string password)
        {

            //var user = users.Find(x => x.UserName.Equals(Username, StringComparison.InvariantCultureIgnoreCase)
            //&& x.Password == Password);
            //if(user!=null)
            //{
            //    return user;
            //}
            //return null;
            var user = await _dbContext.users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower()
          && x.Password == password);
            if(user==null)
            {
                return null;
            }
          var userRoles=  await _dbContext.userRoles.Where(x=>x.UserId==user.Id).ToListAsync();
            if(userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach(var userRole in userRoles)
                {
                    var role = await _dbContext.roles.FirstOrDefaultAsync(x=>x.id==userRole.RoleId);
                    if(role!=null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}
