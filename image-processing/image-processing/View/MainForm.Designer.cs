namespace image_processing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binarizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erosionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dilatationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skeletonizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterBlobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importShapesKnowledgeBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportShapesKnowledgeBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importShapeAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeDistributionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ImageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.grayLabel = new System.Windows.Forms.Label();
            this.cordsLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.aToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.shapeToolStripMenuItem,
            this.generatorToolStripMenuItem,
            this.statisticToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aToolStripMenuItem1});
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // FileToolStripMenuItem
            // 
            resources.ApplyResources(this.FileToolStripMenuItem, "FileToolStripMenuItem");
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            // 
            // OpenToolStripMenuItem
            // 
            resources.ApplyResources(this.OpenToolStripMenuItem, "OpenToolStripMenuItem");
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            resources.ApplyResources(this.reloadToolStripMenuItem, "reloadToolStripMenuItem");
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            resources.ApplyResources(this.filtersToolStripMenuItem, "filtersToolStripMenuItem");
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.binarizationToolStripMenuItem,
            this.erosionToolStripMenuItem,
            this.dilatationToolStripMenuItem,
            this.openingToolStripMenuItem,
            this.closingToolStripMenuItem,
            this.skeletonizationToolStripMenuItem,
            this.filterBlobsToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            // 
            // binarizationToolStripMenuItem
            // 
            resources.ApplyResources(this.binarizationToolStripMenuItem, "binarizationToolStripMenuItem");
            this.binarizationToolStripMenuItem.Name = "binarizationToolStripMenuItem";
            this.binarizationToolStripMenuItem.Click += new System.EventHandler(this.binarizationToolStripMenuItem_Click);
            // 
            // erosionToolStripMenuItem
            // 
            resources.ApplyResources(this.erosionToolStripMenuItem, "erosionToolStripMenuItem");
            this.erosionToolStripMenuItem.Name = "erosionToolStripMenuItem";
            this.erosionToolStripMenuItem.Click += new System.EventHandler(this.erosionToolStripMenuItem_Click);
            // 
            // dilatationToolStripMenuItem
            // 
            resources.ApplyResources(this.dilatationToolStripMenuItem, "dilatationToolStripMenuItem");
            this.dilatationToolStripMenuItem.Name = "dilatationToolStripMenuItem";
            this.dilatationToolStripMenuItem.Click += new System.EventHandler(this.dilatationToolStripMenuItem_Click);
            // 
            // openingToolStripMenuItem
            // 
            resources.ApplyResources(this.openingToolStripMenuItem, "openingToolStripMenuItem");
            this.openingToolStripMenuItem.Name = "openingToolStripMenuItem";
            this.openingToolStripMenuItem.Click += new System.EventHandler(this.openingToolStripMenuItem_Click);
            // 
            // closingToolStripMenuItem
            // 
            resources.ApplyResources(this.closingToolStripMenuItem, "closingToolStripMenuItem");
            this.closingToolStripMenuItem.Name = "closingToolStripMenuItem";
            this.closingToolStripMenuItem.Click += new System.EventHandler(this.closingToolStripMenuItem_Click);
            // 
            // skeletonizationToolStripMenuItem
            // 
            resources.ApplyResources(this.skeletonizationToolStripMenuItem, "skeletonizationToolStripMenuItem");
            this.skeletonizationToolStripMenuItem.Name = "skeletonizationToolStripMenuItem";
            this.skeletonizationToolStripMenuItem.Click += new System.EventHandler(this.skeletonizationToolStripMenuItem_Click);
            // 
            // filterBlobsToolStripMenuItem
            // 
            resources.ApplyResources(this.filterBlobsToolStripMenuItem, "filterBlobsToolStripMenuItem");
            this.filterBlobsToolStripMenuItem.Name = "filterBlobsToolStripMenuItem";
            this.filterBlobsToolStripMenuItem.Click += new System.EventHandler(this.filterBlobsToolStripMenuItem_Click);
            // 
            // shapeToolStripMenuItem
            // 
            resources.ApplyResources(this.shapeToolStripMenuItem, "shapeToolStripMenuItem");
            this.shapeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeToolStripMenuItem,
            this.importShapesKnowledgeBaseToolStripMenuItem,
            this.exportShapesKnowledgeBaseToolStripMenuItem});
            this.shapeToolStripMenuItem.Name = "shapeToolStripMenuItem";
            // 
            // analyzeToolStripMenuItem
            // 
            resources.ApplyResources(this.analyzeToolStripMenuItem, "analyzeToolStripMenuItem");
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Click += new System.EventHandler(this.analyzeToolStripMenuItem_Click);
            // 
            // importShapesKnowledgeBaseToolStripMenuItem
            // 
            resources.ApplyResources(this.importShapesKnowledgeBaseToolStripMenuItem, "importShapesKnowledgeBaseToolStripMenuItem");
            this.importShapesKnowledgeBaseToolStripMenuItem.Name = "importShapesKnowledgeBaseToolStripMenuItem";
            this.importShapesKnowledgeBaseToolStripMenuItem.Click += new System.EventHandler(this.importShapesKnowledgeBaseToolStripMenuItem_Click);
            // 
            // exportShapesKnowledgeBaseToolStripMenuItem
            // 
            resources.ApplyResources(this.exportShapesKnowledgeBaseToolStripMenuItem, "exportShapesKnowledgeBaseToolStripMenuItem");
            this.exportShapesKnowledgeBaseToolStripMenuItem.Name = "exportShapesKnowledgeBaseToolStripMenuItem";
            this.exportShapesKnowledgeBaseToolStripMenuItem.Click += new System.EventHandler(this.exportShapesKnowledgeBaseToolStripMenuItem_Click);
            // 
            // generatorToolStripMenuItem
            // 
            resources.ApplyResources(this.generatorToolStripMenuItem, "generatorToolStripMenuItem");
            this.generatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importShapeAnalyzerToolStripMenuItem});
            this.generatorToolStripMenuItem.Name = "generatorToolStripMenuItem";
            // 
            // importShapeAnalyzerToolStripMenuItem
            // 
            resources.ApplyResources(this.importShapeAnalyzerToolStripMenuItem, "importShapeAnalyzerToolStripMenuItem");
            this.importShapeAnalyzerToolStripMenuItem.Name = "importShapeAnalyzerToolStripMenuItem";
            this.importShapeAnalyzerToolStripMenuItem.Click += new System.EventHandler(this.generateMicrostructureToolStripMenuItem_Click);
            // 
            // statisticToolStripMenuItem
            // 
            resources.ApplyResources(this.statisticToolStripMenuItem, "statisticToolStripMenuItem");
            this.statisticToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeDistributionToolStripMenuItem});
            this.statisticToolStripMenuItem.Name = "statisticToolStripMenuItem";
            // 
            // sizeDistributionToolStripMenuItem
            // 
            resources.ApplyResources(this.sizeDistributionToolStripMenuItem, "sizeDistributionToolStripMenuItem");
            this.sizeDistributionToolStripMenuItem.Name = "sizeDistributionToolStripMenuItem";
            this.sizeDistributionToolStripMenuItem.Click += new System.EventHandler(this.sizeDistributionToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            // 
            // aToolStripMenuItem
            // 
            resources.ApplyResources(this.aToolStripMenuItem, "aToolStripMenuItem");
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Name = "panel1";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // ImageFileDialog
            // 
            this.ImageFileDialog.FileName = "openFileDialog1";
            resources.ApplyResources(this.ImageFileDialog, "ImageFileDialog");
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.volumeLabel);
            this.panel2.Controls.Add(this.grayLabel);
            this.panel2.Controls.Add(this.cordsLabel);
            this.panel2.Controls.Add(this.sizeLabel);
            this.panel2.Name = "panel2";
            // 
            // volumeLabel
            // 
            resources.ApplyResources(this.volumeLabel, "volumeLabel");
            this.volumeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.volumeLabel.Name = "volumeLabel";
            // 
            // grayLabel
            // 
            resources.ApplyResources(this.grayLabel, "grayLabel");
            this.grayLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grayLabel.Name = "grayLabel";
            // 
            // cordsLabel
            // 
            resources.ApplyResources(this.cordsLabel, "cordsLabel");
            this.cordsLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cordsLabel.Name = "cordsLabel";
            // 
            // sizeLabel
            // 
            resources.ApplyResources(this.sizeLabel, "sizeLabel");
            this.sizeLabel.BackColor = System.Drawing.Color.Transparent;
            this.sizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sizeLabel.Name = "sizeLabel";
            // 
            // aToolStripMenuItem1
            // 
            resources.ApplyResources(this.aToolStripMenuItem1, "aToolStripMenuItem1");
            this.aToolStripMenuItem1.Name = "aToolStripMenuItem1";
            this.aToolStripMenuItem.Text = "asdasd";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog ImageFileDialog;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binarizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem erosionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dilatationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skeletonizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeDistributionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importShapeAnalyzerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterBlobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shapeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.Label grayLabel;
        private System.Windows.Forms.Label cordsLabel;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importShapesKnowledgeBaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportShapesKnowledgeBaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem1;
    }
}

