using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using CSBelt.Models;

namespace CSBelt.Models
{
   public class MyContext : DbContext
   {
      public MyContext(DbContextOptions options) : base(options) {}
      public DbSet<User> Users {get;set;}
      public DbSet<Plan> Plans {get;set;}
      public DbSet<Association> Associations {get;set;}

      public void Create(Plan plan)
      {
         Plans.Add(plan);
         SaveChanges();
      }

      public void Delete(int planid)
      {
         Plan PlanToRemove = Plans.FirstOrDefault(w => w.PlanId == planid);
         Plans.Remove(PlanToRemove);
         SaveChanges();
      }

      public void Join(Association association)
      {
         Associations.Add(association);
         SaveChanges();
      }

      public void Leave(Association association)
      {
         Associations.Remove(association);
         SaveChanges();
      }
   }
}