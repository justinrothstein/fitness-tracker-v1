using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FitnessTrackerV1.Models
{
    public class Routine
    {
        public int RoutineID { get; set; }
        [DisplayName("Routine Name")]
        [Required(ErrorMessage = "Routine Name is required.")]
        public string RoutineName { get; set; }
        [DisplayName("Routine Goal")]
        [Required(ErrorMessage = "Routine Goal is required.")]
        public string RoutineGoal { get; set; }
        public string UserID { get; set; }
        [DisplayName("Active Routine")]
        public bool IsActive { get; set; }

    }
}