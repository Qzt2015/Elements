using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elements
{
    [Serializable]
    public class Capacitor : Element //电容
    {
        public Capacitor(float v1, float v2, float v3, float v4, int v5)
        {
            X = v1;
            Y = v2;
            W = v3;
            H = v4;
            B = 2F;
            signal = v5;
            name = "电容";
        }



        //public override void Draw(Graphics g, Pen mypen)
        //{
        //    rect = new RectangleF(X, Y, W * B, H * B);

        //    g.DrawEllipse(mypen, X, Y + (H / 2 - 2) * B, 4 * B, 4 * B);
        //    g.DrawLine(mypen, X + 4 * B, Y + H / 2 * B, X + W * 2 / 5 * B, Y + H / 2 * B);
        //    g.DrawLine(mypen, X + W * 2 / 5 * B, Y, X + W * 2 / 5 * B, Y + H * B);
        //    g.DrawLine(mypen, X + W * 3 / 5 * B, Y, X + W * 3 / 5 * B, Y + H * B);
        //    g.DrawLine(mypen, X + W * 3 / 5 * B, Y + H / 2 * B, X + (W - 4) * B, Y + H / 2 * B);
        //    g.DrawEllipse(mypen, X + (W - 4) * B, Y + (H / 2 - 2) * B, 4 * B, 4 * B);

        //    //g.DrawRectangle(mypen, Rectangle.Round(rect));
        //}

        //public RectangleF rr[1];//左接线柱
        private RectangleF r3;//左连接线
        private RectangleF r4;//中线1
        private RectangleF r5;//中线2
        private RectangleF r6;//右连接线
        //public RectangleF rr[2];//右接线柱

        private void location()
        {
            int B2 = 2;
            rr[1] = new RectangleF(0, (H / 2 * B - 2 * B2), 4, 4);
            r3 = new RectangleF(0, H / 2 * B, W * 2 / 5, 0);
            r4 = new RectangleF(W * 2 / 5 * B, 0, 0, H);
            r5 = new RectangleF(W * 3 / 5 * B, 0, 0, H);
            r6 = new RectangleF(W * 3 / 5 * B, H / 2 * B, (W - 4) - (W * 3 / 5), 0);
            rr[2] = new RectangleF((W - 4) * B, (H / 2 * B - 2 * B2), 4, 4);
            rr[1] = Move_Rect(rr[1], X, Y, B2);
            r3 = Move_Rect(r3, X, Y, B);
            r4 = Move_Rect(r4, X, Y, B);
            r5 = Move_Rect(r5, X, Y, B);
            r6 = Move_Rect(r6, X, Y, B);
            rr[2] = Move_Rect(rr[2], X, Y, B2);

            PointF center = new PointF((rr[1].X + rr[2].X + rr[2].Width) / 2, rr[1].Y + rr[1].Height / 2);
            rr[1] = RectRotate(rr[1], Angle, center);
            r3 = RectRotate(r3, Angle, center);
            r4 = RectRotate(r4, Angle, center);
            r5 = RectRotate(r5, Angle, center);
            r6 = RectRotate(r6, Angle, center);
            rr[2] = RectRotate(rr[2], Angle, center);

            //字
            rect = new RectangleF(X, Y, W * B, H * B);
            rect = RectRotate(rect, Angle, center);
        }

        public override void Draw(Graphics g, Pen mypen,SolidBrush brush)
        {
            location();

            //g.DrawEllipse(mypen, rr[1]);
            g.FillEllipse(brush, rr[1]);
            g.DrawLine(mypen, r3.X, r3.Y, r3.X + r3.Width, r3.Y + r3.Height);
            g.DrawLine(mypen, r4.X, r4.Y, r4.X + r4.Width, r4.Y + r4.Height);
            g.DrawLine(mypen, r5.X, r5.Y, r5.X + r5.Width, r5.Y + r5.Height);
            g.DrawLine(mypen, r6.X, r6.Y, r6.X + r6.Width, r6.Y + r6.Height);
            //g.DrawEllipse(mypen, rr[2]);
            g.FillEllipse(brush, rr[2]);

            g.DrawString(name + "  电压:" + vbase, new Font("微软雅黑", 7 * (float)(Math.Ceiling(double.Parse(B.ToString())))), Brushes.Red, new RectangleF(X + W / 4 * B, Y - H * B, W * (B + 1), H * (B + 1)));
            //g.DrawRectangle(mypen, Rectangle.Round(rect));
        }
    }
}
