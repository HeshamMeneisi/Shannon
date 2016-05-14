using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shannon
{
    class SFGEdge
    {
        SFGNode a, b;
        List<PointF> defpoints = new List<PointF>();
        public SFGEdge(SFGNode a, SFGNode b)
        {
            this.a = a;
            this.b = b;
        }

        public SFGNode A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public SFGNode B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public int Length { get { return Math.Abs(b.ID - a.ID); } }
        public bool IsFeedback { get { return a.ID > b.ID; } }

        public bool IsLoop
        {
            get { return a == b; }
        }

        public List<PointF> DefPoints
        {
            get
            {
                return defpoints;
            }

            set
            {
                defpoints = value;
            }
        }

        public Arrow Arrow { get; internal set; }

        internal void AddPoint(float v1, float v2)
        {
            defpoints.Add(new PointF(v1, v2));
        }

        internal void AddPoint(PointF p)
        {
            defpoints.Add(p);
        }

        public override string ToString()
        {
            return a.ASCID + b.ASCID;
        }
    }
}
