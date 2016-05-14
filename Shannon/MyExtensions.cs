using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shannon
{
    public static class MyExtensions
    {
        public static Rectangle ToRectangle(this RectangleF rect)
        {
            return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }
        public static PointF Add(this PointF p, PointF p2, float sc = 1)
        {
            return new PointF((p.X + p2.X) * sc, (p.Y + p2.Y) * sc);
        }
    }
}
