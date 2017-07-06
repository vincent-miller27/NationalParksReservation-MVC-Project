using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using NationalParksReservation.Models;

namespace NationalParksReservation.DAL
{
    public class CampSiteSqlDAL
    {
        private string connectionString;
        private const string SQL_PopulateSiteInfo = "SELECT name, open_from_mm, open_to_mm, daily_fee FROM campground WHERE campground_id = @campground_id;";

        public CampSiteSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public CampSearch RetrieveInfo(int id)
        {
            try
            {
                CampSearch c = new CampSearch();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_PopulateSiteInfo, connection);

                    cmd.Parameters.AddWithValue("@campground_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground cg = new Campground();
                        int open = Convert.ToInt32(reader["open_from_mm"]);
                        string openMonth = cg.ToMonth(open);
                        int close = Convert.ToInt32(reader["open_to_mm"]);
                        string closeMonth = cg.ToMonth(close);

                        c.CampgroundId = id;
                        c.CampgroundName = Convert.ToString(reader["name"]);
                        c.OpenMonth = openMonth;
                        c.OpenMonthNumber = open;
                        c.CloseMonth = closeMonth;
                        c.CloseMonthNumber = close;
                        c.DailyFee = Convert.ToDouble(reader["daily_fee"]);
                    }

                    return c;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}