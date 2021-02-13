using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    public class Option
    {
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Category { get; set; }
        [Required]
        [Range(1, 9, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int NoOfJokes { get; set; }
        // Reason for this is that you have a firstname and lastname validation in your code
        // otherwise will have it just name
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ReplaceFirstNameWith { get; set; }
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ReplaceLastNameWith { get; set; }
    }
}
