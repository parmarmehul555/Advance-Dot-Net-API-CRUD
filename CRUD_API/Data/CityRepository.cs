using CRUD_API.Models;
using Microsoft.Data.SqlClient;

namespace CRUD_API.Data
{
    public class CityRepository
    {
        private readonly IConfiguration _configuration;

        public CityRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Select All
        public List<CityModel> SelectAll()
        {
            List<CityModel> cities = new List<CityModel>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("MyConnectionString")))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("PR_LOC_City_SelectAll", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CityModel city = new CityModel();
                    city.CityID = Convert.ToInt32(reader["CityID"]);
                    city.StateID = Convert.ToInt32(reader["StateID"]);
                    city.CountryID = Convert.ToInt32(reader["CountryID"]);
                    city.CityName = Convert.ToString(reader["CityName"]);
                    city.CityCode = Convert.ToString(reader["CityCode"]);
                    cities.Add(city);
                }
                conn.Close();
            }
            return cities;
        }
        #endregion

        #region Delete
        public bool Delete(int CityID)
        {

            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("MyConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_City_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CityID", CityID);
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;

        }
        #endregion
    }
}
