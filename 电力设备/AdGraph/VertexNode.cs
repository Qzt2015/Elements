using System;
using System.Collections.Generic;
using System.Text;

namespace NonLinearStruct
{
    [Serializable]
    /// <summary>
    /// �ڽӱ�����еĽ��
    /// </summary>
    public class VertexNode
    {
        private string vertexName;
        private bool visited;
        private EdgeNode firstNode;
        /// <summary>
        /// ��ȡ�����ö��������
        /// </summary>
        public string VertexName
        {
            get
            {
                return vertexName;
            }
            set
            {
                vertexName = value;
            }
        }
        /// <summary>
        /// ��ȡ�����ö����Ƿ񱻷���
        /// </summary>
        public bool Visited
        {
            get
            {
                return visited;
            }
            set
            {
                visited = value;
            }
        }
        /// <summary>
        /// ��ȡ�����ö���ĵ�һ���ڽӵ�
        /// </summary>
        public EdgeNode FirstNode
        {
            get
            {
                return firstNode;
            }
            set
            {
                firstNode = value;
            }
        }
        /// <summary>
        /// ��ʼ��VertexNode�����ʵ��
        /// </summary>
        /// <param name="vName">���������</param>
        public VertexNode(string vName)//���캯��
        {
            this.vertexName = vName;
            this.visited = false;
            this.firstNode = null;
        }
        /// <summary>
        /// ��ʼ��VertexNode�����ʵ��
        /// </summary>
        /// <param name="vName">���������</param>
        /// <param name="firstNode">����ĵ�һ���ڽӵ�</param>
        public VertexNode(string vName, EdgeNode firstNode)
        {
            this.vertexName = vName;
            this.visited = false;
            this.firstNode = firstNode;
        }
    }
}
