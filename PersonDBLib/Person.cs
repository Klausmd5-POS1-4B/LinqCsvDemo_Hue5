using System;

namespace PersonDBLib
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int AddressId { get; set; }
        public Adress Adress { get; set; }


        public override string ToString() => $"{Firstname} {Lastname} [{BirthDate:dd.MM.yyyy}]";
    }
}