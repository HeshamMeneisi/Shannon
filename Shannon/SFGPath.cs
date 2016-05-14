using System;
using System.Collections.Generic;
using System.Text;

namespace Shannon
{
    public class SFGPath
    {
        Dictionary<SFGPath, bool> intersectioncach = new Dictionary<SFGPath, bool>();
        LinkedList<SFGNode> nodes = new LinkedList<SFGNode>();
        public SFGPath(SFGNode nnode = null, SFGPath ext = null)
        {
            if (nnode != null)
                nodes.AddFirst(nnode);
            if (ext != null)
            {
                foreach (SFGNode node in ext.nodes)
                    nodes.AddLast(node);
            }
        }

        public LinkedList<SFGNode> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
            }
        }

        public int ID { get; set; }
        public string HTML
        {
            get
            {
                return nodes.Count >= 2 && nodes.First.Value == nodes.Last.Value ?
                    "<a href =\"?c:" + ID + "\">L<sub>" + ID + "</sub></a>"
                    : "<a href =\"?p:" + ID + "\">P<sub>" + ID + "</sub></a>";
            }
        }

        internal bool Intersects(SFGPath cb)
        {
            if (cb == null) return false;
            if (intersectioncach.ContainsKey(cb)) return intersectioncach[cb];
            foreach (SFGNode n in nodes)
            {
                if (cb.nodes.Contains(n))
                    return intersectioncach[cb] = true;
            }
            return intersectioncach[cb] = false;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SFGNode n in nodes)
                sb.Append(n.ASCID + ",");
            return "{" + sb.ToString().Trim(',') + "}";
        }
        string val = null;
        public string GetValue()
        {
            if (val != null) return val;
            if (nodes.Count < 2) return "";
            StringBuilder sb = new StringBuilder();
            var n = nodes.First;
            string name = "";
            while (n.Next != null)
            {
                name = n.Value.ASCID + n.Next.Value.ASCID;
                GUIManager.GetInstance().FormatEdgeName(ref name);
                sb.Append("<a href=\"?e:" + n.Value.ID + "," + n.Next.Value.ID + "\">" + name + "</a>");
                n = n.Next;
                if (n.Next != null) sb.Append("*");
            }
            return val = sb.ToString();
        }
        public void ResetCaching()
        {
            intersectioncach.Clear();
        }
        internal bool IntersectsAny(List<SFGPath> paths)
        {
            foreach (var c in paths)
                if (Intersects(c))
                    return true;
            return false;
        }
    }
}