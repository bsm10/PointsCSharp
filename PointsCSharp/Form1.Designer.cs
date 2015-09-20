namespace DotsGame
{
    public partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pbxBoard = new System.Windows.Forms.PictureBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитькакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.предварительныйпросмотрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.легкоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.среднеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тяжелоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.networkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.антиалToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветКурсораToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветИгрокаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветПротивникаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выделитьШаблонToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.точкаОтсчетаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.точкаХодаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.любаяТочкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пустаяТочкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.содержаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.опрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.listMovesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBoard)).BeginInit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMovesBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxBoard
            // 
            this.pbxBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxBoard.BackColor = System.Drawing.Color.White;
            this.pbxBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbxBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxBoard.Location = new System.Drawing.Point(0, 32);
            this.pbxBoard.Margin = new System.Windows.Forms.Padding(4);
            this.pbxBoard.Name = "pbxBoard";
            this.pbxBoard.Size = new System.Drawing.Size(461, 519);
            this.pbxBoard.TabIndex = 0;
            this.pbxBoard.TabStop = false;
            this.pbxBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pbxBoard_Paint);
            this.pbxBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxBoard_MouseClick);
            this.pbxBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxBoard_MouseDown);
            this.pbxBoard.MouseEnter += new System.EventHandler(this.pbxBoard_MouseEnter);
            this.pbxBoard.MouseLeave += new System.EventHandler(this.pbxBoard_MouseLeave);
            this.pbxBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxBoard_MouseMove);
            this.pbxBoard.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pbxBoard_MouseWheel);
            // 
            // menuStrip
            // 
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(326, 28);
            this.menuStrip.TabIndex = 2;
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.toolStripSeparator,
            this.сохранитьToolStripMenuItem,
            this.сохранитькакToolStripMenuItem,
            this.toolStripSeparator1,
            this.печатьToolStripMenuItem,
            this.предварительныйпросмотрToolStripMenuItem,
            this.toolStripSeparator2,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "&Файл";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.создатьToolStripMenuItem.Text = "&Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.открытьToolStripMenuItem.Text = "&Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(283, 6);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.сохранитьToolStripMenuItem.Text = "&Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитькакToolStripMenuItem
            // 
            this.сохранитькакToolStripMenuItem.Name = "сохранитькакToolStripMenuItem";
            this.сохранитькакToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.сохранитькакToolStripMenuItem.Text = "Сохранить &как";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(283, 6);
            // 
            // печатьToolStripMenuItem
            // 
            this.печатьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.печатьToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.печатьToolStripMenuItem.Text = "&Печать";
            // 
            // предварительныйпросмотрToolStripMenuItem
            // 
            this.предварительныйпросмотрToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.предварительныйпросмотрToolStripMenuItem.Name = "предварительныйпросмотрToolStripMenuItem";
            this.предварительныйпросмотрToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.предварительныйпросмотрToolStripMenuItem.Text = "Предварительный про&смотр";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(283, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.выходToolStripMenuItem.Text = "Вы&ход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelToolStripMenuItem,
            this.boardToolStripMenuItem,
            this.networkToolStripMenuItem,
            this.антиалToolStripMenuItem,
            this.цветКурсораToolStripMenuItem,
            this.цветИгрокаToolStripMenuItem,
            this.цветПротивникаToolStripMenuItem,
            this.autoplayToolStripMenuItem,
            this.выделитьШаблонToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.optionsToolStripMenuItem.Text = "&Опции";
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.легкоToolStripMenuItem,
            this.среднеToolStripMenuItem,
            this.тяжелоToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.levelToolStripMenuItem.Text = "&Сложность";
            // 
            // легкоToolStripMenuItem
            // 
            this.легкоToolStripMenuItem.CheckOnClick = true;
            this.легкоToolStripMenuItem.Name = "легкоToolStripMenuItem";
            this.легкоToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.легкоToolStripMenuItem.Text = "легко";
            this.легкоToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.легкоToolStripMenuItem_CheckStateChanged);
            // 
            // среднеToolStripMenuItem
            // 
            this.среднеToolStripMenuItem.CheckOnClick = true;
            this.среднеToolStripMenuItem.Name = "среднеToolStripMenuItem";
            this.среднеToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.среднеToolStripMenuItem.Text = "средне";
            this.среднеToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.среднеToolStripMenuItem_CheckStateChanged);
            // 
            // тяжелоToolStripMenuItem
            // 
            this.тяжелоToolStripMenuItem.Checked = true;
            this.тяжелоToolStripMenuItem.CheckOnClick = true;
            this.тяжелоToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.тяжелоToolStripMenuItem.Name = "тяжелоToolStripMenuItem";
            this.тяжелоToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.тяжелоToolStripMenuItem.Text = "тяжело";
            this.тяжелоToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.тяжелоToolStripMenuItem_CheckStateChanged);
            // 
            // boardToolStripMenuItem
            // 
            this.boardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.boardToolStripMenuItem.Name = "boardToolStripMenuItem";
            this.boardToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.boardToolStripMenuItem.Text = "&Размер доски";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 27);
            this.toolStripTextBox1.Text = "10";
            this.toolStripTextBox1.ToolTipText = "Enter decimal number";
            this.toolStripTextBox1.Click += new System.EventHandler(this.toolStripTextBox1_Click);
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // networkToolStripMenuItem
            // 
            this.networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            this.networkToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.networkToolStripMenuItem.Text = "&Настройки сети";
            // 
            // антиалToolStripMenuItem
            // 
            this.антиалToolStripMenuItem.Checked = true;
            this.антиалToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.антиалToolStripMenuItem.Name = "антиалToolStripMenuItem";
            this.антиалToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.антиалToolStripMenuItem.Text = "&Smoothing mode(Антиалиасинг)";
            this.антиалToolStripMenuItem.Click += new System.EventHandler(this.антиалToolStripMenuItem_Click);
            // 
            // цветКурсораToolStripMenuItem
            // 
            this.цветКурсораToolStripMenuItem.Name = "цветКурсораToolStripMenuItem";
            this.цветКурсораToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.цветКурсораToolStripMenuItem.Text = "Цвет курсора...";
            this.цветКурсораToolStripMenuItem.Click += new System.EventHandler(this.цветКурсораToolStripMenuItem_Click);
            // 
            // цветИгрокаToolStripMenuItem
            // 
            this.цветИгрокаToolStripMenuItem.Name = "цветИгрокаToolStripMenuItem";
            this.цветИгрокаToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.цветИгрокаToolStripMenuItem.Text = "Цвет игрока...";
            this.цветИгрокаToolStripMenuItem.Click += new System.EventHandler(this.цветИгрокаToolStripMenuItem_Click);
            // 
            // цветПротивникаToolStripMenuItem
            // 
            this.цветПротивникаToolStripMenuItem.Name = "цветПротивникаToolStripMenuItem";
            this.цветПротивникаToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.цветПротивникаToolStripMenuItem.Text = "Цвет противника...";
            this.цветПротивникаToolStripMenuItem.Click += new System.EventHandler(this.цветПротивникаToolStripMenuItem_Click);
            // 
            // autoplayToolStripMenuItem
            // 
            this.autoplayToolStripMenuItem.Name = "autoplayToolStripMenuItem";
            this.autoplayToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.autoplayToolStripMenuItem.Text = "Autoplay";
            this.autoplayToolStripMenuItem.Click += new System.EventHandler(this.autoplayToolStripMenuItem_Click);
            // 
            // выделитьШаблонToolStripMenuItem
            // 
            this.выделитьШаблонToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.точкаОтсчетаToolStripMenuItem,
            this.точкаХодаToolStripMenuItem,
            this.любаяТочкаToolStripMenuItem,
            this.пустаяТочкаToolStripMenuItem});
            this.выделитьШаблонToolStripMenuItem.Name = "выделитьШаблонToolStripMenuItem";
            this.выделитьШаблонToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.выделитьШаблонToolStripMenuItem.Text = "Выделить шаблон";
            this.выделитьШаблонToolStripMenuItem.Click += new System.EventHandler(this.выделитьШаблонToolStripMenuItem_Click);
            // 
            // точкаОтсчетаToolStripMenuItem
            // 
            this.точкаОтсчетаToolStripMenuItem.Name = "точкаОтсчетаToolStripMenuItem";
            this.точкаОтсчетаToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.точкаОтсчетаToolStripMenuItem.Text = "Точка отсчета";
            this.точкаОтсчетаToolStripMenuItem.Click += new System.EventHandler(this.точкаОтсчетаToolStripMenuItem_Click);
            // 
            // точкаХодаToolStripMenuItem
            // 
            this.точкаХодаToolStripMenuItem.Name = "точкаХодаToolStripMenuItem";
            this.точкаХодаToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.точкаХодаToolStripMenuItem.Text = "Точка хода";
            this.точкаХодаToolStripMenuItem.Click += new System.EventHandler(this.точкаХодаToolStripMenuItem_Click);
            // 
            // любаяТочкаToolStripMenuItem
            // 
            this.любаяТочкаToolStripMenuItem.Name = "любаяТочкаToolStripMenuItem";
            this.любаяТочкаToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.любаяТочкаToolStripMenuItem.Text = "Любая точка кроме враж";
            // 
            // пустаяТочкаToolStripMenuItem
            // 
            this.пустаяТочкаToolStripMenuItem.Name = "пустаяТочкаToolStripMenuItem";
            this.пустаяТочкаToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.пустаяТочкаToolStripMenuItem.Text = "Пустая точка";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.содержаниеToolStripMenuItem,
            this.toolStripSeparator5,
            this.опрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.справкаToolStripMenuItem.Text = "Спра&вка";
            // 
            // содержаниеToolStripMenuItem
            // 
            this.содержаниеToolStripMenuItem.Name = "содержаниеToolStripMenuItem";
            this.содержаниеToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.содержаниеToolStripMenuItem.Text = "&Содержание";
            this.содержаниеToolStripMenuItem.Click += new System.EventHandler(this.содержаниеToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(185, 6);
            // 
            // опрограммеToolStripMenuItem
            // 
            this.опрограммеToolStripMenuItem.Name = "опрограммеToolStripMenuItem";
            this.опрограммеToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.опрограммеToolStripMenuItem.Text = "&О программе...";
            this.опрограммеToolStripMenuItem.Click += new System.EventHandler(this.опрограммеToolStripMenuItem_Click);
            // 
            // listMovesBindingSource
            // 
            this.listMovesBindingSource.DataMember = "ListMoves";
            this.listMovesBindingSource.DataSource = this.gameBindingSource;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 555);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(461, 28);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 23);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(182, 23);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // gameBindingSource
            // 
            this.gameBindingSource.DataSource = typeof(DotsGame.Game);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 583);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pbxBoard);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Dots";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBoard)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMovesBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.PictureBox pbxBoard;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boardToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитькакToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem печатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem предварительныйпросмотрToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem содержаниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem опрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem антиалToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цветКурсораToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem цветИгрокаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цветПротивникаToolStripMenuItem;
        //public System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.BindingSource gameBindingSource;
        private System.Windows.Forms.BindingSource listMovesBindingSource;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem autoplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выделитьШаблонToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem точкаОтсчетаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem точкаХодаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem любаяТочкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пустаяТочкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem легкоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem среднеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem тяжелоToolStripMenuItem;
    }
}

