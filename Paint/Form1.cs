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
        XElement root = XDocument.Load("../../config.xml").Element("config");

        Color penColor;
        float penWidth;

        const string Eng = "English";
        string language = null;

        Point X, Y;

        public bool isClicked = false;

        string[] figureNames = null, figureNamesLan = null;
        string[] figureColors = null, figureColorsLan = null;

        List<Color> colorList = new List<Color>()
        {
            Color.Black, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple, Color.Gray
        };

        List<string> languageList = new List<string>()
        {
            "English", "Русский"
        };


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

            Init();
        }


        private void Init()
        {

            // reading the language of the program interface
            language = null;
            try
            {
                foreach(XElement lan in root.Elements("language"))
                {
                    language = lan.Attribute("lang").Value;
                    bool correct = false;
                    foreach(string lang in languageList)
                    {
                        if (language == lang)
                        {
                            correct = true;
                        }
                    }
                    if (correct == false)
                    {
                        language = Eng;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // creating of components info arrays
            figureNames = new string[] { "Line", "Rectangle", "Square", "Rhombous", "Circle", "Ellipse" };
            if (language == "Русский")
            {
                figureNamesLan = new string[] { "Линия", "Прямоугольник", "Квадрат", "Ромб", "Окружность", "Эллипс" };
            }

            figureColors = new string[] { "Black", "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Gray" };
            if (language == "Русский")
            {
                figureColorsLan = new string[] { "Чёрный", "Красный", "Оранжевый", "Желтый", "Зеленый", "Синий", "Фиолетовый", "Серый" };
            }


            // reading info about buttons for figure drawing from .xml file
            int butLength = 0, butWidth = 0;
            try
            {
                foreach (XElement but in root.Elements("button"))
                {
                    XElement sizeL = but.Element("length");
                    butLength = Int32.Parse(sizeL.Value);
                    XElement sizeW = but.Element("width");
                    butWidth = Int32.Parse(sizeW.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            string Width = null;
            string col = null;
            try
            {
                foreach(XElement pen in root.Elements("pen"))
                {
                    penWidth = float.Parse(pen.Element("width").Value);
                    Width = pen.Element("width").Value;
                    col = pen.Element("color").Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
            Color backColor = Color.White;
            string backClr = null;
            try
            {
                foreach (XElement canvclr in root.Elements("canvas"))
                {
                    backClr = canvclr.Attribute("color").Value;
                    bool correct = false;
                    for (int i = 0; i < figureColors.Length; i++)
                    {
                        if (backClr == figureColors[i])
                        {
                            correct = true;
                        }
                    }
                    if (correct == false)
                    {
                        backClr = "White";
                    }
                    foreach(var clr in colorList)
                    {
                        if ((clr).ToString().Contains(backClr))
                        {
                            backColor = clr;
                        }
                    }
                    pictureBox1.BackColor = backColor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            List<FigureButtonInfo> figureButtonInfoArr = new List<FigureButtonInfo>();
            List<FigureColorInfo> figureColorInfoArr = new List<FigureColorInfo>();

            
            foreach (var Creator in figureCreatorList.Creators)
            {
                int i = -1;
                foreach (var FigureName in figureNames)
                {
                    i++;
                    if ((Creator).ToString().Contains(FigureName))
                    {
                        if (language == Eng)
                            figureButtonInfoArr.Add(new FigureButtonInfo
                            {
                                figureName = FigureName,
                                creator = Creator
                            });
                        else 
                        {
                            string name = figureNamesLan[i];
                            figureButtonInfoArr.Add(new FigureButtonInfo
                            {
                                figureName = name,
                                creator = Creator
                            });
                        }
                    }
                }
            }

            foreach (var Color in colorList)
            {
                int i = -1;
                foreach (var ColorName in figureColors)
                {
                    i++;
                    if ((Color).ToString().Contains(ColorName))
                    {
                        if (language == Eng)
                        {
                            figureColorInfoArr.Add(new FigureColorInfo
                            {
                                colorName = ColorName,
                                color = Color
                            });
                         }
                        else 
                        {
                            string name = figureColorsLan[i];
                            figureColorInfoArr.Add(new FigureColorInfo
                            {
                                colorName = name,
                                color = Color
                            });
                        }
                    }
                }
            }


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
                if (radioButton1.Name == Width)
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
                if ((language != Eng && radioButton.Name == figureColorsLan[Array.IndexOf(figureColors, col)]) 
                    || radioButton.Name == col)
                {
                    radioButton.Checked = true;
                    penColor = (Color)colorInfo.color;
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

            if (language == Eng)
            {
                SerializeBtn.Text = "Serialize";
                DeserializeBtn.Text = "Deserialize";
                ConfigBtn.Text = "Config";
                LanguageBox.Text = "Language";
                PenWidthBox.Text = "Ren width";
                CanvasClrBox.Text = "Canvas color";
                PenColorBox.Text = "Pen color";
            }
            else if (language == "Русский")
            {
                SerializeBtn.Text = "Сериализовать";
                DeserializeBtn.Text = "Десериализовать";
                ConfigBtn.Text = "Конфигурировать";
                LanguageBox.Text = "Язык";
                PenWidthBox.Text = "Ширина пера";
                CanvasClrBox.Text = "Цвет полотна";
                PenColorBox.Text = "Цвет пера";

            }

            InsertBoxItems(CanvasClrBox);
            InsertBoxItems(PenColorBox);

        } 

        private void InsertBoxItems(ComboBox box)
        {
            for (int i = 0; i < figureColors.Length; i++)
            {
                if (language == Eng)
                {
                    box.Items[i] = figureColors[i];
                }
                else
                {
                    box.Items[i] = figureColorsLan[i];
                }
            }
        }

        // events
        private void FigureWidth_ChechedChange(object sender, EventArgs e)
        {
            RadioButton checkedItem = (RadioButton)sender;
            penWidth = (float)checkedItem.Tag;
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
                figure.Pen = new Pen(penColor, penWidth);
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


        private void ConfigBtn_Click(object sender, EventArgs e)
        {
            XElement root = xDoc.Element("config");

            foreach (XElement elem in root.Elements("language"))
            {
                if (LanguageBox.SelectedItem != null)
                    elem.Attribute("lang").Value = LanguageBox.SelectedItem.ToString();
            }

            foreach (XElement elem in root.Elements("pen"))
            {
                if (PenWidthBox.SelectedItem != null)
                    elem.Element("width").Value = PenWidthBox.SelectedItem.ToString();
                if (PenColorBox.SelectedItem != null)
                {
                    if (language == Eng)
                        elem.Element("color").Value = PenColorBox.SelectedItem.ToString();
                    else
                        elem.Element("color").Value = figureColors[Array.IndexOf(figureColorsLan, PenColorBox.SelectedItem.ToString())];
                }
            }

            foreach (XElement elem in root.Elements("canvas"))
            {
                if (CanvasClrBox.SelectedItem != null)
                {
                    if (language == Eng)
                        elem.Attribute("color").Value = CanvasClrBox.SelectedItem.ToString();
                    else
                        elem.Attribute("color").Value = figureColors[Array.IndexOf(figureColorsLan, CanvasClrBox.SelectedItem.ToString())];
                }
            }

            if ((LanguageBox.SelectedItem == null) && (PenWidthBox.SelectedItem == null) && (CanvasClrBox.SelectedItem == null) && (PenColorBox.SelectedItem == null))
            {
                foreach (XElement elem in root.Elements("pen"))
                {
                    elem.Element("width").Value = penWidth.ToString();
                    elem.Element("color").Value = penColor.ToString().Remove(0, 7).Remove(penColor.ToString().Remove(0, 7).IndexOf(']'));
                    string kek = elem.Element("color").Value;
                }
                foreach(XElement elem in root.Elements("canvas"))
                {
                    elem.Attribute("color").Value = pictureBox1.BackColor.ToString().Remove(0, 7).Remove(pictureBox1.BackColor.ToString().Remove(0, 7).IndexOf(']'));
                }
                MessageBox.Show("Current configuration saved. Restart the application to activate it!");
            }
            else
                MessageBox.Show("Restart the application for configuration settings to be activated");

            xDoc.Save("../../config.xml");

            //Init();
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
