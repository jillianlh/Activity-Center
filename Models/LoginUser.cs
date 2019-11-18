using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSBelt.Models
{
   public class LoginUser
   {
      [Required(ErrorMessage="Hey your email is required, thank you so much")]
      [EmailAddress(ErrorMessage="Hey that's invalid, try again, thank you so much")]
      public string LoginEmail {get;set;}

      [DataType(DataType.Password)]
      [Required(ErrorMessage="Hey your password is required, thank you so much")]
      public string LoginPassword {get;set;}
   }

}
