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
        string fileName = "figures.txt";
        readonly JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        public void Serialize(List<Figure> list)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate | FileMode.Truncate))
            {
                var json = JsonConvert.SerializeObject(list, Formatting.None, settings);
                var writeStream = new StreamWriter(fs);
                writeStream.Write(json);
                writeStream.Flush();
            }
        }

        public void Deserialize(List<Figure> list)
        {
            list.Clear();
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
                    catch (JsonReaderException)
                    {
                        MessageBox.Show("Deserialization error! Check the file content!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        list.Clear();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
