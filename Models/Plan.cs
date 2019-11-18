using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using CSBelt.Models;

namespace CSBelt.Models
{
   public class Plan
   {
      [Key]
      public int PlanId {get;set;}

      public int PlannerId {get;set;}
      public string PlannerName {get;set;}

      public List<Association> Participants {get;set;}

      [Required(ErrorMessage="Hey this requires a title, thank you so much")]
      public string Title {get;set;}

      [Required(ErrorMessage="Hey you have to choose a date, thank you so much")]
      public DateTime Date {get;set;}

      [Required(ErrorMessage="Hey this has to go on for at least a minute, thank you so much")]
      public int Duration {get;set;}

      [Required(ErrorMessage="Are we talking hours? Minutes? What is this?")]
      public string DurationHour {get;set;}

      [Required(ErrorMessage="Hey you have to tell us what we're doing here, thank you so much")]
      public string Description {get;set;}

      public DateTime CreatedAt {get;set;} = DateTime.Now;
      public DateTime UpdatedAt {get;set;} = DateTime.Now;
   }
}