using Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Data
{
    public static class DbInitializer
    {
        public static void Initializer(this ContactContext context)
        {
            context.Database.EnsureCreated();
            if (context.PeopleContacts.Any())
            {
                return;
            }
            var peopleContacts = new PeopleContact[]
            {
                new PeopleContact{FirstName="A", LastName="B", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a boy"},
                new PeopleContact{FirstName="C", LastName="D", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a teenager"},
                new PeopleContact{FirstName="E", LastName="F", PhoneNumber="324324324", Gender=Gender.Female, Description="Im a female"},
                new PeopleContact{FirstName="G", LastName="H", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a man"},
                new PeopleContact{FirstName="I", LastName="J", PhoneNumber="14354353", Gender=Gender.Female, Description="Im a girl"},
                new PeopleContact{FirstName="K", LastName="L", PhoneNumber="123143534213", Gender=Gender.Female, Description="Im a women"},
                new PeopleContact{FirstName="M", LastName="N", PhoneNumber="123123213213", Gender=Gender.Female, Description="Im a boy"},
                 new PeopleContact{FirstName="A", LastName="B", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a boy"},
                new PeopleContact{FirstName="C", LastName="D", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a teenager"},
                new PeopleContact{FirstName="E", LastName="F", PhoneNumber="324324324", Gender=Gender.Female, Description="Im a female"},
                new PeopleContact{FirstName="G", LastName="H", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a man"},
                new PeopleContact{FirstName="I", LastName="J", PhoneNumber="14354353", Gender=Gender.Female, Description="Im a girl"},
                new PeopleContact{FirstName="K", LastName="L", PhoneNumber="123143534213", Gender=Gender.Female, Description="Im a women"},
                new PeopleContact{FirstName="M", LastName="N", PhoneNumber="123123213213", Gender=Gender.Female, Description="Im a boy"},
                 new PeopleContact{FirstName="A", LastName="B", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a boy"},
                new PeopleContact{FirstName="C", LastName="D", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a teenager"},
                new PeopleContact{FirstName="E", LastName="F", PhoneNumber="324324324", Gender=Gender.Female, Description="Im a female"},
                new PeopleContact{FirstName="G", LastName="H", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a man"},
                new PeopleContact{FirstName="I", LastName="J", PhoneNumber="14354353", Gender=Gender.Female, Description="Im a girl"},
                new PeopleContact{FirstName="K", LastName="L", PhoneNumber="123143534213", Gender=Gender.Female, Description="Im a women"},
                new PeopleContact{FirstName="M", LastName="N", PhoneNumber="123123213213", Gender=Gender.Female, Description="Im a boy"},
                 new PeopleContact{FirstName="A", LastName="B", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a boy"},
                new PeopleContact{FirstName="C", LastName="D", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a teenager"},
                new PeopleContact{FirstName="E", LastName="F", PhoneNumber="324324324", Gender=Gender.Female, Description="Im a female"},
                new PeopleContact{FirstName="G", LastName="H", PhoneNumber="123123213213", Gender=Gender.Male, Description="Im a man"},
                new PeopleContact{FirstName="I", LastName="J", PhoneNumber="14354353", Gender=Gender.Female, Description="Im a girl"},
                new PeopleContact{FirstName="K", LastName="L", PhoneNumber="123143534213", Gender=Gender.Female, Description="Im a women"},
                new PeopleContact{FirstName="M", LastName="N", PhoneNumber="123123213213", Gender=Gender.Female, Description="Im a boy"}
            };

            context.AddRange(peopleContacts);
            context.SaveChanges();
        }
    }
}
