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
    }
}
