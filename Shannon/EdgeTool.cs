using System;
using System.Windows.Forms;

namespace Shannon
{
    public class EdgeTool : CursorTool
    {
        public EdgeTool(GUIManager m) : base(m)
        { }

        public override void Dispose()
        {
            n1 = n2 = null;
            parent.ClearSelection();
            parent.WorkArea.Refresh();
            base.Dispose();
        }
        SFGNode n1, n2;

        public Action<SFGNode, SFGNode, bool> EdgeSelected { get; internal set; }

        protected override void OnNodeSelected(SFGNode dn)
        {
            if (n1 == null)
            {
                if ((n1 = dn) != null)
                {
                    parent.SelectedNodes.Add(n1);
                    parent.WorkArea.Refresh();
                }
            }
            else
            {
                if ((n2 = dn) != null)
                {
                    OnPairSelected(n1, n2);                    
                    n1 = n2 = null;
                    parent.SelectedNodes.Clear();
                    parent.WorkArea.Refresh();
                }
            }
        }

        protected virtual void OnPairSelected(SFGNode n1, SFGNode n2)
        {
            parent.CreateSelectEdge(n1, n2);
        }
    }
}