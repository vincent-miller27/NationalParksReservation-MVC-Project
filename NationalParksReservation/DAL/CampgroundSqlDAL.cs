using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using NationalParksReservation.Models;

namespace NationalParksReservation.DAL
{
    public class CampgroundSqlDAL
    {
        private string connectionString;
        private const string SQL_GetCampsites = "SELECT c.campground_id, p.name AS pname, c.name, c.open_from_mm, c.open_to_mm, c.daily_fee FROM campground c JOIN park p ON p.park_id = c.park_id WHERE c.park_id = @park_Id;";

        public CampgroundSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Campground> ListCampgrounds(int parkSelected)
        {
            List<Campground> campgroundsList = new List<Campground>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetCampsites, connection);

                    cmd.Parameters.AddWithValue("@park_id", parkSelected);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = new Campground();
                        int open = Convert.ToInt32(reader["open_from_mm"]);
                        string openMonth = c.ToMonth(open);
                        int close = Convert.ToInt32(reader["open_to_mm"]);
                        string closeMonth = c.ToMonth(close);

                        c.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        c.ParkName = Convert.ToString(reader["pname"]);
                        c.CampgroundName = Convert.ToString(reader["name"]);
                        c.OpenMonth = openMonth;
                        c.CloseMonth = closeMonth;
                        c.DailyFee = Convert.ToDouble(reader["daily_fee"]);

                        campgroundsList.Add(c);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return campgroundsList;
        }
    }
}