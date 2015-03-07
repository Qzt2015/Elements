using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net ;
using System.Threading;
using System.Net.Sockets ;
using System.IO;

namespace SocketClient
{
    public partial class Form1 : Form
    {
        private Socket m_ClientSocket = null;
        private NetworkStream m_NetworkStream = null;

        public Form1()
        {
            InitializeComponent();
        }

        
        // 创建Socket客户端
        private void button1_Click(object sender, EventArgs e)
        {

            if (m_ClientSocket == null)
            {
                m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                txtInfo.Text = txtInfo.Text + DateTime.Now.ToString() + ":" + "Socket客户端创建成功。"+"\r\n";
          
            }
            else
            {
                txtInfo.Text  = txtInfo.Text  +DateTime.Now.ToString()+":"  +"Socket客户端已创建。"+"\r\n";
            }

        }

        // 连接服务器
        private void button3_Click(object sender, EventArgs e)
        {
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
                Thread thread = new Thread(new ThreadStart(GetMsgFromClient));
                thread.Start();
                txtInfo.Text = txtInfo.Text + DateTime.Now.ToString() + ":" + "Socket客户端连接服务器成功。" + "\r\n";
            }
            else if (m_ClientSocket == null)
            {
                txtInfo.Text = txtInfo.Text + DateTime.Now.ToString() + ":" + "Socket客户端未创建。" + "\r\n";
            }
            else if (m_ClientSocket != null && m_ClientSocket.Connected)
            {
                txtInfo.Text = txtInfo.Text + DateTime.Now.ToString() + ":" + "Socket客户端已连接服务器。" + "\r\n";
            }
        }

        // 从服务器接收数据
        private void GetMsgFromClient()
        {
            while (true)
            {
                string recv = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = m_ClientSocket.Receive(recvBytes, recvBytes.Length, 0);
                recv += Encoding.BigEndianUnicode.GetString(recvBytes, 0, bytes);
                MessageBox.Show(recv,"我是客户端");
            }
        }

        // 断开服务器连接
        private void button4_Click(object sender, EventArgs e)
        {
            if (m_ClientSocket != null && m_ClientSocket.Connected)
            {
                m_ClientSocket.Disconnect(true); //关闭套接字连接并可以重用套接字
                //m_ClientSocket.Close();
                txtInfo.Text = txtInfo.Text + DateTime.Now.ToString() + ":" + "Socket客户端关闭连接成功。" + "\r\n";
            }
        }

        // 发送信息
        private void button2_Click(object sender, EventArgs e)
        {
            string sendString = txtSender.Text;
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(sendString);
            m_ClientSocket.Send(bytes, bytes.Length, 0);
            txtInfo.Text = txtInfo.Text + DateTime.Now.ToString() + ":" + "发送信息：" + sendString + "\r\n";
        }

        

    

   }
  
}