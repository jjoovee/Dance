using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Client
    {
    //Kliento id
    public int Id { get; set; }
    //Kliento vardas
    public string Name { get; set; }
    //Kliento pavardė
    public string surname { get; set; }
    //Kliento adresas
    public string Adrress { get; set; }
    //Telefono numeris
    public string Phone { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
  }
}
