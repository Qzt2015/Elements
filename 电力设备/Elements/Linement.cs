using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elements
{
    [Serializable]
    public class Linement : Element //线路
    {
        public Linement(float v1, float v2, float v3, float v4, int v5)
        {
            X = v1;
            Y = v2;
            W = v3;
            H = v4;
            B = 2F;
            signal = v5;
            name = "线路";
        }   



        //public override void Draw(Graphics g, Pen mypen)
        //{
        //    rect = new RectangleF(X, Y, W * B, H * B);

        //    g.DrawEllipse(mypen, X, Y, 4 * B, 4 * B);
        //    g.DrawLine(mypen, X + 4 * B, Y + (H / 2) * B, X + W * B, Y + (H / 2) * B);

        //    //g.DrawRectangle(mypen, Rectangle.Round(rect));
        //}

        //public RectangleF rr[1];//连接柱
        private RectangleF r2;//连接线

        private void location()
        {
            int B2 = 2;
            rr[1] = new RectangleF(0, (H / 2 * B- 2*B2) , 4, 4);
            r2 = new RectangleF(0, H / 2 * B, W , 0);
            rr[1] = Move_Rect(rr[1], X, Y, B2);
            r2 = Move_Rect(r2, X, Y, B);

            PointF center = new PointF((rr[1].X + r2.X + r2.Width) / 2, rr[1].Y + rr[1].Height / 2);
            rr[1] = RectRotate(rr[1], Angle, center);
            r2 = RectRotate(r2, Angle, center);

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

            g.DrawString(name + "  电压:" + vbase, new Font("微软雅黑", 7 * (float)(Math.Ceiling(double.Parse(B.ToString())))), Brushes.Red, new RectangleF(X + W / 4 * B, Y - H * B, W * (B + 1), H * (B + 1)));
            //g.DrawRectangle(mypen, Rectangle.Round(rect));
        }
    }
}
