using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elements
{
    [Serializable]
    public class Link : Element //连线线
    {
        double vbase;//母线基准电压
        double vreal;//实测电压

        public double Vbase
        {
            get { return vbase; }
            set { vbase = value; }
        }
        public double Vreal
        {
            get { return vreal; }
            set { vreal = value; }
        }

        public Element startElement;//记录始点所在设备
        public Element endElement;//记录终点所在设备
        public int startPoint;//记录始点所在设备的具体节点
        public int endPoint;//记录终点所在设备的具体节点

        public List<PointF> Locus;//记录鼠标拖动中的点轨迹
        public List<PointF> Inflection;//记录拐点
        public List<RectangleF> rectLine = new List<RectangleF>();//连线所在矩形
        public List<RectangleF> rectInflection = new List<RectangleF>();//拐点所在矩形

        public Link(float v1, float v2, float v3, float v4, float v5)
        {
            X = v1;
            Y = v2;
            W = v3;
            H = v4;
            B = v5;

            Locus = new List<PointF>();
            Inflection = new List<PointF>();
            PointF p1 = new PointF(X, Y);
            PointF p2 = new PointF(W, H);
            Locus.Add(p1);
            Locus.Add(p2);
            Inflection.Add(p1);
            Inflection.Add(p2);
        }

        public Link(PointF startPoint, PointF endPoint, float v5)
        {
            X = startPoint.X;//始点坐标
            Y = startPoint.Y;
            W = endPoint.X;//终点坐标
            H = endPoint.Y;
            B = v5;

            Locus = new List<PointF>();
            Inflection = new List<PointF>();
            PointF p1 = new PointF(X, Y);
            PointF p2 = new PointF(W, H);
            Locus.Add(p1);
            Locus.Add(p2);
            Inflection.Add(p1);//将始点与终点加入拐点列表中
            Inflection.Add(p2);
        }

        public override void Draw(Graphics g, Pen mypen, SolidBrush brush)
        {
            if (Locus.Count == 0)//判断是否是第一次画线
            {
                for (int i = 0; i < Inflection.Count - 1; i++)
                {
                    g.FillEllipse(brush, rectInflection[i]);
                    g.DrawLine(mypen, Inflection[i], Inflection[i + 1]);

                    //if (rectLine.Count != 0 && i < rectLine.Count)
                    //{
                    //    g.DrawRectangle(mypen, Rectangle.Round(rectLine[i]));
                    //}
                }
            }
            else
            {
                for (int i = 0; i < Locus.Count - 1; i++)
                {
                    g.DrawLine(mypen, Locus[i], Locus[i + 1]);
                }
            }
        }
    }
}
