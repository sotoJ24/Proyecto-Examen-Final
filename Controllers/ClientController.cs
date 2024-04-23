using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Proyecto____Examen_Final.Models;

namespace Proyecto____Examen_Final.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController] 
    public class ClientController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllClients()
        {
            try
            {
                List<ClientModel> clients = new List<ClientModel>();

                using (MySqlConnection connection = ConfigDB.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT * FROM ClientsPJD";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientModel client = new ClientModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ClientName = reader["ClientName"].ToString(),
                                    ClientLastName = reader["ClientLastName"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    ClientKey = reader["ClientKey"].ToString()
                                };

                                clients.Add(client);
                            }
                        }
                    }
                }

                return Ok(clients);
            }
            catch (Exception ex)
            {
              return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpPost("{clientId}")]
        public IActionResult CreateClientSession(int clientId)
        {
            try
            {
                if (!ValidateClient(clientId))
                {
                    return Unauthorized();
                }

                string sessionId = GenerateSession();

                StoreOrUpdateSessionInTable(clientId, sessionId);

             
                return Ok(sessionId);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    
        private bool ValidateClient(string clientId)
        {
          
            using (MySqlConnection connection = ConfigDB.GetConnection()) 
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM ClientKeysPJD WHERE ClientId = @ClientId";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private string GenerateSession()
        {
          
            return Guid.NewGuid().ToString();
        }

        private void StoreOrUpdateSessionInTable(int clientId, string sessionId)
        {
            using (MySqlConnection connection = ConfigDB.GetConnection())
            {
                connection.Open();

                string selectSql = "SELECT COUNT(*) FROM SessionTablePJD WHERE ClientId = @ClientId";
                using (MySqlCommand selectCmd = new MySqlCommand(selectSql, connection))
                {
                    selectCmd.Parameters.AddWithValue("@ClientId", clientId);
                    int count = Convert.ToInt32(selectCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // ClientId exists, update the record
                        string updateSql = "UPDATE SessionTablePJD SET SessionToken = @SessionToken WHERE ClientId = @ClientId";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateSql, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@SessionToken", sessionId);
                            updateCmd.Parameters.AddWithValue("@ClientId", clientId);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // ClientId doesn't exist, insert a new record
                        string insertSql = "INSERT INTO SessionTablePJD (ClientId, SessionToken) VALUES (@ClientId, @SessionToken)";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertSql, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@ClientId", clientId);
                            insertCmd.Parameters.AddWithValue("@SessionToken", sessionId);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

    }
}
