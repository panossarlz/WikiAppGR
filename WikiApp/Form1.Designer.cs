namespace WikiApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnListen = new Button();
            btnSave = new Button();
            lblTitle = new Label();
            rtbExtract = new RichTextBox();
            pictureBox1 = new PictureBox();
            dgvFavorites = new DataGridView();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFavorites).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(326, 116);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(240, 23);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(608, 116);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 41);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnListen
            // 
            btnListen.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnListen.Location = new Point(729, 116);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(102, 41);
            btnListen.TabIndex = 2;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(729, 423);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 41);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(38, 126);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(62, 30);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "TITLE";
            // 
            // rtbExtract
            // 
            rtbExtract.Location = new Point(326, 174);
            rtbExtract.Name = "rtbExtract";
            rtbExtract.Size = new Size(376, 290);
            rtbExtract.TabIndex = 5;
            rtbExtract.Text = "";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(38, 174);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(262, 290);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // dgvFavorites
            // 
            dgvFavorites.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFavorites.Location = new Point(729, 174);
            dgvFavorites.Name = "dgvFavorites";
            dgvFavorites.Size = new Size(191, 233);
            dgvFavorites.TabIndex = 7;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(837, 423);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(89, 41);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(938, 539);
            Controls.Add(btnDelete);
            Controls.Add(dgvFavorites);
            Controls.Add(pictureBox1);
            Controls.Add(rtbExtract);
            Controls.Add(lblTitle);
            Controls.Add(btnSave);
            Controls.Add(btnListen);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFavorites).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnListen;
        private Button btnSave;
        private Label lblTitle;
        private RichTextBox rtbExtract;
        private PictureBox pictureBox1;
        private DataGridView dgvFavorites;
        private Button btnDelete;
    }
}
