using Server.EntityFramework;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    internal class EFService : IEFService
    {
        private readonly DataContext context;

        public EFService()
        {
            this.context = new DataContext();
        }

        public bool CheckCredentials(UserCredentials userCredentials)
        {
            if (userCredentials == null)
                return false;

            UserCredentials? user = (from u in context.Users.AsParallel()
                        where u.Name == userCredentials.Name
                        select u).FirstOrDefault();

            if (user == null || 
                user.Name != userCredentials.Name ||
                user.Password != userCredentials.Password)
            {
                return false;
            }

            return true;
        }

        public void CreateUser(UserCredentials credentials)
        {
            try
            {
                context.Users.Add(credentials);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
