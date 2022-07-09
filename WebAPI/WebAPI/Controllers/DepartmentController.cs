using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string ConnectionString = _configuration.GetConnectionString("CompanyManagement");

            string query = @"select * from Department";

            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using(SqlCommand command =  new SqlCommand(query, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(DepartmentModel model)
        {
            string ConnectionString = _configuration.GetConnectionString("CompanyManagement");

            string query = @"insert into Department values ('" + model.Name + "')";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return new JsonResult("Added " + model.Name+ " department successfully.");
        }

        [HttpPut]
        public JsonResult Put(DepartmentModel model)
        {
            string ConnectionString = _configuration.GetConnectionString("CompanyManagement");

            string query = @"update Department set Name = '" + model.Name + "' where ID = " + model.Id;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return new JsonResult("Updated " + model.Name + " department successfully.");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string ConnectionString = _configuration.GetConnectionString("CompanyManagement");

            string query = @"delete Department where ID = " + id;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return new JsonResult("Delete department successfully.");
        }
    }
}
