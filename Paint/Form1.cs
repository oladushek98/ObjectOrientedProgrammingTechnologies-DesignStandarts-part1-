using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AbstractClassLibrary;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Collections;

namespace Paint
{
    public partial class Form1 : Form
    {

        public static FigureList figureList = new FigureList();
        FigureCreatorList figureCreatorList = new FigureCreatorList();
        //List<Figure> userFigureList = null;
        ICreator figureCreator;
        Figure figure, copiedFigure;
        UserFigure constUserFigure = new UserFigure();

        XDocument xDoc = XDocument.Load("../../config.xml");
        XElement root = XDocument.Load("../../config.xml").Element("config");

        Color penColor;
        float penWidth;

        const string Eng = "English";
        string language = null;

        Point X, Y;

        public bool isClicked = false;
        public bool isUserFigure = false;
        public bool isFirstUserFigure = false;
        public bool IsPastButtonPressed = false;
        public bool IsFirstPast = false;

        string[] figureNames = null, figureNamesLan = null;
        string[] figureColors = null, figureColorsLan = null;

        List<Color> colorList = new List<Color>()
        {
            Color.Black, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple, Color.Gray, Color.White
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

            AddUserFigures();

        }


        private void Init()
        {

            List<FigureButtonInfo> figureButtonInfoArr = new List<FigureButtonInfo>();
            List<FigureColorInfo> figureColorInfoArr = new List<FigureColorInfo>();


            // reading the language of the program interface
            language = null;
            try
            {
                foreach(XElement lan in root.Elements("language"))
                {
                    language = lan.Attribute("lang").Value;
                    if (languageList.IndexOf(language) == -1)
                        language = Eng;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // creating of components info arrays
            figureNames = new string[] { "Line", "Rectangle", "Square", "Rhombous", "Circle", "Ellipse" };
            figureColors = new string[] { "Black", "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Gray", "White" };
            if (language == "Русский")
            {
                figureColorsLan = new string[] { "Чёрный", "Красный", "Оранжевый", "Желтый", "Зеленый", "Синий", "Фиолетовый", "Серый", "Белый" };
                figureNamesLan = new string[] { "Линия", "Прямоугольник", "Квадрат", "Ромб", "Окружность", "Эллипс" };
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
                butLength = 80;
                butWidth = 23;
            }

            
            string Width = null;
            string col = null;
            try
            {
                foreach(XElement pen in root.Elements("pen"))
                {
                    penWidth = float.Parse(pen.Element("width").Value);
                    if (penWidth < 1 || penWidth > 5 || (penWidth - Math.Truncate(penWidth) != 0))
                        penWidth = 3;
                    Width = penWidth.ToString();
                    col = pen.Element("color").Value;
                    if (Array.IndexOf(figureColors, col) == -1)
                        col = "Black";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                penWidth = 3;
                col = "Black";
                Width = penWidth.ToString();
            }

          
            Color backColor = Color.White;
            string backClr = null;
            try
            {
                foreach (XElement canvclr in root.Elements("canvas"))
                {
                    backClr = canvclr.Attribute("color").Value;
                    if (Array.IndexOf(figureColors, backClr) == -1)
                        backClr = "White";
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


        private void MakeInfoArr<T>(List<T> fh) where T:new()
        {
            T a = new T();
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

        private void UserFigureButton_Click(object sender, EventArgs e)
        {
            isUserFigure = true;
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            UserFigure tempUserFigure = (UserFigure)clickedItem.Tag;
            figure = (UserFigure)tempUserFigure.Clone();
            constUserFigure = (UserFigure)tempUserFigure.Clone();
            isFirstUserFigure = true;
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isUserFigure /*&& !isFirstUserFigure*/)
            {
                figure = (UserFigure)constUserFigure.Clone();
                isClicked = true;
            }
            else
            {
                if (figureCreator != null)
                {
                    figure = figureCreator.Create();
                    figure.Pen = new Pen(penColor, penWidth);
                    isClicked = true;
                }
            }
            X = new Point(e.X, e.Y);
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            /*isClicked = false;
            if (figure != null)
            {
                figureList.ReadyFigures.Add(figure);
            }*/
            if (isClicked)
            {
                isClicked = false;
                figure.Add(figureList.ReadyFigures);
                isFirstUserFigure = false;
            }
        }

        private void Repaint(Graphics g)
        {
            if (figureList.ReadyFigures.Count > 0)
            {
                foreach (var fig in figureList.ReadyFigures)
                {
                    fig.Draw(g, fig.Pen, fig.StartPoint, fig.FinishPoint);
                }
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
            Repaint(e.Graphics);           
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
            serializer.Deserialize(figureList.ReadyFigures);
            pictureBox1.Invalidate();
            figure = null;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsPastButtonPressed)
            {
                Point temp = copiedFigure.StartPoint;
                copiedFigure.StartPoint = new Point(e.X, e.Y);
                copiedFigure.FinishPoint = new Point(copiedFigure.StartPoint.X + Math.Abs((temp.X - copiedFigure.FinishPoint.X)),
                                                    copiedFigure.StartPoint.Y + Math.Abs((temp.Y - copiedFigure.FinishPoint.Y)));
                figureList.ReadyFigures.Add(copiedFigure);
                Graphics g = pictureBox1.CreateGraphics();
                Repaint(g);
                IsPastButtonPressed = false;
                IsFirstPast = false;
            }
        }

       

        private void SaveCustomFigureBtn_Click(object sender, EventArgs e)
        {
            Serializer serializer = new Serializer();
            if (figureList.ReadyFigures.Count != 0)
            {
                serializer.Serialize(figureList.ReadyFigures);
            }
            else
            {
                MessageBox.Show("Nothing to save!");
            }
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
                }
                foreach(XElement elem in root.Elements("canvas"))
                {
                    elem.Attribute("color").Value = pictureBox1.BackColor.ToString().Remove(0, 7).Remove(pictureBox1.BackColor.ToString().Remove(0, 7).IndexOf(']'));
                }
                MessageBox.Show("Current configuration saved. Restart the application to activate it!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Restart the application for configuration settings to be activated", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            try
            {
                xDoc.Save("../../config.xml");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // .dll plugins adding
        private void AddPlugins()
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

        private void AddUserFigures()
        {
            UserFigure.fieldHeight = pictureBox1.Size.Width;
            UserFigure.fieldWidth = pictureBox1.Size.Height;
            String localDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var userFiguresFiles = Directory.EnumerateFiles(localDirectory, "*UserFigure.txt");
            ToolStripMenuItem item;
            Button button;
            int X = 700;
            int Y = 800;
            foreach (var userFigureFile in userFiguresFiles)
            {
                try
                {
                    int i = 1;
                    Stream fileStream = File.Open(userFigureFile, FileMode.Open);
                    Serializer serializer = new Serializer();
                    UserFigure userFigure = new UserFigure() { userFigureList = serializer.Deserialize_UserFigure(fileStream) };
                    /* button = new Button();
                     button.Tag = userFigure;
                     button.Text = "UserFigure" + i.ToString(); //+ Array.IndexOf((Array)userFiguresFiles, userFigureFile).ToString();
                     i++;
                     button.Click += new EventHandler(UserFigureButton_Click);
                     button.Location = new Point(X, Y);
                     Y += 50;
                     button.Name = button.Text;
                     button.Size = new Size(70, 30);
                     button.UseVisualStyleBackColor = true;
                     button.Visible = true;
                     this.Controls.Add(button);
                     button.BringToFront();
                     if (Controls.Contains(button))
                         MessageBox.Show("haha" + button.Name + button.Location.ToString());*/
                    item = new ToolStripMenuItem()
                    {
                        Tag = userFigure
                    };
                    item.Click += new EventHandler(UserFigureButton_Click);
                    toolStripMenuItem1.DropDownItems.Add(item);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            
            /*catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }
    }
}
