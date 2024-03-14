using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public class Zderzenie
    {
       public double u1x, u1y, u2x, u2y;
        public int id1, id2;

        public Zderzenie(Kula kula1, Kula kula2)
        {
            id1 = kula1.id; 
            id2 = kula2.id;

            double m1 = kula1.mass;
            double m2 = kula2.mass;
            double v1x = kula1.dx;
            double v1y = kula1.dy;
            double v2x = kula2.dx;
            double v2y = kula2.dy;

             u1x = (v1x * (m1 - m2) + (2 * m2 * v2x)) / (m1 + m2);
             u1y = (v1y * (m1 - m2) + (2 * m2 * v2y)) / (m1 + m2);

             u2x = (v2x * (m2 - m1) + (2 * m1 * v1x)) / (m1 + m2);
             u2y = (v2y * (m2 - m1) + (2 * m1 * v1y)) / (m1 + m2);
        }
    }
}
