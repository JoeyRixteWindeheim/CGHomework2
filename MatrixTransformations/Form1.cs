using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        // Axes
        Axis axis;

        // Objects
        Cube cube1;
        DisplayValues displayValues;
        Annimation annimation;

        Vector3 Camera { get
            {

                var z = (float)(Math.Sin(phi+Math.PI/2) * CameraDistance);
                var distance = Math.Cos(phi + Math.PI / 2) * CameraDistance;
                var x = (float)(Math.Sin(theta) * distance);
                var y = (float)(Math.Cos(theta) * distance);

                return new Vector3(x, y, z);
            } 
        }

        private static double theta 
        {
            get
            {
                return Theta * Math.PI / 180;
            }
            set
            {
                Theta = (int)(value * 180/ Math.PI);
            }
        }

        private static double phi
        {
            get
            {
                return Phi * Math.PI / 180;
            }
            set
            {
                Phi = (int)(value * 180 / Math.PI);
            }
        }
        public static int CameraDistance;

        public static double Theta;
        public static double Phi;

        public static int d;

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        public Form1()
        {
            InitializeComponent();

            d = 20;
            Theta = -100;
            Phi = -10;
            CameraDistance = 20;



            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.DoubleBuffered = true;

            // Define axes
            axis = new Axis(100);

            // Create object
            cube1 = new Cube(Color.Black);

            displayValues = new DisplayValues(cube1);
            annimation = new Annimation(this,cube1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            axis.Draw(e.Graphics, VectorListToScreenCoords(ToIsometric(ViewTransformation(axis.vb))));

            cube1.Draw(e.Graphics, VectorListToScreenCoords(ToIsometric(ViewTransformation(cube1.vb))));

            displayValues.Draw(e.Graphics);
            annimation.Animate();
        }


        public List<Vector2> VectorListToScreenCoords(List<Vector2> vectors)
        {
            int screenWidth = WIDTH;
            int screenHeight = HEIGHT;
            Vector2 Translation = new Vector2(screenWidth / 2, screenHeight / 2 * -1);
            List<Vector2> returnlist = new List<Vector2>();

            foreach (Vector2 vector in vectors)
            {
                Vector2 returnvector = vector + Translation;
                returnvector.y = returnvector.y * -1;
                returnlist.Add(returnvector);
            }
            return returnlist;
        }

        public List<Vector3> ViewTransformation(List<Vector3> vectors)
        {

            Matrix thetaMatrix = MatrixTransformations.GetZRotationMatrix(theta);
            Matrix phiMatrix = MatrixTransformations.GetYRotationMatrix(phi + Math.PI/2);

            thetaMatrix.Invert();
            phiMatrix.Invert();

            List<Vector3> Transformed = new List<Vector3>();
            foreach(Vector3 vector in vectors)
            {
                //var newvector = vector - Camera;
                Matrix vectorMatrix = new Matrix(vector);
                vectorMatrix = thetaMatrix * vectorMatrix;
                vectorMatrix = phiMatrix * vectorMatrix;
                Transformed.Add(vectorMatrix.ToVector3() - Camera);
            }
            return Transformed;
        }


        public List<Vector2> ProjectionTransformation(List<Vector3> vectors)
        {
            List<Vector2> projection = new List<Vector2>();
            foreach(Vector3 vector in vectors)
            {
                var projectionmatrix = MatrixTransformations.getPerspectiveTransformation(vector.Z, d);

                var matrix = new Matrix(vector);

                projection.Add((projectionmatrix * matrix).ToVector2());
            }
            return projection;
        }


        public List<Vector2> ToIsometric(List<Vector3> vectors)
        {
            List<Vector2> returnlist = new List<Vector2>();

            foreach (Vector3 vector in vectors)
            {
                Vector2 returnvector = new Vector2(vector.X, vector.Y);
                returnvector.y = returnvector.y * -1;
                returnlist.Add(returnvector);
            }
            return returnlist;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


            //translation
            if (e.KeyCode == Keys.Up)
            {
                cube1.Translate(new Vector3(0, 2, 0));
            }
            if (e.KeyCode == Keys.Down)
            {
                cube1.Translate(new Vector3(0, -2, 0));
            }
            if (e.KeyCode == Keys.Left)
            {
                cube1.Translate(new Vector3(-2, 0, 0));
            }
            if (e.KeyCode == Keys.Right)
            {
                cube1.Translate(new Vector3(2, 0, 0));
            }
            if (e.KeyCode == Keys.PageUp)
            {
                cube1.Translate(new Vector3(0, 0, 2));
            }
            if (e.KeyCode == Keys.PageDown)
            {
                cube1.Translate(new Vector3(0, 0, -2));
            }


            if (e.Shift)
            {
                //rotation
                if (e.KeyCode == Keys.Z)
                {
                    cube1.RotationZ--;
                }
                if (e.KeyCode == Keys.Y)
                {
                    cube1.RotationY--;
                }
                if (e.KeyCode == Keys.X)
                {
                    cube1.RotationX--;
                }
                //scale
                if (e.KeyCode == Keys.S)
                {
                    cube1.Size--;
                }

                //d
                if (e.KeyCode == Keys.D)
                {
                    d--;
                }
                //theta
                if (e.KeyCode == Keys.T)
                {
                    Theta--;
                }
                //phi
                if (e.KeyCode == Keys.P)
                {
                    Phi--;
                }


            }
            else
            {
                //rotation
                if (e.KeyCode == Keys.Z)
                {
                    cube1.RotationZ++;
                }
                if (e.KeyCode == Keys.Y)
                {
                    cube1.RotationY++;
                }
                if (e.KeyCode == Keys.X)
                {
                    cube1.RotationX++;
                }
                //scale
                if (e.KeyCode == Keys.S)
                {
                    cube1.Size++;
                }

                //d
                if (e.KeyCode == Keys.D)
                {
                    d++;
                }
                //theta
                if (e.KeyCode == Keys.T)
                {
                    Theta++;
                }
                //phi
                if (e.KeyCode == Keys.P)
                {
                    Phi++;
                }
            }

            if(e.KeyCode == Keys.OemMinus)
            {
                CameraDistance++;
            }
            if (e.KeyCode == Keys.Oemplus)
            {
                CameraDistance--;
            }


            if(e.KeyCode == Keys.A)
            {
                annimation.StartAnimation();
            }


            if (e.KeyCode == Keys.C)
            {
                annimation.StopAnimation();
                cube1.Reset();
            }


            Invalidate();

            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}
