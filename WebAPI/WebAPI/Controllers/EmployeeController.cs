using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string ConnectionString = _configuration.GetConnectionString("CompanyManagement");

            string query = @"select 
                                 e.ID,
                                 e.Name,
                                 d.Name as Department,
                                 convert(varchar(10),e.DateOfJoining,120) as DateOfJoining,
                                 e.PhotoFileName
                             from Employee e 
                             left join Department d on e.DepartmentID = d.ID";

            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(EmployeeModel model)
        {
            string ConnectionString = _configuration.GetConnectionString("CompanyManagement");

            string query = @"insert into Employee (Name, DepartmentID, DateOfJoining, PhotoFileName)
                             values ('" + model.Name + "', " + model.DeparmentID + ", '" + model.DateOfJoining + "','" + model.PhotoFileName + "')";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return new JsonResult("Added " + model.Name + " employee successfully.");
        }

        [HttpPut]
        public JsonResult Put(EmployeeModel model)
        {
            string ConnectionString = _configuration.GetConnectionString("CompanyManagement");

            string query = @"update Employee 
                             set Name = '" + model.Name + @"',
                                 DepartmentID = " + model.Id + @",
                                 DateOfJoining = '" + model.DateOfJoining + @"',
                                 PhotoFileName = '" + model.PhotoFileName + @"'
                             where ID = " + model.Id;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return new JsonResult("Updated " + model.Name + " employee successfully.");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string ConnectionString = _configuration.GetConnectionString("CompanyManagement");

            string query = @"delete Employee where ID = " + id;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return new JsonResult("Delete employee successfully.");
        }
    }
}
