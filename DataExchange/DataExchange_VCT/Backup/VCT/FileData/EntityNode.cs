///////////////////////////////////////////////////////////
//  EntityNode.cs
//  Implementation of the Class EntityNode
//  Generated by Enterprise Architect
//  Created on:      08-四月-2011 13:45:30
//  Original author: Administrator
///////////////////////////////////////////////////////////


using System.Collections.Generic;
using System;

namespace DIST.DGP.DataExchange.VCT.FileData {
	/// <summary>
	/// VCT实体节点类
	/// </summary>
    [Serializable]
	public class EntityNode {

        /// <summary>
        /// 在集合中的唯一标识号
        /// </summary>
        public int IndexID;
        //{
        //    get
        //    {
        //        return m_nIndex;
        //    }
        //    set
        //    {
        //        m_nIndex = value;
        //    }
        //}
        //private int m_nIndex;

		/// <summary>
		/// 标识码
		/// </summary>
        public int EntityID;
        //{
        //    get
        //    {
        //        return m_nEntityID;
        //    }
        //    set
        //    {
        //        m_nEntityID = value;
        //    }
        //}
        //private int m_nEntityID;

		/// <summary>
		/// 要素代码
		/// </summary>
        public string FeatureCode;
        //{
        //    get
        //    {
        //        return m_strFeatureCode;
        //    }
        //    set
        //    {
        //        m_strFeatureCode = value;
        //    }
        //}
        //private string m_strFeatureCode;


		/// <summary>
		/// 图形表现编码
		/// </summary>
        public string Representation;
        //{
        //    get
        //    {
        //        return m_strRepresentation;
        //    }
        //    set
        //    {
        //        m_strRepresentation = value;
        //    }
        //}
        //private string m_strRepresentation;

		public EntityNode()
        {
            //m_nEntityID = -1;
            //m_strFeatureCode = "";
            //m_strRepresentation = "";
            EntityID = -1;
            FeatureCode = "";
            Representation = "Unknown";
        }

        //~EntityNode(){

        //}

        //public virtual void Dispose(){

        //}

        /// <summary>
        /// 输出VCT节点
        /// </summary>
        public override string ToString()
        {
            string strNode = this.EntityID.ToString();
            strNode += "\r\n" + this.FeatureCode;
            strNode += "\r\n" + this.Representation;
            return strNode;
        }
	}//end EntityNode

    /// <summary>
    /// 实体节点集合类
    /// </summary>
    public class EntityNodes : List<EntityNode>
    {
        /// <summary>
        /// 实体标识吗码与索引之间的映射
        /// </summary>
        Dictionary<int, int> arrEntityID = new Dictionary<int, int>();
        //private int[] arrEntityID = new int[100000000];//2147483647

        public EntityNodes()
        {
            Init();
        }

        /// <summary>
        /// 初始化索引
        /// </summary>
        private void Init()
        {
            arrEntityID.Clear();
            //for (int i = 0; i < arrEntityID.Length; i++)
            //{
            //    arrEntityID[i] = -1;
            //}
        }

        /// <summary>
        /// 添加实体节点
        /// </summary>
        public new void Add(EntityNode item)
        {
            base.Add(item);
            //arrEntityID[item.EntityID] = this.Count - 1;
            arrEntityID[item.EntityID] = this.Count - 1;
        }

        ///// <summary>
        ///// 添加实体节点集合
        ///// </summary>
        //public new void AddRange(IEnumerable<EntityNode> collection)
        //{
        //    IEnumerator<EntityNode> pIEnumerable = null;

        //    while ((pIEnumerable = collection.GetEnumerator()) != null)
        //    {
        //        this.Add(pIEnumerable.Current);
        //    }
        //}

        /// <summary>
        /// 根据实体标识码获取实体节点
        /// </summary>
        public EntityNode GetItemByEntityID(int nEntityID)
        {
            int nIndex = arrEntityID[nEntityID];
            if (nIndex != -1)
                return this[nIndex];
            return null;
        }

        /// <summary>
        /// 重载排序
        /// </summary>
        public new void Sort()
        {
            //先排序
            base.Sort();
            //再清除原有索引
            Init();
            //最后重建索引
            for (int i = 0; i < this.Count; i++)
            {
                arrEntityID[this[i].EntityID] = i;
            }
        }
    }//end EntityNodes
}//end namespace FileData