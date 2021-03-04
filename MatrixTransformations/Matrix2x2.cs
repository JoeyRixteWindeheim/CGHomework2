using System;
using System.Text;

namespace MatrixTransformations
{
    public class Matrix2x2
    {
        float[,] mat = new float[2, 2];

        public Matrix2x2()
        {
            mat[0, 0] = 1; mat[0, 1] = 0;
            mat[1, 0] = 0; mat[1, 1] = 1;
        }
        public Matrix2x2(float m11, float m12,
                      float m21, float m22)
        {
            mat[0, 0] = m11; mat[0, 1] = m12;
            mat[1, 0] = m21; mat[1, 1] = m22;
        }

        public Matrix2x2(Vector2 v)
        {
            mat[0, 0] = v.x; mat[0, 1] = 0;
            mat[1, 0] = v.y; mat[1, 1] = 0;
        }

        public static Matrix2x2 operator +(Matrix2x2 m1, Matrix2x2 m2)
        {
            var m11 = m1.mat[0, 0] + m2.mat[0, 0];
            var m12 = m1.mat[0, 1] + m2.mat[0, 1];
            var m21 = m1.mat[1, 0] + m2.mat[1, 0];
            var m22 = m1.mat[1, 1] + m2.mat[1, 1];
            return new Matrix2x2(m11, m12, m21, m22);
        }

        public static Matrix2x2 operator -(Matrix2x2 m1, Matrix2x2 m2)
        {
            var m11 = m1.mat[0, 0] - m2.mat[0, 0];
            var m12 = m1.mat[0, 1] - m2.mat[0, 1];
            var m21 = m1.mat[1, 0] - m2.mat[1, 0];
            var m22 = m1.mat[1, 1] - m2.mat[1, 1];
            return new Matrix2x2(m11, m12, m21, m22);
        }
        public static Matrix2x2 operator *(Matrix2x2 m1, float f)
        {
            var m11 = m1.mat[0, 0] * f;
            var m12 = m1.mat[0, 1] * f;
            var m21 = m1.mat[1, 0] * f;
            var m22 = m1.mat[1, 1] * f;
            return new Matrix2x2(m11, m12, m21, m22);
        }

        private static float MultiplyArray(float[] a1,float[] a2)
        {
            float total = 0;
            for(int i = 0; i < a1.Length; i++)
            {
                total += a1[i] * a2[i];
            }
            return total;
        }

        public float[] GetColumn(int index)
        {
            return new float[] { mat[0, index], mat[1, index] };
        }
        public float[] GetRow(int index)
        {
            return new float[] { mat[index, 0], mat[index, 1] };
        }

        public static Matrix2x2 operator *(float f, Matrix2x2 m1)
        {
            return m1 * f;
        }
        public static Matrix2x2 operator *(Matrix2x2 m1, Matrix2x2 m2)
        {

            var m11 = MultiplyArray(m1.GetRow(0),m2.GetColumn(0));
            var m12 = MultiplyArray(m1.GetRow(0), m2.GetColumn(1));
            var m21 = MultiplyArray(m1.GetRow(1), m2.GetColumn(0));
            var m22 = MultiplyArray(m1.GetRow(1), m2.GetColumn(1));
            return new Matrix2x2(m11, m12, m21, m22);
        }

        public static Vector2 operator *(Matrix2x2 m1, Vector2 v)
        {
            return (m1 * new Matrix2x2(v)).ToVector();
        }

        public static Matrix2x2 Identity()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"({mat[0, 0]},{mat[0, 1]})({mat[1, 0]},{mat[1, 1]})";
        }

        public Vector2 ToVector()
        {
            return new Vector2(mat[0,0],mat[1,0]);
        }

        public Vector2 ToRVector()
        {
            return new Vector2(mat[0, 0], mat[1, 0]);
        }


        public Matrix2x2 ScaleMatrix(float s)
        {
            Matrix2x2 ScaleMatrix = new Matrix2x2(s, 0, 0, s);

            return this * ScaleMatrix;
        }

        public Matrix2x2 RotateMatrix(float d)
        {
            float m11 = (float)Math.Cos((double)d);
            float m12 = -(float)Math.Sin((double)d);
            float m21 = (float)Math.Sin((double)d);
            float m22 = (float)Math.Cos((double)d);
            Matrix2x2 RotateMatrix = new Matrix2x2(m11, m12, m21, m22);

            return RotateMatrix * this;
        }
    }
}
