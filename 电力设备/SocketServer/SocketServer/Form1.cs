using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace SocketServer
{
    public partial class Form1 : Form
    {
        private string serverIP;
        private int port;
        private Socket m_ServerSocket = null;
        private Socket m_TempSocket = null;
        private Thread m_thread = null;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = true;//关键代码

        }


        // 开启服务器（创建Socket）
        private void button3_Click(object sender, EventArgs e)
        {
            //获取本机IP地址
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipa = ipe.AddressList[2];
            //serverIP = this.txtIP.Text.ToString();
            port = 2000;

            // 创建EndPoint
            //IPAddress ipAddress = IPAddress.Parse(serverIP);
            //IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
            IPEndPoint endPoint = new IPEndPoint(ipa, port);

            // 创建Socket
            if (m_ServerSocket == null)
            {
                m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_ServerSocket.Bind(endPoint);
                this.textBox2.Text = this.textBox2.Text + DateTime.Now.ToString() + ":开启服务器成功！" + "\r\n";
            }
            else
            {
                this.textBox2.Text = this.textBox2.Text + DateTime.Now.ToString() + ":服务器已开启！" + "\r\n";
            }
        }

        // 关闭服务器
        private void button4_Click(object sender, EventArgs e)
        {
            m_thread.Abort(); 
            m_ServerSocket.Close();
            this.textBox2.Text = this.textBox2.Text + DateTime.Now.ToString() + ":" + "服务器关闭。" + "\r\n";
        }

        // 启动监听
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_ServerSocket != null && m_ServerSocket.IsBound)
            {
                // 启动监听
                m_ServerSocket.Listen(0);
                this.textBox2.Text = this.textBox2.Text + DateTime.Now.ToString() + ":" + "服务器开启侦听" + "\r\n";

                // 创建新线程，接收客户端的连接
                m_thread  = new Thread(new ThreadStart(AcceptFromClient));
                m_thread.Start();
             
            }
        }

        // 接收客户端的连接
        private void AcceptFromClient()
        {
            while (true)
            {
                m_TempSocket = m_ServerSocket.Accept();
                //while (true)
                //{
                    Thread thread = new Thread(new ThreadStart(GetMsgFromClient));
                    Thread thread1 = new Thread(new ThreadStart(SendMsgToClient));
                    thread.Start();
                    thread1.Start();
                //}
            }
        }

        // 从客户端接收数据
        private void GetMsgFromClient()
        {
            while (true)
            {
                string recv = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = m_TempSocket.Receive(recvBytes, recvBytes.Length, 0);
                recv += Encoding.BigEndianUnicode.GetString(recvBytes, 0, bytes);
                //this.textBox2.Text = this.textBox2.Text + System.DateTime.Now.ToString() + " 服务器收到消息" + ":" + recv + "\r\n";
                ////反馈
                //string sendString = "老子收到了，有事上奏，无事退朝";
                //byte[] bytes1 = Encoding.BigEndianUnicode.GetBytes(sendString);
                //m_TempSocket.Send(bytes1, bytes1.Length, 0);
            }
        }

        // 关闭监听
        private void button2_Click(object sender, EventArgs e)
        {
            m_thread.Abort();
            if (m_TempSocket != null)
            {
                m_TempSocket.Close();
            }
            this.textBox2.Text = this.textBox2.Text + DateTime.Now.ToString() + ":关闭监听成功。" + "\r\n";
            
        }
        //发送数据
        private void button6_Click(object sender, EventArgs e)
        {
            SendMsgToClient();
            //string sendString = "皇上有旨~有事上奏，无事哪凉快哪呆着去~";
            //byte[] bytes1 = Encoding.BigEndianUnicode.GetBytes(sendString);
            //m_TempSocket.Send(bytes1, bytes1.Length, 0);
        }

        private void SendMsgToClient()
        {
            while (true)
            {
                
                Random r = new Random();
                int v = r.Next(220);
                string sendString = v.ToString();
                byte[] bytes1 = Encoding.BigEndianUnicode.GetBytes(sendString);
                m_TempSocket.Send(bytes1, bytes1.Length, 0);
                Thread.Sleep(1000);
                
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}