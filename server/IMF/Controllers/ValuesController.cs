using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.OleDb;

namespace IMF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            string connectionString =
        "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
            + @"C:\projects\IMF\sample.dmx;User Id=admin;Password=;";

            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT TOP 35 OName, OValue17  from tbl_TSeries17 "
                    + "WHERE OName < 13 "
                    + ";";

            // Specify the parameter value.
            //int paramValue = 5;

            var response = new List<object[]>();

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (OleDbConnection connection =
                new OleDbConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                OleDbCommand command = new OleDbCommand(queryString, connection);
                // command.Parameters.AddWithValue("@OName", paramValue);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //string companyCode = reader.GetValue(0).ToString();
                        //string agentId = reader.GetString(1);

                        var row = new object[] { reader[0], reader[1] };

                        response.Add(row);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return response;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
