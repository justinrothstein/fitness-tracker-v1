using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FitnessTrackerV1.Models
{
    public class Routine
    {
        public int RoutineID { get; set; }
        [DisplayName("Routine")]
        public string RoutineName { get; set; }
        [DisplayName("Goal")]
        public string RoutineGoal { get; set; }
        public string UserID { get; set; }

    }
}