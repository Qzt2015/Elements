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
            Control.CheckForIllegalCrossThreadCalls = false;

        }


        // 开启服务器（创建Socket）
        private void button3_Click(object sender, EventArgs e)
        {
            serverIP = this.txtIP.Text.ToString();
            port = Convert.ToInt32(this.txtPort.Text);

            // 创建EndPoint
            IPAddress ipAddress = IPAddress.Parse(serverIP);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

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
                while (true)
                {
                    Thread thread = new Thread(new ThreadStart(GetMsgFromClient));
                    thread.Start();
                }
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
                this.textBox2.Text = this.textBox2.Text + System.DateTime.Now.ToString() + " 服务器收到消息" + ":" + recv + "\r\n";
            }
        }

        // 从客户端接收文件
        private void GetFileFromClient()
        {
            string newPath = "f:\\编程.doc";
            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }

            // 将网络流转为文件流
            FileStream fileStream = new FileStream(newPath, FileMode.OpenOrCreate);   
            // 读取网络流
            NetworkStream networkStream = new NetworkStream(m_TempSocket);  

            while (true)
            {
                Byte[] bufs = new Byte[1024];
                int curLen = networkStream.Read(bufs,0,bufs.Length);
                //if (curLen <= 0)
                //    break;
                fileStream.Write(bufs, 0, curLen); 

            }

            fileStream.Flush(); 
            fileStream.Close();
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
    }
}