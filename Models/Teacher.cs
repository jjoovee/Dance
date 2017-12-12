using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Teacher
    {
    //Mokytojo id
    public int Id { get; set; }
    //Mokytojo vardas
    public string Name { get; set; }
    //Mokytojo pavardė
    public string Surname { get; set; }
    //Mokytojo vedamas užsiėmimas
    public ICollection<Enrollment> Enrollments { get; set; }
    //Telefono numeris
    public string Phone { get; set; }
  }
}
