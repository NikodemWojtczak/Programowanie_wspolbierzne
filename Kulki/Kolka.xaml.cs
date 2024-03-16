using Projekt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kulki
{
    public partial class Kolka : Window
    {
        List<Kula> kulki = new List<Kula>();
        List<Zderzenie> zderzenia = new List<Zderzenie>();

        int kulkaSize = 30;

        public Kolka(int k)
        {
            InitializeComponent();
            Loaded += (s, e) => { InitializeRectangles(k); };
        }

        private void InitializeRectangles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                double x, y;
                while (true)
                {
                    bool isRunning = false;
                    Random random = new Random();
                    x = random.NextDouble() * (MainCanvas.ActualWidth - kulkaSize);
                    y = random.NextDouble() * (MainCanvas.ActualHeight - kulkaSize);
                    if (i == 0) break;
                    foreach (var item in kulki)
                    {
                        isRunning = true;
                        double dy = Math.Abs(item.y - y);
                        double dx = Math.Abs(item.x - x);
                        if (dx <= 2 * kulkaSize && dy <= 2 * kulkaSize)
                        {
                            isRunning = false;
                            break;
                        }
                    }
                    if (!isRunning) continue;
                    break;
                }

                Kula kula = new Kula(x, y, i, kulkaSize);
                kulki.Add(kula);

                Dispatcher.Invoke(() =>
                {
                    Canvas.SetLeft(kula.rect, kula.x);
                    Canvas.SetTop(kula.rect, kula.y);
                    MainCanvas.Children.Add(kula.rect);
                });

                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        czyDoszloDoZderzenia(kula.id);
                        Dispatcher.Invoke(() =>
                        {
                            kula.updatePosition(MainCanvas.ActualWidth, MainCanvas.ActualHeight, Canvas.GetLeft(kula.rect),
                            Canvas.GetTop(kula.rect));
                            Canvas.SetLeft(kula.rect, kula.x);
                            Canvas.SetTop(kula.rect, kula.y);
                        });

                        Thread.Sleep(5);
                    }
                })
                { IsBackground = true };
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }

        private readonly object _lockObject = new object();
        void czyDoszloDoZderzenia(int id)
        {
            lock (_lockObject)
            {
                Kula kula1 = kulki[id];

                for (int i = 0; i <= zderzenia.Count - 1; i++)
                {
                    Zderzenie zderzenie = zderzenia[i];
                    if (kula1.id == zderzenie.id2)
                    {
                        kula1.dx = zderzenie.u2x;
                        kula1.dy = zderzenie.u2y;
                        zderzenia.Remove(zderzenie);
                        return;
                    }
                }
                foreach (Kula k in kulki)
                {
                    if (k.id == id) continue;

                    double dx = k.x - kula1.x;
                    double dy = k.y - kula1.y;
                    dx = Math.Abs(dx);
                    dy = Math.Abs(dy);
                    double distance = Math.Sqrt(dx * dx + dy * dy);
                    double minDistance = 2 * Math.Sqrt((kula1.Width / 2 / 1.4) * (kula1.Width / 2 / 1.4) + (k.Width / 2 / 1.4) * (k.Width / 2 / 1.4));

                    if (distance <= minDistance)
                    {
                        Zderzenie zderzenie = new Zderzenie(kula1, k);
                        kula1.dx = zderzenie.u1x;
                        kula1.dy = zderzenie.u1y;
                        zderzenia.Add(zderzenie);
                        LogCollisionData("Nastąpiła kolizja: " + zderzenie.ToString());
                        return;
                    }

                }
            }
        }

        public void LogCollisionData(string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("collisions.json", true))
                {
                    writer.WriteLine(data);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Błąd zapisu do pliku: " + ex.Message);
            }
        }
    }
}
