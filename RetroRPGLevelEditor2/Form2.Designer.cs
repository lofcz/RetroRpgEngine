namespace RetroRPGLevelEditor2
{
    partial class Form2
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
            this.oCompile = new System.Windows.Forms.Button();
            this.oSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // oCompile
            // 
            this.oCompile.Location = new System.Drawing.Point(9, 19);
            this.oCompile.Name = "oCompile";
            this.oCompile.Size = new System.Drawing.Size(152, 41);
            this.oCompile.TabIndex = 1;
            this.oCompile.Text = "Compile";
            this.oCompile.UseVisualStyleBackColor = true;
            this.oCompile.Click += new System.EventHandler(this.oCompile_Click);
            // 
            // oSave
            // 
            this.oSave.Location = new System.Drawing.Point(9, 66);
            this.oSave.Name = "oSave";
            this.oSave.Size = new System.Drawing.Size(152, 41);
            this.oSave.TabIndex = 2;
            this.oSave.Text = "Save and close";
            this.oSave.UseVisualStyleBackColor = true;
            this.oSave.Click += new System.EventHandler(this.oSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.oSave);
            this.groupBox1.Controls.Add(this.oCompile);
            this.groupBox1.Location = new System.Drawing.Point(611, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 212);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 214);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form2";
            this.Text = "Code editor";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button oCompile;
        private System.Windows.Forms.Button oSave;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}