using HotelBooking.BAL;
using NUnit.Framework;

namespace Test
{
    public class MathTest
    {
        private CustomMath math;

        [SetUp]
        public void Setup()
        {
            math = new CustomMath();
        }

        [Test]
        public void Additional_01()
        {
            Assert.AreEqual(10, math.Additional(11, -1));
        }

        [Test]
        public void Additional_02()
        {
            Assert.AreNotEqual(10, math.Additional(4, 7));
        }

        [Test]
        public void Subtraction_01()
        {
            Assert.AreEqual(10, math.Subtraction(11, 1));
        }

        [Test]
        public void Subtraction_02()
        {
            Assert.AreNotEqual(12, math.Subtraction(12, 1));
        }

        [Test]
        public void Multiply_01()
        {
            Assert.AreEqual(-2, math.Multiply(-1, 2));
        }

        [Test]
        public void Multiply_02()
        {
            Assert.AreNotEqual(23, math.Multiply(11, 2));
        }

        [Test]
        public void Division_01()
        {
            Assert.AreNotEqual(10, math.Division(10, 0));
        }

        [Test]
        public void Division_02()
        {
            Assert.AreNotEqual(3, math.Division(11, 3));
        }

        [Test]
        public void DivisonByZero()
        {
            Assert.AreEqual(float.MinValue, math.Division(3, 0));
        }
    }
}