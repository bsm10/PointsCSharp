namespace DotsGame
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
            this.lstDbg1 = new System.Windows.Forms.ListBox();
            this.lstDbg2 = new System.Windows.Forms.ListBox();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.txtDotStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstDbg1
            // 
            this.lstDbg1.AccessibleName = "";
            this.lstDbg1.FormattingEnabled = true;
            this.lstDbg1.HorizontalScrollbar = true;
            this.lstDbg1.ItemHeight = 16;
            this.lstDbg1.Location = new System.Drawing.Point(16, 37);
            this.lstDbg1.Margin = new System.Windows.Forms.Padding(4);
            this.lstDbg1.Name = "lstDbg1";
            this.lstDbg1.Size = new System.Drawing.Size(199, 116);
            this.lstDbg1.TabIndex = 0;
            // 
            // lstDbg2
            // 
            this.lstDbg2.AccessibleName = "lstDbg2";
            this.lstDbg2.FormattingEnabled = true;
            this.lstDbg2.HorizontalScrollbar = true;
            this.lstDbg2.ItemHeight = 16;
            this.lstDbg2.Location = new System.Drawing.Point(223, 37);
            this.lstDbg2.Margin = new System.Windows.Forms.Padding(4);
            this.lstDbg2.Name = "lstDbg2";
            this.lstDbg2.Size = new System.Drawing.Size(218, 116);
            this.lstDbg2.TabIndex = 2;
            this.lstDbg2.SelectedIndexChanged += new System.EventHandler(this.lstDbg2_SelectedIndexChanged);
            // 
            // txtDebug
            // 
            this.txtDebug.BackColor = System.Drawing.SystemColors.Info;
            this.txtDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDebug.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDebug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtDebug.Location = new System.Drawing.Point(15, 160);
            this.txtDebug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(426, 150);
            this.txtDebug.TabIndex = 4;
            // 
            // txtDotStatus
            // 
            this.txtDotStatus.BackColor = System.Drawing.SystemColors.Info;
            this.txtDotStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDotStatus.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDotStatus.ForeColor = System.Drawing.Color.Navy;
            this.txtDotStatus.Location = new System.Drawing.Point(15, 315);
            this.txtDotStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDotStatus.Multiline = true;
            this.txtDotStatus.Name = "txtDotStatus";
            this.txtDotStatus.Size = new System.Drawing.Size(427, 269);
            this.txtDotStatus.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 594);
            this.Controls.Add(this.txtDotStatus);
            this.Controls.Add(this.txtDebug);
            this.Controls.Add(this.lstDbg2);
            this.Controls.Add(this.lstDbg1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox lstDbg2;
        public System.Windows.Forms.TextBox txtDebug;
        public System.Windows.Forms.TextBox txtDotStatus;
        public System.Windows.Forms.ListBox lstDbg1;
    }
}