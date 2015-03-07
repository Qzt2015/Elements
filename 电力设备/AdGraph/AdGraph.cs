using System;
using System.Collections.Generic;
using System.Text;

namespace NonLinearStruct
{
    [Serializable]
    /// <summary>
    /// ͼ������������ʵ��--�����ڽӱ�
    /// </summary>
    public class AdGraph
    {
        public List<VertexNode> vertexList;//����
        private int vertexCount;

        /// <summary>
        /// ��ȡͼ�Ľ����
        /// </summary>
        public int VertexCount
        {
            get
            {
                return vertexCount;
            }
        }
        /// <summary>
        /// ��ʼ��AdGraph�����ʵ��
        /// </summary>
        /// <param name="vCount">ͼ�н��ĸ���</param>
        public AdGraph()
        {
            vertexList = new List<VertexNode>();
        }
        /// <summary>
        /// ��ȡ������ͼ�и���������
        /// </summary>
        /// <param name="index">������ƴ��㿪ʼ������</param>
        /// <returns>ָ����������������</returns>
        public string this[int index]
        {
            get
            {
                if (index < 0 || index > vertexCount - 1)
                    throw new Exception("����λ����Ч");
                return vertexList[index] == null ? "NULL" : vertexList[index].VertexName;
            }
            set
            {
                if (index < 0 || index > vertexCount)
                    throw new Exception("����λ����Ч");
                if (index == vertexCount)
                    vertexList.Add(new VertexNode(value));
                else
                    vertexList[index].VertexName = value;

                vertexCount = vertexList.Count;
            }
        }
        private int GetIndex(string VertexName)
        {
            //�õ�����ڽ����е�λ��
            int i;
            for (i = 0; i < vertexCount; i++)
            {
                if (vertexList[i] != null && vertexList[i].VertexName == VertexName)
                    break;
            }
            return i == vertexCount ? -1 : i;
        }
        /// <summary>
        /// ��ͼ�ӱ�
        /// </summary>
        /// <param name="StartVertexName">��ʼ��������</param>
        /// <param name="EndVertexName">��ֹ��������</param>
        /// <param name="Weight">���ϵ�Ȩֵ</param>
        public void AddEdge(string StartVertexName, string EndVertexName, double Weight)
        {
            int i = GetIndex(StartVertexName);
            int j = GetIndex(EndVertexName);

            if (i == -1 || j == -1)
                throw new Exception("ͼ�в����ڸñ�.");

            EdgeNode temp = vertexList[i].FirstNode;
            if (temp == null)
            {
                vertexList[i].FirstNode = new EdgeNode(j, Weight);
            }
            else
            {
                while (temp.Next != null)
                    temp = temp.Next;
                temp.Next = new EdgeNode(j, Weight);
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="StartVertexName">��ʼ��������</param>
        /// <param name="EndVertexName">��ֹ��������</param>
        public void DeleteEdge(string StartVertexName, string EndVertexName)
        {
            int i = GetIndex(StartVertexName);
            int j = GetIndex(EndVertexName);

            if (i == -1 || j == -1)
                throw new Exception("ͼ�в����ڸñ�.");

            EdgeNode temp = vertexList[i].FirstNode;
            if (temp == null)
            {
                throw new Exception("ͼ�в����ڸñ�.");
            }
            else
            {
                if (temp.Index == j)
                {
                    vertexList[i].FirstNode = temp.Next;
                }
                else
                {
                    while (temp.Next != null)
                    {
                        if (temp.Next.Index == j)
                        {
                            temp.Next = temp.Next.Next;
                            break;
                        }
                        temp = temp.Next;
                    }
                }
            }
        }

        private void DFS(int i, ref string DFSResult)
        {
            //������������ݹ麯��
            vertexList[i].Visited = true;
            DFSResult += vertexList[i].VertexName + "\n";
            EdgeNode p = vertexList[i].FirstNode;
            while (p != null)
            {
                if (vertexList[p.Index].Visited == true)
                    p = p.Next;
                else
                    DFS(p.Index, ref DFSResult);
            }
        }
        /// <summary>
        /// �õ����������������
        /// </summary>
        /// <param name="StartVertexName">�������������������ʼ������</param>
        /// <returns>���������������</returns>
        public string DFSTraversal(string StartVertexName)
        {
            string DFSResult = string.Empty;
            int i = GetIndex(StartVertexName);
            if (i != -1)
            {
                for (int j = 0; j < vertexCount; j++)
                    vertexList[j].Visited = false;
                DFS(i, ref DFSResult);
            }
            return DFSResult;
        }

        /// <summary>
        /// �õ����������������
        /// </summary>
        /// <param name="startNodeName">���й��������������ʼ������</param>
        /// <returns>���������������</returns>
        public string BFSTraversal(string startNodeName)
        {
            string BFSResult = string.Empty;
            int i = GetIndex(startNodeName);
            if (i != -1)
            {
                for (int j = 0; j < vertexCount; j++)
                    vertexList[j].Visited = false;
                vertexList[i].Visited = true;
                BFSResult += vertexList[i].VertexName + "\n";
                List<int> Q = new List<int>();
                Q.Add(i);
                while (Q.Count != 0)
                {
                    int j = Q[0];
                    Q.RemoveAt(0);
                    EdgeNode p = vertexList[j].FirstNode;
                    while (p != null)
                    {
                        if (vertexList[p.Index].Visited == false)
                        {
                            vertexList[p.Index].Visited = true;
                            BFSResult += vertexList[p.Index].VertexName + "\n";
                            Q.Add(p.Index);
                        }
                        p = p.Next;
                    }
                }
            }
            return BFSResult;
        }

    }
}
