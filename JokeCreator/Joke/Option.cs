using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    public class Option
    {
        [Required]
        public string Category { get; set; }
        [Required]
        public int NoOfJokes { get; set; }
        // Reason for this is that you have a firstname and lastname validation in your code
        // otherwise will have it just name
        [Required]
        public string ReplaceFirstNameWith { get; set; }
        [Required]
        public string ReplaceLastNameWith { get; set; }
    }
}
