﻿namespace DotsGame
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
        /// Required method for Designer support -do not modify
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
            this.открытьПоследнююToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитькакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.редакторПаттерновToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.содержаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.опрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.listMovesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsStatusFoo = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolEditorPattern = new System.Windows.Forms.ToolStrip();
            this.tlsТочкаОтсчета = new System.Windows.Forms.ToolStripButton();
            this.tlsПустая = new System.Windows.Forms.ToolStripButton();
            this.tlsКромеВражеской = new System.Windows.Forms.ToolStripButton();
            this.tlsТочкаХода = new System.Windows.Forms.ToolStripButton();
            this.tlsMakePattern = new System.Windows.Forms.ToolStripButton();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.gameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBoard)).BeginInit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMovesBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolEditorPattern.SuspendLayout();
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
            this.pbxBoard.Location = new System.Drawing.Point(0, 27);
            this.pbxBoard.Name = "pbxBoard";
            this.pbxBoard.Size = new System.Drawing.Size(371, 416);
            this.pbxBoard.TabIndex = 0;
            this.pbxBoard.TabStop = false;
            this.pbxBoard.Click += new System.EventHandler(this.pbxBoard_Click);
            this.pbxBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pbxBoard_Paint);
            this.pbxBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxBoard_MouseClick);
            this.pbxBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxBoard_MouseDown);
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
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(164, 24);
            this.menuStrip.TabIndex = 2;
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.открытьПоследнююToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.toolStripSeparator,
            this.сохранитьToolStripMenuItem,
            this.сохранитькакToolStripMenuItem,
            this.toolStripSeparator2,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem.Text = "&Файл";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.создатьToolStripMenuItem.Text = "&Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // открытьПоследнююToolStripMenuItem
            // 
            this.открытьПоследнююToolStripMenuItem.Name = "открытьПоследнююToolStripMenuItem";
            this.открытьПоследнююToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.открытьПоследнююToolStripMenuItem.Text = "Быстая загрузка";
            this.открытьПоследнююToolStripMenuItem.Click += new System.EventHandler(this.открытьПоследнююToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.открытьToolStripMenuItem.Text = "&Открыть...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(225, 6);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.сохранитьToolStripMenuItem.Text = "&Быстрое сохранение";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитькакToolStripMenuItem
            // 
            this.сохранитькакToolStripMenuItem.Name = "сохранитькакToolStripMenuItem";
            this.сохранитькакToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.сохранитькакToolStripMenuItem.Text = "Сохранить &как";
            this.сохранитькакToolStripMenuItem.Click += new System.EventHandler(this.сохранитькакToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
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
            this.редакторПаттерновToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.optionsToolStripMenuItem.Text = "&Опции";
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.легкоToolStripMenuItem,
            this.среднеToolStripMenuItem,
            this.тяжелоToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.levelToolStripMenuItem.Text = "&Сложность";
            this.levelToolStripMenuItem.Visible = false;
            // 
            // легкоToolStripMenuItem
            // 
            this.легкоToolStripMenuItem.CheckOnClick = true;
            this.легкоToolStripMenuItem.Name = "легкоToolStripMenuItem";
            this.легкоToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.легкоToolStripMenuItem.Text = "легко";
            this.легкоToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.легкоToolStripMenuItem_CheckStateChanged);
            // 
            // среднеToolStripMenuItem
            // 
            this.среднеToolStripMenuItem.CheckOnClick = true;
            this.среднеToolStripMenuItem.Name = "среднеToolStripMenuItem";
            this.среднеToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.среднеToolStripMenuItem.Text = "средне";
            this.среднеToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.среднеToolStripMenuItem_CheckStateChanged);
            // 
            // тяжелоToolStripMenuItem
            // 
            this.тяжелоToolStripMenuItem.CheckOnClick = true;
            this.тяжелоToolStripMenuItem.Name = "тяжелоToolStripMenuItem";
            this.тяжелоToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.тяжелоToolStripMenuItem.Text = "тяжело";
            this.тяжелоToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.тяжелоToolStripMenuItem_CheckStateChanged);
            // 
            // boardToolStripMenuItem
            // 
            this.boardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.boardToolStripMenuItem.Name = "boardToolStripMenuItem";
            this.boardToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.boardToolStripMenuItem.Text = "&Размер доски";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 21);
            this.toolStripTextBox1.Text = "10";
            this.toolStripTextBox1.ToolTipText = "Enter decimal number";
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // networkToolStripMenuItem
            // 
            this.networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            this.networkToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.networkToolStripMenuItem.Text = "&Настройки сети";
            this.networkToolStripMenuItem.Visible = false;
            // 
            // антиалToolStripMenuItem
            // 
            this.антиалToolStripMenuItem.Checked = true;
            this.антиалToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.антиалToolStripMenuItem.Name = "антиалToolStripMenuItem";
            this.антиалToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.антиалToolStripMenuItem.Text = "&Smoothing mode(Антиалиасинг)";
            this.антиалToolStripMenuItem.Click += new System.EventHandler(this.антиалToolStripMenuItem_Click);
            // 
            // цветКурсораToolStripMenuItem
            // 
            this.цветКурсораToolStripMenuItem.Name = "цветКурсораToolStripMenuItem";
            this.цветКурсораToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.цветКурсораToolStripMenuItem.Text = "Цвет курсора...";
            this.цветКурсораToolStripMenuItem.Click += new System.EventHandler(this.цветКурсораToolStripMenuItem_Click);
            // 
            // цветИгрокаToolStripMenuItem
            // 
            this.цветИгрокаToolStripMenuItem.Name = "цветИгрокаToolStripMenuItem";
            this.цветИгрокаToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.цветИгрокаToolStripMenuItem.Text = "Цвет игрока...";
            this.цветИгрокаToolStripMenuItem.Click += new System.EventHandler(this.цветИгрокаToolStripMenuItem_Click);
            // 
            // цветПротивникаToolStripMenuItem
            // 
            this.цветПротивникаToolStripMenuItem.Name = "цветПротивникаToolStripMenuItem";
            this.цветПротивникаToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.цветПротивникаToolStripMenuItem.Text = "Цвет противника...";
            this.цветПротивникаToolStripMenuItem.Click += new System.EventHandler(this.цветПротивникаToolStripMenuItem_Click);
            // 
            // autoplayToolStripMenuItem
            // 
            this.autoplayToolStripMenuItem.Name = "autoplayToolStripMenuItem";
            this.autoplayToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.autoplayToolStripMenuItem.Text = "Autoplay";
            this.autoplayToolStripMenuItem.Click += new System.EventHandler(this.autoplayToolStripMenuItem_Click);
            // 
            // редакторПаттерновToolStripMenuItem
            // 
            this.редакторПаттерновToolStripMenuItem.Name = "редакторПаттерновToolStripMenuItem";
            this.редакторПаттерновToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.редакторПаттерновToolStripMenuItem.Text = "Редактор паттернов";
            this.редакторПаттерновToolStripMenuItem.Click += new System.EventHandler(this.редакторПаттерновToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.содержаниеToolStripMenuItem,
            this.toolStripSeparator5,
            this.опрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.справкаToolStripMenuItem.Text = "Спра&вка";
            // 
            // содержаниеToolStripMenuItem
            // 
            this.содержаниеToolStripMenuItem.Name = "содержаниеToolStripMenuItem";
            this.содержаниеToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.содержаниеToolStripMenuItem.Text = "&Содержание";
            this.содержаниеToolStripMenuItem.Click += new System.EventHandler(this.содержаниеToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(158, 6);
            // 
            // опрограммеToolStripMenuItem
            // 
            this.опрограммеToolStripMenuItem.Name = "опрограммеToolStripMenuItem";
            this.опрограммеToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
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
            this.toolStripStatusLabel2,
            this.tlsStatusFoo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 446);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(371, 28);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(50, 23);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(140, 23);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // tlsStatusFoo
            // 
            this.tlsStatusFoo.Name = "tlsStatusFoo";
            this.tlsStatusFoo.Size = new System.Drawing.Size(0, 23);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Dots files (*.dts)|*.dts";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Dots files (*.dts)|*.dts";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(247, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "90";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(284, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "z";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolEditorPattern
            // 
            this.toolEditorPattern.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolEditorPattern.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolEditorPattern.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsТочкаОтсчета,
            this.tlsПустая,
            this.tlsКромеВражеской,
            this.tlsТочкаХода,
            this.tlsMakePattern,
            this.toolExit});
            this.toolEditorPattern.Location = new System.Drawing.Point(0, 0);
            this.toolEditorPattern.Name = "toolEditorPattern";
            this.toolEditorPattern.Size = new System.Drawing.Size(371, 27);
            this.toolEditorPattern.TabIndex = 31;
            this.toolEditorPattern.Text = "toolStrip1";
            this.toolEditorPattern.Visible = false;
            // 
            // tlsТочкаОтсчета
            // 
            this.tlsТочкаОтсчета.CheckOnClick = true;
            this.tlsТочкаОтсчета.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsТочкаОтсчета.Image = ((System.Drawing.Image)(resources.GetObject("tlsТочкаОтсчета.Image")));
            this.tlsТочкаОтсчета.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsТочкаОтсчета.Name = "tlsТочкаОтсчета";
            this.tlsТочкаОтсчета.Size = new System.Drawing.Size(24, 24);
            this.tlsТочкаОтсчета.Text = "Точка отсчета";
            // 
            // tlsПустая
            // 
            this.tlsПустая.CheckOnClick = true;
            this.tlsПустая.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsПустая.Image = ((System.Drawing.Image)(resources.GetObject("tlsПустая.Image")));
            this.tlsПустая.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsПустая.Name = "tlsПустая";
            this.tlsПустая.Size = new System.Drawing.Size(24, 24);
            this.tlsПустая.Text = "Отметить точку";
            // 
            // tlsКромеВражеской
            // 
            this.tlsКромеВражеской.CheckOnClick = true;
            this.tlsКромеВражеской.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsКромеВражеской.Image = ((System.Drawing.Image)(resources.GetObject("tlsКромеВражеской.Image")));
            this.tlsКромеВражеской.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsКромеВражеской.Name = "tlsКромеВражеской";
            this.tlsКромеВражеской.Size = new System.Drawing.Size(24, 24);
            this.tlsКромеВражеской.Text = "Любая, кроме вражеской";
            // 
            // tlsТочкаХода
            // 
            this.tlsТочкаХода.CheckOnClick = true;
            this.tlsТочкаХода.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsТочкаХода.Image = ((System.Drawing.Image)(resources.GetObject("tlsТочкаХода.Image")));
            this.tlsТочкаХода.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsТочкаХода.Name = "tlsТочкаХода";
            this.tlsТочкаХода.Size = new System.Drawing.Size(24, 24);
            this.tlsТочкаХода.Text = "Точка хода";
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
            // 
            // toolExit
            // 
            this.toolExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolExit.Image = ((System.Drawing.Image)(resources.GetObject("toolExit.Image")));
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(115, 24);
            this.toolExit.Text = "Выйти из редактора";
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // gameBindingSource
            // 
            this.gameBindingSource.DataSource = typeof(DotsGame.Game);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 474);
            this.Controls.Add(this.toolEditorPattern);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pbxBoard);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
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
            this.toolEditorPattern.ResumeLayout(false);
            this.toolEditorPattern.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem легкоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem среднеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem тяжелоToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tlsStatusFoo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem открытьПоследнююToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ToolStrip toolEditorPattern;
        public System.Windows.Forms.ToolStripButton tlsТочкаОтсчета;
        public System.Windows.Forms.ToolStripButton tlsПустая;
        public System.Windows.Forms.ToolStripButton tlsКромеВражеской;
        public System.Windows.Forms.ToolStripButton tlsТочкаХода;
        private System.Windows.Forms.ToolStripButton tlsMakePattern;
        private System.Windows.Forms.ToolStripMenuItem редакторПаттерновToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolExit;
    }
}

