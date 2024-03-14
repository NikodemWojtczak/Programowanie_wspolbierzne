using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Projekt.Models
{
    public class Kula
    {
        private static readonly Random random = new Random();
        public int RadiusX = 180;
        public int RadiusY = 180;
        public int Width;
        public int Height;
        public int boxSise;
        public SolidColorBrush Fill;
        public SolidColorBrush Stroke;
        public int mass;
        public double dx;
        public double dy;
        public double x;
        public double y;
        public int id;
        public Rectangle rect;

        public Kula(double x, double y, int id, int boxSieze)
        {
            this.id = id;
            this.boxSise = boxSieze;
            Width = boxSieze;
            Height = boxSieze;
            this.x = x;
            this.y = y;
            var c = new SolidColorBrush(GetRandomColor());
            this.dx =  random.Next(2) * 2 - 1;
            this.dy = random.Next(2) * 2 - 1;
            mass = random.Next(4) + 1;
            this.Fill = c;
            this.Stroke = c;

             rect = new Rectangle
            {
                RadiusX = this.RadiusX,
                RadiusY = this.RadiusY,
                Width = this.Width,
                Height = this.Height,
                Fill = this.Fill,
                Stroke = this.Stroke,
            };
        }

        public void updatePosition(double actualWidth, double actualHeight, double positionX, double positionY)
        {
            this.x = positionX + this.dx;
            this.y = positionY+ this.dy;

            if (x <= 0 || x >= actualWidth - Width) this.dx = -this.dx;
            if (y <= 0 || y >= actualHeight - Height) this.dy = -this.dy;

        }

        Color GetRandomColor()
        {
            return Color.FromArgb(255, (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
        }
    }
}
