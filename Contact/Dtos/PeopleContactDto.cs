using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Models;

namespace Contact.Dtos
{
    public class PeopleContactDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
