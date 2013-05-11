namespace GameofLife
{
    partial class MainForm
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
            this.btnRun = new System.Windows.Forms.Button();
            this.btnIterate = new System.Windows.Forms.Button();
            this.lblGeneration = new System.Windows.Forms.Label();
            this.plDisplay = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getHashDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performanceDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollV = new System.Windows.Forms.VScrollBar();
            this.scrollH = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPopulation = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(612, 478);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(93, 28);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnIterate
            // 
            this.btnIterate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIterate.Location = new System.Drawing.Point(513, 478);
            this.btnIterate.Name = "btnIterate";
            this.btnIterate.Size = new System.Drawing.Size(93, 28);
            this.btnIterate.TabIndex = 3;
            this.btnIterate.Text = "Iterate";
            this.btnIterate.UseVisualStyleBackColor = true;
            this.btnIterate.Click += new System.EventHandler(this.btnIterate_Click);
            // 
            // lblGeneration
            // 
            this.lblGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGeneration.AutoSize = true;
            this.lblGeneration.Location = new System.Drawing.Point(77, 441);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(71, 13);
            this.lblGeneration.TabIndex = 4;
            this.lblGeneration.Text = "GEN COUNT";
            // 
            // plDisplay
            // 
            this.plDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plDisplay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.plDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDisplay.Location = new System.Drawing.Point(12, 42);
            this.plDisplay.Name = "plDisplay";
            this.plDisplay.Size = new System.Drawing.Size(673, 360);
            this.plDisplay.TabIndex = 5;
            this.plDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.plDisplay_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(717, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testingToolStripMenuItem
            // 
            this.testingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getHashDataToolStripMenuItem,
            this.performanceDataToolStripMenuItem});
            this.testingToolStripMenuItem.Name = "testingToolStripMenuItem";
            this.testingToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.testingToolStripMenuItem.Text = "Testing";
            // 
            // getHashDataToolStripMenuItem
            // 
            this.getHashDataToolStripMenuItem.Name = "getHashDataToolStripMenuItem";
            this.getHashDataToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.getHashDataToolStripMenuItem.Text = "Get Hash Data";
            this.getHashDataToolStripMenuItem.Click += new System.EventHandler(this.getHashDataToolStripMenuItem_Click);
            // 
            // performanceDataToolStripMenuItem
            // 
            this.performanceDataToolStripMenuItem.Name = "performanceDataToolStripMenuItem";
            this.performanceDataToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.performanceDataToolStripMenuItem.Text = "Performance Data";
            this.performanceDataToolStripMenuItem.Click += new System.EventHandler(this.performanceDataToolStripMenuItem_Click);
            // 
            // scrollV
            // 
            this.scrollV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollV.Location = new System.Drawing.Point(688, 42);
            this.scrollV.Name = "scrollV";
            this.scrollV.Size = new System.Drawing.Size(17, 360);
            this.scrollV.TabIndex = 7;
            this.scrollV.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollV_Scroll);
            // 
            // scrollH
            // 
            this.scrollH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollH.Location = new System.Drawing.Point(12, 405);
            this.scrollH.Name = "scrollH";
            this.scrollH.Size = new System.Drawing.Size(673, 17);
            this.scrollH.TabIndex = 8;
            this.scrollH.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollH_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 441);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Generation:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 461);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Population:";
            // 
            // lblPopulation
            // 
            this.lblPopulation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPopulation.AutoSize = true;
            this.lblPopulation.Location = new System.Drawing.Point(77, 461);
            this.lblPopulation.Name = "lblPopulation";
            this.lblPopulation.Size = new System.Drawing.Size(76, 13);
            this.lblPopulation.TabIndex = 11;
            this.lblPopulation.Text = "POPULATION";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 483);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Runtime:";
            // 
            // lblRuntime
            // 
            this.lblRuntime.AutoSize = true;
            this.lblRuntime.Location = new System.Drawing.Point(77, 483);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(57, 13);
            this.lblRuntime.TabIndex = 13;
            this.lblRuntime.Text = "RUNTIME";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 518);
            this.Controls.Add(this.lblRuntime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPopulation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scrollH);
            this.Controls.Add(this.scrollV);
            this.Controls.Add(this.plDisplay);
            this.Controls.Add(this.lblGeneration);
            this.Controls.Add(this.btnIterate);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Game of Life";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnIterate;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.Panel plDisplay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getHashDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem performanceDataToolStripMenuItem;
        private System.Windows.Forms.VScrollBar scrollV;
        private System.Windows.Forms.HScrollBar scrollH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPopulation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRuntime;
    }
}

