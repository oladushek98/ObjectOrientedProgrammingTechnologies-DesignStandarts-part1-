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
            this.AllFigures.Location = new System.Drawing.Point(941, 679);
            this.AllFigures.Name = "AllFigures";
            this.AllFigures.Size = new System.Drawing.Size(96, 35);
            this.AllFigures.TabIndex = 2;
            this.AllFigures.Text = "All figures";
            this.AllFigures.UseVisualStyleBackColor = true;
            this.AllFigures.Click += new System.EventHandler(this.button1_Click);
            // 
            // CleanBtn
            // 
            this.CleanBtn.Location = new System.Drawing.Point(940, 740);
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
            this.SerializeBtn.Location = new System.Drawing.Point(886, 820);
            this.SerializeBtn.Name = "SerializeBtn";
            this.SerializeBtn.Size = new System.Drawing.Size(75, 23);
            this.SerializeBtn.TabIndex = 4;
            this.SerializeBtn.Text = "Serialize";
            this.SerializeBtn.UseVisualStyleBackColor = true;
            this.SerializeBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // DeserializeBtn
            // 
            this.DeserializeBtn.Location = new System.Drawing.Point(976, 820);
            this.DeserializeBtn.Name = "DeserializeBtn";
            this.DeserializeBtn.Size = new System.Drawing.Size(98, 23);
            this.DeserializeBtn.TabIndex = 5;
            this.DeserializeBtn.Text = "Deserialize";
            this.DeserializeBtn.UseVisualStyleBackColor = true;
            this.DeserializeBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 855);
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
    }
}

