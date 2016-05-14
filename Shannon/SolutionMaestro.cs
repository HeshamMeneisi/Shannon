using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shannon
{
    static class SolutionMaestro
    {
        static Form activeform;
        static SFGSolver latestsolver;
        private static bool gchanged;

        public static void SolveAndDisplay(SFGSolver solver)
        {
            gchanged = false;
            latestsolver = solver;
            StringBuilder html = new StringBuilder();
            html.Append("<input type=\"button\" onClick = \"window.print()\" value = \"Print Solution\"/><br/>");
            solver.Solve();
            if (solver.Paths.Count == 0)
                html.Append("No forward paths could be found (<font color=\"#FF0000\">Open Circuit</font>): <b>TF = 0</b>");
            else
            {
                StringBuilder als = new StringBuilder();
                #region Symbols
                #region Paths
                foreach (var path in solver.Paths)
                    als.Append(path.HTML + " = " + path.GetValue() + "<br />");
                als.Append("<br />");
                #endregion
                #region Cycles
                foreach (var cycle in solver.Cycles)
                    als.Append(cycle.HTML + " = " + cycle.GetValue() + "<br />");
                als.Append("<br />");
                #endregion
                html.Append(OutputFormatter.GetCollapsable(als.ToString(), "<font color=#ff0000>Symbol Definitions:</font><br/>"));
                html.Append("<br/>");
                #endregion
                #region Analysis
                int c = 2;
                if (solver.NonTouchingCycles.Count > 0)
                {
                    als.Clear();
                    foreach (var ntlcs in solver.NonTouchingCycles)
                    {
                        als.Append((c++) + " Non-touching cycles: <br/>");
                        foreach (var ntcg in ntlcs)
                        {
                            foreach (var ntc in ntcg)
                            {
                                als.Append(ntc.HTML);
                            }
                            als.Append("<br/>");
                        }
                    }
                    html.Append(OutputFormatter.GetCollapsable(als.ToString(), "<font color=#ff0000>Loop Analysis:</font><br/>"));
                    html.Append("<br/>");
                }
                #endregion
                #region Deltas
                als.Clear();
                c = 1;
                als.Append("&#916; = " + solver.GetDelta() + "<br /><br />");
                foreach (var delta in solver.GetPDeltas())
                    als.Append("&#916;<sub>" + c++ + "</sub> = " + delta + "<br />");
                html.Append(OutputFormatter.GetCollapsable(als.ToString(), "<font color=#ff0000>Deltas:</font><br/>"));
                html.Append("<br/>");
                #endregion
                #region TF
                string d = "&#916;";
                StringBuilder e = new StringBuilder();
                for (int i = 1; i <= solver.Paths.Count; i++)
                {
                    e.Append("(" + solver.Paths[i - 1].HTML + " * &#916;<sub>" + i + "</sub>)");
                    if (i < solver.Paths.Count) e.Append(" + ");
                }
                html.Append(OutputFormatter.GetXEQFrc("TF", e.ToString(), d));
                html.Append("<br />");
                #endregion
            }
            string fhtm = html.ToString();
            activeform = new solFrm(fhtm, SolMessage);
            activeform.Show();
        }

        private static void SolMessage(Form sender, string m)
        {
            GUIManager.GetInstance().SelectedTool = new CursorTool(GUIManager.GetInstance());
            if (sender != activeform || gchanged)
            {
                MessageBox.Show("This obsolete solution cannot reference current graph.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (latestsolver == null) throw new Exception("Alien message received.");
            string query = m.TrimStart('?');
            string[] data = query.Split(':');
            string t;
            int i1, i2 = 0;
            if (data.Length < 2) InvalidQuery();
            else
            {
                t = data[0];
                data = data[1].Split(',');
                if (t == "e" && data.Length < 2) InvalidQuery();
                else
                {
                    if (!int.TryParse(data[0], out i1) | (t == "e" && !int.TryParse(data[1], out i2)))
                        InvalidQuery();
                    else
                        switch (t)
                        {
                            case "e":
                                var nodes = latestsolver.Graph.Nodes;
                                int nc = nodes.Count;
                                if (i1 <= nc && i2 <= nc)
                                {
                                    var n1 = nodes.ElementAt(i1 - 1);
                                    var n2 = nodes.ElementAt(i2 - 1);
                                    if (!n1.Neighbours.Contains(n2)) InvalidData();
                                    else
                                        GUIManager.GetInstance().SelectEdge(n1, n2);
                                }
                                else InvalidData();
                                break;
                            case "p":
                                if (i1 <= latestsolver.Paths.Count)
                                    GUIManager.GetInstance().HighlightPath(latestsolver.Paths.ElementAt(i1 - 1));
                                else InvalidData();
                                break;
                            case "c":
                                if (i1 <= latestsolver.Cycles.Count)
                                    GUIManager.GetInstance().HighlightPath(latestsolver.Cycles.ElementAt(i1 - 1));
                                else InvalidData();
                                break;
                        }
                }
            }
        }

        internal static void NotifyChanged(SFGraph graph)
        {
            gchanged = true;
        }

        private static void InvalidData()
        {
            MessageBox.Show("Invalid selection data, the graph might have changed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void InvalidQuery()
        {
            MessageBox.Show("Invalid query generated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
