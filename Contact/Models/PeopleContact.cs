using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class PeopleContact
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
