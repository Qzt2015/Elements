using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Elements
{
    [Serializable]
    public class Break : Element //开关
    {
        public Break(float v1, float v2, float v3, float v4, int v5)
        {
            X = v1;
            Y = v2;
            W = v3;
            H = v4;
            B = 2F;
            signal = v5;
            name = "开关";
            OnOff = false;
        }


        //public override void Draw(Graphics g, Pen mypen)
        //{
        //    rect = new RectangleF(X, Y, W * B, H * B);

        //    g.DrawEllipse(mypen, X, Y + (H / 2 - 2) * B, 4 * B, 4 * B);
        //    g.DrawLine(mypen, X + 4 * B, Y + H / 2 * B, X + W * 1 / 3 * B, Y + H / 2 * B);
        //    g.DrawRectangle(mypen, X + W * 1 / 3 * B, Y, W * 1 / 3 * B, H * B);
        //    g.DrawLine(mypen, X + W * 2 / 3 * B, Y + H / 2 * B, X + (W - 4) * B, Y + H / 2 * B);
        //    g.DrawEllipse(mypen, X + (W - 4) * B, Y + (H / 2 - 2) * B, 4 * B, 4 * B);

        //    //g.DrawRectangle(mypen, Rectangle.Round(rect));
        //}

        //public RectangleF rr[1];//左接线柱
        private RectangleF r3;//左连接线
        private RectangleF r4;//中矩形
        private RectangleF r5;//右连接线
        //public RectangleF rr[2];//右接线柱

        private void location()
        {
            int B2 = 2;
            rr[1] = new RectangleF(0, (H / 2 * B- 2*B2) , 4, 4);
            r3 = new RectangleF(0, H / 2 * B, W * 1 / 3 , 0);
            r4 = new RectangleF(W * 1 / 3 * B, 0, W * 1 / 3, H);
            r5 = new RectangleF(W * 2 / 3 * B, H / 2 * B, (W - 4 - W * 2 / 3), 0);
            rr[2] = new RectangleF((W - 4) * B, (H / 2 * B- 2*B2) , 4, 4);
            rr[1] = Move_Rect(rr[1], X, Y, B2);
            r3 = Move_Rect(r3, X, Y, B);
            r4 = Move_Rect(r4, X, Y, B);
            r5 = Move_Rect(r5, X, Y, B);
            rr[2] = Move_Rect(rr[2], X, Y, B2);

            PointF center = new PointF(r4.X + r4.Width / 2, r4.Y + r4.Height / 2);
            rr[1] = RectRotate(rr[1], Angle, center);
            r3 = RectRotate(r3, Angle, center);
            r4 = RectRotate(r4, Angle, center);
            r5 = RectRotate(r5, Angle, center);
            rr[2] = RectRotate(rr[2], Angle, center);

            //字
            rect = new RectangleF(X, Y, W * B, H * B);
            rect = RectRotate(rect, Angle, center);
        }

        public override void Draw(Graphics g, Pen mypen, SolidBrush brush)
        {
            location();

            //g.DrawEllipse(mypen, rr[1]);
            g.FillEllipse(brush, rr[1]);
            g.DrawLine(mypen, r3.X, r3.Y, r3.X + r3.Width, r3.Y + r3.Height);
            if (OnOff)
            {
                g.FillRectangle(brush, Rectangle.Round(r4));
            }
            else
            {
                g.DrawRectangle(mypen, Rectangle.Round(r4));
            }
            g.DrawLine(mypen, r5.X, r5.Y, r5.X + r5.Width, r5.Y + r5.Height);
            //g.DrawEllipse(mypen, rr[2]);
            g.FillEllipse(brush, rr[2]);

            //g.DrawRectangle(mypen, Rectangle.Round(rr[1]));
            //g.DrawRectangle(mypen, Rectangle.Round(r3));
            //g.DrawRectangle(mypen, Rectangle.Round(r5));

            g.DrawString(name + "  电压:" + vbase, new Font("微软雅黑", 7 * (float)(Math.Ceiling(double.Parse(B.ToString())))), Brushes.Red, new RectangleF(X + W / 4 * B, Y - H * 3 / 2 * B, W * (B + 1), H * (B + 1)));
            //g.DrawRectangle(mypen, Rectangle.Round(new RectangleF(X + W / 4 * B, Y - H * 3 / 2 * B, W * (B + 1), H * (B + 1))));
        }

        //public Element Clone()
        //{
        //    Break bk = new Break();
        //    bk.X = this.X;
        //    bk.Y = this.Y;
        //    bk.W = this.W;
        //    bk.H = this.H;
        //    bk.B = this.B;
        //    return bk;
        //}


    }
}
