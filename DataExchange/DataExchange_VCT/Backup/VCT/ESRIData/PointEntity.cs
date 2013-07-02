///////////////////////////////////////////////////////////
//  PointEntity.cs
//  Implementation of the Class PointEntity
//  Generated by Enterprise Architect
//  Created on:      08-����-2011 13:45:32
//  Original author: Administrator
///////////////////////////////////////////////////////////




using DIST.DGP.DataExchange.VCT.FileData;
using DIST.DGP.DataExchange.VCT.ESRIData;
using ESRI.ArcGIS.Geodatabase;
using System;
using ESRI.ArcGIS.Geometry;
using DIST.DGP.DataExchange.VCT.Metadata;
namespace DIST.DGP.DataExchange.VCT.ESRIData {
	/// <summary>
	/// ��ʵ����
	/// </summary>
	public class PointEntity : FeatureEntity {

		public PointNode m_PointNode;
        public IFeatureClass m_pFeatureClass;
		public PointEntity()
        {

		}
        public PointEntity(EntityNode node)
        {
            this.EntityID = node.EntityID;
            this.FeatureCode = node.FeatureCode;
            this.Representation = node.Representation;
            //CreateFeature(node);
        }

		~PointEntity(){

		}

		public override void Dispose(){
            base.Dispose();

		}

		/// <summary>
		/// ������ʵ��
		/// </summary>
        /// <param name="pITable">���ݱ�����</param>
        /// <param name="entinyNode">VCT�ռ�ʵ��ڵ�</param>
        public override void CreateFeature(ITable pITable, EntityNode entinyNode)
        {
            try
            {
                PointNode pPointNode = entinyNode as PointNode;
                if (pPointNode != null)
                {
                    IFeatureClass pFeatureCls = pITable as IFeatureClass;
                    this.Feature = pFeatureCls.CreateFeature();

                    ///��ʶ�븳ֵ
                    int dBSMIndex = -1;
                    dBSMIndex = this.Feature.Fields.FindField(m_strEntityIDFiled);
                    if (dBSMIndex != -1)
                        this.Feature.set_Value(dBSMIndex, pPointNode.EntityID);

                    ///Ҫ�ش��븳ֵ
                    int dSYDMIndex = -1;
                    dSYDMIndex = this.Feature.Fields.FindField(m_strYSDMField);
                    if (dSYDMIndex != -1)
                        this.Feature.set_Value(dSYDMIndex, pPointNode.FeatureCode);

                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(pPointNode.PointInfoNode.X, pPointNode.PointInfoNode.Y);

                    (this.Feature as IFeature).Shape = pPoint;

                    this.Feature.Store();
                }
            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
            }
        }

		/// <summary>
		/// ��ȡVCT��ʵ��ڵ�
		/// </summary>
		public override EntityNode GetEntityNode()
        {
            try
            {
                m_PointNode = new PointNode();
                IFeature pFeature = this.Feature as IFeature;
                ///��ʶ�븳ֵ
                int dBSMIndex = -1;
                dBSMIndex = this.Feature.Fields.FindField(m_strEntityIDFiled);
                if (dBSMIndex != -1)
                   m_PointNode.EntityID=Convert.ToInt32( this.Feature.get_Value(dBSMIndex));

                ///Ҫ�ش��븳ֵ
                //int dSYDMIndex = -1;
                //dSYDMIndex = this.Feature.Fields.FindField("YSDM");
                //if (dSYDMIndex != -1)
                //    m_PointNode.FeatureCode = this.Feature.get_Value(dSYDMIndex).ToString();
                //string sAttriTableName = (pFeature.Class as IDataset).Name;
                //m_PointNode.FeatureCode = MetaDataFile.GetFeatureCodeByName(sAttriTableName);
                m_PointNode.FeatureCode = this.FeatureCode;
                ///�����������ͺ�ͼ�α��ֱ��븳ֵ
                m_PointNode.PointCount = 1;

                //ͨ���������õ����ͣ��ֱ�׼ͳһ����Ϊ�����㣩
                m_PointNode.PointType =Convert.ToInt32( Metadata.MetaDataFile.GraphConfig.GetGraphSymbol("POINTFEATURETYPE", "SinglePoint"));
                //m_PointNode.Representation = pFeature.Class.AliasName;

                //��ȡ�ռ���������ֵ
                IPoint pPoint = pFeature.Shape as IPoint;
                PointInfoNode pInfoNode =new PointInfoNode(pPoint.X,pPoint.Y);
                m_PointNode.PointInfoNode = pInfoNode;

                return m_PointNode;
            }
            catch(Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return null;
            }
		}
      
 

	}//end PointEntity

}//end namespace ESRIData