using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Drawing2D;

using Elements;
using NonLinearStruct;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace DrawElements
{
    public partial class Form1 : Form
    {
        #region 前三次变量
        int jishuqi = 1;
        AdGraph ag;
        int signal;//统一索引
        bool zhuose = false;

        List<Link> line;//储存连线 
        List<Element> elt;  //用于将所画的电力设备存储起来   
        //List<Element> eltOld; //用于存储读取出来的原始elt，用于比较是否发生了修改
        bool IsChanged;//用于比较是否发生了修改
        string name; //用于存储所打开文件的文件名
        string Save_which;//判断是保存or另存为 
        string Draw_which;
        string Action;//记录动作状态
        bool IsMov;//是否处于移动状态
        bool IsLin;//是否处于连线状态

        PointF currPt;
        RectangleF currRc;
        int MoveI = -1;//选中设备
        int lineI = -1;//选中连线
        int linePointI = -1;//记录连线对应节点索引
        int InflectionI = -1;
        #endregion
        private int colorNum; //通电设备数目

        private Socket m_ClientSocket = null;
        private NetworkStream m_NetworkStream = null;

        public Form1()//初始化
        {
            InitializeComponent();
            line = new List<Link>();
            elt = new List<Element>();
            name = string.Empty;
            Save_which = "SaveAs";
            Draw_which = string.Empty;
            IsChanged = false;
            Action = "";
            IsMov = false;
            IsLin = false;
            ag = new AdGraph();
            signal = -1;

            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)//绘制出List中所有存储的电力设备
        {
            //双缓冲处理因图形移动时产生的重绘闪屏问题
            Rectangle rect = e.ClipRectangle;
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, rect);
            Graphics g = myBuffer.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.Clear(this.BackColor);
            Pen pen = new Pen(Color.Black, 1f);
            SolidBrush brush = new SolidBrush(Color.Black);

            for (int i = elt.Count - 1; i >= 0; i--)
            {
                elt[i].Draw(g, pen, brush);
            }
            for (int j = 0; j < line.Count; j++)
            {
                line[j].Draw(g, pen, brush);
            }

            #region 着色
            coloured(g, pen, brush);
            #endregion

            pen.Color = Color.Black;
            brush.Color = Color.Black;

            pen.Width = 1F;


            myBuffer.Render(e.Graphics);
            g.Dispose();
            myBuffer.Dispose();//释放资源
        }

        public void coloured(Graphics g, Pen pen, SolidBrush brush)//着色
        {
            colorNum = 0;
            foreach (Element elts in elt)
            {
                elts.Tongdian = false;
            }
            
            pen.Width = 2F;
            pen.Color = Color.Red;
            brush.Color = Color.Red;
            if (zhuose == true)
            {
                for (int i = 0; i < elt.Count; i++)
                {
                    List<Link> templine = new List<Link>();
                    string[] temp = new string[3];
                    string[] tempNew = new string[0];//当长度不够的时候自动分配相应数组
                    if (elt[i].name == "发电机")
                    {
                        for (int ii = 0; ii < 3; ii++)//为了和interface定义的节点数组呼应而已
                        {
                            string traversal = elt[i].signal.ToString() + "_" + ii.ToString();
                            temp[ii] = ag.DFSTraversal(traversal);  //深度优先搜索得到与发电机相连设备标记                                
                            tempNew = temp[ii].Split('\n');//拆分后为“某设备_某节点”形式
                            for (int j = 0; j < tempNew.Length; j++)
                            {
                                string[] s = tempNew[j].Split('_');
                                tempNew[j] = s[0];//得到设备编号
                            }

                            //已找到设备编号，开始绘制
                            if (tempNew != null)
                            {
                                //for (int j = 0; j < tempNew.Length; j++)
                                for (int j = 0; j < tempNew.Length - 1; j++)
                                {
                                    if (tempNew[j] != null)
                                    {
                                        for (int tempelt = 0; tempelt < elt.Count; tempelt++)//设备
                                        {
                                            //elt[tempelt].Tongdian = false;
                                            if (elt[tempelt].OnOff)//开关状态
                                            {
                                                if (elt[tempelt].signal == int.Parse(tempNew[j]))
                                                {
                                                    elt[tempelt].Draw(g, pen, brush);
                                                    elt[tempelt].Tongdian = true;
                                                    colorNum++;
                                                }
                                            }
                                        }
                                        for (int L = 0; L < line.Count; L++)//线
                                        {
                                            if (line[L].startElement.signal == int.Parse(tempNew[j]))
                                            {
                                                templine.Add(line[L]);//存入临时数组，待判断末尾决定是否着色
                                            }
                                        }
                                    }
                                }

                                //开始执行绘制
                                for (int j = 0; j < tempNew.Length - 1; j++)
                                {
                                    if (tempNew[j] != null)
                                    {
                                        for (int L2 = 0; L2 < templine.Count; L2++)//线着色有BUG
                                        {
                                            if (templine[L2].endElement != null && templine[L2].endElement.signal == int.Parse(tempNew[j]))
                                            {
                                                //if (templine[L2].endElement.Tongdian==true)
                                                templine[L2].Draw(g, pen, brush);
                                            }
                                            //if (templine[L2].startElement.name == "发电机")
                                            //{
                                            //    templine[L2].Draw(g, pen, brush);
                                            //}
                                            ////else if (templine[L2].endElement != null && templine[L2].endElement.signal == int.Parse(tempNew[j]))
                                            ////{
                                            ////    templine[L2].Draw(g, pen, brush);
                                            ////}
                                            //else if (templine[L2].endElement.Tongdian == true)
                                            //{
                                            //    templine[L2].Draw(g, pen, brush);
                                            //}
                                            //else//找不经过设备直接通过线相连的线
                                            //{
                                            //    int tempL = L2;
                                            //    int L3;
                                            //    while (true)//迭代找线线相连
                                            //    {
                                            //        for (L3 = 0; L3 < templine.Count; L3++)//找一个接线柱的连线
                                            //        {
                                            //            if (templine[L3].endElement == templine[tempL].startElement && templine[L3].endPoint == templine[tempL].startPoint)
                                            //            {
                                            //                tempL = L3;
                                            //                break;
                                            //            }
                                            //        }
                                            //        if (L3 == templine.Count)
                                            //            break;
                                            //        jishuqi++;
                                            //        label1.Text = jishuqi.ToString();
                                            //    }
                                            //    if (templine[tempL].startElement.name == "发电机")
                                            //    {
                                            //        templine[L2].Draw(g, pen, brush);
                                            //    }

                                            //}

                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            //发送数字
            try
            {
                if (m_ClientSocket.Connected)
                {
                    string sendString = colorNum.ToString();
                    byte[] bytes = Encoding.BigEndianUnicode.GetBytes(sendString);
                    m_ClientSocket.Send(bytes, bytes.Length, 0);
                }
                
            }
            catch
            {

            }
        }

        public void refresh() //设置缓冲属性并刷新重绘窗体
        {
            //构建缓冲区前需设置显示图元控件的几个属性，否则效果不明显
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |             // 双缓冲
                                ControlStyles.ResizeRedraw |
                                ControlStyles.AllPaintingInWmPaint, true);  // 禁止擦除背景.
            this.Refresh();//重绘窗体

            Act();//判断操作按钮是否可用
        }

        private void Act() //对操作按钮的状态判断
        {
            if (elt.Count != 0)//画图区绘入设备后，启用操作按钮
            {
                contextMenuStrip1.Enabled = true;
                操作ToolStripMenuItem.Enabled = true;
            }
            else if (elt.Count == 0)//复位操作按钮
            {
                contextMenuStrip1.Enabled = false;
                操作ToolStripMenuItem.Enabled = false;
                Action = String.Empty;
                Draw_which = String.Empty;
            }
        }

        public void ChooseElement(MouseEventArgs e)//判断画哪个电力设备
        {
            signal++;

            float x = (float)e.X;
            float y = (float)e.Y;

            switch (Draw_which)
            {
                case "母线":
                    Element muxian = new Bus(x, y, 50F, 10F, signal);
                    elt.Add(muxian);
                    break;
                case "开关":
                    Break kaiguan = new Break(x, y, 100F, 10F, signal);
                    elt.Add(kaiguan);
                    break;
                case "刀闸":
                    KnifeSwitch daozha = new KnifeSwitch(x, y, 100F, 10F, signal);
                    elt.Add(daozha);
                    break;
                case "二绕阻变压器":
                    Transformer_2 errao = new Transformer_2(x, y, 100F, -1F, signal);//高是宽的1/5,在类中定义
                    elt.Add(errao);
                    break;
                case "三绕阻变压器":
                    Transformer_3 sanrao = new Transformer_3(x, y, 100F, -1F, signal);//高是单个圆的(1 / 3 + 3 / 5)，在类中定义
                    elt.Add(sanrao);
                    break;
                case "电容":
                    Capacitor dianrong = new Capacitor(x, y, 100F, 15F, signal);
                    elt.Add(dianrong);
                    break;
                case "线路":
                    Linement xianlu = new Linement(x, y, 50F, 10F, signal);
                    elt.Add(xianlu);
                    break;
                case "接地线":
                    GroundWire dianxian = new GroundWire(x, y, 30F, 30F, signal);
                    elt.Add(dianxian);
                    break;
                case "发电机":
                    Generator dianji = new Generator(x, y, 100F, 100 / 3F, signal);//高是宽的1/3
                    elt.Add(dianji);
                    break;
            }
            for (int i = 0; i < 3; i++)//每个图元分配三个node，都存进AdGraph中
            {
                ag[3 * signal + i] = signal.ToString() + "_" + i.ToString();
            }

            if (elt[elt.Count - 1].OnOff)//如果图元为开启状态，则为同一元件间的node加边
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i != j)
                            ag.AddEdge(ag[3 * signal + i], ag[3 * signal + j], -1);
                    }
                }
            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left)) //按下鼠标左键才能进行绘制、移动等操作
            {
                currPt = e.Location;

                switch (Action)
                {
                    case "IsDrw":
                        {
                            ChooseElement(e);
                            this.refresh();
                        }
                        break;
                    case "IsMov":
                        {
                            for (MoveI = elt.Count - 1; MoveI >= 0; MoveI--)//判断当前鼠标点击的位置是否在各个所画设备的外接矩形中
                            {
                                if (elt[MoveI].rect.Contains(currPt))
                                {
                                    IsMov = true;
                                    break;
                                }
                            }

                            for (lineI = line.Count - 1; lineI >= 0; lineI--)
                            {
                                int i;
                                for (i = 0; i < line[lineI].rectLine.Count; i++)
                                {
                                    if (line[lineI].rectLine[i].Contains(currPt) || line[lineI].rectInflection[i].Contains(currPt))
                                    {
                                        IsMov = true;
                                        if (line[lineI].rectInflection[i].Contains(currPt))
                                        {
                                            InflectionI = i;
                                        }
                                        break;
                                    }
                                }
                                if (i != line[lineI].rectLine.Count)
                                    break;
                            }

                        }
                        break;

                    case "IsDel"://todo:删除着色后的连线
                        {
                            deleteElement();
                        }
                        break;

                    case "IsRot":
                        {
                            rotateElement();
                        }
                        break;

                    case "IsInZooming":
                        {
                            zoomElement();
                        }
                        break;

                    case "IsOutZooming":
                        {
                            zoomElement();
                        }
                        break;

                    case "IsLining":
                        {
                            MoveI = getNode();

                            if (MoveI != -1)
                            {
                                Link lk = new Link(currPt, currPt, 1F);
                                lk.startElement = elt[MoveI];
                                if (linePointI != -1)
                                    lk.startPoint = linePointI;
                                line.Add(lk);
                                lineI = line.IndexOf(lk);
                                IsLin = true;
                            }
                        }
                        break;

                    default:
                        {
                        }
                        break;

                }//end switch

                this.refresh();//更新节点所在矩形框
                reset();//更新连线List中各节点信息

                IsChanged = true;
            }

            if (e.Button.Equals(MouseButtons.Right))//右键菜单栏
            {
                MoveI = -1;
                lineI = -1;
                linePointI = -1;

                currPt = e.Location;
                for (MoveI = elt.Count - 1; MoveI >= 0; MoveI--)//判断当前鼠标点击的位置是否在各个所画设备的外接矩形中
                {
                    if (elt[MoveI].rect.Contains(currPt))
                    {
                        break;
                    }
                }
                for (lineI = line.Count - 1; lineI >= 0; lineI--)//判断当前鼠标点击的位置是否在各条连线中
                {
                    int j = -1;
                    for (j = 0; j < line[lineI].rectLine.Count; j++)//判断当前鼠标点击的位置是否在各个拐点中
                    {
                        if (line[lineI].rectLine[j].Contains(currPt))
                        {
                            linePointI = j;//用于增加拐点
                            break;
                        }
                    }
                    if (j != line[lineI].rectLine.Count)
                    {
                        break;
                    }
                }

                开关ToolStripMenuItem1.Visible = false;
                加点ToolStripMenuItem.Visible = false;
                if (MoveI != -1)
                {
                    if (elt[MoveI] is Elements.KnifeSwitch || elt[MoveI] is Elements.Break)//只允许刀闸跟开关使用
                    {
                        开关ToolStripMenuItem1.Visible = true;
                    }
                }
                if (lineI != -1)
                {
                    加点ToolStripMenuItem.Visible = true;
                }

                this.refresh();
                reset();//更新连线List中各节点信息
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            currPt = e.Location;
            if (IsLin == true)
            {
                MoveI = getNode();

                if (MoveI != -1)
                {
                    if (line[lineI].startElement == elt[MoveI])
                    {
                        MessageBox.Show("连线无效，连接了设备本身！");
                        line.RemoveAt(lineI);

                        lineI = -1;
                        MoveI = -1;
                    }
                    else
                    {
                        line[lineI].endElement = elt[MoveI];
                        if (linePointI != -1)
                            line[lineI].endPoint = linePointI;
                        line[lineI].W = currRc.X + currRc.Width / 2;
                        line[lineI].H = currRc.Y + currRc.Width / 2;

                        //加入ag中
                        string tempNode1 = line[lineI].startElement.signal.ToString() + "_" + line[lineI].startPoint.ToString();
                        string tempNode2 = line[lineI].endElement.signal.ToString() + "_" + line[lineI].endPoint.ToString();
                        ag.AddEdge(tempNode1, tempNode2, 1);
                        ag.AddEdge(tempNode2, tempNode1, 1);

                        lineI = -1;
                        MoveI = -1;
                    }
                }
                else
                {
                    line.RemoveAt(lineI);
                    lineI = -1;
                }
            }

            InflectionI = -1;

            IsMov = false;
            IsLin = false;

            this.refresh();
            reset();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsLin)
            {
                line[lineI].W = e.X;
                line[lineI].H = e.Y;
                line[lineI].Locus.Add(new PointF(line[lineI].W, line[lineI].H));

                currPt = e.Location;
                MoveI = getNode();//判断鼠标所到之处是否存在结点，已达到实时更新视图的目的

                this.refresh(); //刷新让Form1不断重绘
            }
            else if (IsMov)
            {
                float dx = 0, dy = 0;
                dx = e.X - currPt.X;
                dy = e.Y - currPt.Y;
                currPt = e.Location;

                if (MoveI >= 0) //移动单个图元
                {
                    elt[MoveI].X += (int)dx;
                    elt[MoveI].Y += (int)dy;

                    foreach (Link b in line)
                    {
                        if (b.startElement == elt[MoveI])
                        {
                            b.X += (int)dx;
                            b.Y += (int)dy;

                            b.Inflection.RemoveAt(0);
                            b.Inflection.Insert(0, new PointF(b.X, b.Y));
                        }
                        if (b.endElement == elt[MoveI])
                        {
                            b.W += (int)dx;
                            b.H += (int)dy;

                            b.Inflection.RemoveAt(b.Inflection.Count - 1);
                            b.Inflection.Add(new PointF(b.W, b.H));
                        }
                    }
                }

                if (InflectionI >= 0) //移动拐点
                {
                    line[lineI].Inflection[InflectionI] = currPt;
                    line[lineI].rectInflection.RemoveAt(InflectionI);
                    line[lineI].rectInflection.Insert(InflectionI, new RectangleF(line[lineI].Inflection[InflectionI].X - 4, line[lineI].Inflection[InflectionI].Y - 4, 8, 8));

                }

                this.refresh();
                reset();
            }

        }

        private void zoomElement()//放大缩小
        {
            for (MoveI = elt.Count - 1; MoveI >= 0; MoveI--)
            {
                if (elt[MoveI].rect.Contains(currPt))
                {
                    if (Action == "IsInZooming") elt[MoveI].B *= 1.1F;
                    else if (Action == "IsOutZooming") elt[MoveI].B /= 1.1F;
                    break;
                }
            }
        }

        private void rotateElement()//旋转
        {
            for (MoveI = elt.Count - 1; MoveI >= 0; MoveI--)
            {
                if (elt[MoveI].rect.Contains(currPt))
                {
                    elt[MoveI].Angle += 90;
                    break;
                }
            }
        }

        private void deleteElement()
        {
            # region 删除设备和相关节点
            for (MoveI = elt.Count - 1; MoveI >= 0; MoveI--)
            {
                if (elt[MoveI].rect.Contains(currPt))
                {
                    break;
                }
            }

            if (MoveI != -1)
            {

                List<Link> tempDel = new List<Link>();
                for (int i = 0; i < line.Count; i++)
                {
                    tempDel.Add(line[i]); //硬拷贝，不然删除后会影响链表
                }
                foreach (Link b in tempDel)//删除相关连线
                {
                    if (b.startElement == elt[MoveI])
                    {
                        string tempNode1 = b.startElement.signal.ToString() + "_" + b.startPoint.ToString();
                        string tempNode2 = b.endElement.signal.ToString() + "_" + b.endPoint.ToString();
                        ag.DeleteEdge(tempNode1, tempNode2);
                        ag.DeleteEdge(tempNode2, tempNode1);
                        line.Remove(b);
                    }
                    if (b.endElement == elt[MoveI])
                    {
                        string tempNode1 = b.startElement.signal.ToString() + "_" + b.startPoint.ToString();
                        string tempNode2 = b.endElement.signal.ToString() + "_" + b.endPoint.ToString();
                        ag.DeleteEdge(tempNode1, tempNode2);
                        ag.DeleteEdge(tempNode2, tempNode1);
                        line.Remove(b);
                    }
                }
                tempDel.Clear();

                //for (int i = 0; i < 3; i++)//删除内部连线-->相关连线包括内部连线
                //{
                //    for (int j = 0; j < 3; j++)
                //    {
                //        if (i != j && ag[3 * elt[MoveI].signal + j] != null)
                //            ag.DeleteEdge(ag[3 * elt[MoveI].signal + i], ag[3 * elt[MoveI].signal + j]);
                //    }
                //}
                for (int i = 0; i < 3; i++)//注销相关node
                {
                    ag[3 * elt[MoveI].signal + i] = null;
                }

                elt.RemoveAt(MoveI);
                MoveI = -1;//防止删除操作后,滚轮缩放将对上一个电力设备进行缩放
            }

            #endregion


            for (lineI = line.Count - 1; lineI >= 0; lineI--)
            {
                //删除拐点
                for (int i = 1; i < line[lineI].rectInflection.Count; i++)
                {
                    if (line[lineI].rectInflection[i].Contains(currPt))
                    {
                        line[lineI].Inflection.RemoveAt(i);
                        line[lineI].rectInflection.RemoveAt(i);

                        break;
                    }
                }

                // 删除连线
                for (int i = 0; i < line[lineI].rectLine.Count; i++)
                {
                    if (line[lineI].rectLine[i].Contains(currPt))
                    {
                        //删除图中的边
                        string tempNode1 = line[lineI].startElement.signal.ToString() + "_" + line[lineI].startPoint;
                        string tempNode2 = line[lineI].endElement.signal.ToString() + "_" + line[lineI].endPoint;
                        ag.DeleteEdge(tempNode1, tempNode2);
                        ag.DeleteEdge(tempNode2, tempNode1);

                        line.RemoveAt(lineI);
                        break;
                    }
                }

            }
            lineI = -1;

            this.refresh();
        }

        private int getNode()//判断点击的位置是否在各个所画设备的某个节点中
        {
            int I;
            for (I = elt.Count - 1; I >= 0; I--)
            {
                if (elt[I].rect.Contains(currPt))//找具体图元
                {
                    for (linePointI = 0; linePointI < elt[I].rr.Length; linePointI++)//找具体节点
                    {
                        if (elt[I].rr[linePointI].Contains(currPt))
                        {
                            break;//跳出寻找节点循环
                        }
                    }
                    if (linePointI != elt[I].rr.Length)
                    {
                        currRc = elt[I].rr[linePointI];
                        currPt = new PointF(currRc.X + currRc.Width / 2, currRc.Y + currRc.Width / 2);
                        break;//跳出寻找图元循环
                    }
                    else
                    {
                        linePointI = -1;
                    }
                }
            }

            return I;
        }

        private void reset()//遍历更新图元节点变化
        {
            //关于图元节点
            for (int i = elt.Count - 1; i >= 0; i--)
            {
                foreach (Link b in line)
                {
                    if (b.startElement == elt[i])
                    {
                        currRc = elt[i].rr[b.startPoint];
                        b.X = currRc.X + currRc.Width / 2;
                        b.Y = currRc.Y + currRc.Width / 2;

                        b.Inflection.RemoveAt(0);
                        b.Inflection.Insert(0, new PointF(currRc.X + currRc.Width / 2, currRc.Y + currRc.Width / 2));
                        b.Locus = new List<PointF>();
                    }
                    if (b.endElement == elt[i])
                    {
                        currRc = elt[i].rr[b.endPoint];
                        b.W = currRc.X + currRc.Width / 2;
                        b.H = currRc.Y + currRc.Width / 2;

                        b.Inflection.RemoveAt(b.Inflection.Count - 1);
                        b.Inflection.Add(new PointF(currRc.X + currRc.Width / 2, currRc.Y + currRc.Width / 2));
                        b.Locus = new List<PointF>();
                    }
                }
            }

            ////关于连线拐点
            for (int i = 0; i < line.Count; i++)
            {
                line[i].rectInflection.Clear();//清除拐点外接矩形
                line[i].rectLine.Clear();//清除线外接矩形
                for (int j = 0; j < line[i].Inflection.Count - 1; j++)
                {
                    line[i].rectInflection.Add(new RectangleF(line[i].Inflection[j].X - 4, line[i].Inflection[j].Y - 4, 8F, 8F));
                    //两个拐点之间的连线（）
                    line[i].rectLine.Add(new RectangleF((line[i].Inflection[j].X < line[i].Inflection[j + 1].X) ? line[i].Inflection[j].X : line[i].Inflection[j + 1].X,
                        (line[i].Inflection[j].Y < line[i].Inflection[j + 1].Y) ? line[i].Inflection[j].Y : line[i].Inflection[j + 1].Y,
                        (line[i].Inflection[j].X - line[i].Inflection[j + 1].X) != 0 ? Math.Abs((line[i].Inflection[j].X - line[i].Inflection[j + 1].X)) : 4,
                        (line[i].Inflection[j].Y - line[i].Inflection[j + 1].Y) != 0 ? Math.Abs((line[i].Inflection[j].Y - line[i].Inflection[j + 1].Y)) : 4));
                }
            }

            this.refresh();

        }

        private delegate void FrmRefreshDelegate();
        private void GetMsgFromServer()//从服务器接收数据
        {
            while (true)
            {
                string recv = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = m_ClientSocket.Receive(recvBytes, recvBytes.Length, 0);
                recv = Encoding.BigEndianUnicode.GetString(recvBytes, 0, bytes);
                //recv = bytes.ToString();
                //MessageBox.Show(recv, "我是客户端");
                for (int i = 0; i < elt.Count; i++)
                {
                    if (elt[i].Tongdian)
                        elt[i].Vbase = int.Parse(recv);
                    else
                        elt[i].Vbase = 0;
                }
                Invoke(new FrmRefreshDelegate(refresh));
            }
        }



        #region 存储相关的代码
        List<object> saveload = new List<object>();
        /// <summary>
        /// 存储操作（序列化）
        /// </summary>
        private void Serialize()
        {
            if (Save_which == "SaveAs")
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = "DataFile";
                if (name != string.Empty)//记录上一次打开文件的文件名
                {
                    saveFile.FileName = name;
                }
                saveFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                saveFile.Filter = "数据文件(*.dat)|*.dat|所有文件(*.*)|*.*";
                saveFile.Title = "保存数据文件";
                saveFile.AddExtension = true;
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(saveFile.FileName, FileMode.Create);
                    BinaryFormatter bf = new BinaryFormatter();
                    try
                    {
                        saveload = new List<object>();
                        saveload.Add(elt);
                        saveload.Add(line);
                        saveload.Add(ag);
                        saveload.Add(signal);

                        bf.Serialize(fs, saveload);
                        MessageBox.Show("保存成功");
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show("保存失败");
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
            else if (Save_which == "Save")
            {
                if (name == string.Empty)//记录上一次打开文件的文件名
                {
                    name = "DataFile";
                }
                FileStream fs = new FileStream(name, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    saveload = new List<object>();
                    saveload.Add(elt);
                    saveload.Add(line);
                    saveload.Add(ag);
                    saveload.Add(signal);

                    bf.Serialize(fs, saveload);
                    MessageBox.Show("保存成功");
                }
                catch (Exception e)
                {

                    MessageBox.Show("保存失败");
                }
                finally
                {
                    fs.Close();
                }
            }
        }
        /// <summary>
        /// 打开操作（反序列化）
        /// </summary>
        private void DeSerialize()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            openFile.Filter = "数据文件(*.dat)|*.dat|所有文件(*.*)|*.*";
            openFile.Title = "打开数据文件";
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFile.FileName, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    saveload = bf.Deserialize(fs) as List<object>;
                    elt = saveload[0] as List<Element>;
                    line = saveload[1] as List<Link>;
                    ag = saveload[2] as AdGraph;
                    signal = (int)saveload[3];

                    //将文件名显示在窗体上
                    char[] spliter = { '\\' };
                    string[] temp = openFile.FileName.Split(spliter);
                    name = temp[temp.Length - 1];
                    this.Text = name;
                }
                catch (Exception e)
                {
                    MessageBox.Show("读取失败");
                }
                finally
                {
                    fs.Close();
                }
            }
        }
        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (eltOld != elt)            
            //{
            //    if (elt.Count != 0)
            if (IsChanged)
            {
                DialogResult dr = MessageBox.Show("是否保存更改？", "提示 ", MessageBoxButtons.YesNoCancel);

                if (dr == DialogResult.Yes)
                {
                    Serialize();
                }

                else if (dr == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            //}
        }

        #region 菜单栏
        private void menu_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem tsm = sender as ToolStripMenuItem;
            //if (tsm != "")
            //{
                
            switch (tsm.Name)
            {
                #region 绘制菜单
                case "母线ToolStripMenuItem":
                    {
                        Draw_which = "母线";
                        Action = "IsDrw";
                    }
                    break;

                case "开关ToolStripMenuItem":
                    {
                        Draw_which = "开关";
                        Action = "IsDrw";
                    }
                    break;

                case "刀闸ToolStripMenuItem":
                    {
                        Draw_which = "刀闸";
                        Action = "IsDrw";
                    }
                    break;

                case "二绕阻变压器ToolStripMenuItem":
                    {
                        Draw_which = "二绕阻变压器";
                        Action = "IsDrw";
                    }
                    break;

                case "三绕阻变压器ToolStripMenuItem":
                    {
                        Draw_which = "三绕阻变压器";
                        Action = "IsDrw";
                    }
                    break;

                case "电容ToolStripMenuItem":
                    {
                        Draw_which = "电容";
                        Action = "IsDrw";
                    }
                    break;

                case "线路ToolStripMenuItem":
                    {
                        Draw_which = "线路";
                        Action = "IsDrw";
                    }
                    break;

                case "接地线ToolStripMenuItem":
                    {
                        Draw_which = "接地线";
                        Action = "IsDrw";
                    }
                    break;

                case "发电机ToolStripMenuItem":
                    {
                        Draw_which = "发电机";
                        Action = "IsDrw";
                    }
                    break;
                #endregion

                #region 操作菜单
                case "移动ToolStripMenuItem":
                    {
                        Action = "IsMov";
                    }
                    break;

                case "删除ToolStripMenuItem":
                    {
                        Action = "IsDel";
                    }
                    break;
                case "旋转ToolStripMenuItem":
                    {
                        Action = "IsRot";
                    }
                    break;
                case "放大ToolStripMenuItem":
                    {
                        Action = "IsInZooming";
                    }
                    break;
                case "缩小ToolStripMenuItem":
                    {
                        Action = "IsOutZooming";
                    }
                    break;
                case "连线ToolStripMenuItem1":
                    {
                        Action = "IsLining";
                    }
                    break;

                case "刷新ToolStripMenuItem":
                    {
                        Action = "";
                        this.refresh();
                        reset();
                    }
                    break;
                case "着色ToolStripMenuItem1":
                    {
                        zhuose = !zhuose;
                        this.refresh();
                    }
                    break;
                case "清空ToolStripMenuItem1":
                    {
                        DialogResult dr = MessageBox.Show("确认清空？", "提示 ", MessageBoxButtons.YesNoCancel);
                        if (dr == DialogResult.Yes)
                        {
                            Graphics graphics = this.CreateGraphics();
                            graphics.Clear(BackColor);

                            elt.Clear();
                            line.Clear();
                            signal = -1;
                            ag = new AdGraph();

                            Act();//对图元为空后一系列动作的判断
                        }
                    }
                    break;


                #endregion

                #region 右键菜单
                case "开关ToolStripMenuItem1":
                    {
                        if (MoveI != -1)
                            elt[MoveI].OnOff = !elt[MoveI].OnOff;

                        //利用节点表的Visited属性判断设备开关状态（已改）-->将所有node均存入ag表中，通过同一设备node间的edge判断其开关状态
                        if (elt[MoveI].OnOff)//如果图元为开启状态，则为同一元件间的node加边，否则删边
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (i != j)
                                        ag.AddEdge(ag[3 * elt[MoveI].signal + i], ag[3 * elt[MoveI].signal + j], -1);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (i != j)
                                        ag.DeleteEdge(ag[3 * elt[MoveI].signal + i], ag[3 * elt[MoveI].signal + j]);
                                }
                            }
                        }
                    }
                    break;
                case "加点ToolStripMenuItem":
                    {
                        line[lineI].Inflection.Insert(linePointI + 1, currPt);
                        reset();
                    }
                    break;


                #endregion

                #region TCP菜单
                case "建立连接ToolStripMenuItem":
                    {
                        if (m_ClientSocket == null)
                        {
                            //创建客户端
                            m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            #region 连接服务器

                            //获取本机IP地址
                            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                            IPAddress ipa = ipe.AddressList[2];
                            //string host = txtIP.Text.Trim();
                            int port = 2000;//端口号
                            //IPAddress ipAddress = IPAddress.Parse(host);
                            //IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
                            IPEndPoint endPoint = new IPEndPoint(ipa, port);

                            if (m_ClientSocket != null && !m_ClientSocket.Connected)
                            {
                                m_ClientSocket.Connect(endPoint);
                                Thread thread = new Thread(new ThreadStart(GetMsgFromServer));
                                thread.Start();
                                MessageBox.Show("连接服务器成功", "我是客户端");
                            }
                            else if (m_ClientSocket == null)
                            {
                                MessageBox.Show("Socket客户端未创建", "我是客户端");
                            }
                            else if (m_ClientSocket != null && m_ClientSocket.Connected)
                            {
                                MessageBox.Show("客户端已连接服务器", "我是客户端");
                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Socket客户端已创建", "我是客户端");
                        }
                    }
                    break;
               
                case "断开服务器ToolStripMenuItem":
                    {
                        if (m_ClientSocket != null && m_ClientSocket.Connected)
                        {
                            m_ClientSocket.Disconnect(true); //关闭套接字连接并可以重用套接字
                            //m_ClientSocket.Close();
                            MessageBox.Show("关闭连接成功", "我是客户端");
                        }
                    }
                    break;
#endregion
            }

            switch (tsm.Name)
            {
                #region 文件菜单
                case "保存ToolStripMenuItem":
                    {
                        if (name == string.Empty)
                        {
                            Serialize();
                        }
                        else
                        {
                            Save_which = "Save";
                            Serialize();
                        }
                    }
                    break;

                case "另存为ToolStripMenuItem":
                    {
                        Save_which = "SaveAs";
                        Serialize();
                    }
                    break;

                case "打开ToolStripMenuItem":
                    {
                        DeSerialize();
                        this.refresh();
                        Draw_which = string.Empty;
                    }
                    break;

                case "退出ToolStripMenuItem":
                    {
                        Close();
                    }
                    break;
                #endregion
            }

            MoveI = -1;
            lineI = -1;
            linePointI = -1;
            this.Refresh();
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 断开服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}