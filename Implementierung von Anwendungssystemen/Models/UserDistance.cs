using System;
using System.Collections.Generic;
using System.Text;

namespace Implementierung_von_Anwendungssystemen.Models
{
    public class UserDistance
    {

        public int DistanceId { get; set; }
        public double Distance { get; set; }
        public string TypeOfSport { get; set; }
        //[RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Format must be VW:XY")]
        public string DayTime { get; set; }
        public int Duration { get; set; }
        public double AverageSpeed { get; set; }
        public int CaloriesBurned { get; set; }

        //[ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }

    }
}
