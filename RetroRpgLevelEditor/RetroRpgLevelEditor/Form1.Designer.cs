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
            this.oCompile = new System.Windows.Forms.Button();
            this.oIDEditor = new System.Windows.Forms.RichTextBox();
            this.oZoomPlus = new System.Windows.Forms.Button();
            this.oZoomMinus = new System.Windows.Forms.Button();
            this.oZoomCenter = new System.Windows.Forms.Button();
            this.CompileCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // oSave
            // 
            this.oSave.Location = new System.Drawing.Point(873, 3);
            this.oSave.Name = "oSave";
            this.oSave.Size = new System.Drawing.Size(174, 50);
            this.oSave.TabIndex = 0;
            this.oSave.Text = "Save";
            this.oSave.UseVisualStyleBackColor = true;
            this.oSave.Click += new System.EventHandler(this.oSave_Click);
            // 
            // oLoad
            // 
            this.oLoad.Location = new System.Drawing.Point(873, 59);
            this.oLoad.Name = "oLoad";
            this.oLoad.Size = new System.Drawing.Size(174, 43);
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
            this.progressBar1.Location = new System.Drawing.Point(873, 347);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(195, 28);
            this.progressBar1.TabIndex = 2;
            // 
            // oCompile
            // 
            this.oCompile.Location = new System.Drawing.Point(873, 108);
            this.oCompile.Name = "oCompile";
            this.oCompile.Size = new System.Drawing.Size(170, 43);
            this.oCompile.TabIndex = 3;
            this.oCompile.Text = "Add ID";
            this.oCompile.UseVisualStyleBackColor = true;
            this.oCompile.Click += new System.EventHandler(this.oCompile_Click);
            // 
            // oIDEditor
            // 
            this.oIDEditor.AcceptsTab = true;
            this.oIDEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.oIDEditor.Location = new System.Drawing.Point(872, 381);
            this.oIDEditor.Name = "oIDEditor";
            this.oIDEditor.Size = new System.Drawing.Size(196, 164);
            this.oIDEditor.TabIndex = 4;
            this.oIDEditor.Text = "";
            this.oIDEditor.TextChanged += new System.EventHandler(this.oIDEditor_TextChanged);
            // 
            // oZoomPlus
            // 
            this.oZoomPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.oZoomPlus.Location = new System.Drawing.Point(1000, 292);
            this.oZoomPlus.Name = "oZoomPlus";
            this.oZoomPlus.Size = new System.Drawing.Size(58, 49);
            this.oZoomPlus.TabIndex = 5;
            this.oZoomPlus.Text = "+";
            this.oZoomPlus.UseVisualStyleBackColor = true;
            this.oZoomPlus.Click += new System.EventHandler(this.oZoomPlus_Click);
            // 
            // oZoomMinus
            // 
            this.oZoomMinus.AllowDrop = true;
            this.oZoomMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.oZoomMinus.Location = new System.Drawing.Point(936, 292);
            this.oZoomMinus.Name = "oZoomMinus";
            this.oZoomMinus.Size = new System.Drawing.Size(58, 49);
            this.oZoomMinus.TabIndex = 6;
            this.oZoomMinus.Text = "-";
            this.oZoomMinus.UseVisualStyleBackColor = true;
            this.oZoomMinus.Click += new System.EventHandler(this.oZoomMinus_Click);
            // 
            // oZoomCenter
            // 
            this.oZoomCenter.AllowDrop = true;
            this.oZoomCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.oZoomCenter.Location = new System.Drawing.Point(872, 292);
            this.oZoomCenter.Name = "oZoomCenter";
            this.oZoomCenter.Size = new System.Drawing.Size(58, 49);
            this.oZoomCenter.TabIndex = 7;
            this.oZoomCenter.Text = "=";
            this.oZoomCenter.UseVisualStyleBackColor = true;
            this.oZoomCenter.Click += new System.EventHandler(this.oZoomCenter_Click);
            // 
            // CompileCode
            // 
            this.CompileCode.Location = new System.Drawing.Point(873, 162);
            this.CompileCode.Name = "CompileCode";
            this.CompileCode.Size = new System.Drawing.Size(169, 50);
            this.CompileCode.TabIndex = 8;
            this.CompileCode.Text = "Compile";
            this.CompileCode.UseVisualStyleBackColor = true;
            this.CompileCode.Click += new System.EventHandler(this.CompileCode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 547);
            this.Controls.Add(this.CompileCode);
            this.Controls.Add(this.oZoomCenter);
            this.Controls.Add(this.oZoomMinus);
            this.Controls.Add(this.oZoomPlus);
            this.Controls.Add(this.oIDEditor);
            this.Controls.Add(this.oCompile);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.oLoad);
            this.Controls.Add(this.oSave);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RetroRPG Level editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button oSave;
        private System.Windows.Forms.Button oLoad;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button oCompile;
        private System.Windows.Forms.RichTextBox oIDEditor;
        private System.Windows.Forms.Button oZoomPlus;
        private System.Windows.Forms.Button oZoomMinus;
        private System.Windows.Forms.Button oZoomCenter;
        private System.Windows.Forms.Button CompileCode;
    }
}

