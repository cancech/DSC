using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class MultiplyOperatorTest
    {
        [TestCase(1, 2, 2)]
        [TestCase(2222, 100, 222200)]
        [TestCase(1.235, 4.56, 5.6316)]
        [TestCase(0.89, -0.89, -0.7921)]
        [TestCase(9999, 0, 0)]
        [TestCase(-45.20, 702.903, -31771.2156)]
        public void TestMultiplication(decimal lhs, decimal rhs, decimal expected)
        {
            Assert.AreEqual(expected, new MultiplyOperator().PerformOperation(lhs, rhs));
        }
    }
}