using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        // Axes
        AxisX x_axis;
        AxisY y_axis;

        // Objects
        Square square;
        Square square2;
        Square square3;
        Cube cube1;
        

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        public Form1()
        {
            InitializeComponent();

            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.DoubleBuffered = true;

            Vector2 v1 = new Vector2();
            Console.WriteLine(v1);
            Vector2 v2 = new Vector2(1, 2);
            Console.WriteLine(v2);
            Vector2 v3 = new Vector2(2, 6);
            Console.WriteLine(v3);
            Vector2 v4 = v2 + v3;
            Console.WriteLine(v4); // 3, 8

            Matrix m1 = new Matrix(2,2);
            m1.mat = new float[2, 2] { { 1, 0 }, { 0, 1 } };
            Console.WriteLine(m1); // 1, 0, 0, 1
            Matrix m2 = new Matrix(2, 2);
            m2.mat = new float[2, 2] { { 2, 4 }, { -1, 3 } };
            Console.WriteLine(m2);
            Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            //Console.WriteLine(m2 * v3); // 28, 16

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);

            // Create object
            square = new Square(Color.Purple,100);
            //square2 = new Square(Color.Cyan, 100);
            //square2.Scale(1.5f);
            //square3 = new Square(Color.Orange, 100);
            //square3.Rotate(0.5f);

            cube1 = new Cube(Color.Black);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            //x_axis.Draw(e.Graphics, VectorListToScreenCoords(x_axis.vb));
            //y_axis.Draw(e.Graphics, VectorListToScreenCoords(y_axis.vb));

            // Draw square
            //square.Draw(e.Graphics, VectorListToScreenCoords(square.vb));
            //square2.Draw(e.Graphics, VectorListToScreenCoords(square2.vb));
            //square3.Draw(e.Graphics, VectorListToScreenCoords(square3.vb));
            cube1.Draw(e.Graphics, ToPerspective(cube1.vb));
        }

        public List<Vector2> VectorListToScreenCoords(List<Vector2> vectors)
        {
            int screenWidth = WIDTH;
            int screenHeight = HEIGHT;
            Vector2 Translation = new Vector2(screenWidth / 2, screenHeight / 2*-1);
            List<Vector2> returnlist = new List<Vector2>();

            foreach(Vector2 vector in vectors)
            {
                Vector2 returnvector = vector + Translation;
                returnvector.y = returnvector.y * -1;
                returnlist.Add(returnvector);
            }
            return returnlist;
        }

        public List<Vector2> ToPerspective(List<Vector3> vectors)
        {
            int screenWidth = WIDTH;
            int screenHeight = HEIGHT;
            int cameraPosition = 100;
            Vector2 Translation = new Vector2(screenWidth / 2, screenHeight / 2 * -1);
            List<Vector2> returnlist = new List<Vector2>();

            foreach (Vector3 vector in vectors)
            {
                Vector2 returnvector = new Vector2(vector.X, vector.Z);
                var distancetocamera = cameraPosition - vector.Y;
                returnvector /= (distancetocamera/100);


                returnvector = returnvector + Translation;
                returnvector.y = returnvector.y * -1;


                returnlist.Add(returnvector);
            }
            return returnlist;
        }

        public List<Vector2> ToIsometric(List<Vector3> vectors)
        {
            int screenWidth = WIDTH;
            int screenHeight = HEIGHT;
            Vector2 Translation = new Vector2(screenWidth / 2, screenHeight / 2 * -1);
            List<Vector2> returnlist = new List<Vector2>();

            foreach (Vector3 vector in vectors)
            {
                Vector2 returnvector = new Vector2(vector.X, vector.Z);
                returnvector = returnvector + Translation;
                returnvector.y = returnvector.y * -1;
                returnlist.Add(returnvector);
            }
            return returnlist;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                square.Scale(1.1f);
            }
            if (e.KeyCode == Keys.Down)
            {
                square.Scale(0.9f);
            }
            if (e.KeyCode == Keys.Left)
            {
                square.Rotate(0.1f);
            }
            if (e.KeyCode == Keys.Right)
            {
                square.Rotate(-0.1f);
            }
            if(e.KeyCode == Keys.Z)
            {
                cube1.RotateZ(0.1f);
            }
            if (e.KeyCode == Keys.Y)
            {
                cube1.RotateY(0.1f);
            }
            if (e.KeyCode == Keys.X)
            {
                cube1.RotateX(0.1f);
            }
            Invalidate();

            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}
