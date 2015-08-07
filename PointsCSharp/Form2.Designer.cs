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
            this.lstPoints = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstRPoints = new System.Windows.Forms.ListBox();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstPoints
            // 
            this.lstPoints.AccessibleName = "lstPoints";
            this.lstPoints.FormattingEnabled = true;
            this.lstPoints.HorizontalScrollbar = true;
            this.lstPoints.ItemHeight = 16;
            this.lstPoints.Location = new System.Drawing.Point(16, 37);
            this.lstPoints.Margin = new System.Windows.Forms.Padding(4);
            this.lstPoints.Name = "lstPoints";
            this.lstPoints.Size = new System.Drawing.Size(264, 228);
            this.lstPoints.TabIndex = 0;
            this.lstPoints.SelectedIndexChanged += new System.EventHandler(this.lstPoints_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "aPoints";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "RelationPoints";
            // 
            // lstRPoints
            // 
            this.lstRPoints.FormattingEnabled = true;
            this.lstRPoints.HorizontalScrollbar = true;
            this.lstRPoints.ItemHeight = 16;
            this.lstRPoints.Location = new System.Drawing.Point(289, 37);
            this.lstRPoints.Margin = new System.Windows.Forms.Padding(4);
            this.lstRPoints.Name = "lstRPoints";
            this.lstRPoints.Size = new System.Drawing.Size(264, 228);
            this.lstRPoints.TabIndex = 2;
            // 
            // txtDebug
            // 
            this.txtDebug.BackColor = System.Drawing.SystemColors.Info;
            this.txtDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDebug.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDebug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtDebug.Location = new System.Drawing.Point(16, 272);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(537, 227);
            this.txtDebug.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 516);
            this.Controls.Add(this.txtDebug);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstRPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPoints);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstRPoints;
        public System.Windows.Forms.TextBox txtDebug;
    }
}