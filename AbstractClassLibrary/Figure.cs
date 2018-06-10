using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;



namespace AbstractClassLibrary
{
    [DataContract]
    public abstract class Figure
    {
        [DataMember]
        public Point StartPoint { get; set; }
        [DataMember]
        public Point FinishPoint { get; set; }
        [DataMember]
        public Point FixedStartPoint { get; set; }
        [DataMember]
        public Point FixedFinishPoint { get; set; }
        [DataMember]
        public Color PenColor { get; set; }
        [DataMember]
        public float PenWidth { get; set; }
        
        
        public Pen Pen {
            get
            {
                return new Pen(PenColor, PenWidth);
            }
            set
            {
                PenColor = value.Color;
                PenWidth = value.Width;
            }
        }
        abstract public void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint);

        public virtual void Add(List<Figure> list)
        {
            list.Add(this);
        }

        public abstract object Clone();

    }

}
