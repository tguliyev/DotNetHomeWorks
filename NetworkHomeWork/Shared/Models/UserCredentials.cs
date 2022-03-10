using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class UserCredentials
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public UserCredentials(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }
    }
}