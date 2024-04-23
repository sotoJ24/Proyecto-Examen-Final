using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using Proyecto____Examen_Final.Models;
using Mysqlx.Prepare;
using Google.Protobuf.WellKnownTypes;

namespace Proyecto____Examen_Final.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser(string session, string UserName, string Email)
        {
            try
            {
                // Validate session
                int sessionId = ValidateSession(session);
                if (sessionId == -1)
                {
                    return Unauthorized("Invalid session");
                }

                // Validate user input
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email))
                {
                    return BadRequest("Name and Email are required.");
                }

                // Save user to database
                InsertUser(session, UserName, Email);

                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex.Message}");
            }
        }

        // Method to validate session and return SessionId
        private int ValidateSession(int session)
        {
            try
            {
                using (MySqlConnection connection = ConfigDB.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT SessionId FROM SessionTablePJD WHERE SessionToken = @SessionToken";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@SessionToken", session);

                        object result = cmd.ExecuteScalar();

                        // If the result is not null, return SessionId
                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            // Invalid session
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating session: {ex.Message}");
            }
        }


        // Method to insert user into database
        private void InsertUser(string session, string UserName, string Email)
        {
            try
            {
                int SessionId = GetSessionID(session);

                using (MySqlConnection connection = ConfigDB.GetConnection())
                {
                    connection.Open();

                    string sql = "INSERT INTO UsersPJD (UserName, Email, SessionId) VALUES (@UserName, @Email, @SessionId)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@SessionId", SessionId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting user: {ex.Message}");
            }
        }

        private int GetSessionID(string session)
        {
            try
            {
                using (MySqlConnection connection = ConfigDB.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT SessionId FROM SessionTablePJD WHERE SessionToken = @SessionToken";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@SessionToken", session);

                        //Executes the query and stores the result in a variable
                        object result = cmd.ExecuteScalar();

                        // If the result is not null, convert it to whole and return it
                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            //Handling a case in which a valid session is not found
                            //return a default value or throw an exception, depending on your preference
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // handling errors 
                throw new Exception($"Error retrieving session ID: {ex.Message}");
            }
        }




    }
}
