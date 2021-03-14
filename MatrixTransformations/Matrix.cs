using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    public class Matrix
    {
        public float[,] mat;
        public int X => mat.GetLength(0);
        public int Y => mat.GetLength(1);

        public Matrix() { }
        public Matrix(int x, int y)
        {
            mat = new float[x, y];
        }

        public Matrix(Vector3 vector)
        {
            mat = new float[1, 4] {{ vector.X ,  vector.Y ,  vector.Z,1 } };
        }

        public Matrix(Vector2 vector)
        {
            mat = new float[2, 2] { { vector.x, 0 }, { vector.y, 0 } };
        }

        public static Matrix operator +(Matrix m1,Matrix m2)
        {
            if(m1.X != m2.X || m1.Y != m2.Y)
            {
                return null;
            }

            Matrix rMatrix = new Matrix(m1.X, m1.Y);

            for(int x =0;x<m1.X;x++)
            {
                for (int y = 0; y < m1.Y; y++)
                {
                    rMatrix.mat[x, y] = m1.mat[x, y] + m2.mat[x, y];
                }
            }
            return rMatrix;
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.X != m2.X || m1.Y != m2.Y)
            {
                return null;
            }

            Matrix rMatrix = new Matrix(m1.X, m1.Y);

            for (int x = 0; x < m1.X; x++)
            {
                for (int y = 0; y < m1.Y; y++)
                {
                    rMatrix.mat[x, y] = m1.mat[x, y] - m2.mat[x, y];
                }
            }
            return rMatrix;
        }

        public static Matrix operator *(Matrix m1, float f)
        {
            Matrix rMatrix = new Matrix(m1.X, m1.Y);

            for (int x = 0; x < m1.X; x++)
            {
                for (int y = 0; y < m1.Y; y++)
                {
                    rMatrix.mat[x, y] = m1.mat[x, y] *f;
                }
            }
            return rMatrix;
        }



        public static Matrix operator *(float f, Matrix m1)
        {
            return m1 * f;
        }
        private static float MultiplyArray(float[] a1, float[] a2)
        {
            float total = 0;
            for (int i = 0; i < a1.Length; i++)
            {
                total += a1[i] * a2[i];
            }
            return total;
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.X != m2.Y)
            {
                return null;
            }

            Matrix rMatrix = new Matrix(m2.X,m1.Y);
            for (int x = 0; x < m2.X; x++)
            {
                for (int y = 0; y < m1.Y; y++)
                {
                    var row = m1.GetRow(y);
                    var col = m2.GetColumn(x);
                    rMatrix.mat[x, y] = MultiplyArray(row, col);
                }
            }
            return rMatrix;
        }
        public float[] GetColumn(int index)
        {
            float[] rFloat = new float[Y];
            for(int i = 0; i < Y; i++)
            {
                rFloat[i] = mat[ index,i];
            }
            return rFloat;
        }
        public float[] GetRow(int RowIndex)
        {
            float[] rFloat = new float[X];
            for (int i = 0; i < X; i++)
            {
                rFloat[i] = mat[i,RowIndex];
            }
            return rFloat;
        }


        public override string ToString()
        {
            return $"({mat[0, 0]},{mat[0, 1]})({mat[1, 0]},{mat[1, 1]})";
        }

        public Vector2 ToVector2()
        {
            if (Y < 2) { return null; }
            return new Vector2(mat[0, 0], mat[0, 1]);
        }

        public Vector3 ToVector3()
        {
            if (Y < 3) { return null; }
            return new Vector3(mat[0, 0], mat[0, 1],mat[0,2]);
        }

        public void Invert()
        {
            for(int x = 0; x < X; x++)
            {
                for (int y = 0; y < X; y++)
                {
                    if (x != y)
                    {
                        mat[x, y] = -mat[x, y];
                    }
                }
            }
        }
    }
}
