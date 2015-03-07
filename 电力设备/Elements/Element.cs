using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Elements
{
    [Serializable]
    public abstract class Element : Object
    {
        protected int vbase;
        public int Vbase
        {
            get { return vbase; }
            set { vbase = value; }
        }
        #region 私有属性及其封装

        //私有属性
        private float x, y;//起始点坐标
        private float w, h;//图形的宽度与高度
        private float b;//缩放比例   
        private float angle = 0;

        public RectangleF rect;//外接矩形

        //私有属性封装
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public float W
        {
            get { return w; }
            set { w = value; }
        }
        public float H
        {
            get { return h; }
            set { h = value; }
        }
        public float B
        {
            get { return b; }
            set { b = value; }
        }
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        #endregion

        public bool OnOff=true;
        public bool Tongdian = false;
        public string name;
        public int signal;//索引值
        public RectangleF[] rr=new RectangleF[3];//存储节点

        //公共方法
        public virtual void Draw(Graphics g, Pen mypen)//paper,pen      
        {
        }

        public virtual void Draw(Graphics g, Pen mypen,float angle)//paper,pen      
        {
        }        

        public virtual void Draw(Graphics g, Pen mypen,SolidBrush brush)//paper,pen      
        {
        }

        public string GetName(int signal)
        {
            if (this.signal == signal)
                return this.name;
            else
                return null;
        }

        //public bool is_pick(PointF p)
        //{
        //    RectangleF r = new RectangleF(X, Y, W * B, H * B);
        //    return rect.Contains(p);
        //}

        //public abstract Element Clone();

        /// <summary>   
        /// 对一个坐标点按照一个中心进行旋转   
        /// </summary>    
        /// <param name="p1">要旋转的点</param>   
        /// <param name="angle">旋转角度，笛卡尔直角坐标</param>   
        /// <returns></returns>         
        public PointF PointRotate(PointF p1, double angle, PointF cankao)
        {
            PointF tmp = new PointF();
            double Hud = angle * Math.PI / 180; /*角度变成弧度*/
            double x1 = (p1.X - cankao.X) * Math.Cos(Hud) + (p1.Y - cankao.Y) * Math.Sin(Hud) + cankao.X;
            double y1 = -(p1.X - cankao.X) * Math.Sin(Hud) + (p1.Y - cankao.Y) * Math.Cos(Hud) + cankao.Y;
            tmp.X = (float)x1;
            tmp.Y = (float)y1;
            return tmp;
        }

        /// <summary>
        /// 对一个矩形按照一个中心进行旋转   
        /// </summary>
        /// <param name="r"></param>
        /// <param name="sita"></param>
        /// <param name="cankaodian"></param>
        /// <returns></returns>
        public RectangleF RectRotate(RectangleF r, double angle, PointF cankao)
        {
            PointF p1 = new PointF();
            PointF p2 = new PointF();

            p1 = r.Location;
            p2.X = r.Location.X + r.Width;
            p2.Y = r.Location.Y + r.Height;

            p1 = PointRotate(p1, angle, cankao);
            p2 = PointRotate(p2, angle, cankao);

            PointF p = new PointF();
            float w;
            float h;
            p.X = (float)Math.Min(p1.X, p2.X);
            p.Y = (float)Math.Min(p1.Y, p2.Y);
            w = (float)Math.Abs(p1.X - p2.X);
            h = (float)Math.Abs(p1.Y - p2.Y);

            RectangleF tmp = new RectangleF(p.X, p.Y, w, h);
            return tmp;
        }

        /// <summary>
        /// 移动拾取矩形
        /// </summary>
        /// <param name="r"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public RectangleF Move_Rect(RectangleF r, float x, float y, float b)
        {
            r.X = (r.X + x);
            r.Y = (r.Y + y);
            r.Width = r.Width * b;
            r.Height = r.Height * b;
            return r;
        }
    }
}
