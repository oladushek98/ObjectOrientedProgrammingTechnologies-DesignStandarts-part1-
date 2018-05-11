using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {

        FigureList figureList = new FigureList();
        FigureCreatorList figureCreatorList = new FigureCreatorList();
        ICreator figureCreator;
        Figure figure;

        Color penColor = Color.Black;
        float width = 3;

        Point X, Y;

        public bool isClicked = false;

        public struct FigureButtonInfo
        {
            public string figureName;
            public ICreator creator;
        }

        public struct FigureColorInfo
        {
            public string colorName;
            public Color color;
        }

        public struct FigureWidthInfo
        {
            public string widthValue;
            public float width;
        }
        

        public Form1()
        {
            InitializeComponent();

            FigureButtonInfo[] figureButtonInfoArr = new FigureButtonInfo[]
            {
                new FigureButtonInfo { figureName = "Line", creator = new Line_Creator() },
                new FigureButtonInfo { figureName = "Circle", creator = new Circle_Creator() },
                new FigureButtonInfo { figureName = "Ellipse", creator = new Ellipse_Creator() },
                new FigureButtonInfo { figureName = "Rectangle", creator = new Rectangle_Creator() },
                new FigureButtonInfo { figureName = "Rhombus", creator = new Rhombus_Creator() },
                new FigureButtonInfo { figureName = "Square", creator = new Square_Creator() }
            };

            FigureColorInfo[] figureColorInfoArr = new FigureColorInfo[]
            {
                new FigureColorInfo { colorName = "Black", color = Color.Black  },
                new FigureColorInfo { colorName = "Red", color = Color.Red },
                new FigureColorInfo { colorName = "Orange", color = Color.Orange },
                new FigureColorInfo { colorName = "Yellow", color = Color.Yellow },
                new FigureColorInfo { colorName = "Green", color = Color.Green },
                new FigureColorInfo { colorName = "Blue", color = Color.Blue },
                new FigureColorInfo { colorName = "Purple", color = Color.Purple },
                new FigureColorInfo { colorName = "Gray", color = Color.Gray },
            };
               
            FigureWidthInfo[] figureWidthInfoArr = new FigureWidthInfo[]
            {
                new FigureWidthInfo { widthValue = "1", width = 1 },
                new FigureWidthInfo { widthValue = "2", width = 2 },
                new FigureWidthInfo { widthValue = "3", width = 3 },
                new FigureWidthInfo { widthValue = "4", width = 4 },
                new FigureWidthInfo { widthValue = "5", width = 5}
            };

            int X = 900;
            int Y = 150;
            RadioButton radioButton, radioButton1;
            foreach (var widthInfo in figureWidthInfoArr)
            {
                radioButton1 = new RadioButton();
                radioButton1.Name = widthInfo.widthValue;
                radioButton1.Text = widthInfo.widthValue;
                if (radioButton1.Name == "3")
                {
                    radioButton1.Checked = true;
                }
                radioButton1.CheckedChanged += new EventHandler(FigureWidth_ChechedChange);
                radioButton1.Location = new Point(X, Y);
                radioButton1.AutoSize = true;
                Y += 50;
                radioButton1.Tag = widthInfo.width;
                radioButton1.UseVisualStyleBackColor = true;
                Controls.Add(radioButton1);
            }

            X = 800;
            Y = 150;
            foreach (var colorInfo in figureColorInfoArr)
            {
                radioButton = new RadioButton();
                radioButton.Name = colorInfo.colorName;
                radioButton.Text = colorInfo.colorName;
                if (radioButton.Name == "Black")
                {
                    radioButton.Checked = true;
                }
                radioButton.CheckedChanged += new EventHandler(FigureColor_CheckedChange);
                radioButton.Location = new Point(X, Y);
                radioButton.AutoSize = true;
                Y += 50;
                radioButton.Tag = colorInfo.color;
                radioButton.UseVisualStyleBackColor = true;
                Controls.Add(radioButton);
            }

            Button button;
            X = 700;
            Y = 150;
            foreach (var figureInfo in figureButtonInfoArr)
            {
                button = new Button();
                button.Text = figureInfo.figureName;
                button.Tag = figureInfo.creator;
                button.Click += new EventHandler(FigureButton_Click);
                button.Location = new Point(X, Y);
                Y += 50;
                button.Name = figureInfo.figureName;
                button.Size = new Size(75, 23);
                button.UseVisualStyleBackColor = true;
                Controls.Add(button);
            }
        }

        private void FigureWidth_ChechedChange(object sender, EventArgs e)
        {
            RadioButton checkedItem = (RadioButton)sender;
            width = (float)checkedItem.Tag;
        }

        private void FigureColor_CheckedChange(object sender, EventArgs e)
        {
            RadioButton checkedItem = (RadioButton)sender;
            penColor = (Color)checkedItem.Tag;
        }

        private void FigureButton_Click(object sender, EventArgs e)
        {
            Button clickedItem = (Button)sender;
            figureCreator = (ICreator)clickedItem.Tag;
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (figureCreator != null)
            {
                figure = figureCreator.Create();
                figure.Pen = new Pen(penColor, width);
                isClicked = true;
                X = new Point(e.X, e.Y);
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isClicked = false;
            if (figure != null)
            {
                figureList.ReadyFigures.Add(figure);
            }
        }

        public void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (figure != null)
            {
                figure.StartPoint = X;
                figure.FinishPoint = Y;
                figure.Draw(e.Graphics, figure.Pen, figure.StartPoint, figure.FinishPoint);
            }
            if (figureList.ReadyFigures.Count > 0)
            {
                foreach (var fig in figureList.ReadyFigures)
                {
                    fig.Draw(e.Graphics, fig.Pen, fig.StartPoint, fig.FinishPoint);
                }
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked)
            {
                Y = new Point(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void CleanBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
        }

        private void SerializeBtn_Click(object sender, EventArgs e)
        {
            var serializer = new Serializer();
            serializer.Serialize(figureList.ReadyFigures);
        }

        private void DeserializeBtn_Click(object sender, EventArgs e)
        {
            var serializer = new Serializer();
            pictureBox1.Invalidate();
            serializer.Deserialize(figureList.ReadyFigures);
            pictureBox1.Invalidate();
            figure = null;
        }

        private void AllFigures_Click(object sender, EventArgs e)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            FigureList FigureList = new FigureList();
            int startX = 80;
            int startY = 20;
            int finishX = 160;
            int finishY = startY + 60;

            foreach (var fig in FigureList.Figures)
            {
                Figure figure = fig;
                var pen = new Pen(penColor, width);
                fig.StartPoint = new Point(startX, startY);
                fig.FinishPoint = new Point(finishX, finishY);
                figure.Draw(graphics, pen, fig.StartPoint, fig.FinishPoint);
                startY += 100;
                finishY = startY + 50;

                if (figure != null)
                    FigureList.ReadyFigures.Add(figure);

                if (FigureList.ReadyFigures.Count > 0)
                {
                    foreach (var readyfig in FigureList.ReadyFigures)
                    {
                        readyfig.Draw(graphics, pen, readyfig.StartPoint, readyfig.FinishPoint);
                    }
                }
            }
        }
    }
}
