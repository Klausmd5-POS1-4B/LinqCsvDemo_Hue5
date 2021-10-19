using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PersonDBLib
{
    public class PersonContext
    {
        public PersonContext()
        {
            InitializeAdresses();
            InitializePersons();
            InitializeForeignKeys();
        }

        private void InitializeForeignKeys()
        {
            /* 
             var adressMap = new Dictionary<int, Adress>();
             foreach(var adress in Adresses)
             {
                 adressMap[adress.Id] = adress;
             }*/

            var adressMap = Adresses.ToDictionary(x => x.Id);

            /*foreach(var person in Persons)
            {
                int adressId = person.AddressId;
                var adress = adressMap[adressId];
                person.Adress = adress;
            }*/
            Persons.ForEach(x => x.Adress = adressMap[x.AddressId]);

        }

        private void InitializeAdresses()
        {
            Adresses = File.ReadAllLines(@"csv/adresses.csv")
            .Skip(1)
            .Select(x => x.Split(",")) // str[]
            .Select(x => new Adress
            {
                Id = int.Parse(x[0]),
                Country = x[1],
                City = x[2],
                PostalCode = int.TryParse(x[3], out int val) ? val : null,
                StreetName = x[4],
                StreetNumber = int.Parse(x[5]),

            })
            .ToList();
        }

        private void InitializePersons()
        {
            Persons = File.ReadAllLines(@"csv/persons.csv") //collection string
            .Skip(1)
            .Select(x => x.Split(",")) // str[]
            .Select(x => new Person
            {
                Id = int.Parse(x[0]),
                Firstname = x[1],
                Lastname = x[2],
                Email = x[3],
                Gender = x[4],
                BirthDate = DateTime.ParseExact(x[5], "dd.MM.yyyy", null),
                AddressId = int.Parse(x[6]),
            })
            .ToList();
        }

        public List<Person> Persons { get; set; }
        public List<Adress> Adresses { get; set; }
    }
}
