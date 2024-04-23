using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Proyecto____Examen_Final.Models;

namespace Proyecto____Examen_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyConnectionDBController : ControllerBase
    {
        [HttpGet]
        public IActionResult CheckDatabaseConnection()
        {
            try
            {
                if (IsDatabaseConnected())
                {
                    return Ok("Database connection is successful.");
                }
                else
                {
                    return StatusCode(500, "Database connection failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool IsDatabaseConnected()
        {
            try
            {
                using (MySqlConnection connection = ConfigDB.GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
