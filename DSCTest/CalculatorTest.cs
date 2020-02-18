using NUnit.Framework;
using Moq;
using DSC;
using System;

namespace DSCTest
{
    [TestFixture]
    public class CalculatorTest
    {
        private Calculator calc;

        private Mock<IOperator> mockOperator;

        [SetUp]
        public void Setup()
        {
            mockOperator = new Mock<IOperator>();

            calc = new Calculator();
        }

        [Test]
        public void TestEqualsPressedNoNumberInput()
        {
            calc.PerformOperation();
            Assert.AreEqual(0, calc.Number.ValueDecimal());
        }

        [TestCase(1)]
        [TestCase(0.123)]
        [TestCase(-0.0023)]
        [TestCase(999)]
        [TestCase(9.0015838593865720)]
        [TestCase(30680)]
        public void TestEqualsPressedNumberInput(decimal val)
        {
            calc.Number.OverrideValue(val);
            calc.PerformOperation();
            Assert.AreEqual(val, calc.Number.ValueDecimal());
        }

        [TestCase(0, 0, 123)]
        [TestCase(0.123, -0.0023, 321)]
        [TestCase(3839, 65739, 111)]
        [TestCase(-493, -45, -987654)]
        public void TestOperator(decimal lhs, decimal rhs, decimal retValue)
        {
            mockOperator.Setup(m => m.PerformOperation(lhs, rhs)).Returns(retValue);

            calc.Number.OverrideValue(lhs);
            calc.SetOperator(mockOperator.Object);
            calc.Number.OverrideValue(rhs);
            calc.PerformOperation();

            mockOperator.Verify(m => m.PerformOperation(lhs, rhs));
            Assert.AreEqual(retValue, calc.Number.ValueDecimal());

            VerifyAllChecked();

            // Upon completion of the operation the dummy operation returns
            calc.PerformOperation();
            Assert.AreEqual(retValue, calc.Number.ValueDecimal());
            VerifyAllChecked();
        }

        [Test]
        public void TestClear()
        {
            calc.Number.OverrideValue(43);
            Assert.AreEqual(43, calc.Number.ValueDecimal());
            calc.SetOperator(mockOperator.Object);
            Assert.AreEqual(43, calc.Number.ValueDecimal());
            calc.Number.OverrideValue(-9493);
            Assert.AreEqual(-9493, calc.Number.ValueDecimal());

            calc.Clear();
            Assert.AreEqual(0, calc.Number.ValueDecimal());
            mockOperator.Verify(m => m.PerformOperation(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never());
            VerifyAllChecked();
        }

        [Test]
        public void TestChainOperations()
        {
            mockOperator.Setup(m => m.PerformOperation(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(3);
            Mock<IOperator> mockOperator2 = new Mock<IOperator>();

            calc.Number.OverrideValue(1);
            calc.SetOperator(mockOperator.Object);
            calc.Number.OverrideValue(2);
            calc.SetOperator(mockOperator2.Object);
            mockOperator.Verify(m => m.PerformOperation(1, 2));
            calc.Number.OverrideValue(4);
            calc.PerformOperation();
            mockOperator2.Verify(m => m.PerformOperation(3, 4));
        }

        [Test]
        public void TestExceptionDuringOperation()
        {
            mockOperator.Setup(m => m.PerformOperation(It.IsAny<decimal>(), It.IsAny<decimal>())).Throws(new Exception());

            calc.SetOperator(mockOperator.Object);
            calc.PerformOperation();

            mockOperator.Verify(m => m.PerformOperation(0, 0));
            Assert.AreEqual("NaN", calc.Number.ValueString());
            Assert.AreEqual(0, calc.Number.ValueDecimal());
        }

        /// <summary>
        /// Verify that all mocked interaction has been accounted for.
        /// </summary>
        private void VerifyAllChecked()
        {
            mockOperator.VerifyNoOtherCalls();
        }
    }
}
