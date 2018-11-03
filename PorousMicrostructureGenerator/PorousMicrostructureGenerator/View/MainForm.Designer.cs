namespace Generator
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
            this.clearShapeDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importShapesKnowledgeBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportShapesKnowledgeBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importShapeAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeDistributionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageHistogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ImageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.shapeCountLabel = new System.Windows.Forms.Label();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.grayLabel = new System.Windows.Forms.Label();
            this.cordsLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.shapeToolStripMenuItem,
            this.generatorToolStripMenuItem,
            this.statisticToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(936, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Enabled = false;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.binarizationToolStripMenuItem,
            this.erosionToolStripMenuItem,
            this.dilatationToolStripMenuItem,
            this.openingToolStripMenuItem,
            this.closingToolStripMenuItem,
            this.skeletonizationToolStripMenuItem,
            this.filterBlobsToolStripMenuItem});
            this.filtersToolStripMenuItem.Enabled = false;
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // binarizationToolStripMenuItem
            // 
            this.binarizationToolStripMenuItem.Name = "binarizationToolStripMenuItem";
            this.binarizationToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.binarizationToolStripMenuItem.Text = "Binarization";
            this.binarizationToolStripMenuItem.Click += new System.EventHandler(this.binarizationToolStripMenuItem_Click);
            // 
            // erosionToolStripMenuItem
            // 
            this.erosionToolStripMenuItem.Name = "erosionToolStripMenuItem";
            this.erosionToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.erosionToolStripMenuItem.Text = "Erosion";
            this.erosionToolStripMenuItem.Click += new System.EventHandler(this.erosionToolStripMenuItem_Click);
            // 
            // dilatationToolStripMenuItem
            // 
            this.dilatationToolStripMenuItem.Name = "dilatationToolStripMenuItem";
            this.dilatationToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.dilatationToolStripMenuItem.Text = "Dilatation";
            this.dilatationToolStripMenuItem.Click += new System.EventHandler(this.dilatationToolStripMenuItem_Click);
            // 
            // openingToolStripMenuItem
            // 
            this.openingToolStripMenuItem.Name = "openingToolStripMenuItem";
            this.openingToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openingToolStripMenuItem.Text = "Opening";
            this.openingToolStripMenuItem.Click += new System.EventHandler(this.openingToolStripMenuItem_Click);
            // 
            // closingToolStripMenuItem
            // 
            this.closingToolStripMenuItem.Name = "closingToolStripMenuItem";
            this.closingToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.closingToolStripMenuItem.Text = "Closing";
            this.closingToolStripMenuItem.Click += new System.EventHandler(this.closingToolStripMenuItem_Click);
            // 
            // skeletonizationToolStripMenuItem
            // 
            this.skeletonizationToolStripMenuItem.Name = "skeletonizationToolStripMenuItem";
            this.skeletonizationToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.skeletonizationToolStripMenuItem.Text = "Skeletonization";
            this.skeletonizationToolStripMenuItem.Click += new System.EventHandler(this.skeletonizationToolStripMenuItem_Click);
            // 
            // filterBlobsToolStripMenuItem
            // 
            this.filterBlobsToolStripMenuItem.Name = "filterBlobsToolStripMenuItem";
            this.filterBlobsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.filterBlobsToolStripMenuItem.Text = "Filter Blobs";
            this.filterBlobsToolStripMenuItem.Click += new System.EventHandler(this.filterBlobsToolStripMenuItem_Click);
            // 
            // shapeToolStripMenuItem
            // 
            this.shapeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeToolStripMenuItem,
            this.clearShapeDatabaseToolStripMenuItem,
            this.importShapesKnowledgeBaseToolStripMenuItem,
            this.exportShapesKnowledgeBaseToolStripMenuItem});
            this.shapeToolStripMenuItem.Name = "shapeToolStripMenuItem";
            this.shapeToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.shapeToolStripMenuItem.Text = "Shape";
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.Enabled = false;
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.analyzeToolStripMenuItem.Text = "Analyze current image";
            this.analyzeToolStripMenuItem.Click += new System.EventHandler(this.analyzeToolStripMenuItem_Click);
            // 
            // clearShapeDatabaseToolStripMenuItem
            // 
            this.clearShapeDatabaseToolStripMenuItem.Name = "clearShapeDatabaseToolStripMenuItem";
            this.clearShapeDatabaseToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.clearShapeDatabaseToolStripMenuItem.Text = "Clear shape database";
            this.clearShapeDatabaseToolStripMenuItem.Click += new System.EventHandler(this.clearShapeDatabaseToolStripMenuItem_Click);
            // 
            // importShapesKnowledgeBaseToolStripMenuItem
            // 
            this.importShapesKnowledgeBaseToolStripMenuItem.Name = "importShapesKnowledgeBaseToolStripMenuItem";
            this.importShapesKnowledgeBaseToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.importShapesKnowledgeBaseToolStripMenuItem.Text = "Import shape database";
            this.importShapesKnowledgeBaseToolStripMenuItem.Click += new System.EventHandler(this.importShapesKnowledgeBaseToolStripMenuItem_Click);
            // 
            // exportShapesKnowledgeBaseToolStripMenuItem
            // 
            this.exportShapesKnowledgeBaseToolStripMenuItem.Name = "exportShapesKnowledgeBaseToolStripMenuItem";
            this.exportShapesKnowledgeBaseToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.exportShapesKnowledgeBaseToolStripMenuItem.Text = "Export shape database";
            this.exportShapesKnowledgeBaseToolStripMenuItem.Click += new System.EventHandler(this.exportShapesKnowledgeBaseToolStripMenuItem_Click);
            // 
            // generatorToolStripMenuItem
            // 
            this.generatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importShapeAnalyzerToolStripMenuItem});
            this.generatorToolStripMenuItem.Name = "generatorToolStripMenuItem";
            this.generatorToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.generatorToolStripMenuItem.Text = "Generator";
            // 
            // importShapeAnalyzerToolStripMenuItem
            // 
            this.importShapeAnalyzerToolStripMenuItem.Name = "importShapeAnalyzerToolStripMenuItem";
            this.importShapeAnalyzerToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.importShapeAnalyzerToolStripMenuItem.Text = "Generate Microstructure";
            this.importShapeAnalyzerToolStripMenuItem.Click += new System.EventHandler(this.generateMicrostructureToolStripMenuItem_Click);
            // 
            // statisticToolStripMenuItem
            // 
            this.statisticToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeDistributionToolStripMenuItem,
            this.imageHistogramToolStripMenuItem});
            this.statisticToolStripMenuItem.Name = "statisticToolStripMenuItem";
            this.statisticToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.statisticToolStripMenuItem.Text = "Statistic";
            // 
            // sizeDistributionToolStripMenuItem
            // 
            this.sizeDistributionToolStripMenuItem.Name = "sizeDistributionToolStripMenuItem";
            this.sizeDistributionToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.sizeDistributionToolStripMenuItem.Text = "Size Distribution";
            this.sizeDistributionToolStripMenuItem.Click += new System.EventHandler(this.sizeDistributionToolStripMenuItem_Click);
            // 
            // imageHistogramToolStripMenuItem
            // 
            this.imageHistogramToolStripMenuItem.Name = "imageHistogramToolStripMenuItem";
            this.imageHistogramToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.imageHistogramToolStripMenuItem.Text = "Image Histogram";
            this.imageHistogramToolStripMenuItem.Click += new System.EventHandler(this.imageHistogramToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aToolStripMenuItem.Text = "Shape analyzer";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(912, 425);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ImageFileDialog
            // 
            this.ImageFileDialog.FileName = "openFileDialog1";
            this.ImageFileDialog.Title = "Load Image";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.shapeCountLabel);
            this.panel2.Controls.Add(this.volumeLabel);
            this.panel2.Controls.Add(this.grayLabel);
            this.panel2.Controls.Add(this.cordsLabel);
            this.panel2.Controls.Add(this.sizeLabel);
            this.panel2.Location = new System.Drawing.Point(12, 454);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(2);
            this.panel2.Size = new System.Drawing.Size(912, 23);
            this.panel2.TabIndex = 2;
            // 
            // shapeCountLabel
            // 
            this.shapeCountLabel.AutoSize = true;
            this.shapeCountLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.shapeCountLabel.Location = new System.Drawing.Point(438, 2);
            this.shapeCountLabel.MaximumSize = new System.Drawing.Size(200, 15);
            this.shapeCountLabel.MinimumSize = new System.Drawing.Size(100, 15);
            this.shapeCountLabel.Name = "shapeCountLabel";
            this.shapeCountLabel.Size = new System.Drawing.Size(100, 15);
            this.shapeCountLabel.TabIndex = 4;
            // 
            // volumeLabel
            // 
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.volumeLabel.Location = new System.Drawing.Point(323, 2);
            this.volumeLabel.MaximumSize = new System.Drawing.Size(200, 15);
            this.volumeLabel.MinimumSize = new System.Drawing.Size(100, 15);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(100, 15);
            this.volumeLabel.TabIndex = 3;
            // 
            // grayLabel
            // 
            this.grayLabel.AutoSize = true;
            this.grayLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grayLabel.Location = new System.Drawing.Point(217, 2);
            this.grayLabel.MaximumSize = new System.Drawing.Size(200, 15);
            this.grayLabel.MinimumSize = new System.Drawing.Size(100, 15);
            this.grayLabel.Name = "grayLabel";
            this.grayLabel.Size = new System.Drawing.Size(100, 15);
            this.grayLabel.TabIndex = 2;
            // 
            // cordsLabel
            // 
            this.cordsLabel.AutoSize = true;
            this.cordsLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cordsLabel.Location = new System.Drawing.Point(111, 2);
            this.cordsLabel.MaximumSize = new System.Drawing.Size(200, 15);
            this.cordsLabel.MinimumSize = new System.Drawing.Size(100, 15);
            this.cordsLabel.Name = "cordsLabel";
            this.cordsLabel.Size = new System.Drawing.Size(100, 15);
            this.cordsLabel.TabIndex = 1;
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.BackColor = System.Drawing.Color.Transparent;
            this.sizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sizeLabel.Location = new System.Drawing.Point(5, 2);
            this.sizeLabel.MaximumSize = new System.Drawing.Size(200, 15);
            this.sizeLabel.MinimumSize = new System.Drawing.Size(100, 15);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(100, 15);
            this.sizeLabel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 476);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Porous Microstructure Generator";
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
        private System.Windows.Forms.ToolStripMenuItem imageHistogramToolStripMenuItem;
        private System.Windows.Forms.Label shapeCountLabel;
        private System.Windows.Forms.ToolStripMenuItem clearShapeDatabaseToolStripMenuItem;
    }
}

