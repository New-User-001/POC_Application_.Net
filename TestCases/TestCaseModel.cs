using System.Collections.Generic;

namespace TestCases{
    public class TestCaseDto
    {
        public List<TestCase> TestCases { get; set; }
    }

    public class TestCase
    {
        public string MethodName { get; set; }
        public object[] Input { get; set; }
        public string ExpectedOutput { get; set; }
    }
}