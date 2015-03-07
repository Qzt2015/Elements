using System;
using System.Collections.Generic;
using System.Text;

namespace NonLinearStruct
{
    [Serializable]
    /// <summary>
    /// 邻接表顶点表中的结点
    /// </summary>
    public class VertexNode
    {
        private string vertexName;
        private bool visited;
        private EdgeNode firstNode;
        /// <summary>
        /// 获取或设置顶点的名字
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
        /// 获取或设置顶点是否被访问
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
        /// 获取或设置顶点的第一个邻接点
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
        /// 初始化VertexNode类的新实例
        /// </summary>
        /// <param name="vName">顶点的名字</param>
        public VertexNode(string vName)//构造函数
        {
            this.vertexName = vName;
            this.visited = false;
            this.firstNode = null;
        }
        /// <summary>
        /// 初始化VertexNode类的新实例
        /// </summary>
        /// <param name="vName">顶点的名字</param>
        /// <param name="firstNode">顶点的第一个邻接点</param>
        public VertexNode(string vName, EdgeNode firstNode)
        {
            this.vertexName = vName;
            this.visited = false;
            this.firstNode = firstNode;
        }
    }
}
