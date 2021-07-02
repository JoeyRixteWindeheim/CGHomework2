using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MatrixTransformations;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class Matrix4DUnitTests
    {
        Matrix CreateMatrix(float[] expectedMatrixValues)
        {
            Matrix resultedMatrix = new Matrix(4, 4);
            for(int x = 0; x < 4; x++)
            {
                for(int y = 0; y < 4; y++)
                {
                    resultedMatrix.mat[x, y] = expectedMatrixValues[x * 4 + y];
                }
            }
            return resultedMatrix;
        }
        [TestMethod]
        [DataRow(new float[]
                {1, 0, 0, 0,
                 0, 1, 0, 0,
                 0, 0, 1, 0,
                 0, 0, 0, 1})]
        public void Test_GetNothingMatrix(float[] expected)
        {
            // arrange
            Matrix expected4DNothingMatrix = CreateMatrix(expected);

            Matrix actualNothingMatrix = MatrixTransformations.MatrixTransformations.GetNothingMatrix();
            // act

            // assert
            // loop is needed because if asserting .mat, does so by reference
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Assert.AreEqual(expected4DNothingMatrix.mat[x,y], actualNothingMatrix.mat[x, y]);
                }
            }

        }

        [TestMethod]
        [DataRow(new float[]
                {1, 0, 0, 0,
                 0, 1, 0, 0,
                 0, 0, 1, 0,
                 0, 0, 0, 1},
            new float[]
                {3, 0, 9, 0,
                 0, 1, 0, 0,
                 0, 2, 6, 0,
                 0, 0, 0, 1},
            new float[]
                {4, 0, 9, 0,
                 0, 2, 0, 0,
                 0, 2, 7, 0,
                 0, 0, 0, 2})]
        public void Test_addition(float[] firstMatrixToAdd, float[] secondMatrixToAdd, float[] expectedMatrix)
        {
            // arrange
            Matrix firstMatrix = CreateMatrix(firstMatrixToAdd);
            Matrix secondMatrix = CreateMatrix(secondMatrixToAdd);
            

            Matrix expected = CreateMatrix(expectedMatrix);
            // act
            Matrix resultingMatrix = firstMatrix + secondMatrix;

            // assert
            // loop is needed because if asserting .mat, does so by reference
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Assert.AreEqual(expected.mat[x, y], resultingMatrix.mat[x, y]);
                }
            }

        }

        [TestMethod]
        [DataRow(new float[]
                {1, 0, 0, 0,
                 0, 1, 0, 0,
                 0, 0, 1, 0,
                 0, 0, 0, 1},
        new float[]
                    {3, 0, 9, 0,
                     0, 1, 0, 0,
                     0, 2, 6, 0,
                     0, 0, 0, 1},
        new float[]
                    {-2, 0, -9, 0,
                     0, 0, 0, 0,
                     0, -2, -5, 0,
                     0, 0, 0, 0})]
        public void Test_subtraction(float[] firstMatrixToAdd, float[] secondMatrixToAdd, float[] expectedMatrix)
        {
            // arrange
            Matrix firstMatrix = CreateMatrix(firstMatrixToAdd);
            Matrix secondMatrix = CreateMatrix(secondMatrixToAdd);


            Matrix expected = CreateMatrix(expectedMatrix);
            // act
            Matrix resultingMatrix = firstMatrix - secondMatrix;

            // assert
            // loop is needed because if asserting .mat, does so by reference
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Assert.AreEqual(expected.mat[x, y], resultingMatrix.mat[x, y]);
                }
            }

        }

        [TestMethod]
        [DataRow(new float[]
                    {3, 0, 9, 0,
                     0, 1, 0, 0,
                     0, 2, 6, 0,
                     0, 0, 0, 1},
            new float[]
                {0, 0, 3, 0,
                 0, 5, 1, 6,
                 8, 3, 1, 7,
                 0, 2, 0, 1},
            new float[]
                    {0, 6, 18, 0,
                     0, 7, 6, 6,
                     24, 5, 78, 07,
                     0, 2, 0, 1})]
        public void Test_multiplied(float[] firstMatrixToAdd, float[] secondMatrixToAdd, float[] expectedMatrix)
        {
            // arrange
            Matrix firstMatrix = CreateMatrix(firstMatrixToAdd);
            Matrix secondMatrix = CreateMatrix(secondMatrixToAdd);


            Matrix expected = CreateMatrix(expectedMatrix);

            // act
            Matrix resultingMatrix = firstMatrix * secondMatrix;

            // assert
            // loop is needed because if asserting .mat, does so by reference
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Assert.AreEqual(expected.mat[x, y], resultingMatrix.mat[x, y]);
                }
            }

        }



    }
}
