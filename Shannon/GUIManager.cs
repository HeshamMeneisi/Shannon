using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Shannon
{
    public class GUIManager
    {
        static GUIManager instance = null;
        private Panel workarea;
        private GUITool selectedtool;
        public float MaxScale = 4;
        public float MinScale = 0.25f;
        float width = 10;
        float height = 10;
        float scale = 1;
        bool supressredraw = false;
        PointF displacement = new PointF(0, 0);
        Dictionary<SFGNode, PointF> nodecache = new Dictionary<SFGNode, PointF>();
        List<SFGEdge> edgecache = new List<SFGEdge>();
        SFGraph graphld = null;
        bool propchged = false;
        HashSet<SFGNode> selectednodes = new HashSet<SFGNode>();
        List<SFGEdge> selectededges = new List<SFGEdge>();

        // TODO: Move fields to properties
        #region Properties
        Pen GridPen = Pens.Blue;
        Pen NodePen = Pens.Cyan;
        Brush LabelBrush = Brushes.Cyan;
        Brush SBrush = Brushes.Green;
        Brush EBrush = Brushes.Orange;
        Pen Selection = new Pen(Color.White) { Width = 4, DashPattern = new float[] { 0.2f, 1 } };
        Pen FREdgePen = Pens.Green;
        Pen FBEdgePen = Pens.Red;
        Brush NIDBrush = Brushes.Fuchsia;
        Font NIDFont = new Font("Arial", 10);
        private Brush ArrowBrush = Brushes.White;
        int EdgeSpacing = 25;
        float GridSpacing = 80;
        int NodeDiameter = 30;
        #endregion
        internal void CreateSelectEdge(SFGNode n1, SFGNode n2)
        {
            if (!SelectEdge(n1, n2))
            {
                n1.Neighbours.Add(n2);
                graphld.NotifyChanged();
                propchged = true;
                CacheData(graphld);
                SelectEdge(n1, n2);
            }
        }

        private void Redraw()
        {
            if (!supressredraw)
                workarea.Refresh();
        }

        public bool SelectEdge(SFGNode n1, SFGNode n2, bool alone = true)
        {
            if (n1.Neighbours.Contains(n2))
            {
                if (alone)
                    selectededges.Clear();
                var found = edgecache.Find(e => e.A == n1 && e.B == n2);
                if (found != null)
                    selectededges.Add(found);
                else
                    throw new Exception("Cache corruption detected.");
                Redraw();
                OnEdgeSelected(n1, n2, alone);
                return true;
            }
            return false;
        }

        internal bool FormatEdgeName(ref string name)
        {
            if (graphld.LabelDictionary.ContainsKey(name)) { name = graphld.LabelDictionary[name]; return true; }
            return false;
        }

        private void OnEdgeSelected(SFGNode n1, SFGNode n2, bool alone)
        {
            EdgeSelected(n1, n2, alone);
        }

        internal void Init(Panel panel)
        {
            workarea = panel;
        }

        public static GUIManager GetInstance()
        {
            if (instance != null) return instance;
            return instance = new GUIManager();
        }
        float ScaledArcSpacing
        {
            get { return EdgeSpacing * scale; }
        }
        float ScaledWidth
        {
            get { return width * GridSpacing * Scale; }
        }
        float ScaledHeight
        {
            get { return height * GridSpacing * Scale; }
        }
        float ScaledGridSpacing
        {
            get { return GridSpacing * Scale; }
        }

        public float Scale
        {
            get
            {
                return scale;
            }

            set
            {
                if (value < MaxScale && value > MinScale)
                {
                    scale = value;
                    propchged = true;
                }
            }
        }

        public float ScaledNodeD
        {
            get
            {
                return NodeDiameter * scale;
            }
        }

        public SFGraph LastDrawn
        {
            get
            {
                return graphld;
            }
        }

        public HashSet<SFGNode> SelectedNodes
        {
            get
            {
                return selectednodes;
            }
        }

        public GUITool SelectedTool
        {
            get
            {
                return selectedtool;
            }

            set
            {
                if (selectedtool != null) selectedtool.Dispose();
                selectedtool = value;
                OnToolChanged();
            }
        }

        private void OnToolChanged()
        {
            if (ToolChanged != null)
                ToolChanged(selectedtool);
        }

        internal void HighlightPath(SFGPath path)
        {
            ClearSelection();
            var n = path.Nodes.First;
            supressredraw = true;
            while (n.Next != null)
            {
                SelectEdge(n.Value, n.Next.Value, false);
                selectednodes.Add(n.Value);
                n = n.Next;
            }
            selectednodes.Add(n.Value);
            supressredraw = false;
            Redraw();
        }
        public void ClearSelection()
        {
            selectednodes.Clear();
            selectededges.Clear();
            OnEdgeSelected(null, null, true);
        }
        public Panel WorkArea
        {
            get
            {
                return workarea;
            }
        }

        internal List<SFGEdge> SelectedEdges
        {
            get
            {
                return selectededges;
            }
        }

        public Action<GUITool> ToolChanged { get; set; }
        public Action<SFGNode, SFGNode, bool> EdgeSelected { get; internal set; }

        internal void AddNodeAfter(SFGNode n)
        {
            graphld.Nodes.AddAfter(graphld.Nodes.Find(n), new SFGNode());
            graphld.NotifyChanged();
            Redraw();
        }

        public void MoveGrid(float xd, float yd)
        {
            displacement = new PointF(displacement.X + xd, displacement.Y + yd);
        }

        internal void AddNodeBefore(SFGNode n)
        {
            graphld.Nodes.AddBefore(graphld.Nodes.Find(n), new SFGNode());
            graphld.NotifyChanged();
            Redraw();
        }

        internal void DeleteEdge(SFGEdge e)
        {
            ClearSelection();
            string name = e.ToString();
            if (graphld.LabelDictionary.ContainsKey(name)) graphld.LabelDictionary.Remove(name);
            e.A.Neighbours.Remove(e.B);
            graphld.NotifyChanged();
            Redraw();
        }

        public void Draw(SFGraph ng, Graphics graphics)
        {
            graphld = ng;
            width = ng.Nodes.Count + 1;
            CacheData(ng);
            graphics.Clear(Color.Black);
            DrawGrid(graphics);
            DrawNodes(graphics);
            DrawEdges(graphics);
            DrawOverlay(graphics);
        }

        void CacheData(SFGraph ng)
        {
            if (!ng.Changed && !propchged)
                return;
            ng.Unmark();
            propchged = false;
            ng.AssignIDs();
            nodecache.Clear();
            foreach (SFGNode node in ng.Nodes)
                nodecache[node] = GetNodePos(node);
            edgecache.Clear();
            float r = ScaledNodeD / 2;
            float circr = ScaledNodeD / 6;
            float disp = (r + circr) / (float)Math.Sqrt(2);
            float disp2 = r / (float)Math.Sqrt(2);
            float ad = ScaledNodeD / 5;
            int iup = 0, idw = 0;
            List<SFGEdge> edges = new List<SFGEdge>();
            foreach (SFGNode a in ng.Nodes)
                foreach (SFGNode b in a.Neighbours)
                    edges.Add(new SFGEdge(a, b));
            foreach (var e in edges.OrderBy(e => e.Length).ToList())
            {
                bool up = false;
                var pa = nodecache[e.A];
                var pb = nodecache[e.B];
                // Handle edge type
                if (e.Length == 1 && !e.IsFeedback)
                {
                    e.AddPoint(pa.X + r, pa.Y);
                    e.AddPoint(pb.X - r, pb.Y);
                    e.Arrow = new Arrow(e.DefPoints[1], ArrowDir.Right, ad, ad);
                }
                else if (e.IsLoop)
                {
                    e.AddPoint(pa.X + disp - circr, pa.Y - disp - circr);
                    e.AddPoint(pa.X + disp2 - circr / 2, pa.Y - disp2 - circr / 2);
                }
                else
                {
                    if (up = !e.IsFeedback)
                        iup++;
                    else idw++;
                    pa = new PointF(pa.X, pa.Y + (up ? -r : r));
                    pb = new PointF(pb.X, pb.Y + (up ? -r : r));
                    e.AddPoint(pa);
                    float c = pa.Y == pb.Y ? 3 : 10;
                    // C1
                    e.AddPoint(pa.X + (pb.X - pa.X) / c, pa.Y + (up ? -iup : idw) * ScaledArcSpacing);
                    // C2
                    e.AddPoint(pa.X + (pb.X - pa.X) / c * (c - 1), pa.Y + (up ? -iup : idw) * ScaledArcSpacing);
                    e.AddPoint(pb);
                    float ax = (pb.X + pa.X) / 2;
                    float ay = pa.Y + (e.DefPoints[2].Y - pa.Y) * 0.75f;
                    e.Arrow = new Arrow(new PointF(ax, ay), e.IsFeedback ? ArrowDir.Left : ArrowDir.Right, ad, ad);
                }
                edgecache.Add(e);
            }
            selectededges.Clear();
        }

        internal void LabelSelectedEdge(string label)
        {
            if (selectededges.Count == 1)
                graphld.LabelDictionary[selectededges.First().ToString()] = label;
        }
        public void ClearAllData()
        {
            ClearSelection();
            graphld.LabelDictionary.Clear();
            nodecache.Clear();
            edgecache.Clear();
            propchged = true;
        }
        internal void ShiftNode(SFGNode n, int v)
        {
            n.VShift += v;
            propchged = true;
            if (Math.Abs(n.VShift) > height / 2) height += 2;
            Redraw();
        }

        void DrawNodes(Graphics graphics)
        {
            float r = ScaledNodeD / 2;
            graphics.FillEllipse(SBrush, nodecache[graphld.Start].X + displacement.X - r, nodecache[graphld.Start].Y + displacement.Y - r, ScaledNodeD, ScaledNodeD);
            graphics.FillEllipse(EBrush, nodecache[graphld.End].X + displacement.X - r, nodecache[graphld.End].Y + displacement.Y - r, ScaledNodeD, ScaledNodeD);
            foreach (SFGNode node in nodecache.Keys)
                graphics.DrawEllipse(NodePen, nodecache[node].X + displacement.X - r, nodecache[node].Y + displacement.Y - r, ScaledNodeD, ScaledNodeD);
        }
        void DrawEdge(SFGEdge edge, Graphics graphics, Pen epen)
        {
            float r = ScaledNodeD / 2;
            float circd = ScaledNodeD / 3;
            var pen = epen != null ? epen : edge.IsLoop ? Pens.White : edge.IsFeedback ? FBEdgePen : FREdgePen;
            if (edge.Length == 1 && !edge.IsFeedback)
            {
                if (edge.DefPoints.Count < 2)
                    throw new Exception("Cache corruption detected.");
                graphics.DrawLine(pen, edge.DefPoints[0].Add(displacement), edge.DefPoints[1].Add(displacement));
            }
            else if (edge.IsLoop)
            {
                if (edge.DefPoints.Count < 2)
                    throw new Exception("Cache corruption detected.");
                var p = edge.DefPoints[0].Add(displacement);
                graphics.DrawEllipse(pen, p.X, p.Y, circd, circd);
                p = edge.DefPoints[1].Add(displacement);
                graphics.FillEllipse(Brushes.Crimson, p.X, p.Y, circd / 2, circd / 2);
            }
            else
            {
                if (edge.DefPoints.Count < 4)
                    throw new Exception("Cache corruption detected.");
                graphics.DrawBezier(pen, edge.DefPoints[0].Add(displacement), edge.DefPoints[1].Add(displacement), edge.DefPoints[2].Add(displacement), edge.DefPoints[3].Add(displacement));
            }
            if (edge.Arrow != null)
                DrawArrow(edge.Arrow, graphics);
        }

        private void DrawArrow(Arrow arrow, Graphics graphics)
        {
            graphics.FillPolygon(ArrowBrush, arrow.GetPoints(displacement).ToArray());
        }

        void DrawEdges(Graphics graphics)
        {
            foreach (SFGEdge edge in edgecache)
            {
                // Handle edge type
                DrawEdge(edge, graphics, null);
            }
        }
        void DrawOverlay(Graphics graphics)
        {
            var font = new Font(NIDFont.FontFamily, NIDFont.Size * scale, NIDFont.Style);
            foreach (SFGNode node in nodecache.Keys)
            {
                //graphics.FillEllipse(NodeCenterBrush, center.X - ScaledNodeD * 0.1f, center.Y - ScaledNodeD * 0.1f, ScaledNodeD / 5, ScaledNodeD / 5);
                SizeF disp = graphics.MeasureString(node.ASCID, font);
                disp = new SizeF(disp.Width / 2 - displacement.X, disp.Height / 2 - displacement.Y);
                graphics.DrawString(node.ASCID, font, NIDBrush, nodecache[node] - disp);
            }
            foreach (SFGEdge edge in edgecache)
            {
                string name = edge.ToString();
                if (FormatEdgeName(ref name))
                {
                    var sz = graphics.MeasureString(name, NIDFont);
                    PointF p;
                    if (edge.Length == 1 && !edge.IsFeedback)
                        p = nodecache[edge.A].Add(nodecache[edge.B], 0.5f).Add(displacement).Add(new PointF(-sz.Width / 2, -sz.Height / 2));
                    else if (edge.IsLoop)
                        p = nodecache[edge.A].Add(new PointF(0, -ScaledNodeD / 2)).Add(displacement);
                    else
                    {
                        p = edge.Arrow.Head.Add(displacement);
                        if (edge.Arrow.Direction == ArrowDir.Left)
                            p = p.Add(new PointF(-sz.Width, -sz.Height));
                    }
                    graphics.DrawString(name, NIDFont, LabelBrush, p);
                }
            }
            List<SFGNode> to_rm = new List<SFGNode>();
            foreach (var selectednode in selectednodes)
            {
                if (!nodecache.Keys.Contains(selectednode))
                    to_rm.Add(selectednode);
                else
                {
                    float r = ScaledNodeD / 2;
                    graphics.DrawEllipse(Selection, nodecache[selectednode].X + displacement.X - r, nodecache[selectednode].Y + displacement.Y - r, ScaledNodeD, ScaledNodeD);
                }
            }
            foreach (var selectededge in selectededges)
                DrawEdge(selectededge, graphics, Selection);
            foreach (var n in to_rm) selectednodes.Remove(n);
        }

        PointF GetNodePos(SFGNode node)
        {
            int i = node.ID;
            return new PointF(i * ScaledGridSpacing, ScaledHeight / 2 + ScaledGridSpacing * node.VShift);
        }

        public void DrawGrid(Graphics graphics)
        {
            for (int i = 0; i <= width; i++)
                graphics.DrawLine(GridPen, i * ScaledGridSpacing + displacement.X, displacement.Y, i * ScaledGridSpacing + displacement.X, ScaledHeight + displacement.Y);
            for (int i = 0; i <= height; i++)
                graphics.DrawLine(GridPen, displacement.X, i * ScaledGridSpacing + displacement.Y, ScaledWidth + displacement.X, i * ScaledGridSpacing + displacement.Y);
        }

        public Size GetClosestValidSize(Size size)
        {
            return new Size((int)(Math.Floor(size.Width / GridSpacing) * GridSpacing), (int)(Math.Floor(size.Height / GridSpacing) * GridSpacing));
        }

        internal SFGNode GetNodeAt(Point location)
        {
            float r = ScaledNodeD / 2;
            foreach (var node in nodecache.Keys)
            {
                var p = nodecache[node];
                if (Math.Pow(p.X - location.X + displacement.X, 2) + Math.Pow(p.Y - location.Y + displacement.Y, 2) <= Math.Pow(r, 2)) return node;
            }
            return null;

        }
    }
}