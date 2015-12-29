namespace RetroRpgLevelEditor
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
            this.oSave = new System.Windows.Forms.Button();
            this.oLoad = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // oSave
            // 
            this.oSave.Location = new System.Drawing.Point(799, 12);
            this.oSave.Name = "oSave";
            this.oSave.Size = new System.Drawing.Size(174, 74);
            this.oSave.TabIndex = 0;
            this.oSave.Text = "Save";
            this.oSave.UseVisualStyleBackColor = true;
            this.oSave.Click += new System.EventHandler(this.oSave_Click);
            // 
            // oLoad
            // 
            this.oLoad.Location = new System.Drawing.Point(798, 112);
            this.oLoad.Name = "oLoad";
            this.oLoad.Size = new System.Drawing.Size(174, 77);
            this.oLoad.TabIndex = 1;
            this.oLoad.Text = "Load";
            this.oLoad.UseVisualStyleBackColor = true;
            this.oLoad.Click += new System.EventHandler(this.oLoad_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(805, 223);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(166, 28);
            this.progressBar1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 518);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.oLoad);
            this.Controls.Add(this.oSave);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button oSave;
        private System.Windows.Forms.Button oLoad;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

