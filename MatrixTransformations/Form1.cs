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
        Cube cube1;
        DisplayValues displayValues;
        

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        public Form1()
        {
            InitializeComponent();

            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.DoubleBuffered = true;

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);

            // Create object
            cube1 = new Cube(Color.Black);
            displayValues = new DisplayValues(cube1);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            //x_axis.Draw(e.Graphics, VectorListToScreenCoords(x_axis.vb));
            //y_axis.Draw(e.Graphics, VectorListToScreenCoords(y_axis.vb));

            cube1.Draw(e.Graphics, ToPerspective(cube1.vb));
            displayValues.Draw(e.Graphics);
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
                cube1.Translate(new Vector3(0, 0, 2));
            }
            if (e.KeyCode == Keys.Down)
            {
                cube1.Translate(new Vector3(0, 0, -2));
            }
            if (e.KeyCode == Keys.Left)
            {
                cube1.Translate(new Vector3(-2, 0, 0));
            }
            if (e.KeyCode == Keys.Right)
            {
                cube1.Translate(new Vector3(2, 0, 0));
            }
            if (e.KeyCode == Keys.Z)
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
            if (e.KeyCode == Keys.C)
            {
                cube1.Reset();
            }
            Invalidate();

            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}
