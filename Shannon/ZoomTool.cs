using System;
using System.Windows.Forms;

namespace Shannon
{
    internal class ZoomTool : GUITool
    {
        ZoomType type;
        const float sens = 0.1f;
        public ZoomTool(Panel p, GUIManager m, ZoomType t) : base(m)
        {
            type = t;
            p.MouseDown += mdown;
        }

        private void mdown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                parent.Scale *= (1 + sens * (type == ZoomType.Out ? -1 : 1));
                parent.WorkArea.Refresh();
            }
        }

        public override void Dispose()
        {
            parent.WorkArea.MouseDown -= mdown;
        }

        public enum ZoomType { In, Out }
    }
}