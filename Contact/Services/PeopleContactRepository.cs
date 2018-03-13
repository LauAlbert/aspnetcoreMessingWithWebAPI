using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Data;
using Contact.Helpers;
using Contact.Models;

namespace Contact.Services
{
    public class PeopleContactRepository : IPeopleContactRepository
    {
        private readonly ContactContext _context;

        public PeopleContactRepository(ContactContext ctx)
        {
            _context = ctx;
        }

        public void AddPeopleContact(PeopleContact contact)
        {
            contact.DateAdded = DateTime.Now;
            _context.Add(contact);
        }

        public void DeletePeopleContact(int id)
        {
            var target = GetPeopleContact(id);
            _context.Remove(target);
        }

        public PeopleContact GetPeopleContact(int id)
        {
            return _context.PeopleContacts.Where(pc => pc.ID == id).FirstOrDefault();
        }

        public PagedList<PeopleContact> GetPeopleContacts(ContactsResourceParameters contactsResourceParameters)
        {
            var collectionBeforePaging = _context.PeopleContacts
                .OrderBy(pc => pc.FirstName)
                .ThenBy(pc => pc.LastName);

            return PagedList<PeopleContact>.Create(collectionBeforePaging, contactsResourceParameters.PageNumber, contactsResourceParameters.PageSize);
        }

        public void PatchPeopleContact(PeopleContact contact)
        {
            contact.DateAdded = DateTime.Now;
        }

        public bool PeopleContactExist(int id)
        {
            return _context.PeopleContacts.Any(s => s.ID == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() > 0);
        }

        public void UpdatePeopleContact(PeopleContact contact)
        {
            contact.DateAdded = DateTime.Now;
        }
    }
}
