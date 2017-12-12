using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Enrollment
    {
    public int EnrollmentID { get; set; }
    public int DanceId { get; set; }
    public int ClientId { get; set; }
    public int TeacherId { get; set; }
    public Dance Dance { get; set; }
    public Client Client { get; set; }
    public Teacher Teachers { get; set; }
  }
}
