using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contact.Data;
using Contact.Dtos;
using Contact.Helpers;
using Contact.Models;
using Contact.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Controllers
{
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        private readonly IPeopleContactRepository _context;
        private readonly IUrlHelper _urlHelper;

        public ContactsController(IPeopleContactRepository ctx, IUrlHelper urlHelper)
        {
            _context = ctx;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetAuthors")]
        public IActionResult GetContacts(ContactsResourceParameters contactsResourceParameters)
        {
            var peopleContactsFromRepo = _context.GetPeopleContacts(contactsResourceParameters);

            var previousPageLink = peopleContactsFromRepo.HasPrevious ? CreateContactsResourceUri(contactsResourceParameters, ResourceUriType.PreviousPage) : null;


            var nextPageLink = peopleContactsFromRepo.HasNext ? CreateContactsResourceUri(contactsResourceParameters, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = peopleContactsFromRepo.Count,
                pageSize = peopleContactsFromRepo.PageSize,
                currentPage = peopleContactsFromRepo.CurrentPage,
                totalPages = peopleContactsFromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            var result = Mapper.Map<IEnumerable<PeopleContactDto>>(peopleContactsFromRepo);
            return Ok(result);
        }


        private string CreateContactsResourceUri(ContactsResourceParameters contactsResourceParameters, ResourceUriType type)
        {
            switch(type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetAuthors",
                        new
                        {
                            pageNumber = contactsResourceParameters.PageNumber - 1,
                            pageSize = contactsResourceParameters.PageSize
                        });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetAuthors",
                        new
                        {
                            pageNumber = contactsResourceParameters.PageNumber + 1,
                            pageSize = contactsResourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link("GetAuthors",
                        new
                        {
                            pageNumber = contactsResourceParameters.PageNumber,
                            pageSize = contactsResourceParameters.PageSize
                        });

            }
        }


        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult GetContact(int id)
        {
            var peopleContactFromRepo = _context.GetPeopleContact(id);
            if (peopleContactFromRepo == null)
            {
                return NotFound();
            }
            return Ok(peopleContactFromRepo);

        }

        [HttpPost()]
        public IActionResult AddContact([FromBody] PeopleContactPostDto ContactInfo)
        {
            if (ContactInfo == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newContact = Mapper.Map<PeopleContact>(ContactInfo);
            _context.AddPeopleContact(newContact);
            if (!_context.Save())
            {
                return StatusCode(500, "A problem occured");
            }

            return CreatedAtRoute("GetContact", new { id = newContact.ID }, newContact);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] PeopleContactPutDto ContactInfo)
        {
            if (ContactInfo == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var contactFromDb = _context.GetPeopleContact(id);
            if (contactFromDb == null)
            {
                return NotFound();
            }

            Mapper.Map(ContactInfo, contactFromDb);
            _context.UpdatePeopleContact(contactFromDb);

            if (!_context.Save())
            {
                return StatusCode(500, "A problem occured");
            }

            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult PatchContact(int id, [FromBody] JsonPatchDocument<PeopleContactPutDto> ContactInfo)
        {
            if (ContactInfo == null)
            {
                return BadRequest();
            }
            var contactFromDb = _context.GetPeopleContact(id);
            if (contactFromDb == null)
            {
                return NotFound();
            }
            var thingsToPatch = Mapper.Map<PeopleContactPutDto>(contactFromDb);
            ContactInfo.ApplyTo(thingsToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            TryValidateModel(thingsToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Mapper.Map(thingsToPatch, contactFromDb);

            if (!_context.Save())
            {
                return StatusCode(500, "A problem occured");
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            if (!_context.PeopleContactExist(id))
            {
                return NotFound();
            }
            _context.DeletePeopleContact(id);
            if (!_context.Save())
            {
                return StatusCode(500, "A problem occured");
            }
            return NoContent();
        }
    }
}