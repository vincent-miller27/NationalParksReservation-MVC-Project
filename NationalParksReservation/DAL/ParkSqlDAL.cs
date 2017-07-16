using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using NationalParksReservation.Models;

namespace NationalParksReservation.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;
        private const string SQL_ParkList = "SELECT * FROM park ORDER BY name ASC;";
        private const string SQL_GetParkInfo = "SELECT * FROM park WHERE park_id = @park_id;";

        public ParkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> output = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_ParkList, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();

                        p.ParkId = Convert.ToInt32(reader["park_id"]);
                        p.ParkName = Convert.ToString(reader["name"]);
                        p.ParkLocation = Convert.ToString(reader["location"]);
                        p.DateEstablished = Convert.ToDateTime(reader["establish_date"]);
                        p.ParkAreaSqAcres = Convert.ToInt32(reader["area"]);
                        p.AnnualParkVisitorCount = Convert.ToInt32(reader["visitors"]);
                        p.ParkDescription = Convert.ToString(reader["description"]);

                        output.Add(p);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return output;
        }

        public Park GenerateParkName(int parkId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkInfo, connection);
                    cmd.Parameters.AddWithValue("@park_id", parkId);

                    Park p = new Park();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        p.ParkId = Convert.ToInt32(reader["park_id"]);
                        p.ParkName = Convert.ToString(reader["name"]);
                        p.ParkLocation = Convert.ToString(reader["location"]);
                        p.DateEstablished = Convert.ToDateTime(reader["establish_date"]);
                        p.ParkAreaSqAcres = Convert.ToInt32(reader["area"]);
                        p.AnnualParkVisitorCount = Convert.ToInt32(reader["visitors"]);
                        p.ParkDescription = Convert.ToString(reader["description"]);
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