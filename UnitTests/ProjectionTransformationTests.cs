using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MatrixTransformations;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ProjectionTransformationTests
    {
        [TestMethod]
        [DataRow(-5, 50)]
        public void Addition_VectorAndVector(int vectorZ, int d)
        {
            // arrange
            //var projectionmatrix = MatrixTransformations.getPerspectiveTransformation(vector.Z, d);
            var projectionmatrix = MatrixTransformations.MatrixTransformations.getPerspectiveTransformation(vectorZ, d);
            Matrix expectedMatrix = new Matrix(4, 2);
            expectedMatrix.mat[0, 0] = -(d / vectorZ);
            expectedMatrix.mat[1, 1] = -(d / vectorZ);
            // act

            // assert
            Assert.AreEqual(expectedMatrix.mat[0, 0], projectionmatrix.mat[0, 0]);
            Assert.AreEqual(expectedMatrix.mat[1, 1], projectionmatrix.mat[1, 1]);
        }

        [TestMethod]
        [DataRow(20, 10, 50, 2, 1, -5)]
        public void projTransformation()
        {
            // arrange
            Form1.d = 50;
            Vector2 expectedProjectionTransformation = new Vector2(20, 10);
            Vector3 inputVector3 = new Vector3(2, 1, -5);
            List<Vector3> vec3List = new List<Vector3>();
            vec3List.Add(inputVector3);

            // act
            List<Vector2> outputVec2 = Form1.ProjectionTransformation(vec3List);

            // assert
            // Assert.AreEqual failed. Expected:<20>. Actual:<0>. 
            Assert.AreEqual(expectedProjectionTransformation.x, outputVec2[0].x);
            Assert.AreEqual(expectedProjectionTransformation.y, outputVec2[0].y);
        }

    }
}
