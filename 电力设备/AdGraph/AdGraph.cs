using System;
using System.Collections.Generic;
using System.Text;

namespace NonLinearStruct
{
    [Serializable]
    /// <summary>
    /// 图抽象数据类型实现--利用邻接表
    /// </summary>
    public class AdGraph
    {
        public List<VertexNode> vertexList;//结点表
        private int vertexCount;

        /// <summary>
        /// 获取图的结点数
        /// </summary>
        public int VertexCount
        {
            get
            {
                return vertexCount;
            }
        }
        /// <summary>
        /// 初始化AdGraph类的新实例
        /// </summary>
        /// <param name="vCount">图中结点的个数</param>
        public AdGraph()
        {
            vertexList = new List<VertexNode>();
        }
        /// <summary>
        /// 获取或设置图中各结点的名称
        /// </summary>
        /// <param name="index">结点名称从零开始的索引</param>
        /// <returns>指定索引处结点的名称</returns>
        public string this[int index]
        {
            get
            {
                if (index < 0 || index > vertexCount - 1)
                    throw new Exception("索引位置无效");
                return vertexList[index] == null ? "NULL" : vertexList[index].VertexName;
            }
            set
            {
                if (index < 0 || index > vertexCount)
                    throw new Exception("索引位置无效");
                if (index == vertexCount)
                    vertexList.Add(new VertexNode(value));
                else
                    vertexList[index].VertexName = value;

                vertexCount = vertexList.Count;
            }
        }
        private int GetIndex(string VertexName)
        {
            //得到结点在结点表中的位置
            int i;
            for (i = 0; i < vertexCount; i++)
            {
                if (vertexList[i] != null && vertexList[i].VertexName == VertexName)
                    break;
            }
            return i == vertexCount ? -1 : i;
        }
        /// <summary>
        /// 给图加边
        /// </summary>
        /// <param name="StartVertexName">起始结点的名字</param>
        /// <param name="EndVertexName">终止结点的名字</param>
        /// <param name="Weight">边上的权值</param>
        public void AddEdge(string StartVertexName, string EndVertexName, double Weight)
        {
            int i = GetIndex(StartVertexName);
            int j = GetIndex(EndVertexName);

            if (i == -1 || j == -1)
                throw new Exception("图中不存在该边.");

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
        /// 删边
        /// </summary>
        /// <param name="StartVertexName">起始结点的名字</param>
        /// <param name="EndVertexName">终止结点的名字</param>
        public void DeleteEdge(string StartVertexName, string EndVertexName)
        {
            int i = GetIndex(StartVertexName);
            int j = GetIndex(EndVertexName);

            if (i == -1 || j == -1)
                throw new Exception("图中不存在该边.");

            EdgeNode temp = vertexList[i].FirstNode;
            if (temp == null)
            {
                throw new Exception("图中不存在该边.");
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
            //深度优先搜索递归函数
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
        /// 得到深度优先搜索序列
        /// </summary>
        /// <param name="StartVertexName">进行深度优先搜索的起始点名称</param>
        /// <returns>深度优先搜索序列</returns>
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
        /// 得到广度优先搜索序列
        /// </summary>
        /// <param name="startNodeName">进行广度优先搜索的起始点名称</param>
        /// <returns>广度优先搜索序列</returns>
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
