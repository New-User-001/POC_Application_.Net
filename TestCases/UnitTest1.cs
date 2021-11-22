using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using ApplicationPOC;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace TestCases
{
    public class Tests
    {
        public List<object> jsonResult = new List<object>();
        public TestCaseDto testCaseDto;
        public string currentMethod;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CheckTheStringLength()
        {

            var obj = new POC();
            obj.stringAdd();
            Assert.LessOrEqual(20, obj.s3.Length);
        }

        [Test]
        public void CheckTheStringLengthPositive()
        {
            var obj = new POC();

            obj.stringAdd();
            Assert.LessOrEqual(13, obj.s3.Length);
        }

        [Test]
        public void DivideTest()
        {
            int n1 = 10, n2 = 20;
            var obj = new POC();
            Assert.Catch(delegate{ obj.Divide(n1, n2); });
        }

        [TearDown]
        public void TrackTestResultsData()
        {
            jsonResult.Add(new { TestName= TestContext.CurrentContext.Test.MethodName.ToString(), TestMethod = currentMethod,  Status = TestContext.CurrentContext.Result.Outcome.Status.ToString() });
        }

        [OneTimeTearDown]
        public void PostResults()
        {
            //Send the result data to Azure Function
            HttpClient client = new HttpClient();
            var url = "http://localhost:7071/api/TestResultEnqueue";
            var json = JsonConvert.SerializeObject(jsonResult);
            HttpResponseMessage response = client.PostAsync(url, new StringContent(json.ToString(), Encoding.UTF8, "application/json")).Result;
        }
    }
}