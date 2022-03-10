using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseProject;
using EFcorePlugin.EntityFrameworkConf;

namespace EFcorePlugin
{
    internal class UserService : IUserService
    {
        private readonly DataContext context;
        public UserService()
        {
            this.context = new DataContext();
        }

        public IEnumerable<User> GetUsers() => this.context.Users;
        
        public IEnumerable<Gender> GetGenders() => this.context.Genders;

        public void Add(User user)
        {
            context.Users?.Add(user);
            context.SaveChanges();
        }

        public User? GetById(int id)
        {
            return (from u in context.Users 
                    where u.Id == id 
                    select u).FirstOrDefault();
        }

        public void Update(User UpdatingUser)
        {
            context.Users.Update(UpdatingUser);
            context.SaveChanges();
        }

        public void Delete(User DeletingUser)
        {
            context.Remove(DeletingUser);
            context.SaveChanges();
        }
    }
}
