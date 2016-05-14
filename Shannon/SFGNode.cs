using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Shannon
{
    public class SFGNode
    {
        #region GUIManager fields
        int id;
        int vshift = 0;
        #endregion
        List<SFGNode> neighbours = new List<SFGNode>();
        const int MinID = 1;
        const int MaxID = 52;
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public string ASCID
        {
            get { return GetSymb(id); }
        }
        public List<SFGNode> Neighbours
        {
            get
            {
                return neighbours;
            }

            set
            {
                neighbours = value;
            }
        }

        public int VShift
        {
            get
            {
                return vshift;
            }

            set
            {
                vshift = value;
            }
        }

        public void ConnectTo(SFGNode sFGNode)
        {
            neighbours.Add(sFGNode);
        }
        private string GetSymb(int id)
        {
            StringBuilder sb = new StringBuilder();
            do
            {
                id -= 1;
                int i = id % 52;
                sb.Insert(0, (char)(i < 26 ? 0x61 + i : 0x41 + i - 26));
                id /= 52;
            } while (id > 0);
            return sb.ToString();
        }
        bool ishot = false;
        public List<SFGPath> GetAllPathsTo(SFGNode value, bool forwardonly = false)
        {
            List<SFGPath> paths = new List<SFGPath>();
            if (value.id == id)
            {
                paths.Add(new SFGPath(this));
                return paths;
            }
            else if (!ishot)
            {
                ishot = true;
                foreach (SFGNode n in neighbours)
                {
                    if (forwardonly && n.id < id) continue;
                    var npaths = n.GetAllPathsTo(value, forwardonly);
                    if (npaths != null)
                        foreach (SFGPath p in npaths)
                            paths.Add(new SFGPath(this, p));
                }
                ishot = false;
                return paths;
            }
            return null;
        }

        internal void MarkExcluded()
        {
            ishot = true;
        }
        public void UnmarkExcluded()
        {
            ishot = false;
        }
        public override string ToString()
        {
            return ID.ToString();
        }
    }
}