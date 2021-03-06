///////////////////////////////////////////////////////////
//  AnnotationEntity.cs
//  Implementation of the Class AnnotationEntity
//  Generated by Enterprise Architect
//  Created on:      08-四月-2011 13:45:29
//  Original author: Administrator
///////////////////////////////////////////////////////////




using DIST.DGP.DataExchange.VCT.FileData;
using DIST.DGP.DataExchange.VCT.ESRIData;
using ESRI.ArcGIS.Geodatabase;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using DIST.DGP.DataExchange.VCT.Metadata;
using ESRI.ArcGIS.Display;
namespace DIST.DGP.DataExchange.VCT.ESRIData {
	/// <summary>
	/// 注记实体类
	/// </summary>
	public class AnnotationEntity : FeatureEntity {

		public AnnotationNode m_AnnotationNode;

		public AnnotationEntity(){

		}

		~AnnotationEntity(){

		}

		public override void Dispose()
        {
            base.Dispose();
		}

		/// <summary>
		/// 创建注记实体
		/// </summary>
        /// <param name="pITable">数据表对象</param>
        /// <param name="entinyNode">VCT空间实体节点</param>
        public override void CreateFeature(ITable pITable, EntityNode entinyNode)
        {
            try
            {
                AnnotationNode pAnnotationNode = entinyNode as AnnotationNode;
                if (pAnnotationNode != null)
                {
                    IFeatureClass pFeatureCls = pITable as IFeatureClass;
                    this.Feature = pFeatureCls.CreateFeature();

                    ///标识码赋值
                    int dBSMIndex = -1;
                    dBSMIndex = this.Feature.Fields.FindField(m_strEntityIDFiled);
                    if (dBSMIndex != -1)
                        this.Feature.set_Value(dBSMIndex, pAnnotationNode.EntityID);

                    ///要素代码赋值
                    int dSYDMIndex = -1;
                    dSYDMIndex = this.Feature.Fields.FindField(m_strYSDMField);
                    if (dSYDMIndex != -1)
                        this.Feature.set_Value(dSYDMIndex, pAnnotationNode.FeatureCode);

                    IAnnotationFeature pAnnotationFeature = Feature as IAnnotationFeature;
                    if (pAnnotationFeature != null)
                    {
                        /////注记内容赋值
                        ITextElement pTextElement = new TextElementClass();
                        ITextSymbol pTextSymbol = new TextSymbolClass();
                        pTextSymbol.Angle = pAnnotationNode.Angle;


                        pTextElement.Text = pAnnotationNode.Text;
                        pTextElement.Symbol = pTextSymbol;
                        pAnnotationFeature.Annotation = pTextElement as IElement;
                    }

                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(pAnnotationNode.PointLocation.X, pAnnotationNode.PointLocation.Y);

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
		/// 获取VCT注记实体节点
		/// </summary>
		public override EntityNode GetEntityNode()
        {
            try
            {
                AnnotationNode pAnnotationNode = new AnnotationNode();

                //特征类型统一设置为单点注记
                pAnnotationNode.AnnotationType = Convert.ToInt32(Metadata.MetaDataFile.GraphConfig.GetGraphSymbol("ANNOTATIONFEATURETYPE", "SingPointAnnotation"));
                IFeature pFeature = this.Feature as IFeature;
                if (pFeature != null)
                {
                    ///标识码赋值
                    int dBSMIndex = -1;
                    dBSMIndex = this.Feature.Fields.FindField(m_strEntityIDFiled);
                    if (dBSMIndex != -1)
                        pAnnotationNode.EntityID = Convert.ToInt32(this.Feature.get_Value(dBSMIndex));

                    ///要素代码赋值
                        pAnnotationNode.FeatureCode = this.FeatureCode;

                    ///图形表现赋值 
                     //pAnnotationNode.Representation = pFeature.Class.AliasName;

                    ///注记坐标赋值

                     IGeometry5 pArea = pFeature.Shape as IGeometry5;
                     if (pArea != null)
                     {
                         PointInfoNode pPTInfoNode = new PointInfoNode(pArea.CentroidEx.X, pArea.CentroidEx.Y);
                         pAnnotationNode.PointLocation = pPTInfoNode;
                     }
                    m_AnnotationNode = pAnnotationNode;
                    return pAnnotationNode;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return null;
            }
		}
 

	}//end AnnotationEntity

}//end namespace ESRIData