using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using ApplicationPOC;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestCases
{
    public class Tests
    {
        public List<object> jsonResult = new List<object>();
        public TestCaseDto testCaseDto;

        [SetUp]
        public void Setup()
        {
            using(StreamReader stream = new StreamReader(@"C:\Users\CAAdmin\source\repos\ApplicationPOC\TestCases\TestCase.json"))
            {
                var testCaseJson = stream.ReadToEnd();
                testCaseDto = JsonConvert.DeserializeObject<TestCaseDto>(testCaseJson);
            }
        }

        [Test]
        public void Test1()
        {
            var obj = new POC();
            foreach(var testCase in testCaseDto.TestCases)
            {                
                var methodInfo = obj.GetType().GetMethod(testCase.MethodName);
                var paramInfo = methodInfo.GetParameters();
                object[] constructedInput = new object[paramInfo.Length];
                for(int i = 0; i<paramInfo.Length && testCase.Input.Length == paramInfo.Length; i++){
                    constructedInput[i] =  Convert.ChangeType(testCase.Input[i], paramInfo[i].ParameterType);
                }
                var testResult = obj.GetType().GetMethod(testCase.MethodName).Invoke(obj, testCase.Input);
                var testResultType = testResult.GetType();
                var output = Convert.ChangeType(testCase.ExpectedOutput, testResultType);
                try{
                    Assert.AreEqual(testResult, output);
                    jsonResult.Add(new { TestName = TestContext.CurrentContext.Test.MethodName, TestMethodName = testCase.MethodName, Status = "Pass" });
                }
                catch(Exception ec)
                {
                    jsonResult.Add(new { TestName= TestContext.CurrentContext.Test.MethodName, TestMethodName = testCase.MethodName, Status = "Fail" });
                }
            }

            HttpClient client = new HttpClient();
            var url = "http://localhost:7071/api/TestResultEnqueue";
            var json = JsonConvert.SerializeObject(jsonResult);
            HttpResponseMessage response = client.PostAsync(url, new StringContent(json.ToString(), Encoding.UTF8, "application/json")).Result;
        }

        //[Test]
        // public void Test2()
        // {
        //     var obj = new POC();
        //     var testResult = obj.stringAdd("IIHT", "Techademy");
        //     Assert.AreEqual(testResult, "IIHTTechademy");
        // }

        // [TearDown]
        // public void TrackTestResultsData()
        // {
        //     jsonResult.Add(new { TestName= TestContext.CurrentContext.Result.Outcome.Status.ToString() });
        // }

        // [OneTimeTearDown]
        // public void PostResults()
        // {
        //     //Send the result data to Azure Function
        //     HttpClient client = new HttpClient();
        //     var url = "http://localhost:7071/api/TestResultEnqueue";
        //     var json = JsonConvert.SerializeObject(jsonResult);
        //     HttpResponseMessage response = client.PostAsync(url, new StringContent(json.ToString(), Encoding.UTF8, "application/json")).Result;
        // }
    }
}