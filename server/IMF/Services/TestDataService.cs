using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Filter = System.Collections.Generic.Dictionary<string, string[]>;
// using IMF.Data.Models;

using System.Data;
using System.Data.OleDb;

namespace IMF.Services
{
    public class TestDataService
    {
        public IEnumerable<object> GetTestData(string database, IEnumerable<string> observations)
        {
            var response = new List<object>();

            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={database};" +
                $"User Id=admin;Password=;";

            var obs = String.Join(",", observations);

            string queryString =
                $"SELECT {obs} from tbl_TSeries17; ";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                OleDbCommand command = new OleDbCommand(queryString, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var results = new object[reader.VisibleFieldCount];
                        for(var i = 0; i < reader.VisibleFieldCount; i++)
                        {
                            results[i] = reader[i];
                        }
                        response.Add(results);
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

        //public IEnumerable<TestData> GetTestData(Filter filters)
        //{
        //    var testData = new List<TestData>
        //    {
        //        new TestData
        //        {
        //            SeriesCode = "911_BCAGDP",
        //            YearsValues = new Dictionary<int, double>
        //            {
        //                [2000] = -8.61566208013634,
        //                [2001] = -4.64323011949904,
        //                [2002] = -3.54548016150178,
        //                [2003] = -4.59630162680908,
        //                [2004] = -8.38767511409758
        //            }
        //        },
        //        new TestData
        //        {
        //            SeriesCode = "911BCAXGT_GDP",
        //            YearsValues = new Dictionary<int, double>
        //            {
        //                [2000] = -13.227046316204,
        //                [2001] = -14.9176186215916,
        //                [2002] = -17.4755131233134,
        //                [2003] = -21.1028319146805,
        //                [2004] = -25.759376678149
        //            }
        //        }
        //    };

        //    var testData1 = new TestData1
        //    {
        //        ["911_BCAGDP"] = new Dictionary<int, double>
        //        {
        //            [2000] = -8.61566208013634,
        //            [2001] = -4.64323011949904,
        //            [2002] = -3.54548016150178,
        //            [2003] = -4.59630162680908,
        //            [2004] = -8.38767511409758
        //        },
        //        ["911BCAXGT_GDP"] = new Dictionary<int, double>
        //        {
        //            [2000] = -13.227046316204,
        //            [2001] = -14.9176186215916,
        //            [2002] = -17.4755131233134,
        //            [2003] = -21.1028319146805,
        //            [2004] = -25.759376678149
        //        }
        //    };

        //    if (filters != null)
        //    {

        //        foreach (var filter in filters.Keys)
        //        {
        //            foreach (var obs in testData1[filter].Keys)
        //            {
        //                var test = testData1[filter][obs];
        //            }
        //        }
        //    }

        //    return testData;
        //}
    }
}
