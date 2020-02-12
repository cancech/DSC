using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class DummyOperatorTest
    {
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(-123, -459)]
        [TestCase(0.3493, 0.1943)]
        [TestCase(-0.275, -573.8573)]
        public void TestOperation(decimal lhs, decimal rhs)
        {
            DummyOperator op = new DummyOperator();
            Assert.AreEqual(rhs, op.PerformOperation(lhs, rhs));
        }
    }
}
