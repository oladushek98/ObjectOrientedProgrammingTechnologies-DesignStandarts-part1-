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

namespace Paint
{
    class Serializer
    {
        string fileName = "figures.txt";
        readonly JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
        public void Serialize(List<Figure> list)
        {
            //var jsonFrmatter = new DataContractJsonSerializer(typeof(List<Figure>));
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
            //var jsonFormatter = new DataContractJsonSerializer(typeof(List<Figure>));
            //try
            {
                using (var fStream = File.OpenRead(fileName))
                {
                    // try
                    //{
                    var json = new StreamReader(fStream).ReadToEnd();
                    var figures = JsonConvert.DeserializeObject<List<Figure>>(json, settings);
                    foreach (var figure in figures)
                        list.Add(figure);
                    //}
                }
            }
        }
    }
}
