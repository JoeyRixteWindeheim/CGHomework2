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
        private float _rotationX;
        public float RotationX { 
            get { return _rotationX * (float)(180 / Math.PI); } 
            set {
                float rotation = value * (float)(Math.PI / 180);
                RotateX(rotation - _rotationX);
            }
        }

        private float _rotationY;
        public float RotationY
        {
            get { return _rotationY * (float)(180 / Math.PI); }
            set
            {
                float rotation = value * (float)(Math.PI / 180);
                RotateY(rotation - _rotationY);
            }
        }

        private float _rotationZ;
        public float RotationZ
        {
            get { return _rotationZ * (float)(180 / Math.PI); }
            set
            {
                float rotation = value * (float)(Math.PI / 180);
                RotateZ(rotation - _rotationZ);
            }
        }

        private Vector3 _position;
        public Vector3 Position { get { return _position; } set { Translate(value - _position); } }

        public int PositionX { get => (int)_position.X; set { Position = new Vector3(value, _position.Y, _position.Z); } }
        public int PositionY { get => (int)_position.Y; set { Position = new Vector3(_position.X, value, _position.Z); } }
        public int PositionZ { get => (int)_position.Z; set { Position = new Vector3(_position.X, _position.Y, value); } }

        public Cube(Color color, int size = 100, float weight = 3)
        {
            this.color = color;
            this.weight = weight;
            this.size = size /= 2;
            _position = new Vector3(0, 0, 0);

            vb = new List<Vector3>
            {
                new Vector3(size, size, size),
                new Vector3(size, size, -size),
                new Vector3(size, -size, -size),
                new Vector3(size, -size, size),

                new Vector3(-size, size, size),
                new Vector3(-size, size, -size),
                new Vector3(-size, -size, -size),
                new Vector3(-size, -size, size)
            };

            Edges = new List<Edge>
            {
                // square1
                new Edge(0, 1),
                new Edge(1, 2),
                new Edge(2, 3),
                new Edge(3, 0),
                // square2
                new Edge(4, 5),
                new Edge(5, 6),
                new Edge(6, 7),
                new Edge(7, 4),

                new Edge(0, 4),
                new Edge(1, 5),
                new Edge(2, 6),
                new Edge(3, 7)
            };
        }

        public void Reset()
        {

            RotationZ = 0;
            RotationX = 0;
            RotationY = 0;

            vb = new List<Vector3>
            {
                new Vector3(size, size, size),
                new Vector3(size, size, -size),
                new Vector3(size, -size, -size),
                new Vector3(size, -size, size),

                new Vector3(-size, size, size),
                new Vector3(-size, size, -size),
                new Vector3(-size, -size, -size),
                new Vector3(-size, -size, size)
            };

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
            Vector3 p = _position;
            Translate(new Vector3()-p);
            _rotationX += a;
            if(_rotationX > Math.PI * 2)
            {
                _rotationX -= (float)Math.PI * 2;
            }
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
            Translate(p);
        }

        public void RotateY(float a)
        {
            Vector3 p = _position;
            Translate(new Vector3() - p);
            _rotationY += a;
            if (_rotationY > Math.PI * 2)
            {
                _rotationY -= (float)Math.PI * 2;
            }
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

            Translate(p);
        }

        public void RotateZ(float a)
        {
            Vector3 p = _position;
            Translate(new Vector3() - p);

            _rotationZ += a;
            if (_rotationZ > Math.PI * 2)
            {
                _rotationZ -= (float)Math.PI * 2;
            }
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

            Translate(p);
        }


        public void Translate(Vector3 translation)
        {
            _position += translation;
            for(int i = 0; i< vb.Count;i++)
            {
                vb[i] += translation;
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
