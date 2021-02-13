using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    public class Joke
    {
        public string IconUrl { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Created_At { get; set; }
    }

}
