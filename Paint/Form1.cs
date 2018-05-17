using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AbstractClassLibrary;
using System.Reflection;
using System.IO;
using System.Xml.Linq;

namespace Paint
{
    public partial class Form1 : Form
    {

        FigureList figureList = new FigureList();
        FigureCreatorList figureCreatorList = new FigureCreatorList();
        ICreator figureCreator;
        Figure figure;

        XDocument xDoc = XDocument.Load("../../config.xml");

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

            AddPlugins();

            // reading info about buttons for figure drawing from .xml file
            int butLength = 0, butWidth = 0;
            string butLanguage = "";
            foreach (XElement but in xDoc.Element("config").Elements("button"))
            {
                XElement sizeL = but.Element("length");
                butLength = Int32.Parse(sizeL.Value);
                XElement sizeW = but.Element("width");
                butWidth = Int32.Parse(sizeW.Value);
                butLanguage = but.Element("language").Value;
            }

            // creating of components info arrays
            string[] figureNames = null;
            string[] figureNamesRus = null;
            if (butLanguage == "English")
            {
                figureNames = new string []{ "Line", "Rectangle", "Square", "Rhombous", "Circle", "Ellipse" };
            }
            else if (butLanguage == "Russian")
            {
                figureNames = new string[] { "Line", "Rectangle", "Square", "Rhombous", "Circle", "Ellipse" };
                figureNamesRus = new string []{ "Линия", "Прямоугольник", "Квадрат", "Ромб", "Окружность", "Эллипс" };
            }
                

            List<FigureButtonInfo> figureButtonInfoArr = new List<FigureButtonInfo>();

    
            foreach (var Creator in figureCreatorList.Creators)
            {
                int i = -1;
                foreach (var FigureName in figureNames)
                {
                    i++;
                    if ((Creator).ToString().Contains(FigureName))
                    {
                        
                        if (butLanguage == "English")
                            figureButtonInfoArr.Add(new FigureButtonInfo {
                                figureName = FigureName,
                                creator = Creator
                            });
                        else if (butLanguage == "Russian")
                        {
                            string name = figureNamesRus[i];
                            figureButtonInfoArr.Add(new FigureButtonInfo
                            {
                                figureName = name,
                                creator = Creator
                            });
                        }
                    }
                }   
            }

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
                new FigureWidthInfo { widthValue = "5", width = 5 }
            };


            //components creating
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
                button.Size = new Size(butLength, butWidth);
                button.UseVisualStyleBackColor = true;
                Controls.Add(button);
            }
        }

        // events
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

        // .dll plugins adding
        public void AddPlugins()
        {
            // find a directory of .exe file      
            string AddInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            // .dll files are to be located in the same directory as .exe is  
            var AddInAssemblies = Directory.EnumerateFiles(AddInDir, "*Library.dll");
            // types creating

            foreach (var ass in AddInAssemblies)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(ass);
                    Type[] types = assembly.GetExportedTypes();
                    foreach (var type in types)
                    {
                        if (type.IsClass && typeof(ICreator).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                        {
                            var plugin = Activator.CreateInstance(type);
                            figureCreatorList.Creators.Add((ICreator)plugin);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
