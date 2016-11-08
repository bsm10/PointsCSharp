namespace DotsGame
{
    partial class DebugForm
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
        /// Required method for Designer support -do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugForm));
            this.lstDbg1 = new System.Windows.Forms.ListBox();
            this.lstDbg2 = new System.Windows.Forms.ListBox();
            this.debugInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.txtDotStatus = new System.Windows.Forms.TextBox();
            this.rbtnAuto = new System.Windows.Forms.RadioButton();
            this.rbtnHand = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.chkMove = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.rtbStat = new System.Windows.Forms.RichTextBox();
            this.lstMoves = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlsEditPattern = new System.Windows.Forms.ToolStripButton();
            this.tlsТочкаОтсчета = new System.Windows.Forms.ToolStripButton();
            this.tlsПустая = new System.Windows.Forms.ToolStripButton();
            this.tlsКромеВражеской = new System.Windows.Forms.ToolStripButton();
            this.tlsТочкаХода = new System.Windows.Forms.ToolStripButton();
            this.tlsMakePattern = new System.Windows.Forms.ToolStripButton();
            this.lblBestMove = new System.Windows.Forms.Label();
            this.txtBestMove = new System.Windows.Forms.TextBox();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDrawPattern = new System.Windows.Forms.Button();
            this.debugInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.debugInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.debugInfoBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // lstDbg1
            // 
            this.lstDbg1.AccessibleName = "";
            this.lstDbg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDbg1.FormattingEnabled = true;
            this.lstDbg1.HorizontalScrollbar = true;
            this.lstDbg1.ItemHeight = 16;
            this.lstDbg1.Location = new System.Drawing.Point(16, 37);
            this.lstDbg1.Margin = new System.Windows.Forms.Padding(4);
            this.lstDbg1.Name = "lstDbg1";
            this.lstDbg1.Size = new System.Drawing.Size(127, 132);
            this.lstDbg1.TabIndex = 0;
            // 
            // lstDbg2
            // 
            this.lstDbg2.AccessibleName = "lstDbg2";
            this.lstDbg2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDbg2.FormattingEnabled = true;
            this.lstDbg2.HorizontalScrollbar = true;
            this.lstDbg2.ItemHeight = 16;
            this.lstDbg2.Location = new System.Drawing.Point(143, 37);
            this.lstDbg2.Margin = new System.Windows.Forms.Padding(4);
            this.lstDbg2.Name = "lstDbg2";
            this.lstDbg2.Size = new System.Drawing.Size(463, 132);
            this.lstDbg2.TabIndex = 2;
            // 
            // debugInfoBindingSource
            // 
            this.debugInfoBindingSource.DataSource = typeof(DotsGame.DebugInfo);
            // 
            // txtDebug
            // 
            this.txtDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebug.BackColor = System.Drawing.SystemColors.Info;
            this.txtDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDebug.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDebug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtDebug.Location = new System.Drawing.Point(15, 181);
            this.txtDebug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(342, 171);
            this.txtDebug.TabIndex = 4;
            // 
            // txtDotStatus
            // 
            this.txtDotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDotStatus.BackColor = System.Drawing.SystemColors.Info;
            this.txtDotStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDotStatus.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDotStatus.ForeColor = System.Drawing.Color.Navy;
            this.txtDotStatus.Location = new System.Drawing.Point(13, 357);
            this.txtDotStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDotStatus.Multiline = true;
            this.txtDotStatus.Name = "txtDotStatus";
            this.txtDotStatus.Size = new System.Drawing.Size(267, 192);
            this.txtDotStatus.TabIndex = 5;
            // 
            // rbtnAuto
            // 
            this.rbtnAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnAuto.AutoSize = true;
            this.rbtnAuto.Location = new System.Drawing.Point(456, 185);
            this.rbtnAuto.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnAuto.Name = "rbtnAuto";
            this.rbtnAuto.Size = new System.Drawing.Size(60, 21);
            this.rbtnAuto.TabIndex = 27;
            this.rbtnAuto.TabStop = true;
            this.rbtnAuto.Text = "Авто";
            this.rbtnAuto.UseVisualStyleBackColor = true;
            // 
            // rbtnHand
            // 
            this.rbtnHand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnHand.AutoSize = true;
            this.rbtnHand.Location = new System.Drawing.Point(366, 185);
            this.rbtnHand.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnHand.Name = "rbtnHand";
            this.rbtnHand.Size = new System.Drawing.Size(77, 21);
            this.rbtnHand.TabIndex = 26;
            this.rbtnHand.TabStop = true;
            this.rbtnHand.Text = "Ручной";
            this.rbtnHand.UseVisualStyleBackColor = true;
            this.rbtnHand.CheckedChanged += new System.EventHandler(this.rbtnHand_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(691, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 18);
            this.label4.TabIndex = 25;
            this.label4.Text = "SkillNumSq";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown3.CausesValidation = false;
            this.numericUpDown3.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.numericUpDown3.Location = new System.Drawing.Point(611, 29);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(61, 23);
            this.numericUpDown3.TabIndex = 21;
            this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown3.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown2.CausesValidation = false;
            this.numericUpDown2.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.numericUpDown2.Location = new System.Drawing.Point(611, 56);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(61, 23);
            this.numericUpDown2.TabIndex = 19;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown2.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(609, 116);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Pause ms";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown1.CausesValidation = false;
            this.numericUpDown1.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.numericUpDown1.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(613, 139);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(61, 23);
            this.numericUpDown1.TabIndex = 17;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(679, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Глубина рекурсии";
            // 
            // chkMove
            // 
            this.chkMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMove.AutoSize = true;
            this.chkMove.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkMove.ForeColor = System.Drawing.Color.Navy;
            this.chkMove.Location = new System.Drawing.Point(691, 140);
            this.chkMove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkMove.Name = "chkMove";
            this.chkMove.Size = new System.Drawing.Size(71, 24);
            this.chkMove.TabIndex = 23;
            this.chkMove.Text = "ходы";
            this.chkMove.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(679, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 22;
            this.label3.Text = "SkillLevel";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown4.CausesValidation = false;
            this.numericUpDown4.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.numericUpDown4.Location = new System.Drawing.Point(613, 86);
            this.numericUpDown4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown4.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(61, 23);
            this.numericUpDown4.TabIndex = 24;
            this.numericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown4.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // rtbStat
            // 
            this.rtbStat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbStat.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbStat.Location = new System.Drawing.Point(560, 471);
            this.rtbStat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbStat.Name = "rtbStat";
            this.rtbStat.ReadOnly = true;
            this.rtbStat.Size = new System.Drawing.Size(180, 107);
            this.rtbStat.TabIndex = 29;
            this.rtbStat.Text = "";
            // 
            // lstMoves
            // 
            this.lstMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMoves.BackColor = System.Drawing.SystemColors.Control;
            this.lstMoves.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstMoves.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstMoves.ForeColor = System.Drawing.Color.Teal;
            this.lstMoves.FormattingEnabled = true;
            this.lstMoves.ItemHeight = 18;
            this.lstMoves.Location = new System.Drawing.Point(364, 213);
            this.lstMoves.Margin = new System.Windows.Forms.Padding(4);
            this.lstMoves.Name = "lstMoves";
            this.lstMoves.Size = new System.Drawing.Size(172, 126);
            this.lstMoves.TabIndex = 28;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsEditPattern,
            this.tlsТочкаОтсчета,
            this.tlsПустая,
            this.tlsКромеВражеской,
            this.tlsТочкаХода,
            this.tlsMakePattern});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(836, 27);
            this.toolStrip1.TabIndex = 30;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlsEditPattern
            // 
            this.tlsEditPattern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlsEditPattern.Image = ((System.Drawing.Image)(resources.GetObject("tlsEditPattern.Image")));
            this.tlsEditPattern.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsEditPattern.Name = "tlsEditPattern";
            this.tlsEditPattern.Size = new System.Drawing.Size(91, 24);
            this.tlsEditPattern.Text = "Edit pattern";
            this.tlsEditPattern.Click += new System.EventHandler(this.tlsEditPattern_Click);
            // 
            // tlsТочкаОтсчета
            // 
            this.tlsТочкаОтсчета.CheckOnClick = true;
            this.tlsТочкаОтсчета.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsТочкаОтсчета.Enabled = false;
            this.tlsТочкаОтсчета.Image = ((System.Drawing.Image)(resources.GetObject("tlsТочкаОтсчета.Image")));
            this.tlsТочкаОтсчета.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsТочкаОтсчета.Name = "tlsТочкаОтсчета";
            this.tlsТочкаОтсчета.Size = new System.Drawing.Size(24, 24);
            this.tlsТочкаОтсчета.Text = "Точка отсчета";
            this.tlsТочкаОтсчета.CheckedChanged += new System.EventHandler(this.tlsТочкаОтсчета_CheckedChanged);
            // 
            // tlsПустая
            // 
            this.tlsПустая.CheckOnClick = true;
            this.tlsПустая.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsПустая.Enabled = false;
            this.tlsПустая.Image = ((System.Drawing.Image)(resources.GetObject("tlsПустая.Image")));
            this.tlsПустая.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsПустая.Name = "tlsПустая";
            this.tlsПустая.Size = new System.Drawing.Size(24, 24);
            this.tlsПустая.Text = "Отметить точку";
            this.tlsПустая.CheckedChanged += new System.EventHandler(this.tlsПустая_CheckedChanged);
            // 
            // tlsКромеВражеской
            // 
            this.tlsКромеВражеской.CheckOnClick = true;
            this.tlsКромеВражеской.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsКромеВражеской.Enabled = false;
            this.tlsКромеВражеской.Image = ((System.Drawing.Image)(resources.GetObject("tlsКромеВражеской.Image")));
            this.tlsКромеВражеской.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsКромеВражеской.Name = "tlsКромеВражеской";
            this.tlsКромеВражеской.Size = new System.Drawing.Size(24, 24);
            this.tlsКромеВражеской.Text = "Любая, кроме вражеской";
            this.tlsКромеВражеской.CheckedChanged += new System.EventHandler(this.tlsКромеВражеской_CheckedChanged);
            this.tlsКромеВражеской.Click += new System.EventHandler(this.tlsКромеВражеской_Click);
            // 
            // tlsТочкаХода
            // 
            this.tlsТочкаХода.CheckOnClick = true;
            this.tlsТочкаХода.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsТочкаХода.Enabled = false;
            this.tlsТочкаХода.Image = ((System.Drawing.Image)(resources.GetObject("tlsТочкаХода.Image")));
            this.tlsТочкаХода.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsТочкаХода.Name = "tlsТочкаХода";
            this.tlsТочкаХода.Size = new System.Drawing.Size(24, 24);
            this.tlsТочкаХода.Text = "Точка хода";
            this.tlsТочкаХода.CheckedChanged += new System.EventHandler(this.tlsТочкаХода_CheckedChanged);
            // 
            // tlsMakePattern
            // 
            this.tlsMakePattern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsMakePattern.Image = global::DotsGame.Properties.Resources.Лист;
            this.tlsMakePattern.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsMakePattern.Name = "tlsMakePattern";
            this.tlsMakePattern.Size = new System.Drawing.Size(24, 24);
            this.tlsMakePattern.Text = "MakePattern";
            this.tlsMakePattern.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tlsMakePattern.Click += new System.EventHandler(this.tlsMakePattern_Click);
            // 
            // lblBestMove
            // 
            this.lblBestMove.AutoSize = true;
            this.lblBestMove.Location = new System.Drawing.Point(16, 562);
            this.lblBestMove.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBestMove.Name = "lblBestMove";
            this.lblBestMove.Size = new System.Drawing.Size(79, 17);
            this.lblBestMove.TabIndex = 31;
            this.lblBestMove.Text = "BestMove -";
            // 
            // txtBestMove
            // 
            this.txtBestMove.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBestMove.BackColor = System.Drawing.SystemColors.Info;
            this.txtBestMove.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBestMove.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBestMove.ForeColor = System.Drawing.Color.Maroon;
            this.txtBestMove.Location = new System.Drawing.Point(287, 357);
            this.txtBestMove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBestMove.Multiline = true;
            this.txtBestMove.Name = "txtBestMove";
            this.txtBestMove.Size = new System.Drawing.Size(229, 192);
            this.txtBestMove.TabIndex = 32;
            // 
            // txtPattern
            // 
            this.txtPattern.AcceptsReturn = true;
            this.txtPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPattern.Location = new System.Drawing.Point(523, 286);
            this.txtPattern.Margin = new System.Windows.Forms.Padding(4);
            this.txtPattern.Multiline = true;
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(307, 263);
            this.txtPattern.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(519, 266);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 34;
            this.label5.Text = "Patterns";
            // 
            // btnDrawPattern
            // 
            this.btnDrawPattern.Location = new System.Drawing.Point(588, 254);
            this.btnDrawPattern.Margin = new System.Windows.Forms.Padding(4);
            this.btnDrawPattern.Name = "btnDrawPattern";
            this.btnDrawPattern.Size = new System.Drawing.Size(105, 28);
            this.btnDrawPattern.TabIndex = 35;
            this.btnDrawPattern.Text = "DrawPattern";
            this.btnDrawPattern.UseVisualStyleBackColor = true;
            this.btnDrawPattern.Click += new System.EventHandler(this.btnDrawPattern_Click);
            // 
            // debugInfoBindingSource1
            // 
            this.debugInfoBindingSource1.DataSource = typeof(DotsGame.DebugInfo);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 594);
            this.Controls.Add(this.btnDrawPattern);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPattern);
            this.Controls.Add(this.txtBestMove);
            this.Controls.Add(this.lblBestMove);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.rtbStat);
            this.Controls.Add(this.lstMoves);
            this.Controls.Add(this.rbtnAuto);
            this.Controls.Add(this.rbtnHand);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkMove);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.txtDotStatus);
            this.Controls.Add(this.txtDebug);
            this.Controls.Add(this.lstDbg2);
            this.Controls.Add(this.lstDbg1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DebugForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.MouseEnter += new System.EventHandler(this.Form2_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.debugInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.debugInfoBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox lstDbg2;
        public System.Windows.Forms.TextBox txtDebug;
        public System.Windows.Forms.TextBox txtDotStatus;
        public System.Windows.Forms.ListBox lstDbg1;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.RichTextBox rtbStat;
        public System.Windows.Forms.ListBox lstMoves;
        public System.Windows.Forms.CheckBox chkMove;
        public System.Windows.Forms.RadioButton rbtnAuto;
        public System.Windows.Forms.RadioButton rbtnHand;
        public System.Windows.Forms.NumericUpDown numericUpDown3;
        public System.Windows.Forms.NumericUpDown numericUpDown2;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.NumericUpDown numericUpDown4;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton tlsEditPattern;
        public System.Windows.Forms.ToolStripButton tlsПустая;
        public System.Windows.Forms.ToolStripButton tlsТочкаОтсчета;
        public System.Windows.Forms.ToolStripButton tlsТочкаХода;
        public System.Windows.Forms.ToolStripButton tlsКромеВражеской;
        public System.Windows.Forms.Label lblBestMove;
        public System.Windows.Forms.TextBox txtBestMove;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDrawPattern;
        private System.Windows.Forms.ToolStripButton tlsMakePattern;
        public System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.BindingSource debugInfoBindingSource;
        private System.Windows.Forms.BindingSource debugInfoBindingSource1;
    }
}