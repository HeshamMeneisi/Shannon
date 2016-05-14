using System;
using System.Runtime.Serialization;

namespace Shannon
{
    [Serializable]
    public class EdgeLabel
    {        
        private string edgename;
        private string label;

        public EdgeLabel(string key, string v)
        {
            Edgename = key;
            Label = v;
        }        
        public string Edgename
        {
            get
            {
                return edgename;
            }

            set
            {
                edgename = value;
            }
        }

        public string Label
        {
            get
            {
                return label;
            }

            set
            {
                label = value;
            }
        }
    }
}