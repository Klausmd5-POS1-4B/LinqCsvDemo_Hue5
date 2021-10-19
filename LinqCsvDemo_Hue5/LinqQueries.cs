using PersonDBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LinqCsvDemo
{
  public class LinqQueries
  {
    private readonly PersonContext db;

    public LinqQueries(PersonContext db)
    {
      this.db = db;
    }

    public List<string> MalesStreetNrLessThan10()
    {
            return db.Persons.Where(x => x.Gender == "Male").Where(x => x.Adress.StreetNumber < 10).Select(x => $"{x.Lastname} {x.Firstname}").ToList();
    }

    public List<string> FirstnamesInChina()
    {
            return db.Persons.Where(x => x.Adress.Country == "China").Select(x => x.Firstname).ToList();
    }

    public int MaxStreetNrInCountry(string country)
    {
            return db.Adresses
                .Where(x => x.Country == country)
                .Select(x => x.StreetNumber)
                .Max();
    }

    public List<string> CountriesWithEmailEndingWithOrg()
    {
            return db.Persons.Where(x => x.Email.EndsWith(".org")).Select(x => x.Adress.Country).Distinct().OrderBy(x=>x).ToList();
    }

    public List<Person> PersonsFromIndonesia()
    {
            return db.Persons.Where(x => x.Adress.Country == "Indonesia").OrderBy(x => x.Lastname).Skip(3).Take(4).ToList();
    }
  }
}
