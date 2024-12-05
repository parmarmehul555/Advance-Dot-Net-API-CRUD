using CRUD_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace CRUD_API.Data
{
    public class CountryRepository
    {
        private readonly IConfiguration _configuration;

        public CountryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Select All Country
        public List<CountryModel> SelectAllCountries()
        {
            List<CountryModel> countries = new List<CountryModel>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("MyConnectionString")))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_Country_SelectAll", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CountryModel country = new CountryModel();
                    country.CountryID = Convert.ToInt32(reader["CountryID"]);
                    country.CountryName = reader["CountryName"].ToString();
                    country.CountryCode = reader["CountryCode"].ToString();
                    countries.Add(country);
                }
                conn.Close();
            }
            return countries;
        }
        #endregion

        #region Delete Country
        public bool DeleteCountry(int CountryID)
        {
            try
            {
                String str = _configuration.GetConnectionString("MyConnectionString");
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("PR_LOC_Country_Delete", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CountryID", CountryID);
                    if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Insert Country
        public bool Insert(CountryModel modelCountry)
        {
            try
            {
                string str = _configuration.GetConnectionString("MyConnectionString");
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("PR_LOC_COUNTRY_INSERT", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CountryName", modelCountry.CountryName);
                    cmd.Parameters.AddWithValue("CountryCode", modelCountry.CountryCode);
                    if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Update Country
        public bool Update(CountryModel modelCountry)
        {
            try
            {
                string str = _configuration.GetConnectionString("MyConnectionString");
                using(SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("PR_LOC_COUNTRY_UPDATE", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CountryID", modelCountry.CountryID);
                    cmd.Parameters.AddWithValue("CountryName", modelCountry.CountryName);
                    cmd.Parameters.AddWithValue("CountryCode", modelCountry.CountryCode);
                    if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
