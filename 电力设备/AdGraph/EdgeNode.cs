using System;
using System.Collections.Generic;
using System.Text;

namespace NonLinearStruct
{
    [Serializable]
    /// <summary>
    /// �ڽӱ�߱��ϵĽ��
    /// </summary>
    public class EdgeNode
    {
        private int index;
        private double weight;
        private EdgeNode next;
        /// <summary>
        /// ��ȡ���յ��ڶ��������е�λ��
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }
        }
        /// <summary>
        /// ��ȡ���ϵ�Ȩֵ
        /// </summary>
        public double Weight
        {
            get
            {
                return weight;
            }
        }
        /// <summary>
        /// ��ȡ��������һ���ڽӵ�
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
        /// ��ʼ��EdgeNode�����ʵ��
        /// </summary>
        /// <param name="index">���յ��ڶ��������е�λ��</param>
        /// <param name="Weight">���ϵ�Ȩֵ</param>
        public EdgeNode(int index, double Weight)
        {
            if (index < 0)
                throw new Exception("����λ����Ч.");

            this.index = index;
            this.weight = Weight;
            this.next = null; 
        }
        /// <summary>
        /// ��ʼ��EdgeNode�����ʵ��
        /// </summary>
        /// <param name="index">���յ��ڶ��������е�λ��</param>
        /// <param name="weight">���ϵ�Ȩֵ</param>
        /// <param name="next">��һ���ڽӵ�</param>
        public EdgeNode(int index, double weight, EdgeNode next)
        {
            if (index < 0)
                throw new Exception("����λ����Ч.");

            this.index = index;
            this.weight = weight;
            this.next = next;
        }
    }
}
