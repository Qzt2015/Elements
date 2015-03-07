using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elements
{
    [Serializable]
    public class Generator : Element //发电机
    {
        public Generator(float v1, float v2, float v3, float v4, int v5)
        {
            X = v1;
            Y = v2;
            W = v3;
            H = W * 1 / 3;
            B = 2F;
            signal = v5;
            name = "发电机";
        }

        

        //public override void Draw(Graphics g, Pen mypen)
        //{
        //    rect = new RectangleF(X, Y, W * B, H * B);

        //    g.DrawEllipse(mypen, X, Y + (H / 2 - 2) * B, 4 * B, 4 * B);
        //    g.DrawLine(mypen, X + 4 * B, Y + H / 2 * B, X + W * 1 / 3 * B, Y + H / 2 * B);
        //    g.DrawEllipse(mypen, X + W * 1 / 3 * B, Y, H * B, H * B);
        //    g.DrawArc(mypen, X + W * 1 / 3 * B, Y + (H / 3) * B, H / 2 * B, H / 3 * B, 180, 180);
        //    g.DrawArc(mypen, X + W * 1 / 2 * B, Y + (H / 3) * B, H / 2 * B, H / 3 * B, 0, 180);

        //    //g.DrawLine(mypen, X + W * 2 / 3 * B, Y + H / 2 * B, X + (W - 4) * B, Y + H / 2 * B);
        //    //g.DrawEllipse(mypen, X + (W - 4) * B, Y + (H / 2 - 2) * B, 4 * B, 4 * B);

        //    //g.DrawRectangle(mypen, Rectangle.Round(rect));
        //}

        //public RectangleF rr[1];//左接线柱
        private RectangleF r2;//左连接线
        private RectangleF r3;//右圆
        private RectangleF r4;//左正弦线
        private RectangleF r5;//右正弦线

        private void location()
        {
            int B2 = 2;
            rr[1] = new RectangleF(0, (H / 2 * B- 2*B2) , 4, 4);
            r2 = new RectangleF(0, H / 2 * B, W * 1 / 3 , 0);
            r3 = new RectangleF(W * 1 / 3 * B, 0, H, H);
            r4 = new RectangleF(W * 1 / 3 * B, (H / 3) * B, H / 2, H / 3);
            r5 = new RectangleF(W * 1 / 2 * B, (H / 3) * B, H / 2, H / 3);
            rr[1] = Move_Rect(rr[1], X, Y, B2);
            r2 = Move_Rect(r2, X, Y, B);
            r3 = Move_Rect(r3, X, Y, B);
            r4 = Move_Rect(r4, X, Y, B);
            r5 = Move_Rect(r5, X, Y, B);

            PointF center = new PointF((rr[1].X + r3.X + r3.Width) / 2, rr[1].Y + rr[1].Height / 2);
            rr[1] = RectRotate(rr[1], Angle, center);
            r2 = RectRotate(r2, Angle, center);
            r3 = RectRotate(r3, Angle, center);
            r4 = RectRotate(r4, Angle, center);
            r5 = RectRotate(r5, Angle, center);

            //字
            rect = new RectangleF(X, Y, W * B, H * B);
            rect = RectRotate(rect, Angle, center);
        }

        public override void Draw(Graphics g, Pen mypen,SolidBrush brush)
        {
            location();

            //g.DrawEllipse(mypen, rr[1]);
            g.FillEllipse(brush, rr[1]);
            g.DrawLine(mypen, r2.X, r2.Y, r2.X + r2.Width, r2.Y + r2.Height);
            g.DrawEllipse(mypen, r3);
            g.DrawArc(mypen, r4, 180 + Angle, 180);
            g.DrawArc(mypen, r5, 0 + Angle, 180);

            //g.DrawRectangle(mypen, Rectangle.Round(r3));
            //g.DrawRectangle(mypen, Rectangle.Round(r4));
            //g.DrawRectangle(mypen, Rectangle.Round(r5));

            g.DrawString(name + "  电压:" + vbase, new Font("微软雅黑", 7 * (float)(Math.Ceiling(double.Parse(B.ToString())))), Brushes.Red, new RectangleF(r3.X, r3.Y - H / 2 * B, W * (B + 1), H * (B + 1)));
            //g.DrawRectangle(mypen, Rectangle.Round(rect));
        }
    }
}
