using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    internal interface IEFService
    {
        public bool CheckCredentials(UserCredentials userCredentials);
        public void CreateUser(UserCredentials credentials);
    }
}
