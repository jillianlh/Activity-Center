using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using CSBelt.Models;

namespace CSBelt.Models
{
   public class Association
   {
      [Key]
      public int AssociationId {get;set;}

      public int UserId {get;set;}
      public int PlanId {get;set;}

      public User Planner {get;set;}
      public Plan Plan {get;set;}
   }
}