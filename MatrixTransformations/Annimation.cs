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



        public Annimation(Cube cube)
        {
            Cube = cube;
            animate = false;
        }

        public void StartAnimation()
        {

            phase = 0;
            lastUpdate = DateTime.Now;
            animate = true;

            phi = theta = xrotation = yrotation = 0;
            basescale = Cube.size;

            var ts = new ThreadStart(Animate);
            var backgroundThread = new Thread(ts);
            backgroundThread.Start();
            
        }

        public void StopAnimation()
        {
            animate = false;
        }

        public void Animate()
        {
            while (animate)
            {
                TimeSpan interval = DateTime.Now - lastUpdate;
                lastUpdate = DateTime.Now;



            }
        }

        public void Phase0(TimeSpan interval)
        {
            Cube.Scale((float)(1 + interval.TotalSeconds));
            if(  basescale*1.5 <= Cube.size)
            {
                phase++;
            }
        }
        public void Phase1(TimeSpan interval)
        {
            Cube.Scale((float)(1 - interval.TotalSeconds));
            if (basescale  >= Cube.size)
            {
                phase++;
            }
        }

        public void Phase2(TimeSpan interval)
        {
            Cube.RotationX += (float)interval.TotalSeconds;
            xrotation += interval.TotalSeconds;
            if (xrotation >= 45)
            {
                phase++;
            }
        }
        public void Phase3(TimeSpan interval)
        {
            Cube.RotationX -= (float)interval.TotalSeconds;
            xrotation -= interval.TotalSeconds;
            if (xrotation <= 0)
            {
                phase++;
            }
        }

        public void Phase4(TimeSpan interval)
        {
            Cube.RotationY += (float)interval.TotalSeconds;
            yrotation += interval.TotalSeconds;
            if (yrotation >= 45)
            {
                phase++;
            }
        }
        public void Phase5(TimeSpan interval)
        {
            Cube.RotationY -= (float)interval.TotalSeconds;
            yrotation -= interval.TotalSeconds;
            if (yrotation <= 0)
            {
                phase++;
            }
        }


    }
}
