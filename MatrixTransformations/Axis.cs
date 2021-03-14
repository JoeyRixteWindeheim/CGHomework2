using System.Collections.Generic;
using System.Drawing;

namespace MatrixTransformations
{
    public class Axis
    {
        private int size;

        public List<Vector3> vb;
        public List<Color> colors;
        public List<string> name;
 
        public Axis(int size=100)
        {
            this.size = size;


            vb = new List<Vector3>();
            colors = new List<Color>();
            name = new List<string>();


            vb.Add(new Vector3(size, 0,0));
            colors.Add(Color.Red);
            name.Add("x");

            vb.Add(new Vector3(0, size, 0));
            colors.Add(Color.Blue);
            name.Add("y");

            vb.Add(new Vector3(0, 0, size));
            colors.Add(Color.Yellow);
            name.Add("z");


            vb.Add(new Vector3(0, 0, 0));
        }

        public void Draw(Graphics g, List<Vector2> vb)
        {
            for(int i=0; i < 3; i++)
            {
                DrawAxis(g, vb[i], vb[3], i);
            }
        }

        public void DrawAxis(Graphics g, Vector2 v,Vector2 zero,int index)
        {
            Pen pen = new Pen(colors[index], 2f);
            g.DrawLine(pen, zero.x, zero.y, v.x, v.y);
            Font font = new Font("Arial", 10);
            PointF p = new PointF(v.x, v.y);
            g.DrawString(name[index], font, Brushes.Black, p);
        }
    }
}
