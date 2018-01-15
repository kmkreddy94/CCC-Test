using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Exam;

namespace TestProject
{
    [TestClass]
    public class State
    {

        private HttpResponseMessage HttpCall()
        {
            var client = new HttpClient(); // no HttpServer

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://services.groupkt.com/state/get/USA/all"),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client.SendAsync(request).Result;
        }

        [TestMethod]
        public void ServiceISUpAndRunning()
        {
            using (var response = HttpCall())
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public void SearchCapitalWithAbbreviation()
        {
            Country country = new Country();
            var output = country.GetStateDetailsAsync("http://services.groupkt.com/state/get/USA/all", "AL").GetAwaiter().GetResult();

            Assert.AreEqual(output.capital, "Montgomery");
            Assert.AreEqual(output.largest_city, "Birmingham");
        }

        [TestMethod]
        public void SearchCapitalWithName()
        {
            Country country = new Country();
            var output = country.GetStateDetailsAsync("http://services.groupkt.com/state/get/USA/all", "Alabama").GetAwaiter().GetResult();

            Assert.AreEqual(output.capital, "Montgomery");
            Assert.AreEqual(output.largest_city, "Birmingham");
        }

        //Negative Test Case
        [TestMethod]
        public void SearchCapitalWithInCorrectAbbreviation()
        {
            Country country = new Country();
            var output = country.GetStateDetailsAsync("http://services.groupkt.com/state/get/USA/all", "XYZ").GetAwaiter().GetResult();

            Assert.AreEqual(output, null);
        }

        //Negative Test Case
        [TestMethod]
        public void SearchCapitalWithInCorrectName()
        {
            Country country = new Country();
            var output = country.GetStateDetailsAsync("http://services.groupkt.com/state/get/USA/all", "Test").GetAwaiter().GetResult();

            Assert.AreEqual(output, null);
        }


        //Negative Test Case
        [TestMethod]
        public void SearchCapitalWithEmptyName()
        {
            Country country = new Country();
            var output = country.GetStateDetailsAsync("http://services.groupkt.com/state/get/USA/all", "").GetAwaiter().GetResult();

            Assert.AreEqual(output, null);
        }
    }
}
