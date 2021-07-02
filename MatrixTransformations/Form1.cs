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

        public static Vector3 Camera { get
            {   
                return new Vector3(0, 0, r);
            } 
        }

        private static double theta_radians 
        {
            get
            {
                return (Theta)* Math.PI / 180;
            }
            set
            {
                Theta = (int)(value * 180/ Math.PI);
            }
        }

        private static double phi_radians
        {
            get
            {
                return phi_degrees * Math.PI / 180;
            }
            set
            {
                phi_degrees = (int)(value * 180 / Math.PI);
            }
        }
        public static int r;

        public static double Theta;
        public static double phi_degrees;

        public static int d;

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        public Form1()
        {
            InitializeComponent();

            d = 800;
            Theta = -100;
            phi_degrees = -10;
            r = 10;



            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.DoubleBuffered = true;

            // Define axes
            axis = new Axis(-3);

            // Create object
            cube1 = new Cube(Color.Black, 1);

            displayValues = new DisplayValues(cube1);
            annimation = new Annimation(this,cube1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            axis.Draw(e.Graphics, VectorListToScreenCoords(ProjectionTransformation(ViewTransformation(axis.vb))));

            cube1.Draw(e.Graphics, VectorListToScreenCoords(ProjectionTransformation(ViewTransformation(cube1.vb))));

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
            Matrix viewMatrix = MatrixTransformations.ViewMatrix(r, (float)phi_radians, (float)-theta_radians);

            


/*            Matrix axesRightSideUpMatrix = new Matrix(4, 4);
            axesRightSideUpMatrix.mat = new float[4, 4]
            {
                {(float)Math.Cos(a),-(float)Math.Sin(a),0,0 },
                {(float)Math.Sin(a),(float)Math.Cos(a),0,0 },
                {0,0,1,0 },
                {0,0,0,1 }
            };*/

            List<Vector3> Transformed = new List<Vector3>();
            foreach(Vector3 vector in vectors)
            {
                //var newvector = vector - Camera;
                Matrix vectorMatrix = new Matrix(vector);
                vectorMatrix = viewMatrix * vectorMatrix;
                Transformed.Add(vectorMatrix.ToVector3() - Camera);
            }
            return Transformed;
        }


        public static List<Vector2> ProjectionTransformation(List<Vector3> vectors)
        {
            List<Vector2> projection = new List<Vector2>();
            foreach (Vector3 vector in vectors)
            {
                var projectionmatrix = MatrixTransformations.getPerspectiveTransformation(vector.Z, d);

                var matrix = new Matrix(vector);

                projection.Add((projectionmatrix * matrix).ToVector2());
            }
            return projection;
        }

        /*        public static List<Vector2> ProjectionTransformation(List<Vector3> vb)
                {
                    List<Vector2> result = new List<Vector2>();

                    float delta_x = WIDTH / 2;
                    float delta_y = HEIGHT / 2;
                    foreach (Vector3 v in vb)
                    {
                        Vector2 v2 = new Vector2(v.X + delta_x, delta_y - v.Y);
                        result.Add(v2);
                    }
                    return result;
                }*/


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
                cube1.Translate(new Vector3(0, -0.1f, 0));
            }
            if (e.KeyCode == Keys.Down)
            {
                cube1.Translate(new Vector3(0, 0.1f, 0));
            }
            if (e.KeyCode == Keys.Left)
            {
                cube1.Translate(new Vector3(0.1f, 0, 0));
            }
            if (e.KeyCode == Keys.Right)
            {
                cube1.Translate(new Vector3(-0.1f, 0, 0));
            }
            if (e.KeyCode == Keys.PageUp)
            {
                cube1.Translate(new Vector3(0, 0, -0.1f));
            }
            if (e.KeyCode == Keys.PageDown)
            {
                cube1.Translate(new Vector3(0, 0, 0.1f));
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
                    cube1.Size -= 0.05f;
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
                    phi_degrees--;
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
                    cube1.Size += 0.05f;
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
                    phi_degrees++;
                }
            }

            if(e.KeyCode == Keys.OemMinus)
            {
                r++;
            }
            if (e.KeyCode == Keys.Oemplus)
            {
                r--;
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
