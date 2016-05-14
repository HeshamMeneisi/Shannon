using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Shannon
{
    internal class FileManager
    {
        static string lastpath = "temp.sfg";
        private static readonly string filter = "SFG File|*.sfg";

        public static void SaveToLastPath(SFGraph graph)
        {
            Save(graph, lastpath);
        }
        public static bool Save(SFGraph graph, string path = null)
        {
            if (path == null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = filter;
                if (dlg.ShowDialog() == DialogResult.OK) path = dlg.FileName;
                else return false;
            }
            graph.AssignIDs(); // Make sure nodes have ids
            List<object[]> data = new List<object[]>();
            data.Add(graph.EdgeLabels.ToArray());
            data.Add(new object[] { graph.Start.ID, graph.End.ID });
            foreach (var node in graph.Nodes)
                data.Add(node.Neighbours.Select(n => (object)n.ID).ToArray());
            using (var fs = File.Create(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, data);
            }
            return true;
        }
        public static bool Open(out SFGraph graph, string path = null)
        {
            try
            {
                if (path == null)
                {
                    OpenFileDialog od = new OpenFileDialog();
                    od.Filter = filter;
                    if (od.ShowDialog() == DialogResult.OK)
                        path = od.FileName;
                    else
                    {
                        graph = null;
                        return false;
                    }
                }
                List<object[]> data = new List<object[]>();
                using (var fs = File.OpenRead(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    data = (List<object[]>)bf.Deserialize(fs);
                }
                if (data.Count < 2)
                {
                    graph = null;
                    return false;
                }
                graph = new SFGraph();
                graph.Nodes.Clear();
                for (int j = 0; j < data.Count - 2; j++)
                    graph.Nodes.AddFirst(new SFGNode());
                graph.AssignIDs();
                var nar = graph.Nodes.ToArray();
                var en = data.GetEnumerator();
                en.MoveNext();
                graph.EdgeLabels = en.Current.Cast<EdgeLabel>();
                en.MoveNext();
                object[] se = en.Current;
                graph.Start = nar[(int)se[0] - 1];
                graph.End = nar[(int)se[1] - 1];
                int i = 0;
                while (en.MoveNext())
                {
                    var nbs = en.Current;
                    foreach (int nb in nbs)
                        nar[i].Neighbours.Add(nar[nb - 1]);
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open file." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                graph = null;
                return false;
            }
        }
    }
}