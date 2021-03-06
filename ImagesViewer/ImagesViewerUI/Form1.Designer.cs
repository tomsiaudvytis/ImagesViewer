﻿namespace ImagesViewerUI
{
    partial class ImgViewerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBoxSearchName = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.dataGridPictures = new System.Windows.Forms.DataGridView();
            this.picBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPictures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seach Name";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(205, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtBoxSearchName
            // 
            this.txtBoxSearchName.Location = new System.Drawing.Point(88, 13);
            this.txtBoxSearchName.Name = "txtBoxSearchName";
            this.txtBoxSearchName.Size = new System.Drawing.Size(100, 20);
            this.txtBoxSearchName.TabIndex = 2;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(16, 391);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // dataGridPictures
            // 
            this.dataGridPictures.Location = new System.Drawing.Point(16, 50);
            this.dataGridPictures.Name = "dataGridPictures";
            this.dataGridPictures.ReadOnly = true;
            this.dataGridPictures.Size = new System.Drawing.Size(396, 168);
            this.dataGridPictures.TabIndex = 0;
            this.dataGridPictures.DoubleClick += new System.EventHandler(this.DataGridPictures_DoubleClick);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(436, 50);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(630, 308);
            this.picBox.TabIndex = 4;
            this.picBox.TabStop = false;
            // 
            // ImgViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 418);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.dataGridPictures);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtBoxSearchName);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Name = "ImgViewerForm";
            this.Text = "Images viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPictures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtBoxSearchName;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.DataGridView dataGridPictures;
        private System.Windows.Forms.PictureBox picBox;
    }
}

