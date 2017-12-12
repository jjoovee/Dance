using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Dance
  {
    
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int DanceId { get; set; }
    public string Title { get; set; }
    public int Price { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
  }
}
