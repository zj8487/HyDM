///////////////////////////////////////////////////////////
//  FeatureEntity.cs
//  Implementation of the Class FeatureEntity
//  Generated by Enterprise Architect
//  Created on:      08-四月-2011 13:45:30
//  Original author: Administrator
///////////////////////////////////////////////////////////




using DIST.DGP.DataExchange.VCT.FileData;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
namespace DIST.DGP.DataExchange.VCT.ESRIData
{
    /// <summary>
    /// 要素实体类
    /// </summary>
    public class FeatureEntity : IComparable
    {

        /// <summary>
        /// 实体对象标识码
        /// </summary>
        private int m_nEntityID;
        public int EntityID
        {
            get
            {
                return m_nEntityID;
            }
            set
            {
                m_nEntityID = value;
            }
        }
        /// <summary>
        /// ESRI要素对象
        /// </summary>
        private IRow m_pIFeature;
        public IRow Feature
        {
            get
            {
                return m_pIFeature;
            }
            set
            {
                m_pIFeature = value;
            }
        }
        /// <summary>
        /// 要素代码
        /// </summary>
        private string m_strFeatureCode;
        public string FeatureCode
        {
            get
            {
                return m_strFeatureCode;
            }
            set
            {
                m_strFeatureCode = value;
            }
        }
        /// <summary>
        /// 图形表现编码
        /// </summary>
        private string m_strRepresentation;
        public string Representation
        {
            get
            {
                return m_strRepresentation;
            }
            set
            {
                m_strRepresentation = value;
            }
        }
        public RecordNode m_RecordNode;

        /// <summary>
        /// 标识码字段名称
        /// </summary>
        protected string m_strEntityIDFiled = "";

        /// <summary>
        /// 要素代码字段名称
        /// </summary>
        protected string m_strYSDMField = "";
        public FeatureEntity()
        {
          
        }

        ~FeatureEntity()
        {

        }
        protected IGeometry m_CutGeometry = null;
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

        protected bool m_bCut=false;
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
        public IGeometry GetSubGeometry()
        {
            try
            {
                if (m_pIFeature == null)
                    return null;
                if (m_CutGeometry == null)
                    return (m_pIFeature as IFeature).Shape;
                IGeometry pFeatureShape = (m_pIFeature as IFeature).Shape;
                ITopologicalOperator pTop = m_CutGeometry as ITopologicalOperator;
                //if (pFeatureShape.GeometryType == esriGeometryType.esriGeometryLine||pFeatureShape.GeometryType == esriGeometryType.esriGeometryPolyline)
                //    return pTop.Intersect(pFeatureShape, esriGeometryDimension.esriGeometry0Dimension);
                //else if (pFeatureShape.GeometryType == esriGeometryType.esriGeometryPolygon)
                //{
                //    return pTop.Intersect(pFeatureShape, esriGeometryDimension.esriGeometry2Dimension);
                //}
                return pTop.Intersect(pFeatureShape, pFeatureShape.Dimension);
            }
            catch(Exception ex)
            {
                Logger.WriteLog("裁剪要素【"+(m_pIFeature.Table as IDataset).BrowseName+"】时出错!\r\n");
                return null;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            if (m_pIFeature != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pIFeature);
            m_pIFeature = null;
            GC.Collect();
        }

        /// <summary>
        /// 赋予属性
        /// </summary>
        /// <param name="recordNode">属性信息</param>
        public void AttachAttribute(RecordNode recordNode, TableStructureNode pTableStructNode)
        {
            try
            {
                if (m_pIFeature != null)
                {
                    if (m_pIFeature is IAnnotationFeature)
                    { 
                        ///添加注记数据
                    }
                    else
                    {
                        for (int i = 0; i < pTableStructNode.FieldNodes.Count; i++)
                        {
                            FieldNode pFieldNode = pTableStructNode.FieldNodes[i];
                            string sValue = recordNode.FieldValues[i];
                            m_pIFeature.set_Value(pFieldNode.FieldIndex, sValue);
                        }
                        m_pIFeature.Store();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
        }

        /// <summary>
        /// 创建要素
        /// </summary>
        /// <param name="pITable">数据表对象</param>
        /// <param name="entinyNode">VCT空间实体节点</param>
        public virtual void CreateFeature(ITable pITable, EntityNode entinyNode)
        {

        }

        /// <summary>
        /// 获取VCT空间实体节点
        /// </summary>
        public virtual EntityNode GetEntityNode()
        {

            return null;
        }

        public void SetFeatureKeyFieldName(string sEntityName,string sSYDMField)
        {
            m_strEntityIDFiled = sEntityName;
            m_strYSDMField = sSYDMField;
        }

        /// <summary>
        /// 获取VCT属性记录节点
        /// </summary>
        public RecordNode GetRecordNode(TableStructureNode pTableStructNode)
        {
            try
            {
                RecordNode pRecordNode = new RecordNode();
                /////标识码赋值
                //int dBSMIndex = -1;
                //dBSMIndex = this.Feature.Fields.FindField("BSM");
                //if (dBSMIndex != -1)
                //    pRecordNode.EntityID = Convert.ToInt32(this.Feature.get_Value(dBSMIndex));
                List<string> sListValues = new List<string>();
              
                for (int i = 0; i < pTableStructNode.FieldNodes.Count; i++)
                {
                    //IField pField = this.Feature.Fields.get_Field(i);
                    //string sValue = this.Feature.get_Value(i).ToString();
                    /////只保存属性数据
                    //if (pField.Type != esriFieldType.esriFieldTypeOID
                    //    && pField.Type != esriFieldType.esriFieldTypeGeometry
                    //    && pField.Name.ToUpper() != "SHAPE_LENGTH"
                    //    && pField.Name.ToUpper() != "SHAPE_AREA")
                    //{]
                    
                    FieldNode pFieldNode = pTableStructNode.FieldNodes[i];
                    if (pFieldNode.FieldName == m_strEntityIDFiled)
                    {
                        sListValues.Insert(0, this.Feature.get_Value(pFieldNode.FieldIndex).ToString());
                        pRecordNode.EntityID =Convert.ToInt32( this.Feature.get_Value(pFieldNode.FieldIndex));
                    }
                    else
                    sListValues.Add(this.Feature.get_Value(pFieldNode.FieldIndex).ToString());
                    //}
                }
                pRecordNode.FieldValues = sListValues;
                return pRecordNode;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return null;
            }

        }


        /// <summary>
        /// 操作符“>”
        /// </summary>
        /// <param name="xFeatureEntity">要素实体类</param>
        /// <param name="yFeatureEntity">要素实体类</param>
        public static bool operator >(FeatureEntity xFeatureEntity, FeatureEntity yFeatureEntity)
        {
            if (object.Equals(xFeatureEntity, null))
            {
                return false;
            }
            else
            {
                if (object.Equals(yFeatureEntity, null))
                    return true;
            }
            return xFeatureEntity.EntityID > yFeatureEntity.EntityID ? true : false;
        }

        /// <summary>
        /// 操作符“==”
        /// </summary>
        /// <param name="xFeatureEntity">要素实体类</param>
        /// <param name="yFeatureEntity">要素实体类</param>
        public static bool operator ==(FeatureEntity xFeatureEntity, FeatureEntity yFeatureEntity)
        {
            if (object.Equals(xFeatureEntity, null))
            {
                if (object.Equals(yFeatureEntity, null))
                    return true;
                else
                    return false;
            }
            else
            {
                if (object.Equals(yFeatureEntity, null))
                    return false;
            }
            return xFeatureEntity.EntityID == yFeatureEntity.EntityID ? true : false;
        }

        /// <summary>
        /// 操作符“!=”
        /// </summary>
        /// <param name="xFeatureEntity">要素实体类</param>
        /// <param name="yFeatureEntity">要素实体类</param>
        public static bool operator !=(FeatureEntity xFeatureEntity, FeatureEntity yFeatureEntity)
        {
            if (object.Equals(xFeatureEntity, null))
            {
                if (object.Equals(yFeatureEntity, null))
                    return false;
                else
                    return true;
            }
            else
            {
                if (object.Equals(yFeatureEntity, null))
                    return true;

            }
            return xFeatureEntity.EntityID != yFeatureEntity.EntityID ? true : false;
        }

        /// <summary>
        /// 操作符“<”
        /// </summary>
        /// <param name="xFeatureEntity">要素实体类</param>
        /// <param name="yFeatureEntity">要素实体类</param>
        public static bool operator <(FeatureEntity xFeatureEntity, FeatureEntity yFeatureEntity)
        {
            if (object.Equals(xFeatureEntity, null))
            {
                return true;
            }
            else
            {
                if (object.Equals(yFeatureEntity, null))
                    return false;
            }
            return xFeatureEntity.EntityID < yFeatureEntity.EntityID ? true : false;
        }

      
        public int CompareTo(object obj)
        {
            FeatureEntity objFeatureEntity = obj as FeatureEntity;
            if (this == objFeatureEntity)
                return 0;
            else if (this > objFeatureEntity)
                return 1;

            return -1;
        }
    }//end FeatureEntity

}//end namespace ESRIData