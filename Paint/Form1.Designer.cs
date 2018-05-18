namespace Paint
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AllFigures = new System.Windows.Forms.Button();
            this.CleanBtn = new System.Windows.Forms.Button();
            this.SerializeBtn = new System.Windows.Forms.Button();
            this.DeserializeBtn = new System.Windows.Forms.Button();
            this.LanguageBox = new System.Windows.Forms.ComboBox();
            this.PenWidthBox = new System.Windows.Forms.ComboBox();
            this.CanvasClrBox = new System.Windows.Forms.ComboBox();
            this.ConfigBtn = new System.Windows.Forms.Button();
            this.PenColorBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(37, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(826, 794);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
            // 
            // AllFigures
            // 
            this.AllFigures.Enabled = false;
            this.AllFigures.Location = new System.Drawing.Point(1144, 719);
            this.AllFigures.Name = "AllFigures";
            this.AllFigures.Size = new System.Drawing.Size(96, 35);
            this.AllFigures.TabIndex = 2;
            this.AllFigures.Text = "All figures";
            this.AllFigures.UseVisualStyleBackColor = true;
            this.AllFigures.Visible = false;
            // 
            // CleanBtn
            // 
            this.CleanBtn.Location = new System.Drawing.Point(1144, 760);
            this.CleanBtn.Name = "CleanBtn";
            this.CleanBtn.Size = new System.Drawing.Size(96, 36);
            this.CleanBtn.TabIndex = 3;
            this.CleanBtn.Text = "Clean";
            this.CleanBtn.UseVisualStyleBackColor = true;
            this.CleanBtn.Visible = false;
            this.CleanBtn.Click += new System.EventHandler(this.CleanBtn_Click);
            // 
            // SerializeBtn
            // 
            this.SerializeBtn.Location = new System.Drawing.Point(917, 657);
            this.SerializeBtn.Name = "SerializeBtn";
            this.SerializeBtn.Size = new System.Drawing.Size(125, 35);
            this.SerializeBtn.TabIndex = 4;
            this.SerializeBtn.Text = "Serialize";
            this.SerializeBtn.UseVisualStyleBackColor = true;
            this.SerializeBtn.Click += new System.EventHandler(this.SerializeBtn_Click);
            // 
            // DeserializeBtn
            // 
            this.DeserializeBtn.Location = new System.Drawing.Point(1085, 659);
            this.DeserializeBtn.Name = "DeserializeBtn";
            this.DeserializeBtn.Size = new System.Drawing.Size(155, 33);
            this.DeserializeBtn.TabIndex = 5;
            this.DeserializeBtn.Text = "Deserialize";
            this.DeserializeBtn.UseVisualStyleBackColor = true;
            this.DeserializeBtn.Click += new System.EventHandler(this.DeserializeBtn_Click);
            // 
            // LanguageBox
            // 
            this.LanguageBox.FormattingEnabled = true;
            this.LanguageBox.Items.AddRange(new object[] {
            "English",
            "Русский"});
            this.LanguageBox.Location = new System.Drawing.Point(1544, 129);
            this.LanguageBox.Name = "LanguageBox";
            this.LanguageBox.Size = new System.Drawing.Size(206, 24);
            this.LanguageBox.TabIndex = 6;
            this.LanguageBox.Text = "Language";
            // 
            // PenWidthBox
            // 
            this.PenWidthBox.FormattingEnabled = true;
            this.PenWidthBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.PenWidthBox.Location = new System.Drawing.Point(1544, 381);
            this.PenWidthBox.Name = "PenWidthBox";
            this.PenWidthBox.Size = new System.Drawing.Size(206, 24);
            this.PenWidthBox.TabIndex = 7;
            this.PenWidthBox.Text = "Pen width";
            // 
            // CanvasClrBox
            // 
            this.CanvasClrBox.FormattingEnabled = true;
            this.CanvasClrBox.Items.AddRange(new object[] {
            "Black",
            "Red",
            "Orange",
            "Yellow",
            "Green",
            "Blue",
            "Purple",
            "Gray"});
            this.CanvasClrBox.Location = new System.Drawing.Point(1544, 511);
            this.CanvasClrBox.Name = "CanvasClrBox";
            this.CanvasClrBox.Size = new System.Drawing.Size(206, 24);
            this.CanvasClrBox.TabIndex = 8;
            this.CanvasClrBox.Text = "Canvas color";
            // 
            // ConfigBtn
            // 
            this.ConfigBtn.Location = new System.Drawing.Point(1586, 640);
            this.ConfigBtn.Name = "ConfigBtn";
            this.ConfigBtn.Size = new System.Drawing.Size(116, 36);
            this.ConfigBtn.TabIndex = 9;
            this.ConfigBtn.Text = "Config";
            this.ConfigBtn.UseVisualStyleBackColor = true;
            this.ConfigBtn.Click += new System.EventHandler(this.ConfigBtn_Click);
            // 
            // PenColorBox
            // 
            this.PenColorBox.FormattingEnabled = true;
            this.PenColorBox.Items.AddRange(new object[] {
            "Black",
            "Red",
            "Orange",
            "Yellow",
            "Green",
            "Blue",
            "Purple",
            "Gray"});
            this.PenColorBox.Location = new System.Drawing.Point(1544, 247);
            this.PenColorBox.Name = "PenColorBox";
            this.PenColorBox.Size = new System.Drawing.Size(206, 24);
            this.PenColorBox.TabIndex = 10;
            this.PenColorBox.Text = "Pen color";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1884, 855);
            this.Controls.Add(this.PenColorBox);
            this.Controls.Add(this.ConfigBtn);
            this.Controls.Add(this.CanvasClrBox);
            this.Controls.Add(this.PenWidthBox);
            this.Controls.Add(this.LanguageBox);
            this.Controls.Add(this.DeserializeBtn);
            this.Controls.Add(this.SerializeBtn);
            this.Controls.Add(this.CleanBtn);
            this.Controls.Add(this.AllFigures);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button AllFigures;
        private System.Windows.Forms.Button CleanBtn;
        private System.Windows.Forms.Button SerializeBtn;
        private System.Windows.Forms.Button DeserializeBtn;
        private System.Windows.Forms.ComboBox LanguageBox;
        private System.Windows.Forms.ComboBox PenWidthBox;
        private System.Windows.Forms.ComboBox CanvasClrBox;
        private System.Windows.Forms.Button ConfigBtn;
        private System.Windows.Forms.ComboBox PenColorBox;
    }
}

