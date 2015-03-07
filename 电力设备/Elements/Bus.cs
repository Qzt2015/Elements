using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elements
{
    [Serializable]
    public class Bus : Element //导线
    {
        //double vbase;//母线基准电压
        double vreal;//实测电压

        //public double Vbase
        //{
        //    get { return vbase; }
        //    set { vbase = value; }
        //}
        public double Vreal
        {
            get { return vreal; }
            set { vreal = value; }
        }

        public Bus(float v1, float v2, float v3, float v4, int v5)
        {
            X = v1;
            Y = v2;
            W = v3;
            H = v4;
            B = 2F;
            signal  = v5;
            name = "母线";
            OnOff = true;
        }

        //public override void Draw(Graphics g, Pen mypen)
        //{
        //    rect = new RectangleF(X, Y, W, H);

        //    g.DrawLine(mypen, X, Y + H * B / 2, X + W * B, Y + H * B / 2);
        //}

        /// <param name="angle">旋转角度</param>
        public override void Draw(Graphics g, Pen mypen, SolidBrush brush)
        {
            rect = new RectangleF(0, 0, W, H);
            rect = Move_Rect(rect, X, Y, B);

            PointF center = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            rect = RectRotate(rect, Angle, center);
            if (Angle % 180 == 0)
            {
                g.DrawLine(mypen, rect.X, rect.Y + rect.Height / 2, rect.X + rect.Width, rect.Y + rect.Height / 2);
            }
            else
            {
                g.DrawLine(mypen, rect.X + rect.Width / 2, rect.Y, rect.X + rect.Width / 2, rect.Y + rect.Height);
            }
            //g.DrawRectangle(mypen, Rectangle.Round(rect));
            g.DrawString("母线  电压:" + vbase, new Font("微软雅黑", 7), Brushes.Red, rect);
        }

        //public override void Draw(Graphics g, Pen mypen, float angle)
        //{
        //    float angleHud = Angle * (float)Math.PI / 180; /*角度变成弧度*/
        //    PointF p1 = new PointF(X, Y);
        //    float deltaX = W * B / (float)Math.Cos(angleHud);
        //    float deltaY = H * B / (float)Math.Sin(angleHud);
        //    if (Math.Abs(deltaX) > (W + H) * B)
        //    {
        //        deltaY = W * B;
        //        deltaX = 0;
        //    }
        //    if (Math.Abs(deltaY) > (W + H) * B)
        //    {
        //        deltaX = W * B;
        //        deltaY = 0;
        //    }
        //    PointF p2 = new PointF(X + deltaX, Y + deltaY);

        //    Angle = angle;
        //    p1 = PointRotate(this, p1, Angle);
        //    p2 = PointRotate(this, p2, Angle);
        //    //确定外接矩形
        //    W = Math.Abs(p1.X - p2.X) >= 10 ? Math.Abs(p1.X - p2.X) / B : 10;
        //    H = Math.Abs(p1.Y - p2.Y) >= 10 ? Math.Abs(p1.Y - p2.Y) / B : 10;
        //    SizeF size = new SizeF(W * B, H * B);
        //    PointF rectP = new PointF(   //外接矩形始点
        //        Math.Abs(p1.Y - p2.Y) == H * B ? Math.Min(p1.X, p2.X) - W * B / 2 : Math.Min(p1.X, p2.X),
        //        Math.Abs(p1.X - p2.X) == W * B ? Math.Min(p1.Y, p2.Y) - H * B / 2 : Math.Min(p1.Y, p2.Y));
        //    rect = new RectangleF(rectP, size);
        //    g.DrawLine(mypen, p1, p2);

        //    Rectangle r = Rectangle.Round(rect);
        //    g.DrawRectangle(mypen, r);
        //}
    }
}
