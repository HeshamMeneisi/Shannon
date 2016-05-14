using System.Windows.Forms;

namespace Shannon
{
    internal class SETool : EdgeTool
    {
        public SETool(Panel p, GUIManager m) : base(m)
        { }
        protected override void OnPairSelected(SFGNode n1, SFGNode n2)
        {
            parent.LastDrawn.Start = n1;
            parent.LastDrawn.End = n2;
        }
    }
}