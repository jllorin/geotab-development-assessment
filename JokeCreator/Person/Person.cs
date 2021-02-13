using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Person
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Region { get; private set; }
    }
}
