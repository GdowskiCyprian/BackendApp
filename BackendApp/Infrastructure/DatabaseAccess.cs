using BackendApp.Controllers.ViewModels;
using BackendApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace BackendApp.Infrastructure
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly string _connectionString;

        public DatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<BusinessModel>> GetBusinessModelsAsync()
        {
            var businessModels = new List<BusinessModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetBusinessModels", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var model = new BusinessModel
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                BusinessType = reader["BusinessType"].ToString(),
                                City = reader["City"].ToString(),
                                PostalCode = reader["PostalCode"].ToString(),
                                Street = reader["Street"].ToString(),
                                BuildingNumber = reader["BuildingNumber"].ToString()
                            };
                            businessModels.Add(model);
                        }
                    }
                }
            }

            return businessModels;
        }

        public async Task<int> InsertBusinessModelAsync(BusinessViewModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_InsertBusinessModel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@BusinessType", model.BusinessType);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@PostalCode", model.PostalCode);
                    cmd.Parameters.AddWithValue("@Street", model.Street);
                    cmd.Parameters.AddWithValue("@BuildingNumber", model.BuildingNumber);

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> UpdateBusinessModelAsync(BusinessModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UpdateBusinessModel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@BusinessType", model.BusinessType);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@PostalCode", model.PostalCode);
                    cmd.Parameters.AddWithValue("@Street", model.Street);
                    cmd.Parameters.AddWithValue("@BuildingNumber", model.BuildingNumber);

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteBusinessModelAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_DeleteBusinessModel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
