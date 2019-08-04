using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamin.Registration.DataLayer.Entities;

namespace Tamin.Registration.DataLayer
{
    public class RegistrationDbContext : DbContext
    {
        public RegistrationDbContext() : base("registration_cs")
        {

        }
        public DbSet<RegisterForm> RegisterForms { get; set; }
        public DbSet<Event> Events  { get; set; }

    }
}
