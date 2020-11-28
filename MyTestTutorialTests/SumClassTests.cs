using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyTestTutorial.Tests
{
    [TestClass()]
    public class SumClassTests
    {
        [TestMethod()]
        public void AddTest()
        {
            // Prepare test case
            double[] aArray = new double[]             { 0.0, 1.0,   2.0, -1.0,  10000000000.0 };
            double[] bArray = new double[]             { 0.0, 2.0, -20.0, -2.0, -10000000000.0 };
            double[] addedExpectedArray = new double[] { 0.0, 3.0, -18.0, -3.0,            0.0 };
            // Run test
            for (int i = 0; i < 5; i++)
            {
                double a = aArray[i];
                double b = bArray[i];
                double addedExpected = addedExpectedArray[i];
                double added = SumClass.Add(a, b);
                // Make sure that the results are expected values
                Assert.AreEqual(addedExpected, added);
            }
        }
        [TestMethod()]
        public void SumTest()
        {
            // Prepare test case
            double[] numbers = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            double summedExpected = 15.0;
            double summed = SumClass.Sum(numbers);
            Assert.AreEqual(summedExpected, summed);
        }
    }
}