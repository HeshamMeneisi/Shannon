using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shannon
{
    class SFGSolver
    {
        List<SFGPath> fpaths = new List<SFGPath>();
        List<SFGPath> cycles = new List<SFGPath>();
        List<List<HashSet<SFGPath>>> ntcycles;
        SFGraph graph;
        public SFGSolver(SFGraph g)
        {
            graph = g;
            fpaths = graph.GetForwardPaths();
            cycles = graph.GetAllCycles();
            ntcycles = new List<List<HashSet<SFGPath>>>();
        }
        bool solved = false;

        public List<SFGPath> Paths
        {
            get
            {
                return fpaths;
            }
        }

        public List<SFGPath> Cycles
        {
            get
            {
                return cycles;
            }
        }

        public SFGraph Graph
        {
            get
            {
                return graph;
            }
        }

        public List<List<HashSet<SFGPath>>> NonTouchingCycles { get { return ntcycles; } }

        public void Solve()
        {
            if (solved) return;
            // Find all 2 non-touching cycles
            var temp = new List<HashSet<SFGPath>>();
            for (int i = 0; i < cycles.Count; i++)
            {
                var ca = cycles[i];
                for (int j = i; j < cycles.Count; j++)
                {
                    var cb = cycles[j];
                    if (!ca.Intersects(cb))
                    {
                        HashSet<SFGPath> coup = new HashSet<SFGPath>();
                        coup.Add(ca);
                        coup.Add(cb);
                        temp.Add(coup);
                    }
                }
            }
            if (temp.Count > 0)
            {
                ntcycles.Add(temp);
                // Try to find larger cycles
                foreach (var coup in ntcycles[0])
                {
                    FindMoreNonTouchingCycles(coup);
                }
            }
            solved = true;
        }
        public IEnumerable<Expression> GetPDeltas()
        {
            for (int i = 0; i < fpaths.Count; i++)
                yield return GetDelta(fpaths[i]);
        }
        public Expression GetDelta(SFGPath exc = null)
        {
            if (!solved) Solve();
            int sign = 1;
            var sb = new StringBuilder();
            var delta = new Expression(1);
            var filteredcycles = cycles.Where(c => !c.Intersects(exc)).ToList();
            if (filteredcycles.Count > 0)
            {
                // Add all single cycles
                Expression acyc = new Expression(filteredcycles[0].HTML);
                foreach (var cycle in filteredcycles.Skip(1))
                    acyc.Append(cycle.HTML, "+");
                // Append to main expression
                delta.Append(acyc, "-");
                // Calculate non-touching loop output
                foreach (var cgs in ntcycles)
                {
                    Expression ntcyc = null;
                    foreach (var cg in cgs)
                    {
                        Expression mexp = null;
                        foreach (var c in cg)
                        {
                            if (!c.Intersects(exc))
                            {
                                if (mexp == null) mexp = new Expression(c.HTML);
                                else mexp.Append(c.HTML, "*");
                            }
                            else
                            { mexp = null; break; }
                        }
                        if (mexp != null)
                        {
                            if (ntcyc == null) ntcyc = new Expression(mexp);
                            else ntcyc.Append(mexp, "+");
                        }
                    }
                    if (ntcyc != null)
                        delta.Append(ntcyc, sign > 0 ? "+" : "-");
                    else
                        break;
                    sign *= -1;
                }
            }
            return delta;
        }
        private void FindMoreNonTouchingCycles(HashSet<SFGPath> coup)
        {
            foreach (var cycle in cycles)
                if (!cycle.IntersectsAny(coup.ToList()))
                {
                    HashSet<SFGPath> temp = new HashSet<SFGPath>();
                    foreach (var c in coup) temp.Add(c);
                    temp.Add(cycle);
                    int i = temp.Count - 2;
                    if (i >= ntcycles.Count) ntcycles.Add(new List<HashSet<SFGPath>>());
                    if (!CycleGroupExists(temp))
                    {
                        ntcycles[i].Add(temp);
                        FindMoreNonTouchingCycles(temp);
                    }
                }
        }

        private bool CycleGroupExists(HashSet<SFGPath> temp)
        {
            foreach (var g in ntcycles[temp.Count - 2])
            {
                if (temp.IsSubsetOf(g))
                    return true;
            }
            return false;
        }
    }
}
