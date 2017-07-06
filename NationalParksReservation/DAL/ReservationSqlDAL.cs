using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using NationalParksReservation.Models;

namespace NationalParksReservation.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;
        private const string SQL_MakeReservation = "INSERT INTO reservation (site_id, name, to_date, from_date) VALUES (@site_id, @name, @from_date, @to_date);  SELECT CAST(SCOPE_IDENTITY() as int);";
        private const string SQL_ReservationSearch = "SELECT TOP 5 c.open_from_mm, c.open_to_mm, s.site_id, s.site_number, s.max_occupancy, s.accessible, s.max_rv_length, s.utilities, c.daily_fee from campground c join site s on s.campground_id = c.campground_id where c.campground_id = @campground_id AND max_occupancy >= @max_occupancy AND accessible >= @accessible AND max_rv_length >= @max_rv_length AND utilities >= @utilities AND s.site_id NOT IN (select s.site_id from site s join campground c on c.campground_id = s.campground_id full join reservation r on r.site_id = s.site_id where c.campground_id = @campground_id AND r.from_date >= @from_date AND r.to_date <= @to_date);";
        private const string SQL_ParkReservationSearch = "SELECT TOP 5 c.name, c.open_from_mm, c.open_to_mm, s.site_id, s.site_number, s.max_occupancy, s.accessible, s.max_rv_length, s.utilities, c.daily_fee from campground c join site s on s.campground_id = c.campground_id JOIN park p ON p.park_id = c.park_id where p.park_id = @park_id AND max_occupancy >= @max_occupancy AND accessible >= @accessible AND max_rv_length >= @max_rv_length AND utilities >= @utilities AND s.site_id NOT IN (select s.site_id from site s join campground c on c.campground_id = s.campground_id full join reservation r on r.site_id = s.site_id JOIN park p ON p.park_id = c.park_id where p.park_id = @park_id AND r.from_date >= @from_date AND r.to_date <= @to_date) AND c.open_from_mm <= @open_from_mm AND c.open_to_mm >= @open_to_mm;";

        public ReservationSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int CreateReservation(int siteId, DateTime fromDate, DateTime toDate, string reservationName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_MakeReservation, connection);
                    cmd.Parameters.AddWithValue(@"site_id", siteId);
                    cmd.Parameters.AddWithValue("@name", reservationName);
                    cmd.Parameters.AddWithValue("@from_date", fromDate);
                    cmd.Parameters.AddWithValue("@to_date", toDate);

                    int reservationID = (int)cmd.ExecuteScalar();

                    return reservationID;
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public List<CampSite> FindReservation(CampSearch campSearch)
        {
            try
            {
                int accessible = 0;
                int utilities = 0;
                List<CampSite> availableSites = new List<CampSite>();


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (campSearch.IsAccessible == false)
                    {
                        accessible = 0;
                    }
                    else
                    {
                        accessible = 1;
                    }

                    if (campSearch.NeedUtilities == false)
                    {
                        utilities = 0;
                    }
                    else
                    {
                        utilities = 1;
                    }

                    SqlCommand cmd = new SqlCommand(SQL_ReservationSearch, connection);
                    cmd.Parameters.AddWithValue("@campground_id", campSearch.CampgroundId);
                    cmd.Parameters.AddWithValue("@from_date", campSearch.ArrivalDate);
                    cmd.Parameters.AddWithValue("@to_date", campSearch.DepartureDate);
                    cmd.Parameters.AddWithValue("@max_occupancy", campSearch.MaxOccupancy);
                    cmd.Parameters.AddWithValue("@accessible", accessible);
                    cmd.Parameters.AddWithValue("@max_rv_length", campSearch.RVLength);
                    cmd.Parameters.AddWithValue("@utilities", utilities);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int from = Convert.ToInt32(reader["open_from_mm"]);
                        int to = Convert.ToInt32(reader["open_to_mm"]);

                        if (campSearch.ArrivalDate.Month < from || campSearch.DepartureDate.Month > to)
                        {
                            break;
                        }
                        else if (campSearch.ArrivalDate < DateTime.Today || campSearch.DepartureDate < campSearch.ArrivalDate)
                        {
                            availableSites = null;
                            break;
                        }
                        else
                        {
                            CampSite site = new CampSite();
                            bool isAccessible = Convert.ToBoolean(reader["accessible"]);
                            string YesOrNO = site.BoolToString(isAccessible);
                            bool hasUtilities = Convert.ToBoolean(reader["utilities"]);
                            string YON = site.BoolToString(hasUtilities);

                            site.SiteId = Convert.ToInt32(reader["site_id"]);
                            site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                            site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                            site.Accessible = YesOrNO;
                            site.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                            site.Utilities = YON;
                            site.Cost = Convert.ToDouble(reader["daily_fee"]);

                            availableSites.Add(site);
                        }
                    }
                }

                return availableSites;
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public List<CampSite> FindParkReservation(ParkSearch parkSearch)
        {
            try
            {
                int accessible = 0;
                int utilities = 0;
                List<CampSite> availableSites = new List<CampSite>();


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (parkSearch.IsAccessible == false)
                    {
                        accessible = 0;
                    }
                    else
                    {
                        accessible = 1;
                    }

                    if (parkSearch.NeedUtilities == false)
                    {
                        utilities = 0;
                    }
                    else
                    {
                        utilities = 1;
                    }

                    SqlCommand cmd = new SqlCommand(SQL_ParkReservationSearch, connection);
                    cmd.Parameters.AddWithValue("@park_id", parkSearch.ParkId);
                    cmd.Parameters.AddWithValue("@from_date", parkSearch.ArrivalDate);
                    cmd.Parameters.AddWithValue("@to_date", parkSearch.DepartureDate);
                    cmd.Parameters.AddWithValue("@max_occupancy", parkSearch.MaxOccupancy);
                    cmd.Parameters.AddWithValue("@accessible", accessible);
                    cmd.Parameters.AddWithValue("@max_rv_length", parkSearch.RVLength);
                    cmd.Parameters.AddWithValue("@utilities", utilities);
                    cmd.Parameters.AddWithValue("@open_from_mm", parkSearch.ArrivalDate.Month);
                    cmd.Parameters.AddWithValue("@open_to_mm", parkSearch.ArrivalDate.Month);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (parkSearch.ArrivalDate < DateTime.Today || parkSearch.DepartureDate < parkSearch.ArrivalDate)
                        {
                            availableSites = null;
                            break;
                        }

                        CampSite site = new CampSite();
                        bool isAccessible = Convert.ToBoolean(reader["accessible"]);
                        string YesOrNO = site.BoolToString(isAccessible);
                        bool hasUtilities = Convert.ToBoolean(reader["utilities"]);
                        string YON = site.BoolToString(hasUtilities);

                        site.Campground = Convert.ToString(reader["name"]);
                        site.SiteId = Convert.ToInt32(reader["site_id"]);
                        site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        site.Accessible = YesOrNO;
                        site.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                        site.Utilities = YON;
                        site.Cost = Convert.ToDouble(reader["daily_fee"]);

                        availableSites.Add(site);

                    }
                }

                return availableSites;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
    }
}