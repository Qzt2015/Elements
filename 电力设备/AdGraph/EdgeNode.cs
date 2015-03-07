using System;
using System.Collections.Generic;
using System.Text;

namespace NonLinearStruct
{
    [Serializable]
    /// <summary>
    /// 邻接表边表上的结点
    /// </summary>
    public class EdgeNode
    {
        private int index;
        private double weight;
        private EdgeNode next;
        /// <summary>
        /// 获取边终点在顶点数组中的位置
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }
        }
        /// <summary>
        /// 获取边上的权值
        /// </summary>
        public double Weight
        {
            get
            {
                return weight;
            }
        }
        /// <summary>
        /// 获取或设置下一个邻接点
        /// </summary>
        public EdgeNode Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }
        /// <summary>
        /// 初始化EdgeNode类的新实例
        /// </summary>
        /// <param name="index">边终点在顶点数组中的位置</param>
        /// <param name="Weight">边上的权值</param>
        public EdgeNode(int index, double Weight)
        {
            if (index < 0)
                throw new Exception("索引位置无效.");

            this.index = index;
            this.weight = Weight;
            this.next = null; 
        }
        /// <summary>
        /// 初始化EdgeNode类的新实例
        /// </summary>
        /// <param name="index">边终点在顶点数组中的位置</param>
        /// <param name="weight">边上的权值</param>
        /// <param name="next">下一个邻接点</param>
        public EdgeNode(int index, double weight, EdgeNode next)
        {
            if (index < 0)
                throw new Exception("索引位置无效.");

            this.index = index;
            this.weight = weight;
            this.next = next;
        }
    }
}
