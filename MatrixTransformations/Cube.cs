using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    public class Cube
    {
        Color color;
        private int size;
        private float weight;

        public List<Vector3> vb;
        public List<Edge> Edges;

        public Cube(Color color, int size = 100, float weight = 3)
        {
            this.color = color;
            this.weight = weight;
            size = size / 2;

            vb = new List<Vector3>();
            vb.Add(new Vector3(size, size, size));
            vb.Add(new Vector3(size, size, -size));
            vb.Add(new Vector3(size, -size, -size));
            vb.Add(new Vector3(size, -size, size));

            vb.Add(new Vector3(-size, size, size));
            vb.Add(new Vector3(-size, size, -size));
            vb.Add(new Vector3(-size, -size, -size));
            vb.Add(new Vector3(-size, -size, size));

            Edges = new List<Edge>();
            // square1
            Edges.Add(new Edge(0, 1));
            Edges.Add(new Edge(1, 2));
            Edges.Add(new Edge(2, 3));
            Edges.Add(new Edge(3, 0));
            // square2
            Edges.Add(new Edge(4, 5));
            Edges.Add(new Edge(5, 6));
            Edges.Add(new Edge(6, 7));
            Edges.Add(new Edge(7, 4));

            Edges.Add(new Edge(0, 4));
            Edges.Add(new Edge(1, 5));
            Edges.Add(new Edge(2, 6));
            Edges.Add(new Edge(3, 7));
        }

        public void Draw(Graphics g, List<Vector2> vb)
        {
            Pen pen = new Pen(color, weight);
            foreach(Edge edge in Edges)
            {
                g.DrawLine(pen, 
                    vb[edge.vectorindex1].x, vb[edge.vectorindex1].y,
                    vb[edge.vectorindex2].x, vb[edge.vectorindex2].y);
            }
        }

        public void Scale(float s)
        {
            for (int i = 0; i < 4; i++)
            {
                vb[i] = new Matrix(vb[i]).ScaleMatrix(s).ToVector3();
            }
        }

        public void RotateX(float a)
        {
            Matrix matrix = new Matrix();
            matrix.mat = new float[3, 3] 
            { 
                {1,0,0 }, 
                {0,(float)Math.Cos(a),-(float)Math.Sin(a) }, 
                {0,(float)Math.Sin(a),(float)Math.Cos(a) } 
            };

            for(int i = 0; i < 8; i++)
            {
                vb[i] = (matrix * new Matrix(vb[i])).ToVector3();
            }
        }

        public void RotateY(float a)
        {
            Matrix matrix = new Matrix();
            matrix.mat = new float[3, 3]
            {
                {(float)Math.Cos(a),0,(float)Math.Sin(a) },
                {0,1,0 },
                {-(float)Math.Sin(a),0,(float)Math.Cos(a) }
            };

            for (int i = 0; i < 8; i++)
            {
                vb[i] = (matrix * new Matrix(vb[i])).ToVector3();
            }
        }

        public void RotateZ(float a)
        {
            Matrix matrix = new Matrix();
            matrix.mat = new float[3, 3]
            {
                {(float)Math.Cos(a),-(float)Math.Sin(a),0 },
                {(float)Math.Sin(a),(float)Math.Cos(a),0 },
                {0,0,1 }
            };

            for (int i = 0; i < 8; i++)
            {
                vb[i] = (matrix * new Matrix(vb[i])).ToVector3();
            }
        }

    }

    public struct Edge
    {
        public Edge(int i1,int i2)
        {
            vectorindex1 = i1;
            vectorindex2 = i2;
        }
        public int vectorindex1;
        public int vectorindex2;
    }
}
