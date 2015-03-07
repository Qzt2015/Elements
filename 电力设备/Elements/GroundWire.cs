using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elements
{
    [Serializable]
    public class GroundWire : Element //接地
    {
        public GroundWire(float v1, float v2, float v3, float v4, int v5)
        {
            X = v1;
            Y = v2;
            W = v3;
            H = v4;
            B = 2F;
            signal = v5;
            name = "地线";
        }


        //public override void Draw(Graphics g, Pen mypen)
        //{
        //    rect = new RectangleF(X, Y, W * B, H * B);

        //    g.DrawEllipse(mypen, X + (W / 2 - 2) * B, Y, 4 * B, 4 * B);
        //    g.DrawLine(mypen, X + (W / 2) * B, Y + 4 * B, X + (W / 2) * B, Y + H * 2 / 3 * B);
        //    g.DrawLine(mypen, X, Y + H * 2 / 3 * B, X + W * B, Y + H * 2 / 3 * B);
        //    g.DrawLine(mypen, X + 4 * B, Y + H * 7 / 9 * B, X + (W - 4) * B, Y + H * 7 / 9 * B);
        //    g.DrawLine(mypen, X + 7 * B, Y + H * 8 / 9 * B, X + (W - 7) * B, Y + H * 8 / 9 * B);
        //    g.DrawLine(mypen, X + 10 * B, Y + H * B, X + (W - 10) * B, Y + H * B);

        //    //g.DrawRectangle(mypen, Rectangle.Round(rect));
        //}

        //public RectangleF rr[1];//上接线柱
        private RectangleF r2;//上连接线
        private RectangleF r3;//线1
        private RectangleF r4;//线2
        private RectangleF r5;//线3
        private RectangleF r6;//线4

        private void location()
        {
            int B2 = 2;
            rr[1] = new RectangleF((W / 2 * B- 2*B2) , 0, 4, 4);
            r2 = new RectangleF((W / 2) * B,0, 0, H * 2 / 3 );
            r3 = new RectangleF(0, H * 2 / 3 * B, W, 0);
            r4 = new RectangleF(4 * B, H * 7 / 9 * B, (W - 4 - 4), 0);
            r5 = new RectangleF(7 * B, H * 8 / 9 * B, (W - 7) - 7, 0);
            r6 = new RectangleF(10 * B, H * B, (W - 10) - 10, 0);
            rr[1] = Move_Rect(rr[1], X, Y, B2);
            r2 = Move_Rect(r2, X, Y, B);
            r3 = Move_Rect(r3, X, Y, B);
            r4 = Move_Rect(r4, X, Y, B);
            r5 = Move_Rect(r5, X, Y, B);
            r6 = Move_Rect(r6, X, Y, B);

            PointF center = new PointF(r2.X, (rr[1].Y + r6.Y + rr[1].Height) / 2);
            rr[1] = RectRotate(rr[1], Angle, center);
            r2 = RectRotate(r2, Angle, center);
            r3 = RectRotate(r3, Angle, center);
            r4 = RectRotate(r4, Angle, center);
            r5 = RectRotate(r5, Angle, center);
            r6 = RectRotate(r6, Angle, center);

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
            g.DrawLine(mypen, r3.X, r3.Y, r3.X + r3.Width, r3.Y + r3.Height);
            g.DrawLine(mypen, r4.X, r4.Y, r4.X + r4.Width, r4.Y + r4.Height);
            g.DrawLine(mypen, r5.X, r5.Y, r5.X + r5.Width, r5.Y + r5.Height);
            g.DrawLine(mypen, r6.X, r6.Y, r6.X + r6.Width, r6.Y + r6.Height);

            //g.DrawRectangle(mypen, Rectangle.Round(r1));
            //g.DrawRectangle(mypen, Rectangle.Round(r3));
            //g.DrawRectangle(mypen, Rectangle.Round(r5));

            g.DrawString(name + "  电压:" + vbase, new Font("微软雅黑", 7 * (float)(Math.Ceiling(double.Parse(B.ToString())))), Brushes.Red, new RectangleF(rr[1].X + W / 4 * B, rr[1].Y, W * (B + 1), H * (B + 1)));
            //g.DrawRectangle(mypen, Rectangle.Round(rect));
        }
    }
}
