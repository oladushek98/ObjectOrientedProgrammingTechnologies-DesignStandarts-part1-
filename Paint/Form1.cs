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
       
        Pen pen = new Pen(Color.Black, 3);

        public bool isClicked = false;

        public struct FigureButtonInfo
        {
            public string figureName;
            public ICreator creator;
        }

        Point X, Y;


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

            Button button;
            int X = 700;
            int Y = 150;
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
                figure.Pen = pen;
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
                if (figureList.ReadyFigures.Count > 0)
                {
                    foreach (var fig in figureList.ReadyFigures)
                    {
                        fig.Draw(e.Graphics, fig.Pen, fig.StartPoint, fig.FinishPoint);
                    }
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

        private void button1_Click(object sender, EventArgs e)
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
