using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shannon
{
    class Expression
    {
        List<Expression> partials = new List<Expression>();
        List<string> operations = new List<string>();
        private string symbol = null;
        private string strvalue = null;
        private bool changed = false;

        public Expression(string symbol)
        {
            this.symbol = symbol;
        }
        public Expression(int v)
        {
            symbol = v.ToString();
        }
        public Expression()
        {
        }
        public Expression(Expression epx)
        {
            partials.Add(epx);
        }

        public override string ToString()
        {
            if (!changed && strvalue != null) return strvalue;
            if (symbol != null) return symbol;
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var pexp in partials)
            {
                if (pexp.partials.Count > 1)
                    sb.Append(" (" + pexp.ToString() + ") ");
                else sb.Append(" " + pexp.ToString()+ " ");
                if (i < operations.Count)
                    sb.Append(operations[i++]);
            }
            changed = false;
            return strvalue = sb.ToString();
        }
        public void Append(Expression exp, string op)
        {
            changed = true;
            if (symbol != null)
            {
                partials.Add(new Expression(symbol));
                symbol = null;
            }
            operations.Add(op);
            partials.Add(exp);
        }
        public void Append(string symbol, string op)
        {
            Append(new Expression(symbol), op);
        }
        public void Append(int number, string op)
        {
            Append(new Expression(number.ToString()), op);
        }
    }
}
