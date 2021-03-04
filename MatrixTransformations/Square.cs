using System.Collections.Generic;
using System.Drawing;

namespace MatrixTransformations
{
    public class Square
    {
        Color color;
        private int size;
        private float weight;

        public List<Vector2> vb;

        public Square(Color color, int size = 100, float weight = 3)
        {
            this.color = color;
            this.size = size;
            this.weight = weight;

            vb = new List<Vector2>();
            vb.Add(new Vector2(-size, -size));
            vb.Add(new Vector2(size, -size));
            vb.Add(new Vector2(size, size));
            vb.Add(new Vector2(-size, size));
        }

        public void Draw(Graphics g, List<Vector2> vb)
        {
            Pen pen = new Pen(color, weight);
            g.DrawLine(pen, vb[0].x, vb[0].y, vb[1].x, vb[1].y);
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[2].x, vb[2].y);
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[3].x, vb[3].y);
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[0].x, vb[0].y);
        }

        public void Scale(float s)
        {
            for(int i = 0; i < 4; i++)
            {
                vb[i] = new Matrix2x2(vb[i]).ScaleMatrix(s).ToVector();
            }
        }
        public void Rotate(float d)
        {
            for (int i = 0; i < 4; i++)
            {
                vb[i] = new Matrix2x2(vb[i]).RotateMatrix(d).ToRVector();
            }
        }
    }
}
