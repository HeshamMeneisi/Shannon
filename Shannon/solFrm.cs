using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shannon
{
    public partial class solFrm : Form
    {
        Action<Form, string> MessageToParent;
        public solFrm(string html, Action<Form, string> MessageToParent)
        {
            InitializeComponent();
            webBrowser1.DocumentText = html;
            this.MessageToParent = MessageToParent;
        }

        private void solFrm_Load(object sender, EventArgs e)
        {
            var b = Screen.GetWorkingArea(this);
            Width = b.Width / 2;
            Height = b.Height;
            Location = new Point(Width, 0);
        }
        bool loaded = false;
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!loaded) return;
            e.Cancel = true;
            MessageToParent(this, e.Url.Query);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            loaded = true;
        }
    }
}
