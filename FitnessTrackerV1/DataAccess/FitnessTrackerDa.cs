using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FitnessTrackerV1.Models;

namespace FitnessTrackerV1.DataAccess
{
    public class FitnessTrackerDa
    {

        public List<Routine> FindRoutines(string emailAddress)
        {
            List<Routine> routines = new List<Routine>();

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FitnessTracker"].ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Routine_1 WHERE UserID= '" + emailAddress + "'", con);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Routine routine = new Routine()
                        {
                            RoutineID = (int)reader.GetValue(reader.GetOrdinal("RoutineID")),
                            RoutineName = (string)reader.GetValue(reader.GetOrdinal("RoutineName")),
                            RoutineGoal = (string)reader.GetValue(reader.GetOrdinal("RoutineGoal")),
                            UserID = emailAddress
                        };

                        routines.Add(routine);
                    }
                }
            }
            return routines;
        }
    }
}