using System;
using System.Windows.Forms;

namespace Shannon
{
    public abstract class GUITool:IDisposable
    {        
        protected GUIManager parent;
        public GUITool(GUIManager m)
        {            
            parent = m;
        }
        public abstract void Dispose();
    }
}