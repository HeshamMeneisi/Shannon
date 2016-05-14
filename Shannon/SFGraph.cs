using System;
using System.Collections.Generic;

namespace Shannon
{
    public class SFGraph
    {
        SFGNode start, end;
        LinkedList<SFGNode> nodes = new LinkedList<SFGNode>();
        List<SFGPath> paths;
        List<SFGPath> cycles;
        Dictionary<string, string> edgelabels = new Dictionary<string, string>();

        public Dictionary<string, string> LabelDictionary
        {
            get { return edgelabels; }
        }
        public IEnumerable<EdgeLabel> EdgeLabels
        {
            get
            {
                foreach (string key in edgelabels.Keys)
                    yield return new EdgeLabel(key, edgelabels[key]);
            }
            set
            {
                edgelabels.Clear();
                foreach (EdgeLabel l in value)
                {
                    edgelabels.Add(l.Edgename, l.Label);
                }
            }
        }
        public SFGraph()
        {
            start = new SFGNode();
            end = new SFGNode();
            nodes.AddFirst(start);
            nodes.AddLast(end);
            NotifyChanged();
        }
        public bool Changed { get; private set; }

        public LinkedList<SFGNode> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                NotifyChanged();
                nodes = value;
            }
        }

        public SFGNode Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
                NotifyChanged();
            }
        }

        public SFGNode End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
                NotifyChanged();
            }
        }

        public void AddNode(SFGNode node)
        {
            NotifyChanged();
            nodes.AddLast(node);
        }

        public void Unmark()
        {
            Changed = false;
        }
        public List<SFGPath> GetForwardPaths()
        {
            if (paths != null) return paths;
            paths = new List<SFGPath>();
            if (nodes.Count == 0) return null;
            paths = start.GetAllPathsTo(end);
            int ct = 1;
            foreach (var path in paths)
                path.ID = ct++;
            return paths;
        }
        public void AssignIDs()
        {
            int i = 1;
            foreach (SFGNode node in Nodes)
                node.ID = i++;
        }
        public List<SFGPath> GetAllCycles()
        {
            if (cycles != null) return cycles;
            cycles = new List<SFGPath>();
            if (nodes.Count == 0) return null;
            cycles.Clear();
            int ct = 1;
            foreach (SFGNode n in nodes)
            {
                foreach (SFGNode nb in n.Neighbours)
                {
                    var tmp = nb.GetAllPathsTo(n);
                    // Close loops                   
                    if (tmp != null)
                    {
                        foreach (SFGPath p in tmp)
                        {
                            p.Nodes.AddFirst(p.Nodes.Last.Value);
                            p.ID = ct++;
                        }
                        cycles.AddRange(tmp);
                    }
                }
                n.MarkExcluded();
            }
            foreach (SFGNode n in nodes)
                n.UnmarkExcluded();
            return cycles;
        }
        public bool RemoveNode(SFGNode n)
        {
            if (n == start || n == end) return false;
            nodes.Remove(n);
            foreach (var node in nodes)
                node.Neighbours.Remove(n);
            NotifyChanged();
            return true;
        }
        public void NotifyChanged()
        {
            paths = cycles = null;
            Changed = true;
            SolutionMaestro.NotifyChanged(this);
        }

        internal void ConnectForward()
        {
            var f = nodes.First;
            while (f.Next != null)
            {
                if (!f.Value.Neighbours.Contains(f.Next.Value))
                    f.Value.Neighbours.Add(f.Next.Value);
                f = f.Next;
            }
            NotifyChanged();
        }
    }
}