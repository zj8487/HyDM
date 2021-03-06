///////////////////////////////////////////////////////////
//  FeatureLayer.cs
//  Implementation of the Class FeatureLayer
//  Generated by Enterprise Architect
//  Created on:      08-四月-2011 13:45:31
//  Original author: Administrator
///////////////////////////////////////////////////////////




using DIST.DGP.DataExchange.VCT.ESRIData;
using DIST.DGP.DataExchange.VCT.FileData;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using DIST.DGP.DataExchange.VCT.Metadata;
using ESRI.ArcGIS.Geometry;
namespace DIST.DGP.DataExchange.VCT.ESRIData
{
    /// <summary>
    /// 数据表类
    /// </summary>
    public class FeatureLayer : TableLayer
    {
        public FeatureLayer()
        {
        }

        ~FeatureLayer()
        {

        }

        protected IGeometry m_CutGeometry=null;

        protected List<FeatureEntity> m_FeatureEntys;

        /// <summary>
        /// 要素集合
        /// </summary>
        public List<FeatureEntity> FeatureEntys
        {
            get
            {
                if (m_FeatureEntys == null || m_FeatureEntys.Count == 0)
                    m_FeatureEntys = GetFeatureEntitys();
                return m_FeatureEntys;
            }
        }
        /// <summary>
        /// 裁切范围
        /// </summary>
        public IGeometry CutGeometry
        {
            get
            {
                return m_CutGeometry;
            }
            set
            {
                m_CutGeometry = value;
            }
        }

        protected bool m_bCut = false;
        /// <summary>
        /// 是否裁剪
        /// </summary>
        public bool IsCut
        {
            get
            {
                return m_bCut;
            }
            set
            {
                m_bCut = value;
            }
        }
        public virtual void Dispose()
        {
            if (FeatureEntys != null)
            {
                for (int i = 0; i < FeatureEntys.Count; i++)
                {
                    FeatureEntys[i].Dispose();
                }
            }
            base.Disposing();
        }

        /// <summary>
        /// 创建空间实体
        /// </summary>
        /// <param name="entinyNode">VCT空间实体节点</param>
        public virtual FeatureEntity CreateFeatureEntity(EntityNode entinyNode)
        {

            return null;
        }

        /// <summary>
        /// 创建
        /// </summary>
        public override bool Create(TableStructureNode tableStructureNode)
        {
            base.Create(tableStructureNode);

            return true;
        }

        /// <summary>
        /// 获取要素编码节点
        /// </summary>
        public override FeatureCodeNode GetFeatureCodeNode()
        {
            try
            {
                return base.GetFeatureCodeNode();
            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        public virtual List<FeatureEntity> GetFeatureEntitys()
        {
            return null;
        }

        /// <summary>
        /// 验证指定的要素是否在裁切范围内
        /// </summary>
        /// <param name="pFeatureGeometry"></param>
        /// <returns></returns>
        public virtual bool FeatureInCutRegion(IGeometry pFeatureGeometry)
        {
            try
            {
                if (pFeatureGeometry == null)
                    return false;
                IRelationalOperator pRelation = m_CutGeometry as IRelationalOperator;
                if (pRelation.Crosses(pFeatureGeometry) || pRelation.Contains(pFeatureGeometry)
                    || pRelation.Touches(pFeatureGeometry)|| pRelation.Overlaps(pFeatureGeometry)
                    || pRelation.Within(pFeatureGeometry) || pRelation.Equals(pFeatureGeometry))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                LogAPI.WriteLog("判断裁切范围时出错，系统将不对【" + StructureNode.TableName + "】进行裁切!\r\n");
                return false;
            }
        }
        ///// <summary>
        ///// 通过对象标识码获取实体
        ///// </summary>
        ///// <param name="nEntityID">实体对象标识码</param>
        //public FeatureEntity GetFeatureEntityByID(int nEntityID)
        //{

        //    return null;
        //}

        ///// <summary>
        ///// 通过索引获取实体
        ///// </summary>
        ///// <param name="nIndex">实体索引</param>
        //public FeatureEntity GetFeatureEntityByIndex(int nIndex)
        //{

        //    return null;
        //}


    }//end FeatureLayer

}//end namespace ESRIData