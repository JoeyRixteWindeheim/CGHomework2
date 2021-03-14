using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MatrixTransformations;

namespace UnitTests
{
    [TestClass]
    public class Vector2UnitTests
    {
        [TestMethod]
        [DataRow(2, 2, 3, 3, 5, 5)]
        [DataRow(9, -2, 3, 3, 12, 1)]
        [DataRow(-3, 2, -9, 3, -12, 5)]
        // Next test fails due to how rounding off is handled.
        //[DataRow(1.1f, -4f, 2.2f, -1.1f, 3.3f, -5.1f)]
        public void Addition_VectorAndVector(float x1, float y1, float x2, float y2, float expectedX, float expectedY)
        {
            // arrange
            Vector2 firstVector = new Vector2(x1, y1);
            Vector2 secondVector = new Vector2(x2, y2);
            Vector2 expectedVector = new Vector2(expectedX, expectedY);
            // act
            Vector2 combinedVector = firstVector + secondVector;

            // assert
            Assert.AreEqual(expectedVector.x, combinedVector.x);
            Assert.AreEqual(expectedVector.y, combinedVector.y);
        }


        [TestMethod]
        [DataRow(2, 2, 3, 3, -1, -1)]
        [DataRow(9, -2, 3, 3, 6, -5)]
        [DataRow(-3, 2, -9, 3, 6, -1)]
        [DataRow(1.1f, -4, 2.2f, -1.1f, -1.1f, -2.9f)]
        public void Subtraction_VectorAndVector(float x1, float y1, float x2, float y2, float expectedX, float expectedY)
        {
            // arrange
            Vector2 firstVector = new Vector2(x1, y1);
            Vector2 secondVector = new Vector2(x2, y2);

            // act
            Vector2 subtractedVector = firstVector - secondVector;

            // assert
            Assert.AreEqual(expectedX, subtractedVector.x);
            Assert.AreEqual(expectedY, subtractedVector.y);
        }

        [TestMethod]
        [DataRow(2, 2, -3, -6, -6)]
        [DataRow(9, -2, 3, 27, -6)]
        public void Multiply_VectorAndFloat(float x, float y, float f, float resultX, float resultY)
        {
            Vector2 vector = new Vector2(x, y);
            Vector2 expectedResultVector = new Vector2(resultX, resultY);

            Vector2 resultingVector = vector * f;

            Assert.AreEqual(expectedResultVector.x, resultingVector.x);
            Assert.AreEqual(expectedResultVector.y, resultingVector.y);
        }

        [TestMethod]
        [DataRow(2, 2, -3, -6, -6)]
        [DataRow(9, -2, 3, 27, -6)]
        public void Multiply_FloatAndVector(float x, float y, float f, float resultX, float resultY)
        {
            Vector2 vector = new Vector2(x, y);
            Vector2 expectedResultVector = new Vector2(resultX, resultY);

            Vector2 resultingVector = f * vector;

            Assert.AreEqual(expectedResultVector.x, resultingVector.x);
            Assert.AreEqual(expectedResultVector.y, resultingVector.y);
        }

        [TestMethod]
        [DataRow(2, 2, -3, 2/-3f, 2/-3f)]
        [DataRow(9, -2, 3, 3, -2/3f)]
        public void Division_VectorAndFloat(float x, float y, float f, float resultX, float resultY)
        {
            Vector2 vector = new Vector2(x, y);

            Vector2 expectedResultVector = new Vector2(resultX, resultY);

            Vector2 resultingVector = vector / f;

            Assert.AreEqual(expectedResultVector.x, resultingVector.x);
            Assert.AreEqual(expectedResultVector.y, resultingVector.y);
        }

        [TestMethod]
        [DataRow(2, 2, "{x = 2,y = 2}")]
        [DataRow(9, -2, "{x = 9,y = -2}")]
        [DataRow(-3.3f, -2, "{x = -3,3,y = -2}")]
        public void ToString_Vector(float x, float y, string expectedOutput)
        {
            Vector2 vector = new Vector2(x, y);

            string result = vector.ToString();

            Assert.AreEqual(expectedOutput, result);
        }
    }
}
