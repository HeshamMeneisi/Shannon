using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shannon
{
    public class CursorTool : GUITool
    {
        const int DragSmoothingFactor = 150;
        int MinStep = Screen.PrimaryScreen.WorkingArea.Width / DragSmoothingFactor;
        bool dragging = false;
        Point lp = new Point(0, 0);
        public CursorTool(GUIManager m) : base(m)
        {
            m.WorkArea.MouseDown += mdown;
            m.WorkArea.MouseUp += mup;
            m.WorkArea.MouseMove += mmove;
            m.WorkArea.Parent.MouseWheel += wheel;
        }
        public override void Dispose()
        {
            parent.WorkArea.MouseDown -= mdown;
            parent.WorkArea.MouseUp -= mup;
            parent.WorkArea.MouseMove -= mmove;
            parent.WorkArea.Parent.MouseWheel -= wheel;
            parent.ClearSelection();
            parent.WorkArea.Refresh();
        }
        protected void wheel(object sender, MouseEventArgs e)
        {
            GUIManager.GetInstance().Scale += e.Delta / 500f;
            parent.WorkArea.Refresh();
        }
        SFGNode dn;
        protected void mdown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dn = parent.GetNodeAt(e.Location);
                lp = e.Location;
            }
        }

        protected void mup(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!dragging && parent.GetNodeAt(e.Location) == dn)
                    OnNodeSelected(dn);
                dragging = false;
            }
        }

        protected virtual void OnNodeSelected(SFGNode dn)
        {
            parent.ClearSelection();
            if (dn != null)
                parent.SelectedNodes.Add(dn);
            parent.WorkArea.Refresh();
        }

        protected void mmove(object sender, MouseEventArgs e)
        {
            if (!dragging && e.Button == MouseButtons.Left) dragging = true;
            int dx, dy;
            if (dragging && (Math.Abs(dx = e.Location.X - lp.X) > MinStep | Math.Abs(dy = e.Location.Y - lp.Y) > MinStep))
            {
                GUIManager.GetInstance().MoveGrid(dx, dy);
                lp = e.Location;
                parent.WorkArea.Refresh();
            }
        }
    }
}