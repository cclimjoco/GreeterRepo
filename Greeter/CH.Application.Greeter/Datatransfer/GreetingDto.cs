
using System.ComponentModel.DataAnnotations;

namespace CH.Application.Greeter.Datatransfer
{
   
    public class Greeting
    {
        [Required]
        public int ID { get; set; }
        [StringLength(250, ErrorMessage = "Message cannot be longer than 40 characters.")]
        public string Message { get; set; }
        [Required]
        public string Language { get; set; }
     
    }
}
