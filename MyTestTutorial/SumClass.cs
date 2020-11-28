using System.Linq;

namespace MyTestTutorial
{
    public class SumClass
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }
        public static double Sum(double[] numbers)
        {
            return numbers.Sum();
        }
    }
}
