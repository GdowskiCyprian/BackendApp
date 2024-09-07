using BackendApp.Controllers.ViewModels;
using BackendApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace BackendApp.Infrastructure
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly string _connectionString;
        private readonly ILogger<DatabaseAccess> _logger;

        public DatabaseAccess(string connectionString, ILogger<DatabaseAccess> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<List<BusinessModel>> GetBusinessModelsAsync(int offset)
        {
            var businessModels = new List<BusinessModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetBusinessModels", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@offset", offset);

                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Guid result = new Guid();
                                var model = new BusinessModel
                                {
                                    Id = int.Parse(reader["Id"].ToString()),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    BusinessType = reader["BusinessType"].ToString(),
                                    City = reader["City"].ToString(),
                                    PostalCode = reader["PostalCode"].ToString(),
                                    Street = reader["Street"].ToString(),
                                    BuildingNumber = reader["BuildingNumber"].ToString(),
                                    OwnerId = Guid.TryParse(reader["OwnerId"].ToString(), out result) ? result : result
                                };
                                businessModels.Add(model);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.Log(LogLevel.Error, $"There was error executing GetBusinessModelsAsync method on database: {ex.Message}");
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
                    cmd.Parameters.AddWithValue("@OwnerId", model.OwnerId);

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
                    cmd.Parameters.AddWithValue("@OwnerId", model.OwnerId);

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

        public async Task<List<VisitModel>> GetVisitsAsync(int offset)
        {
            var visits = new List<VisitModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetVisits", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@offset", offset);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var visit = new VisitModel
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                UserId = Guid.Parse(reader["UserId"].ToString()),
                                BusinessId = int.Parse(reader["BusinessId"].ToString()),
                                VisitTime = DateTime.Parse(reader["VisitTime"].ToString()),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                IsConfirmed = bool.Parse(reader["IsConfirmed"].ToString())
                            };
                            visits.Add(visit);
                        }
                    }
                }
            }

            return visits;
        }

        public async Task<int> InsertVisitAsync(VisitViewModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_InsertVisit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@BusinessId", model.BusinessId);
                    cmd.Parameters.AddWithValue("@VisitTime", model.VisitTime);
                    cmd.Parameters.AddWithValue("@Email", model.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsConfirmed", model.IsConfirmed);

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> UpdateVisitAsync(VisitModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UpdateVisit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@BusinessId", model.BusinessId);
                    cmd.Parameters.AddWithValue("@VisitTime", model.VisitTime);
                    cmd.Parameters.AddWithValue("@Email", model.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsConfirmed", model.IsConfirmed);

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteVisitAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_DeleteVisit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<VisitModel>> GetVisitsByDateRangeAsync(DateTime dateFrom, DateTime dateTo, int offset)
        {
            var visits = new List<VisitModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetVisitsByDateRange", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
                    cmd.Parameters.AddWithValue("@DateTo", dateTo);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var visit = new VisitModel
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                UserId = Guid.Parse(reader["UserId"].ToString()),
                                BusinessId = int.Parse(reader["BusinessId"].ToString()),
                                VisitTime = DateTime.Parse(reader["VisitTime"].ToString()),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                IsConfirmed = bool.Parse(reader["IsConfirmed"].ToString())
                            };
                            visits.Add(visit);
                        }
                    }
                }
            }

            return visits;
        }
    }
}
