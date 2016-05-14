using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shannon
{
    static class OutputFormatter
    {
        static string xeqfrc;
        static string expcol;
        public static void LoadFiles()
        {
            xeqfrc = ReadFile("XEQFRHTM.txt");
            expcol = ReadFile("ExpColScript.txt");
        }

        private static string ReadFile(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            name = Application.ProductName + "." + name;
            using (Stream stream = assembly.GetManifestResourceStream(name))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GetXEQFrc(string x, string enumerator, string denom)
        {
            return xeqfrc.Replace("{x}", x).Replace("{enum}", enumerator).Replace("{denom}", denom);
        }
        static int id = 0;
        internal static string GetCollapsable(string content, string title)
        {
            return expcol.Replace("{content}", content).Replace("{title}", title).Replace("{id}", "_" + id++);
        }
    }
}
