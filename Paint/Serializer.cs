using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Windows.Forms;
using AbstractClassLibrary;

namespace Paint
{
    class Serializer
    {
        OpenFileDialog openDialog = new OpenFileDialog();
        SaveFileDialog saveDialog = new SaveFileDialog();
        string fileName = null;
        readonly JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        public void Serialize(List<Figure> list)
        {
            saveDialog.DefaultExt = ".json";
            saveDialog.Filter = "Text files (*.txt)|*txt| JSON files (*.json)|*json";
            saveDialog.ShowDialog();
            string fileName = saveDialog.FileName;
            try
            {                
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate | FileMode.Truncate))
                {
                    var json = JsonConvert.SerializeObject(list, Formatting.None, settings);
                    var writeStream = new StreamWriter(fs);
                    writeStream.Write(json);
                    writeStream.Flush();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Deserialize(List<Figure> list)
        {
            openDialog.DefaultExt = ".json";
            openDialog.Filter = "Text files (*.txt)|*txt| JSON files (*.json)|*json";
            if (list != null)
                list.Clear();
            openDialog.ShowDialog();
            fileName =  openDialog.FileName;
            try
            {
                using (var fStream = File.OpenRead(fileName))
                {
                    try
                    {
                        var json = new StreamReader(fStream).ReadToEnd();
                        var figures = JsonConvert.DeserializeObject<List<Figure>>(json, settings);
                        foreach (var figure in figures)
                            list.Add(figure);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        list.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Figure> Deserialize_UserFigure(Stream fStream)
        {
            try
            {
                List<Figure> list = new List<Figure>();
                using (fStream)
                {
                    try
                    {
                        var json = new StreamReader(fStream).ReadToEnd();
                        var figures = JsonConvert.DeserializeObject<List<Figure>>(json, settings);
                        foreach (var figure in figures)
                            list.Add(figure);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        list.Clear();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Form1.figureList.ReadyFigures;
            }
        }
    }
}
