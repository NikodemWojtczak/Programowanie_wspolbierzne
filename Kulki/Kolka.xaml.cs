using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kulki
{
    public partial class Kolka : Window
    {
        private int move = 1;
        private List<Rectangle> rectangles = new List<Rectangle>();
        private static readonly Random random = new Random();

        public Kolka(int k)
        {
            InitializeComponent();
            Loaded += (s, e) => { InitializeRectangles(k); };
        }

        private void InitializeRectangles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                StartMovingRectangle();
            }
        }
        private void StartMovingRectangle()
        {
            var c = new SolidColorBrush(GetRandomColor());
            // Create the rectangle on the UI thread
            Rectangle rect = new Rectangle
            {
                RadiusX = 180,
                RadiusY = 180,
                Width = 60,
                Height = 60,
                Fill = c,
                Stroke =c,
            };

            // Get initial position for the rectangle

            // Use Dispatcher to add the rectangle to the canvas on the UI thread
            Dispatcher.Invoke(() =>
            {
            double x = random.NextDouble() * (MainCanvas.ActualWidth - rect.Width);
            double y = random.NextDouble() * (MainCanvas.ActualHeight - rect.Height);
                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
                MainCanvas.Children.Add(rect);
            });

            // Now start the thread for moving the rectangle
            Thread thread = new Thread(() =>
            {
                int dx = move * (random.Next(2) * 2 - 1);
         int dy = move *(random.Next(2) * 2 - 1);
                while (true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        double newX = Canvas.GetLeft(rect) + dx;
                        double newY = Canvas.GetTop(rect) + dy;

                        // Reverse direction upon hitting boundaries
                        if (newX <= 0 || newX >= MainCanvas.ActualWidth - rect.Width) dx = -dx;
                        if (newY <= 0 || newY >= MainCanvas.ActualHeight - rect.Height) dy = -dy;

                        Canvas.SetLeft(rect, newX + dx);
                        Canvas.SetTop(rect, newY + dy);
                    });
                    Thread.Sleep(20);
                }
            })
            { IsBackground = true }; // Mark the thread as a background thread
            thread.SetApartmentState(ApartmentState.STA);

            thread.Start();
        }


        public Color GetRandomColor()
        {
            return Color.FromArgb(255, (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
        }
    }















}
