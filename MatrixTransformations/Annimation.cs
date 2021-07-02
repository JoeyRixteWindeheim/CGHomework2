using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    public class Annimation
    {
        private Cube Cube;
        private DateTime lastUpdate;
        private int phase;
        private bool animate;

        private double basescale;
        private double xrotation;
        private double yrotation;
        private double phi;
        private double theta;
        private Form1 form;



        public Annimation(Form1 form, Cube cube)
        {
            Cube = cube;
            animate = false;
            this.form = form;
        }

        public void StartAnimation()
        {
            if (animate)
                return;

            phase = 0;
            lastUpdate = DateTime.Now;
            animate = true;

            phi = theta = xrotation = yrotation = 0;
            basescale = Cube.Size;
            
        }

        public void StopAnimation()
        {
            animate = false;
        }

        public void Animate()
        {
            if(animate)
            {
                TimeSpan interval = DateTime.Now - lastUpdate;
                lastUpdate = DateTime.Now;
                switch (phase)
                {
                    case 0:
                        Phase0(interval); // increase scale
                        break;
                    case 1:
                        Phase1(interval); // decrease scale
                        break;
                    case 2:
                        Phase2(interval); // rotate x
                        break;
                    case 3:
                        Phase3(interval); // rotate x back
                        break;
                    case 4:
                        Phase4(interval); // rotate y
                        break;
                    case 5:
                        Phase5(interval); // rotate y back
                        break;
                    case 6:
                        Phase6(interval); // reset phi and theta
                        break;
                    default:
                        phase = 0;
                        break;
                }
                form.Invalidate();
            }
        }

        public void Phase0(TimeSpan interval)
        {
            Cube.Size += (float)interval.TotalSeconds*5;

            theta -= interval.TotalSeconds * 5;
            Form1.Theta -= interval.TotalSeconds * 5;

            if (  basescale*1.5 <= Cube.Size)
            {
                phase++;
            }
        }
        public void Phase1(TimeSpan interval)
        {
            Cube.Scale((float)(1 - interval.TotalSeconds));

            theta -= interval.TotalSeconds * 5;
            Form1.Theta -= interval.TotalSeconds * 5;
            if (basescale  >= Cube.Size)
            {
                phase++;
            }
        }

        public void Phase2(TimeSpan interval)
        {
            Cube.RotationX += (float)interval.TotalSeconds * 5;
            xrotation += interval.TotalSeconds*5;

            theta -= interval.TotalSeconds * 5;
            Form1.Theta -= interval.TotalSeconds * 5;
            if (xrotation >= 45)
            {
                phase++;
            }
        }
        public void Phase3(TimeSpan interval)
        {
            Cube.RotationX -= (float)interval.TotalSeconds * 5;
            xrotation -= interval.TotalSeconds*5;

            theta -= interval.TotalSeconds * 5;
            Form1.Theta -= interval.TotalSeconds * 5;
            if (xrotation <= 0)
            {
                phase++;
            }
        }

        public void Phase4(TimeSpan interval)
        {
            Cube.RotationY += (float)interval.TotalSeconds * 5;
            yrotation += interval.TotalSeconds*5;

            phi += interval.TotalSeconds * 5;
            Form1.phi_degrees += interval.TotalSeconds * 5;

            if (yrotation >= 45)
            {
                phase++;
            }
        }
        public void Phase5(TimeSpan interval)
        {
            Cube.RotationY -= (float)interval.TotalSeconds * 5;
            yrotation -= interval.TotalSeconds*5;

            phi += interval.TotalSeconds * 5;
            Form1.phi_degrees += interval.TotalSeconds * 5;
            if (yrotation <= 0)
            {
                phase++;
            }
        }

        public void Phase6(TimeSpan interval)
        {
            if(theta < 0)
            {
                theta += interval.TotalSeconds * 5;
                Form1.Theta += interval.TotalSeconds * 5;
            }
            if(phi > 0)
            {
                phi -= interval.TotalSeconds * 5;
                Form1.phi_degrees -= interval.TotalSeconds * 5;
            }
            if(theta >= 0 || phi<=0)
            {
                phase++;
            }
        }


    }
}
