using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shannon
{
    internal class Arrow
    {
        private PointF head;
        private PointF a, b;
        ArrowDir direction;

        public PointF Head { get { return head; } }
        public ArrowDir Direction { get { return direction; } }

        public Arrow(PointF head, ArrowDir dir, float width, float height)
        {
            this.head = head;
            direction = dir;
            float y1, y2, x1, x2;
            switch (dir)
            {
                case ArrowDir.Down:
                    y1 = y2 = head.Y - height;
                    x1 = head.X - width / 2;
                    x2 = head.X + width / 2;
                    break;
                case ArrowDir.Up:
                    y1 = y2 = head.Y + height;
                    x1 = head.X - width / 2;
                    x2 = head.X + width / 2;
                    break;
                case ArrowDir.Left:
                    x1 = x2 = head.X + height;
                    y1 = head.Y - width / 2;
                    y2 = head.Y + width / 2;
                    break;
                case ArrowDir.Right:
                    x1 = x2 = head.X - height;
                    y1 = head.Y - width / 2;
                    y2 = head.Y + width / 2;
                    break;
                default: throw new System.Exception("Invalid arrow type.");
            }
            a = new PointF(x1, y1);
            b = new PointF(x2, y2);
        }

        internal IEnumerable<PointF> GetPoints()
        {
            yield return a;
            yield return head;
            yield return b;
        }

        internal IEnumerable<PointF> GetPoints(PointF displacement)
        {
            yield return a.Add(displacement);
            yield return head.Add(displacement);
            yield return b.Add(displacement);
        }
    }
    public enum ArrowDir
    {
        Up, Down, Left, Right
    }
}