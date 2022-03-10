using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
        public IEnumerable<Gender> GetGenders();
        public void Add(User user);
        public User? GetById(int id);
        public void Update(User UpdatingUser);
        public void Delete(User DeletingUser);
    }
}
