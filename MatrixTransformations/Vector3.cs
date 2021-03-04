using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    public class Vector3
    {
        public float X, Y, Z;

        public Vector3()
        {
            X = Y = Z = 0;
        }

        public Vector3(float x,float y,float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator +(Vector3 v1,Vector3 v2)
        {
            var x = v1.X + v2.X;
            var y = v1.Y + v2.Y;
            var z = v1.Z + v2.Z;
            return new Vector3(x, y, z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            var x = v1.X - v2.X;
            var y = v1.Y - v2.Y;
            var z = v1.Z - v2.Z;
            return new Vector3(x, y, z);
        }

        public static Vector3 operator *(Vector3 v, float f)
        {
            var x = v.X * f;
            var y = v.Y * f;
            var z = v.Z * f;
            return new Vector3(x, y, z);
        }

        public static Vector3 operator *(float f, Vector3 v)
        {
            return v * f;
        }

        public static Vector3 operator /(Vector3 v, float f)
        {
            var x = v.X / f;
            var y = v.Y / f;
            var z = v.Z / f;
            return new Vector3(x, y, z);
        }

        public override string ToString()
        {
            return "{x = " + X + ",y = " + Y + ",z = " + Z + "}";
        }
    }
}
