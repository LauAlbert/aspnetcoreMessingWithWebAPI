using Contact.Helpers;
using Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Services
{
    public interface IPeopleContactRepository
    {
        PagedList<PeopleContact> GetPeopleContacts(ContactsResourceParameters contactsResourceParameters);
        PeopleContact GetPeopleContact(int id);
        bool PeopleContactExist(int id);
        void AddPeopleContact(PeopleContact contact);
        void UpdatePeopleContact(PeopleContact contact);
        void PatchPeopleContact(PeopleContact contact);
        void DeletePeopleContact(int id);
        bool Save();
    }
}
