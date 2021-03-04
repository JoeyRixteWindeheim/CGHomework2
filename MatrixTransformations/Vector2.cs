using System;
using System.Text;

namespace MatrixTransformations
{
    public class Vector2
    {
        public float x, y;

        public Vector2()
        {
            x = y = 0;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            var x = v1.x + v2.x;
            var y = v1.y + v2.y;
            return new Vector2(x, y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            var x = v1.x - v2.x;
            var y = v1.y - v2.y;
            return new Vector2(x, y);
        }

        public static Vector2 operator *(Vector2 v, float f)
        {
            var x = v.x * f;
            var y = v.y * f;
            return new Vector2(x, y);
        }
        public static Vector2 operator *(float f, Vector2 v)
        {
            var x = v.x * f;
            var y = v.y * f;
            return new Vector2(x, y);
        }

        public static Vector2 operator /(Vector2 v, float f)
        {
            var x = v.x / f;
            var y = v.y / f;
            return new Vector2(x, y);
        }

        public override string ToString()
        {
            return "{x = "+x+",y = "+y+"}";
        }
    }
}
