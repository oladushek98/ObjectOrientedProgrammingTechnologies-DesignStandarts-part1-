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
        public Form1()
        {
            InitializeComponent();
        }

        Pen pen = new Pen(Color.Black, 3);


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
