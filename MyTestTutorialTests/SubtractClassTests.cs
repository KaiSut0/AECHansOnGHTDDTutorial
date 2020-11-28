using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTestTutorial;

namespace MyTestTutorialTests
{
    [TestClass]
    public class SubtractClassTests
    {
        [TestMethod]
        public void SubtractTest()
        {
            // Prepare test case
            double[] aArray = new double[] { 2.0, 3.0 };
            double[] bArray = new double[] { 1.0, 2.0 };
            double[] subtructExpectedArray = new double[] { 1.0, 1.0 };
            for(int i = 0; i < 2; i++)
            {
                double a = aArray[i];
                double b = bArray[i];
                double subtructExpected = subtructExpectedArray[i];
                Assert.AreEqual(SubtractClass.Subtract(a, b), subtructExpected);
            }
        }
    }
}
