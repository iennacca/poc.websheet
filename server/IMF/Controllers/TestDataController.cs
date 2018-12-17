using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Filter = System.Collections.Generic.Dictionary<string, string>;

//using IMF.Data.Models;
using IMF.Services;
using Newtonsoft.Json;

namespace IMF.Controllers
{
    [ApiController, Route("api/TestData")]
    public class TestDataController : ControllerBase
    {
        private readonly TestDataService testDataService;
        public TestDataController(TestDataService testDataSvc)
        {
            this.testDataService = testDataSvc;
        }

        // default route api/testdata/ get all data
        [HttpGet]
        public ActionResult<IEnumerable<object>> Get(/*[FromQuery]Filter filters*/ string database, string observations)
        {
            return Ok(testDataService.GetTestData(database, observations.Split(',')) );
        }
    }
}
