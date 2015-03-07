using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elements
{
    [Serializable]
    public class Transformer3 : Element //三绕阻变压器
    {
        public Transformer3(float v1, float v2, float v3, float v4, int v5)
        {
            X = v1;
            Y = v2;
            W = v3;
            dC = W / 3 * 3 / 5;//单个圆直径
            H = W * 11 / 20;
            B = 2F;
            signal = v5;
            name = "三绕组变压器";
        }

        private float dC;

        //public override void Draw(Graphics g, Pen mypen)
        //{
        //    rect = new RectangleF(X, Y, W * B, H * B);

        //    dC = W / 3 * 3 / 5;//单个圆直径
        //    g.DrawEllipse(mypen, X, Y + (dC / 2 - 2) * B + W * 3 / 9 * B, 4 * B, 4 * B);
        //    g.DrawLine(mypen, X + 4 * B, Y + dC / 2 * B + W * 3 / 9 * B, X + W * 1 / 3 * B, Y + dC / 2 * B + W * 3 / 9 * B);
        //    g.DrawEllipse(mypen, X + W * 1 / 3 * B, Y + W * 3 / 9 * B, dC * B, dC * B);//左边圆

        //    g.DrawEllipse(mypen, X + W * 1 / 3 * 7 / 5 * B, Y + W * 3 / 9 * B, dC * B, dC * B);//右边圆
        //    g.DrawLine(mypen, X + W * 2 / 3 * B, Y + dC / 2 * B + W * 3 / 9 * B, X + (W - 4) * B, Y + dC / 2 * B + W * 3 / 9 * B);
        //    g.DrawEllipse(mypen, X + (W - 4) * B, Y + (dC / 2 - 2) * B + W * 3 / 9 * B, 4 * B, 4 * B);

        //    g.DrawEllipse(mypen, X + W * 1 / 3 * 6 / 5 * B, Y + W * 2 / 9 * B, dC * B, dC * B);//上方圆
        //    g.DrawLine(mypen, X + W / 2 * B, Y + 4 * B, X + W / 2 * B, Y + W * 2 / 9 * B);
        //    g.DrawEllipse(mypen, X + (W / 2 - 2) * B, Y, 4 * B, 4 * B);

        //    //g.DrawRectangle(mypen, Rectangle.Round(rect));
        //}

        //public RectangleF rr[1];//左接线柱
        private RectangleF r4;//左连接线
        private RectangleF r5;//左圆
        //public RectangleF rr[2];//右接线柱
        private RectangleF r6;//右连接线
        private RectangleF r7;//右圆
        //public RectangleF rr[3];//右接线柱
        private RectangleF r8;//右连接线
        private RectangleF r9;//上圆

        private void location()
        {
            int B2 = 2;
            rr[1] = new RectangleF(0, (dC / 2 * B - 2 * B2) + W * 3 / 9 * B, 4, 4);
            r4 = new RectangleF(0, dC / 2 * B + W * 3 / 9 * B, W * 1 / 3, 0);
            r5 = new RectangleF(W * 1 / 3 * B, W * 3 / 9 * B, dC, dC);//左圆

            rr[2] = new RectangleF((W - 4) * B, (dC / 2 * B - 2 * B2) + W * 3 / 9 * B, 4, 4);
            r6 = new RectangleF(W * 2 / 3 * B, dC / 2 * B + W * 3 / 9 * B, (W - 4) - W * 2 / 3, 0);
            r7 = new RectangleF(W * 1 / 3 * 7 / 5 * B, W * 3 / 9 * B, dC, dC);//右圆

            rr[0] = new RectangleF((W / 2 * B - 2 * B2), 0, 4, 4);
            r8 = new RectangleF(W / 2 * B, 0, 0, W * 2 / 9);
            r9 = new RectangleF(W * 1 / 3 * 6 / 5 * B, W * 2 / 9 * B, dC, dC);//上圆

            rr[1] = Move_Rect(rr[1], X, Y, B2);
            r4 = Move_Rect(r4, X, Y, B);
            r5 = Move_Rect(r5, X, Y, B);
            rr[2] = Move_Rect(rr[2], X, Y, B2);
            r6 = Move_Rect(r6, X, Y, B);
            r7 = Move_Rect(r7, X, Y, B);
            rr[0] = Move_Rect(rr[0], X, Y, B2);
            r8 = Move_Rect(r8, X, Y, B);
            r9 = Move_Rect(r9, X, Y, B);

            PointF center = new PointF((rr[1].X + rr[2].X + rr[2].Width) / 2, rr[0].Y + r8.Height / 2 + r9.Height);
            //g.DrawLine(mypen, new Point(0, 0), center);
            rr[1] = RectRotate(rr[1], Angle, center);
            r4 = RectRotate(r4, Angle, center);
            r5 = RectRotate(r5, Angle, center);
            rr[2] = RectRotate(rr[2], Angle, center);
            r6 = RectRotate(r6, Angle, center);
            r7 = RectRotate(r7, Angle, center);
            rr[0] = RectRotate(rr[0], Angle, center);
            r8 = RectRotate(r8, Angle, center);
            r9 = RectRotate(r9, Angle, center);

            //字
            rect = new RectangleF(X, Y, W * B, H * B);
            rect = RectRotate(rect, Angle, center);
        }

        public override void Draw(Graphics g, Pen mypen,SolidBrush brush)
        {
            location();

            //g.DrawEllipse(mypen, rr[1]);
            g.FillEllipse(brush, rr[1]);
            g.DrawLine(mypen, r4.X, r4.Y, r4.X + r4.Width, r4.Y + r4.Height);
            g.DrawEllipse(mypen, r5);
            //g.DrawEllipse(mypen, rr[2]);
            g.FillEllipse(brush, rr[2]);
            g.DrawLine(mypen, r6.X, r6.Y, r6.X + r6.Width, r6.Y + r6.Height);
            g.DrawEllipse(mypen, r7);
            //g.DrawEllipse(mypen, rr[0]);
            g.FillEllipse(brush, rr[0]);
            g.DrawLine(mypen, r8.X, r8.Y, r8.X + r8.Width, r8.Y + r8.Height);
            g.DrawEllipse(mypen, r9);

            g.DrawString(name, new Font("微软雅黑", 7 * (float)(Math.Ceiling(double.Parse(B.ToString())))), Brushes.Red, new RectangleF(r8.X + W / 4 * B, r8.Y, W * (B + 1), H * (B + 1)));
            //g.DrawRectangle(mypen, Rectangle.Round(rect));
        }
    }
}
