using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Shannon
{
    public partial class mainFrm : Form
    {
        SFGraph maingraph = new SFGraph();
        public mainFrm()
        {
            InitializeComponent();
            nodecontrols = new ToolStripItem[] { addafter, addbefore, delete/*, pushdown, pushup*/ };
            edgecontrols = new ToolStripItem[] { deleteedge };
            OutputFormatter.LoadFiles();
        }
        ToolStripItem[] nodecontrols;
        ToolStripItem[] edgecontrols;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GUIManager.GetInstance().Draw(maingraph, e.Graphics);
            UpdateActionBar();
        }
        bool supressszchg = false;
        private const int MaxNodeCount = 40;

        private void mainFrm_SizeChanged(object sender, EventArgs e)
        {
            if (supressszchg)
            {
                supressszchg = false;
                return;
            }
            supressszchg = true;
            Size marg = new Size(Width - panel1.Right + panel1.Left, Height - panel1.Bottom + panel1.Top);
            Size = GUIManager.GetInstance().GetClosestValidSize(Size - marg) + marg;
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            GUIManager.GetInstance().Init(panel1);
            GUIManager.GetInstance().EdgeSelected += edgesel;
            GUIManager.GetInstance().ToolChanged += tch;
            GUIManager.GetInstance().SelectedTool = new CursorTool(GUIManager.GetInstance());
        }
        #region NodeControls
        private void addafter_Click(object sender, EventArgs e)
        {
            if (maingraph.Nodes.Count >= MaxNodeCount) NodeLimitReached();
            SFGNode n;
            if ((n = GUIManager.GetInstance().SelectedNodes.First()) != null)
                GUIManager.GetInstance().AddNodeAfter(n);
        }
        bool nlimdis = false;
        private void tch(GUITool newtool)
        {
            // Handle external tool reset
            if (newtool.GetType() == typeof(CursorTool))
            {
                foreach (ToolStripItem item in toolbar.Items)
                    if (item is ToolStripButton)
                        ((ToolStripButton)item).Checked = false;
                cursortool.Checked = true;
            }
            UpdateActionBar();
        }

        private void UpdateActionBar()
        {
            var st = GUIManager.GetInstance().SelectedTool;
            foreach (var c in nodecontrols)
                c.Visible = st.GetType() == typeof(CursorTool) &&
                    GUIManager.GetInstance().SelectedNodes.Count == 1;
            foreach (var c in edgecontrols)
                c.Visible = st.GetType() == typeof(EdgeTool) &
                    GUIManager.GetInstance().SelectedEdges.Count == 1;
        }

        private void NodeLimitReached()
        {
            if (!nlimdis)
                nlimdis = MessageBox.Show("Adding more than 40 nodes will make the solving time extremely long.\nDisable this warning for this session?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        private void addbefore_Click(object sender, EventArgs e)
        {
            if (maingraph.Nodes.Count >= MaxNodeCount) NodeLimitReached();
            SFGNode n;
            if ((n = GUIManager.GetInstance().SelectedNodes.First()) != null)
                GUIManager.GetInstance().AddNodeBefore(n);
        }
        private void delete_Click(object sender, EventArgs e)
        {
            SFGNode n;
            if ((n = GUIManager.GetInstance().SelectedNodes.First()) != null)
            {
                if (maingraph.RemoveNode(n))
                    panel1.Refresh();
                else
                    MessageBox.Show("Cannot delete start/end nodes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pushup_Click(object sender, EventArgs e)
        {
            SFGNode n;
            if ((n = GUIManager.GetInstance().SelectedNodes.First()) != null)
                GUIManager.GetInstance().ShiftNode(n, -1);
        }
        private void pushdown_Click(object sender, EventArgs e)
        {
            SFGNode n;
            if ((n = GUIManager.GetInstance().SelectedNodes.First()) != null)
                GUIManager.GetInstance().ShiftNode(n, 1);
        }
        private void deleteedge_Click(object sender, EventArgs e)
        {
            SFGEdge ed;
            if ((ed = GUIManager.GetInstance().SelectedEdges.First()) != null)
                GUIManager.GetInstance().DeleteEdge(ed);
        }
        #endregion

        #region Tools
        private void tools_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (ToolStripItem item in toolbar.Items)
                if (item is ToolStripButton)
                    ((ToolStripButton)item).Checked = false;
        }
        private void cursortool_Click(object sender, EventArgs e)
        {
            GUIManager.GetInstance().SelectedTool = new CursorTool(GUIManager.GetInstance());
        }

        private void selecttool_Click(object sender, EventArgs e)
        {
            GUIManager.GetInstance().SelectedTool = new EdgeTool(GUIManager.GetInstance());
        }
        private void edgesel(SFGNode n1, SFGNode n2, bool alone)
        {
            if (!alone || n1 == null || n2 == null) HideEdgeNamingField();
            else ShowEdgeNamingField();
        }

        private void HideEdgeNamingField()
        {
            edlabel.Visible = edname.Visible = ednameok.Visible = false;
        }

        private void ShowEdgeNamingField()
        {            
            string name = GUIManager.GetInstance().SelectedEdges.First().ToString();
            edlabel.Text = name;
            GUIManager.GetInstance().FormatEdgeName(ref name);
            edname.Text = name;
            edlabel.Visible = edname.Visible = ednameok.Visible = true;
        }

        private void startend_Click(object sender, EventArgs e)
        {
            GUIManager.GetInstance().SelectedTool = new SETool(panel1, GUIManager.GetInstance());
        }
        private void zoomin_Click(object sender, EventArgs e)
        {
            GUIManager.GetInstance().SelectedTool = new ZoomTool(panel1, GUIManager.GetInstance(), ZoomTool.ZoomType.In);
        }
        private void zoomout_Click(object sender, EventArgs e)
        {
            GUIManager.GetInstance().SelectedTool = new ZoomTool(panel1, GUIManager.GetInstance(), ZoomTool.ZoomType.Out);
        }
        #endregion

        private void solve_Click(object sender, EventArgs e)
        {
            SFGSolver solver = new SFGSolver(maingraph);
            SolutionMaestro.SolveAndDisplay(solver);
        }
        bool saved = false;
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
                FileManager.SaveToLastPath(maingraph);
            else
            {
                if (FileManager.Save(maingraph))
                    saved = true;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saved = false;
            maingraph = new SFGraph();
            GUIManager.GetInstance().ClearAllData();
            panel1.Refresh();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SFGraph g;
            if (FileManager.Open(out g))
            {
                saved = false;
                maingraph = g;
                GUIManager.GetInstance().ClearAllData();
                panel1.Refresh();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.Save(maingraph);
        }

        private void ednameok_Click(object sender, EventArgs e)
        {
            GUIManager.GetInstance().LabelSelectedEdge(edname.Text);
            panel1.Refresh();
        }

        private void connectForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maingraph.ConnectForward();
            panel1.Refresh();
        }
    }
}
