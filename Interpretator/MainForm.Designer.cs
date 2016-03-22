namespace Interpretator
{
    partial class Form_interpretator
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_interpretator));
			this.Run_button = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.Container2 = new System.Windows.Forms.SplitContainer();
			this.Code_info = new System.Windows.Forms.RichTextBox();
			this.Info = new System.Windows.Forms.RichTextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveAsDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGrid = new System.Windows.Forms.DataGridView();
			this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Status = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.label3 = new System.Windows.Forms.Label();
			this.Result = new System.Windows.Forms.RichTextBox();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveInDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label4 = new System.Windows.Forms.Label();
			this.Errors = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Code_panel = new System.Windows.Forms.ToolStripMenuItem();
			this.Detailed_panel = new System.Windows.Forms.ToolStripMenuItem();
			this.Variables_panel = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Container2)).BeginInit();
			this.Container2.Panel1.SuspendLayout();
			this.Container2.Panel2.SuspendLayout();
			this.Container2.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Run_button
			// 
			this.Run_button.BackColor = System.Drawing.Color.MediumAquamarine;
			this.Run_button.Dock = System.Windows.Forms.DockStyle.Right;
			this.Run_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Run_button.Location = new System.Drawing.Point(819, 24);
			this.Run_button.Name = "Run_button";
			this.Run_button.Size = new System.Drawing.Size(57, 455);
			this.Run_button.TabIndex = 1;
			this.Run_button.Text = "RUN";
			this.Run_button.UseVisualStyleBackColor = false;
			this.Run_button.Click += new System.EventHandler(this.Run_button_Click);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(0, 24);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 327);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.Container2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dataGrid);
			this.splitContainer1.Size = new System.Drawing.Size(816, 327);
			this.splitContainer1.SplitterDistance = 567;
			this.splitContainer1.TabIndex = 4;
			// 
			// Container2
			// 
			this.Container2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Container2.Location = new System.Drawing.Point(0, 0);
			this.Container2.Name = "Container2";
			// 
			// Container2.Panel1
			// 
			this.Container2.Panel1.Controls.Add(this.Code_info);
			// 
			// Container2.Panel2
			// 
			this.Container2.Panel2.Controls.Add(this.Info);
			this.Container2.Panel2Collapsed = true;
			this.Container2.Size = new System.Drawing.Size(567, 327);
			this.Container2.SplitterDistance = 347;
			this.Container2.TabIndex = 0;
			// 
			// Code_info
			// 
			this.Code_info.BackColor = System.Drawing.Color.Black;
			this.Code_info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Code_info.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Code_info.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.Code_info.Location = new System.Drawing.Point(0, 0);
			this.Code_info.Name = "Code_info";
			this.Code_info.Size = new System.Drawing.Size(567, 327);
			this.Code_info.TabIndex = 9;
			this.Code_info.Text = resources.GetString("Code_info.Text");
			// 
			// Info
			// 
			this.Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Info.ContextMenuStrip = this.contextMenuStrip1;
			this.Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Info.ForeColor = System.Drawing.SystemColors.Info;
			this.Info.Location = new System.Drawing.Point(0, 0);
			this.Info.Name = "Info";
			this.Info.ReadOnly = true;
			this.Info.Size = new System.Drawing.Size(216, 327);
			this.Info.TabIndex = 7;
			this.Info.Text = "Detailed info:";
			this.Info.TextChanged += new System.EventHandler(this.Info_TextChanged);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsDocumentToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(171, 26);
			// 
			// saveAsDocumentToolStripMenuItem
			// 
			this.saveAsDocumentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsDocumentToolStripMenuItem.Image")));
			this.saveAsDocumentToolStripMenuItem.Name = "saveAsDocumentToolStripMenuItem";
			this.saveAsDocumentToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.saveAsDocumentToolStripMenuItem.Text = "Save as document";
			this.saveAsDocumentToolStripMenuItem.Click += new System.EventHandler(this.saveAsDocumentToolStripMenuItem_Click);
			// 
			// dataGrid
			// 
			this.dataGrid.AllowUserToAddRows = false;
			this.dataGrid.AllowUserToDeleteRows = false;
			this.dataGrid.AllowUserToOrderColumns = true;
			this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Variable,
            this.Type,
            this.Value});
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.Location = new System.Drawing.Point(0, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.ReadOnly = true;
			this.dataGrid.Size = new System.Drawing.Size(245, 327);
			this.dataGrid.TabIndex = 2;
			// 
			// Variable
			// 
			this.Variable.HeaderText = "Variable";
			this.Variable.Name = "Variable";
			this.Variable.ReadOnly = true;
			// 
			// Type
			// 
			this.Type.HeaderText = "Type";
			this.Type.Name = "Type";
			this.Type.ReadOnly = true;
			// 
			// Value
			// 
			this.Value.HeaderText = "Value";
			this.Value.Name = "Value";
			this.Value.ReadOnly = true;
			// 
			// Status
			// 
			this.Status.AutoSize = true;
			this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Status.ForeColor = System.Drawing.Color.SpringGreen;
			this.Status.Location = new System.Drawing.Point(637, 4);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(0, 15);
			this.Status.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.panel1.Controls.Add(this.splitContainer3);
			this.panel1.Controls.Add(this.Status);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 351);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(819, 128);
			this.panel1.TabIndex = 2;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.label3);
			this.splitContainer3.Panel1.Controls.Add(this.Result);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.label4);
			this.splitContainer3.Panel2.Controls.Add(this.Errors);
			this.splitContainer3.Size = new System.Drawing.Size(819, 128);
			this.splitContainer3.SplitterDistance = 398;
			this.splitContainer3.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Parsing result";
			// 
			// Result
			// 
			this.Result.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.Result.ContextMenuStrip = this.contextMenuStrip2;
			this.Result.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Result.ForeColor = System.Drawing.SystemColors.Info;
			this.Result.Location = new System.Drawing.Point(0, 19);
			this.Result.Margin = new System.Windows.Forms.Padding(0);
			this.Result.Name = "Result";
			this.Result.ReadOnly = true;
			this.Result.Size = new System.Drawing.Size(398, 109);
			this.Result.TabIndex = 3;
			this.Result.Text = "";
			this.Result.TextChanged += new System.EventHandler(this.Result_TextChanged);
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveInDocumentToolStripMenuItem});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(170, 26);
			// 
			// saveInDocumentToolStripMenuItem
			// 
			this.saveInDocumentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveInDocumentToolStripMenuItem.Image")));
			this.saveInDocumentToolStripMenuItem.Name = "saveInDocumentToolStripMenuItem";
			this.saveInDocumentToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.saveInDocumentToolStripMenuItem.Text = "Save in document";
			this.saveInDocumentToolStripMenuItem.Click += new System.EventHandler(this.saveInDocumentToolStripMenuItem_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(52, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Warnings";
			// 
			// Errors
			// 
			this.Errors.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.Errors.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Errors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Errors.ForeColor = System.Drawing.Color.Maroon;
			this.Errors.Location = new System.Drawing.Point(0, 19);
			this.Errors.Margin = new System.Windows.Forms.Padding(0);
			this.Errors.Name = "Errors";
			this.Errors.ReadOnly = true;
			this.Errors.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.Errors.Size = new System.Drawing.Size(417, 109);
			this.Errors.TabIndex = 3;
			this.Errors.Text = "";
			this.Errors.TextChanged += new System.EventHandler(this.Errors_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(588, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Status :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Output";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(876, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.saveAsToolStripMenuItem.Text = "Save as";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.Checked = true;
			this.viewToolStripMenuItem.CheckOnClick = true;
			this.viewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Code_panel,
            this.Detailed_panel,
            this.Variables_panel});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// Code_panel
			// 
			this.Code_panel.Checked = true;
			this.Code_panel.CheckOnClick = true;
			this.Code_panel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Code_panel.Name = "Code_panel";
			this.Code_panel.Size = new System.Drawing.Size(152, 22);
			this.Code_panel.Text = "Code panel";
			this.Code_panel.Click += new System.EventHandler(this.Code_panel_Click);
			// 
			// Detailed_panel
			// 
			this.Detailed_panel.CheckOnClick = true;
			this.Detailed_panel.Name = "Detailed_panel";
			this.Detailed_panel.Size = new System.Drawing.Size(152, 22);
			this.Detailed_panel.Text = "Detailed info";
			this.Detailed_panel.Click += new System.EventHandler(this.Detailed_panel_Click);
			// 
			// Variables_panel
			// 
			this.Variables_panel.Checked = true;
			this.Variables_panel.CheckOnClick = true;
			this.Variables_panel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Variables_panel.Name = "Variables_panel";
			this.Variables_panel.Size = new System.Drawing.Size(152, 22);
			this.Variables_panel.Text = "Variables";
			this.Variables_panel.Click += new System.EventHandler(this.Variables_panel_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "File";
			this.openFileDialog1.Filter = "txtx|*.txt";
			this.openFileDialog1.Title = "Open new code instance";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "txt|*.txt";
			this.saveFileDialog.Title = "Save code as";
			// 
			// Form_interpretator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 479);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.Run_button);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form_interpretator";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Interpretator Lab1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Form_interpretator_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.Container2.Panel1.ResumeLayout(false);
			this.Container2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Container2)).EndInit();
			this.Container2.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Run_button;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer Container2;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.RichTextBox Result;
        internal System.Windows.Forms.RichTextBox Errors;
        internal System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        internal System.Windows.Forms.RichTextBox Code_info;
        private System.Windows.Forms.RichTextBox Info;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Code_panel;
        private System.Windows.Forms.ToolStripMenuItem Detailed_panel;
        private System.Windows.Forms.ToolStripMenuItem Variables_panel;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem saveAsDocumentToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.ToolStripMenuItem saveInDocumentToolStripMenuItem;
	}
}

