using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    public class DisplayValues
    {
        private Cube _cube;

        public float XRotation { get => _cube.RotationX; }
        public float YRotation { get => _cube.RotationY; }
        public float ZRotation { get => _cube.RotationZ; }
        public int XPosition { get => _cube.PositionX; }
        public int YPosition { get => _cube.PositionY; }
        public int ZPosition { get => _cube.PositionZ; }

        private int _stringposition;
        private int _fontSize = 10;
        public DisplayValues(Cube cube)
        {
            _cube = cube;
        }
        public void Draw(Graphics g)
        {
            DrawString(g, "x rotation: " + XRotation);
            DrawString(g, "y rotation: " + YRotation);
            DrawString(g, "z rotation: " + ZRotation);
            DrawString(g, "x position: " + XPosition);
            DrawString(g, "y position: " + YPosition);
            DrawString(g, "z position: " + ZPosition);
            DrawString(g, "d: " + Form1.d);
            DrawString(g, "phi: " + Form1.Phi);
            DrawString(g, "theta: " + Form1.Theta);
            DrawString(g, "Zoom: " + Form1.CameraDistance);
            _stringposition = 0;
        }

        private void DrawString(Graphics g,String text)
        {
            Font font = new Font("Arial", _fontSize);
            PointF p = new PointF(0, _stringposition);
            g.DrawString(text, font, Brushes.Red, p);
            _stringposition += _fontSize + 2;
        }
    }
}
