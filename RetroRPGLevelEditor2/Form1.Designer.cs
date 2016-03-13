namespace RetroRPGLevelEditor2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.oThumnail = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.oBookmarks = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.oCamera = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.oExport = new System.Windows.Forms.Button();
            this.oLoad = new System.Windows.Forms.Button();
            this.oSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.oEventLog = new System.Windows.Forms.Label();
            this.EventLogTimer = new System.Windows.Forms.Timer(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.TreeImageList = new System.Windows.Forms.ImageList(this.components);
            this.oNewMap = new System.Windows.Forms.Button();
            this.oMapMeta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.oThumnail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Zeď",
            "Pohyblivá zeď",
            "Hráč",
            "Předmět",
            "Mince",
            "Soupeř"});
            this.comboBox1.Location = new System.Drawing.Point(10, 85);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(112, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
            // 
            // oThumnail
            // 
            this.oThumnail.ErrorImage = global::RetroRPGLevelEditor2.Properties.Resources.wallThumnail;
            this.oThumnail.InitialImage = global::RetroRPGLevelEditor2.Properties.Resources.wallThumnail;
            this.oThumnail.Location = new System.Drawing.Point(125, 9);
            this.oThumnail.Name = "oThumnail";
            this.oThumnail.Size = new System.Drawing.Size(128, 128);
            this.oThumnail.TabIndex = 2;
            this.oThumnail.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.oMapMeta);
            this.groupBox1.Controls.Add(this.oNewMap);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.oEventLog);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.oThumnail);
            this.groupBox1.Controls.Add(this.oBookmarks);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.oCamera);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.oExport);
            this.groupBox1.Controls.Add(this.oLoad);
            this.groupBox1.Controls.Add(this.oSave);
            this.groupBox1.Location = new System.Drawing.Point(1089, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 734);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 460);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "text";
            // 
            // oBookmarks
            // 
            this.oBookmarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.oBookmarks.FormattingEnabled = true;
            this.oBookmarks.Location = new System.Drawing.Point(10, 359);
            this.oBookmarks.Name = "oBookmarks";
            this.oBookmarks.Size = new System.Drawing.Size(240, 21);
            this.oBookmarks.TabIndex = 9;
            this.toolTip1.SetToolTip(this.oBookmarks, "Rychlé záložky\r\n");
            this.oBookmarks.SelectedIndexChanged += new System.EventHandler(this.oBookmarks_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "Zoom (1)\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // oCamera
            // 
            this.oCamera.AutoSize = true;
            this.oCamera.Location = new System.Drawing.Point(7, 441);
            this.oCamera.Name = "oCamera";
            this.oCamera.Size = new System.Drawing.Size(0, 13);
            this.oCamera.TabIndex = 7;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(116, 497);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(137, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Zvýraznit creation code";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // oExport
            // 
            this.oExport.Location = new System.Drawing.Point(118, 460);
            this.oExport.Name = "oExport";
            this.oExport.Size = new System.Drawing.Size(135, 31);
            this.oExport.TabIndex = 5;
            this.oExport.Text = "Exportovat mapu";
            this.oExport.UseVisualStyleBackColor = true;
            this.oExport.Click += new System.EventHandler(this.oExport_Click);
            // 
            // oLoad
            // 
            this.oLoad.Location = new System.Drawing.Point(118, 423);
            this.oLoad.Name = "oLoad";
            this.oLoad.Size = new System.Drawing.Size(135, 31);
            this.oLoad.TabIndex = 4;
            this.oLoad.Text = "Načíst";
            this.oLoad.UseVisualStyleBackColor = true;
            this.oLoad.Click += new System.EventHandler(this.oLoad_Click);
            // 
            // oSave
            // 
            this.oSave.Location = new System.Drawing.Point(118, 386);
            this.oSave.Name = "oSave";
            this.oSave.Size = new System.Drawing.Size(135, 31);
            this.oSave.TabIndex = 3;
            this.oSave.Text = "Uložit";
            this.oSave.UseVisualStyleBackColor = true;
            this.oSave.Click += new System.EventHandler(this.oSave_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(1072, 0);
            this.vScrollBar1.Maximum = 48;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(14, 716);
            this.vScrollBar1.TabIndex = 4;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(9, 711);
            this.hScrollBar1.Maximum = 48;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(1063, 17);
            this.hScrollBar1.TabIndex = 5;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // oEventLog
            // 
            this.oEventLog.AutoSize = true;
            this.oEventLog.BackColor = System.Drawing.Color.Transparent;
            this.oEventLog.Location = new System.Drawing.Point(113, 525);
            this.oEventLog.Name = "oEventLog";
            this.oEventLog.Size = new System.Drawing.Size(53, 13);
            this.oEventLog.TabIndex = 6;
            this.oEventLog.Text = "EventLog";
            // 
            // EventLogTimer
            // 
            this.EventLogTimer.Enabled = true;
            this.EventLogTimer.Interval = 10;
            this.EventLogTimer.Tick += new System.EventHandler(this.EventLogTimer_Tick);
            // 
            // treeView1
            // 
            this.treeView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.treeView1.Location = new System.Drawing.Point(9, 149);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(244, 204);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // TreeImageList
            // 
            this.TreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeImageList.ImageStream")));
            this.TreeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.TreeImageList.Images.SetKeyName(0, "wallMoveable.png");
            this.TreeImageList.Images.SetKeyName(1, "coin.png");
            // 
            // oNewMap
            // 
            this.oNewMap.Location = new System.Drawing.Point(9, 19);
            this.oNewMap.Name = "oNewMap";
            this.oNewMap.Size = new System.Drawing.Size(113, 27);
            this.oNewMap.TabIndex = 11;
            this.oNewMap.Text = "Nová mapa";
            this.oNewMap.UseVisualStyleBackColor = true;
            // 
            // oMapMeta
            // 
            this.oMapMeta.Location = new System.Drawing.Point(10, 52);
            this.oMapMeta.Name = "oMapMeta";
            this.oMapMeta.Size = new System.Drawing.Size(113, 27);
            this.oMapMeta.TabIndex = 12;
            this.oMapMeta.Text = "Metatagy mapy";
            this.oMapMeta.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 737);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "RetroRPG Level editor 2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.oThumnail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox oThumnail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button oExport;
        private System.Windows.Forms.Button oLoad;
        private System.Windows.Forms.Button oSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label oCamera;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.ComboBox oBookmarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label oEventLog;
        private System.Windows.Forms.Timer EventLogTimer;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList TreeImageList;
        private System.Windows.Forms.Button oMapMeta;
        private System.Windows.Forms.Button oNewMap;
    }
}

