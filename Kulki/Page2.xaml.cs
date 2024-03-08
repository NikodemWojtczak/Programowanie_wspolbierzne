using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kulki
{
    /// <summary>
    /// Logika interakcji dla klasy Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            StartMovingRectangle();
        }
        private int dx = 1; // Change in X direction per move
        private int dy = 1; // Change in Y direction per move

        private void StartMovingRectangle()
        {
            Thread moveThread = new Thread(new ThreadStart(MoveRectangleLinearly));
            moveThread.IsBackground = true; // Ensure thread does not prevent application from closing
            moveThread.Start();
        }

        private void MoveRectangleLinearly()
        {
            while (true) // Infinite loop to keep the thread running
            {
                Dispatcher.Invoke(() =>
                {
                    double newX = Canvas.GetLeft(MovingRectangle) + dx;
                    double newY = Canvas.GetTop(MovingRectangle) + dy;

                    // Check for boundaries and reverse direction if needed
                    if (newX <= 0 || newX >= MainCanvas.ActualWidth - MovingRectangle.Width)
                    {
                        dx = -dx; // Reverse direction
                        newX = Math.Max(0, Math.Min(MainCanvas.ActualWidth - MovingRectangle.Width, newX));
                    }
                    if (newY <= 0 || newY >= MainCanvas.ActualHeight - MovingRectangle.Height)
                    {
                        dy = -dy; // Reverse direction
                        newY = Math.Max(0, Math.Min(MainCanvas.ActualHeight - MovingRectangle.Height, newY));
                    }

                    Canvas.SetLeft(MovingRectangle, newX);
                    Canvas.SetTop(MovingRectangle, newY);
                });

                Thread.Sleep(5); // Adjust the speed of movement here
            }
        }
    }

}

