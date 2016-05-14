namespace Shannon
{
    partial class mainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrm));
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.cursortool = new System.Windows.Forms.ToolStripButton();
            this.zoomin = new System.Windows.Forms.ToolStripButton();
            this.zoomout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startend = new System.Windows.Forms.ToolStripButton();
            this.selecttool = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.actionbar = new System.Windows.Forms.ToolStrip();
            this.solve = new System.Windows.Forms.ToolStripButton();
            this.addbefore = new System.Windows.Forms.ToolStripButton();
            this.addafter = new System.Windows.Forms.ToolStripButton();
            this.delete = new System.Windows.Forms.ToolStripButton();
            this.pushup = new System.Windows.Forms.ToolStripButton();
            this.pushdown = new System.Windows.Forms.ToolStripButton();
            this.deleteedge = new System.Windows.Forms.ToolStripButton();
            this.edlabel = new System.Windows.Forms.ToolStripLabel();
            this.edname = new System.Windows.Forms.ToolStripTextBox();
            this.ednameok = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectForwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbar.SuspendLayout();
            this.actionbar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cursortool,
            this.zoomin,
            this.zoomout,
            this.toolStripSeparator1,
            this.startend,
            this.selecttool});
            this.toolbar.Location = new System.Drawing.Point(0, 24);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(53, 396);
            this.toolbar.TabIndex = 0;
            this.toolbar.Text = "toolStrip1";
            this.toolbar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tools_ItemClicked);
            // 
            // cursortool
            // 
            this.cursortool.Checked = true;
            this.cursortool.CheckOnClick = true;
            this.cursortool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cursortool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cursortool.Image = ((System.Drawing.Image)(resources.GetObject("cursortool.Image")));
            this.cursortool.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cursortool.Name = "cursortool";
            this.cursortool.Size = new System.Drawing.Size(50, 52);
            this.cursortool.Text = "Cursor";
            this.cursortool.Click += new System.EventHandler(this.cursortool_Click);
            // 
            // zoomin
            // 
            this.zoomin.CheckOnClick = true;
            this.zoomin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomin.Image = ((System.Drawing.Image)(resources.GetObject("zoomin.Image")));
            this.zoomin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomin.Name = "zoomin";
            this.zoomin.Size = new System.Drawing.Size(50, 52);
            this.zoomin.Text = "Zoom-in";
            this.zoomin.Click += new System.EventHandler(this.zoomin_Click);
            // 
            // zoomout
            // 
            this.zoomout.CheckOnClick = true;
            this.zoomout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomout.Image = ((System.Drawing.Image)(resources.GetObject("zoomout.Image")));
            this.zoomout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomout.Name = "zoomout";
            this.zoomout.Size = new System.Drawing.Size(50, 52);
            this.zoomout.Text = "Zoom-out";
            this.zoomout.Click += new System.EventHandler(this.zoomout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(50, 6);
            // 
            // startend
            // 
            this.startend.CheckOnClick = true;
            this.startend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startend.Image = ((System.Drawing.Image)(resources.GetObject("startend.Image")));
            this.startend.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.startend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startend.Name = "startend";
            this.startend.Size = new System.Drawing.Size(50, 52);
            this.startend.Text = "Define Start/End";
            this.startend.Click += new System.EventHandler(this.startend_Click);
            // 
            // selecttool
            // 
            this.selecttool.CheckOnClick = true;
            this.selecttool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selecttool.Image = ((System.Drawing.Image)(resources.GetObject("selecttool.Image")));
            this.selecttool.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.selecttool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selecttool.Name = "selecttool";
            this.selecttool.Size = new System.Drawing.Size(50, 52);
            this.selecttool.Text = "Edge Create/Select";
            this.selecttool.Click += new System.EventHandler(this.selecttool_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(53, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 341);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // actionbar
            // 
            this.actionbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solve,
            this.addbefore,
            this.addafter,
            this.delete,
            this.pushup,
            this.pushdown,
            this.deleteedge,
            this.edlabel,
            this.edname,
            this.ednameok});
            this.actionbar.Location = new System.Drawing.Point(53, 24);
            this.actionbar.Name = "actionbar";
            this.actionbar.Size = new System.Drawing.Size(561, 55);
            this.actionbar.TabIndex = 2;
            this.actionbar.Text = "toolStrip2";
            // 
            // solve
            // 
            this.solve.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.solve.Image = ((System.Drawing.Image)(resources.GetObject("solve.Image")));
            this.solve.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.solve.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solve.Name = "solve";
            this.solve.Size = new System.Drawing.Size(52, 52);
            this.solve.Text = "Solve.";
            this.solve.Click += new System.EventHandler(this.solve_Click);
            // 
            // addbefore
            // 
            this.addbefore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addbefore.Image = ((System.Drawing.Image)(resources.GetObject("addbefore.Image")));
            this.addbefore.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addbefore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addbefore.Name = "addbefore";
            this.addbefore.Size = new System.Drawing.Size(52, 52);
            this.addbefore.Text = "Add Before.";
            this.addbefore.Click += new System.EventHandler(this.addbefore_Click);
            // 
            // addafter
            // 
            this.addafter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addafter.Image = ((System.Drawing.Image)(resources.GetObject("addafter.Image")));
            this.addafter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addafter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addafter.Name = "addafter";
            this.addafter.Size = new System.Drawing.Size(52, 52);
            this.addafter.Text = "Add After.";
            this.addafter.Click += new System.EventHandler(this.addafter_Click);
            // 
            // delete
            // 
            this.delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(52, 52);
            this.delete.Text = "Delete Node.";
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // pushup
            // 
            this.pushup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pushup.Image = ((System.Drawing.Image)(resources.GetObject("pushup.Image")));
            this.pushup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pushup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pushup.Name = "pushup";
            this.pushup.Size = new System.Drawing.Size(52, 52);
            this.pushup.Text = "Push up.";
            this.pushup.Visible = false;
            this.pushup.Click += new System.EventHandler(this.pushup_Click);
            // 
            // pushdown
            // 
            this.pushdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pushdown.Image = ((System.Drawing.Image)(resources.GetObject("pushdown.Image")));
            this.pushdown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pushdown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pushdown.Name = "pushdown";
            this.pushdown.Size = new System.Drawing.Size(52, 52);
            this.pushdown.Text = "Push down.";
            this.pushdown.Visible = false;
            this.pushdown.Click += new System.EventHandler(this.pushdown_Click);
            // 
            // deleteedge
            // 
            this.deleteedge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteedge.Image = ((System.Drawing.Image)(resources.GetObject("deleteedge.Image")));
            this.deleteedge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteedge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteedge.Name = "deleteedge";
            this.deleteedge.Size = new System.Drawing.Size(52, 52);
            this.deleteedge.Text = "Delete Edge.";
            this.deleteedge.Click += new System.EventHandler(this.deleteedge_Click);
            // 
            // edlabel
            // 
            this.edlabel.Name = "edlabel";
            this.edlabel.Size = new System.Drawing.Size(23, 52);
            this.edlabel.Text = "AA";
            this.edlabel.Visible = false;
            // 
            // edname
            // 
            this.edname.Name = "edname";
            this.edname.Size = new System.Drawing.Size(100, 55);
            this.edname.Visible = false;
            // 
            // ednameok
            // 
            this.ednameok.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ednameok.Image = ((System.Drawing.Image)(resources.GetObject("ednameok.Image")));
            this.ednameok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ednameok.Name = "ednameok";
            this.ednameok.Size = new System.Drawing.Size(26, 52);
            this.ednameok.Text = "Ok";
            this.ednameok.Visible = false;
            this.ednameok.Click += new System.EventHandler(this.ednameok_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(614, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectForwardToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // connectForwardToolStripMenuItem
            // 
            this.connectForwardToolStripMenuItem.Name = "connectForwardToolStripMenuItem";
            this.connectForwardToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.connectForwardToolStripMenuItem.Text = "Connect Forward";
            this.connectForwardToolStripMenuItem.Click += new System.EventHandler(this.connectForwardToolStripMenuItem_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 420);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.actionbar);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shannon - SFG Design";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.SizeChanged += new System.EventHandler(this.mainFrm_SizeChanged);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.actionbar.ResumeLayout(false);
            this.actionbar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip actionbar;
        private System.Windows.Forms.ToolStripButton cursortool;
        private System.Windows.Forms.ToolStripButton selecttool;
        private System.Windows.Forms.ToolStripButton addafter;
        private System.Windows.Forms.ToolStripButton addbefore;
        private System.Windows.Forms.ToolStripButton delete;
        private System.Windows.Forms.ToolStripButton zoomin;
        private System.Windows.Forms.ToolStripButton zoomout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton pushup;
        private System.Windows.Forms.ToolStripButton pushdown;
        private System.Windows.Forms.ToolStripButton solve;
        private System.Windows.Forms.ToolStripButton startend;
        private System.Windows.Forms.ToolStripButton deleteedge;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel edlabel;
        private System.Windows.Forms.ToolStripTextBox edname;
        private System.Windows.Forms.ToolStripButton ednameok;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectForwardToolStripMenuItem;
    }
}

