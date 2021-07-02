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

        public float Size { get => _cube.Size; }

        private int _stringposition;
        private int _fontSize = 10;
        public DisplayValues(Cube cube)
        {
            _cube = cube;
        }
        public void Draw(Graphics g)
        {
            DrawString(g, "x rotation: " + XRotation + " X / x");
            DrawString(g, "y rotation: " + YRotation + " Y / y");
            DrawString(g, "z rotation: " + ZRotation + " Z / z");
            DrawString(g, "cube size: " + (Size * 2) + " S / s");
            DrawString(g, "x position: " + XPosition + " Left / Right");
            DrawString(g, "y position: " + YPosition + " Up / Down");
            DrawString(g, "z position: " + ZPosition + " PgDn / PgUp");
            DrawString(g, "d: " + Form1.d + " D / d");
            DrawString(g, "phi: " + Form1.phi_degrees + " P / p");
            DrawString(g, "theta: " + Form1.Theta + " T / t");
            DrawString(g, "r: " + Form1.r + " + / -");
            DrawString(g, "Animation: A");
            DrawString(g, "Reset: C");
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
