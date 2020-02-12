using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class AddOperatorTest
    {
        [TestCase(1, 2, 3)]
        [TestCase(100, 2222, 2322)]
        [TestCase(1.235, 4.56, 5.795)]
        [TestCase(0.89, -0.89, 0)]
        [TestCase(0, 0, 0)]
        [TestCase(-45.20, -702.903, -748.103)]
        public void TestAddition(decimal lhs, decimal rhs, decimal expected)
        {
            Assert.AreEqual(expected, new AddOperator().PerformOperation(lhs, rhs));
        }
    }
}
