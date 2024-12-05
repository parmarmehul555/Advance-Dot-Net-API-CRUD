using CRUD_API.Models;
using Microsoft.Data.SqlClient;

namespace CRUD_API.Data
{
    public class StateRepository
    {
        private readonly IConfiguration _configuration;

        public StateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<StateModel> SelectAll()
        {
            List<StateModel> states = new List<StateModel>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("MyConnectionString")))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("PR_LOC_State_SelectAll", conn); ;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StateModel state = new StateModel();
                    state.StateID = Convert.ToInt32(reader["StateID"]);
                    state.CountryID = Convert.ToInt32(reader["CountryID"]);
                    state.StateName = reader["StateName"].ToString();
                    state.StateCode = reader["StateCode"].ToString();
                    states.Add(state);
                }
                conn.Close();
            }
            return states;
        }

        #region Delete State
        public bool Delete(int StateID)
        {
            try
            {
                string str = _configuration.GetConnectionString("MyConnectionString");
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("PR_LOC_State_Delete", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("StateID", StateID);
                    if (Convert.ToBoolean(cmd.ExecuteNonQuery))
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
    }
}
