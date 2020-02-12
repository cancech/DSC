using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class MinusOperatorTest
    {
        [TestCase(1, 2, -1)]
        [TestCase(2222, 100, 2122)]
        [TestCase(1.235, 4.56, -3.325)]
        [TestCase(0.89, -0.89, 1.78)]
        [TestCase(0, 0, 0)]
        [TestCase(-45.20, 702.903, -748.103)]
        public void TestSubtraction(decimal lhs, decimal rhs, decimal expected)
        {
            Assert.AreEqual(expected, new MinusOperator().PerformOperation(lhs, rhs));
        }
    }
}