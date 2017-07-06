using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using NationalParksReservation.Models;

namespace NationalParksReservation.DAL
{
    public class ParkSearchSqlDAL
    {
        private string connectionString;
        private const string SQL_PopulateSiteInfo = "SELECT name FROM park WHERE park_id = @park_id;";

        public ParkSearchSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ParkSearch RetrieveInfo(int id)
        {
            try
            {
                ParkSearch p = new ParkSearch();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_PopulateSiteInfo, connection);

                    cmd.Parameters.AddWithValue("@park_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground cg = new Campground();
                        p.ParkId = id;
                        p.ParkName = Convert.ToString(reader["name"]);
                    }

                    return p;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}