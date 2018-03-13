using Contact.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Contact.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) 
            : base(options)
        {
        }

        public DbSet<PeopleContact> PeopleContacts { get; set; }
    }
}
