using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Data
{
  public static class DbInitializer
  {
    public static void Initialize(ApplicationDbContext context)
    {
      context.Database.EnsureCreated();

      // Look for any students.
      if (context.Clients.Any())
      {
        return;   // DB has been seeded
      }

      var clients = new Client[]
      {

            new Client{Name="Carson",surname="Alexander", Adrress="Kaunas, Studentu g. 69", Phone="865265411"},
            new Client{Name="Peter",surname="Alexander", Adrress="Kaunas, Petraburgo g. 9", Phone="865296451"},
            new Client{Name="Sandra",surname="Bu", Adrress="Kaunas, Gedimino g. 60", Phone="865265623"}
      };
      foreach (Client s in clients)
      {
        context.Clients.Add(s);
      }
      context.SaveChanges();

      var teachers = new Teacher[]
      {

            new Teacher{Name="Sandi",Surname="Leteta", Phone="8641532"},
            new Teacher{Name="Laura",Surname="Favelo", Phone="5120532"},
            new Teacher{Name="Linda",Surname="Masiozo", Phone="5631206"},
            new Teacher{Name="Fera",Surname="Fernendento", Phone="461532"}
      };
      foreach (Teacher s in teachers)
      {
        context.Teachers.Add(s);
      }
      context.SaveChanges();

      var dances = new Dance[]
      {
            new Dance{DanceId=1050,Title="Salsa",Price=30},
            new Dance{DanceId=4022,Title="Lombada",Price=30},
            new Dance{DanceId=4041,Title="Valsas",Price=30},
            new Dance{DanceId=1045,Title="Tango",Price=40}
      };
      foreach (Dance c in dances)
      {
        context.Dances.Add(c);
      }
      context.SaveChanges();

      var enrollments = new Enrollment[]
      {
            new Enrollment{ClientId=1,DanceId=1050, TeacherId = 1},
            new Enrollment{ClientId=1,DanceId=4022, TeacherId = 2},
            new Enrollment{ClientId=1,DanceId=4041, TeacherId = 3},
            new Enrollment{ClientId=2,DanceId=1050, TeacherId = 1},
            new Enrollment{ClientId=2,DanceId=4041, TeacherId = 3},
            new Enrollment{ClientId=2,DanceId=1045, TeacherId = 4},
            new Enrollment{ClientId=3,DanceId=1050, TeacherId = 1}

      };
      foreach (Enrollment e in enrollments)
      {
        context.Enrollments.Add(e);
      }
      context.SaveChanges();
    }
  }
}