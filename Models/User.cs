using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using CSBelt.Models;

namespace CSBelt.Models
{
   public class User
   {
      [Key]
      public int UserId {get;set;}

      public List<Association> JoinedPlans {get;set;}

      [Required(ErrorMessage="Hey your first name is required, thank you so much")]
      [MinLength(2, ErrorMessage="Your name must be 2 characters or more, thank you so much")]
      public string FirstName {get;set;}

      [Required(ErrorMessage="Hey your last name is required, thank you so much")]
      [MinLength(2, ErrorMessage="Your name must be 2 characters or more, thank you so much")]
      public string LastName {get;set;}

      [Required(ErrorMessage="Hey your email is required, thank you so much")]
      [EmailAddress(ErrorMessage="Hey that's invalid, try again, thank you so much")]
      public string Email {get;set;}

      [DataType(DataType.Password)]
      [Required(ErrorMessage="Hey your password is required, thank you so much")]
      [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password must meet requirements. Requirements must have at least: 1 letter, 1 number, 1 special character.")]
      [MinLength(8, ErrorMessage="Hey your password must be 8 characters or longer, thank you so much")]
      public string Password {get;set;}

      [NotMapped]
      [DataType(DataType.Password)]
      [Compare("Password", ErrorMessage="Hey your passwords must match, thank you so much")]
      public string Confirm {get;set;}

      public DateTime CreatedAt {get;set;} = DateTime.Now;
      public DateTime UpdatedAt {get;set;} = DateTime.Now;
   }
}