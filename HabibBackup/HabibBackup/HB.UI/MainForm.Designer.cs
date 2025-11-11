
namespace HB.UI
{
    partial class HabibBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HabibBackup));
            this.srcPathBrowseBtn = new System.Windows.Forms.Button();
            this.srcPathTextBox = new System.Windows.Forms.TextBox();
            this.srcLabel = new System.Windows.Forms.Label();
            this.destPathBrowseBtn = new System.Windows.Forms.Button();
            this.destLabel = new System.Windows.Forms.Label();
            this.destPathTextBox = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.nextBtn = new System.Windows.Forms.Button();
            this.feebackLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // srcPathBrowseBtn
            // 
            this.srcPathBrowseBtn.Location = new System.Drawing.Point(543, 25);
            this.srcPathBrowseBtn.Name = "srcPathBrowseBtn";
            this.srcPathBrowseBtn.Size = new System.Drawing.Size(34, 20);
            this.srcPathBrowseBtn.TabIndex = 0;
            this.srcPathBrowseBtn.Text = "...";
            this.srcPathBrowseBtn.UseVisualStyleBackColor = true;
            this.srcPathBrowseBtn.Click += new System.EventHandler(this.srcPathBrowseBtn_Click);
            // 
            // srcPathTextBox
            // 
            this.srcPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.srcPathTextBox.Location = new System.Drawing.Point(15, 25);
            this.srcPathTextBox.Multiline = true;
            this.srcPathTextBox.Name = "srcPathTextBox";
            this.srcPathTextBox.ReadOnly = true;
            this.srcPathTextBox.Size = new System.Drawing.Size(522, 20);
            this.srcPathTextBox.TabIndex = 2;
            this.srcPathTextBox.TextChanged += new System.EventHandler(this.srcPathTextBox_TextChanged);
            // 
            // srcLabel
            // 
            this.srcLabel.AutoSize = true;
            this.srcLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.srcLabel.Location = new System.Drawing.Point(12, 9);
            this.srcLabel.Name = "srcLabel";
            this.srcLabel.Size = new System.Drawing.Size(120, 13);
            this.srcLabel.TabIndex = 3;
            this.srcLabel.Text = "Source Folder Location:";
            this.srcLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // destPathBrowseBtn
            // 
            this.destPathBrowseBtn.Location = new System.Drawing.Point(543, 79);
            this.destPathBrowseBtn.Name = "destPathBrowseBtn";
            this.destPathBrowseBtn.Size = new System.Drawing.Size(34, 20);
            this.destPathBrowseBtn.TabIndex = 1;
            this.destPathBrowseBtn.Text = "...";
            this.destPathBrowseBtn.UseVisualStyleBackColor = true;
            this.destPathBrowseBtn.Click += new System.EventHandler(this.destPathBrowseBtn_Click);
            // 
            // destLabel
            // 
            this.destLabel.AutoSize = true;
            this.destLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destLabel.Location = new System.Drawing.Point(12, 63);
            this.destLabel.Name = "destLabel";
            this.destLabel.Size = new System.Drawing.Size(139, 13);
            this.destLabel.TabIndex = 5;
            this.destLabel.Text = "Destination Folder Location:";
            this.destLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // destPathTextBox
            // 
            this.destPathTextBox.Location = new System.Drawing.Point(15, 79);
            this.destPathTextBox.Multiline = true;
            this.destPathTextBox.Name = "destPathTextBox";
            this.destPathTextBox.ReadOnly = true;
            this.destPathTextBox.Size = new System.Drawing.Size(522, 20);
            this.destPathTextBox.TabIndex = 4;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 163);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(502, 120);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(75, 23);
            this.nextBtn.TabIndex = 7;
            this.nextBtn.Text = "Start";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.Start_Click_1);
            // 
            // feebackLabel
            // 
            this.feebackLabel.AutoSize = true;
            this.feebackLabel.ForeColor = System.Drawing.Color.Green;
            this.feebackLabel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.feebackLabel.Location = new System.Drawing.Point(15, 130);
            this.feebackLabel.Name = "feebackLabel";
            this.feebackLabel.Size = new System.Drawing.Size(150, 13);
            this.feebackLabel.TabIndex = 8;
            this.feebackLabel.Text = "Backup finished with success.";
            this.feebackLabel.Visible = false;
            // 
            // HabibBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 163);
            this.Controls.Add(this.feebackLabel);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.destLabel);
            this.Controls.Add(this.destPathTextBox);
            this.Controls.Add(this.srcLabel);
            this.Controls.Add(this.srcPathTextBox);
            this.Controls.Add(this.destPathBrowseBtn);
            this.Controls.Add(this.srcPathBrowseBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "HabibBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Habib Backup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button srcPathBrowseBtn;
        private System.Windows.Forms.TextBox srcPathTextBox;
        private System.Windows.Forms.Label srcLabel;
        private System.Windows.Forms.Button destPathBrowseBtn;
        private System.Windows.Forms.Label destLabel;
        private System.Windows.Forms.TextBox destPathTextBox;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Label feebackLabel;
    }
}