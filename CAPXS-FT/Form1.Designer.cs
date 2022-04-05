namespace CAPXS_FT
{
    partial class AlignmentTest
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
            this.lbl_Fix_Status = new System.Windows.Forms.Label();
            this.lbl_Fixed_DUT = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.lbl_DUT = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Fix_Status
            // 
            this.lbl_Fix_Status.AutoSize = true;
            this.lbl_Fix_Status.Location = new System.Drawing.Point(12, 17);
            this.lbl_Fix_Status.Name = "lbl_Fix_Status";
            this.lbl_Fix_Status.Size = new System.Drawing.Size(107, 15);
            this.lbl_Fix_Status.TabIndex = 0;
            this.lbl_Fix_Status.Text = "Connection Status:";
            // 
            // lbl_Fixed_DUT
            // 
            this.lbl_Fixed_DUT.AutoSize = true;
            this.lbl_Fixed_DUT.Location = new System.Drawing.Point(12, 38);
            this.lbl_Fixed_DUT.Name = "lbl_Fixed_DUT";
            this.lbl_Fixed_DUT.Size = new System.Drawing.Size(106, 15);
            this.lbl_Fixed_DUT.TabIndex = 1;
            this.lbl_Fixed_DUT.Text = "Device Under Test: ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(643, 343);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(523, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 49);
            this.button1.TabIndex = 3;
            this.button1.Text = "Test Alignment";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.ForeColor = System.Drawing.Color.Silver;
            this.lbl_status.Location = new System.Drawing.Point(125, 17);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(79, 15);
            this.lbl_status.TabIndex = 5;
            this.lbl_status.Text = "Disconnected";
            // 
            // lbl_DUT
            // 
            this.lbl_DUT.AutoSize = true;
            this.lbl_DUT.ForeColor = System.Drawing.Color.Silver;
            this.lbl_DUT.Location = new System.Drawing.Point(124, 38);
            this.lbl_DUT.Name = "lbl_DUT";
            this.lbl_DUT.Size = new System.Drawing.Size(44, 15);
            this.lbl_DUT.TabIndex = 6;
            this.lbl_DUT.Text = "[None]";
            // 
            // AlignmentTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 420);
            this.Controls.Add(this.lbl_DUT);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_Fixed_DUT);
            this.Controls.Add(this.lbl_Fix_Status);
            this.Name = "AlignmentTest";
            this.Text = "Camera Alignment";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Fix_Status;
        private System.Windows.Forms.Label lbl_Fixed_DUT;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label lbl_DUT;
    }
}

