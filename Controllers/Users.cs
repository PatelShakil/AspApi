using AspApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace AspApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public Users(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [HttpGet]
        [Route("GetUsers")]
        public string GetAllUsers()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("AspApiCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from users_master", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            var list = new List<UserModel>();
            if(dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt.Rows[0].Table);
            }
            else
            {
                var res = new Response();
                res.status = 404;
                res.message = "No Data Found";
                return JsonConvert.SerializeObject(res);
            }

            for (var i = 0;i < dt.Rows.Count;i++)
            {
                var user = new UserModel();
                //user.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                //user.Name = Convert.ToString(dt.Rows[i]["Name"]);
                //user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                //list.Add(user);
                
            }
            if(list.Count > 0)
            {
                //return JsonConvert.SerializeObject(list);
            }
            else
            {
                var res = new Response();
                res.status = 404;
                res.message = "No Data Found";
                //return JsonConvert.SerializeObject(res);
            }
        }
    }
}
