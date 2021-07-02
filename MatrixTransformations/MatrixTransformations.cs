using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    public class MatrixTransformations
    {
        public static Matrix getTranslationMatrix(Vector3 translation)
        {
            Matrix TranslationMatrix = GetNothingMatrix();

            TranslationMatrix.mat[0, 3] = translation.X;
            TranslationMatrix.mat[1, 3] = translation.Y;
            TranslationMatrix.mat[2, 3] = translation.Z;
            return TranslationMatrix;
        }

        public static Matrix getScaleMatrix(float s)
        {
            Matrix ScaleMatrix = GetNothingMatrix();

            for (int i = 0; i < 4 - 1; i++)
            {
                ScaleMatrix.mat[i, i] = s;
            }

            return ScaleMatrix;
        }

        public static Matrix GetNothingMatrix()
        {
            Matrix NothingMatrix = new Matrix(4, 4);
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (x == y)
                    {
                        NothingMatrix.mat[x, y] = 1;
                    }
                    else
                    {
                        NothingMatrix.mat[x, y] = 0;
                    }
                }
            }
            return NothingMatrix;
        }

        public static Matrix ViewMatrix(float r, float phiRad, float thetaRad)
        {
            Matrix resultingMatrix = new Matrix();
            resultingMatrix.mat = new float[4, 4] 
            {
                { (float)(-1 * Math.Sin(thetaRad)), (float)(Math.Cos(thetaRad)), 0, 0},
                { (float)(-1 * Math.Cos(thetaRad) * Math.Cos(phiRad)), (float)(-1 * Math.Cos(phiRad) * Math.Sin(thetaRad)), (float)(Math.Sin(phiRad)), 0},
                { (float)(Math.Cos(thetaRad) * Math.Sin(phiRad)), (float)(Math.Sin(thetaRad) * Math.Sin(phiRad)), (float)(Math.Cos(phiRad)), -1 * r},
                { 0, 0, 0, 1}
            };
            return resultingMatrix;
            
        }

        public static Matrix GetXRotationMatrix(double a)
        {
            Matrix matrix = new Matrix();
            matrix.mat = new float[4, 4]
            {
                {1,0,0,0 },
                {0,(float)Math.Cos(a),-(float)Math.Sin(a),0 },
                {0,(float)Math.Sin(a),(float)Math.Cos(a),0 },
                {0,0,0,1 }
            };

            return matrix;
        }

        public static Matrix GetYRotationMatrix(double a)
        {
            Matrix matrix = new Matrix();
            matrix.mat = new float[4, 4]
            {
                {(float)Math.Cos(a),0,(float)Math.Sin(a),0 },
                {0,1,0,0 },
                {-(float)Math.Sin(a),0,(float)Math.Cos(a),0 },
                {0,0,0,1 }
            };

            return matrix;
        }

        public static Matrix GetZRotationMatrix(double a)
        {
            Matrix matrix = new Matrix();
            matrix.mat = new float[4, 4]
            {
                {(float)Math.Cos(a ),-(float)Math.Sin(a),0,0 },
                {(float)Math.Sin(a),(float)Math.Cos(a),0,0 },
                {0,0,1,0 },
                {0,0,0,1 }
            };

            return matrix;
        }

        public static Matrix getPerspectiveTransformation(float z,float d)
        {
            Matrix matrix = new Matrix(4, 2);

            for(int x =0; x < 4; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    if (x == y)
                    {
                        matrix.mat[x, y] = -(d / z);
                    }
                    else
                    {
                        matrix.mat[x, y] = 0;
                    }
                }
            }

            return matrix;
        }
    }
}
