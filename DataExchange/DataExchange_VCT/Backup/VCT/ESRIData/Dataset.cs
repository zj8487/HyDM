///////////////////////////////////////////////////////////
//  Dataset.cs
//  Implementation of the Class Dataset
//  Generated by Enterprise Architect
//  Created on:      08-����-2011 13:45:30
//  Original author: Administrator
///////////////////////////////////////////////////////////




using DIST.DGP.DataExchange.VCT.FileData;
using DIST.DGP.DataExchange.VCT.ESRIData;
using ESRI.ArcGIS.Geodatabase;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using DIST.DGP.DataExchange.VCT.Metadata;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Collections;
namespace DIST.DGP.DataExchange.VCT.ESRIData {
	/// <summary>
	/// ���ݼ���
	/// </summary>
	public class Dataset
    {
        #region ����
       
        /// <summary>
        /// ESRI���ݼ�
        /// </summary>
        public IDataset ESRIDataset
        {
            get
            {
                return m_pIDataset;
            }
            set
            {
                m_pIDataset = value;
            }
        }
        private IDataset m_pIDataset;

       
        /// <summary>
        /// �ռ�ο�
        /// </summary>
        public Projection Projection
        {
            get
            {
                return m_Projection;
            }
            set
            {
                m_Projection = value;
            }
        }
        private Projection m_Projection;


        /// <summary>
        /// MDB�������ӹ����ռ�
        /// </summary>
        public IWorkspace Workspace
        {
            get
            {
                return m_pIWorkspace;
            }
            set
            {
                m_pIWorkspace = value;
            }
        }
        private IWorkspace m_pIWorkspace = null;

        private bool m_bCut = false;
        private IGeometry m_cutGeometry=null;
        #endregion

        /// <summary>
        /// Ҫ�ض����б�
        /// </summary>
        private List<TableLayer> m_FeatureList;

        public Dataset(IDataset pIDataset,IWorkspace pWorkspace,bool bCut,IGeometry pCutGeomtry)
        {
            m_pIDataset = pIDataset;
            m_pIWorkspace = pWorkspace;
            m_cutGeometry = pCutGeomtry;
            m_bCut = bCut;
            InitialProjection();
            InitialFeatureList();
		}

        /// <summary>
        /// ��mdb������vctʱ����ʼ���ռ�ο�
        /// </summary>
        /// <returns></returns>
        private bool InitialProjection()
        {
            if (m_pIDataset == null)
                return false;
            try
            {
                IGeoDataset geoDataset = (IGeoDataset)m_pIDataset;
                ISpatialReference pSpatialReference = geoDataset.SpatialReference;

                m_Projection = new Projection(pSpatialReference, this.m_pIDataset.Workspace);

                return true;
            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return false;
            }
        }

        /// <summary>
        /// ��mdb��vctת��ʱ����ʼ��ͼ���б�
        /// </summary>
        private bool InitialFeatureList()
        {
            try
            {
                if (m_pIDataset == null)
                    return false;
                m_FeatureList = new List<TableLayer>();
                IEnumDataset pEnumDataset = m_pIDataset.Subsets;
                IDataset pSet = pEnumDataset.Next();
                while (pSet != null)
                {
                    FeatureLayer pFeatureLayer = null;
                    IFeatureClass pFeatureCls = pSet as IFeatureClass;


                    string sGeometryType = "";
                    ///�������ļ���ȡҪ������
                    Metadata.MetaTable pMetaTable = Metadata.MetaDataFile.MetaTabls[pSet.Name] as Metadata.MetaTable;
                    if (pMetaTable != null)
                    {
                        sGeometryType = pMetaTable.Type;
                    }
                    else
                    {
                        ///��������ڱ�׼�������򲻴���
                        pSet = pEnumDataset.Next();
                        continue;
                    }
                    ///����Ҫ�����ʹ���vct�ռ����ݽڵ�
                    esriGeometryType pFeatureType = pFeatureCls.ShapeType;
                    if (pFeatureType == esriGeometryType.esriGeometryLine
                        || pFeatureType == esriGeometryType.esriGeometryPolyline)
                    {
                        ///�����߽ڵ�
                        pFeatureLayer = new LineLayer();
                        if (sGeometryType == "")
                            sGeometryType = "Line";
                    }
                    else if (pFeatureType == esriGeometryType.esriGeometryPolygon)
                    {
                        //������ڵ�
                        pFeatureLayer = new PolygonLayer();
                          if (sGeometryType == "")
                            sGeometryType = "Polygon"; 
                    }
                    else if (pFeatureType == esriGeometryType.esriGeometryPoint)
                    {
                        pFeatureLayer = new PointLayer();
                        if (sGeometryType == "")
                            sGeometryType = "Point"; 
                    }
                   
                    ////ע��ͼ��
                    if (pFeatureCls.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        pFeatureLayer = new AnnotationLayer();
                        pFeatureLayer.GeometryType = "Annotation";
                    }

                    pFeatureLayer.CutGeometry =m_cutGeometry;
                    //�ҽӿռ����ݱ�
                    pFeatureLayer.Table = pSet as ITable;
                    pFeatureLayer.GeometryType = sGeometryType;
                    pFeatureLayer.IsCut = m_bCut;
                    pFeatureLayer.FeatureCode = MetaDataFile.GetFeatureCodeByName(pSet.Name);
                    pFeatureLayer.UpdateFieldIndex();

                    m_FeatureList.Add(pFeatureLayer);
                    pSet = pEnumDataset.Next();
                }

                ///�������Ա�����
                IEnumDataset pAttriTalbelDataSet=  m_pIWorkspace.get_Datasets(esriDatasetType.esriDTTable);
                IDataset pAttriDataset = pAttriTalbelDataSet.Next();
                while (pAttriDataset != null)
                 {
                        TableLayer pAttributeTalbe = new TableLayer();

                        ///�������ļ���ȡҪ������
                        Metadata.MetaTable pMetaTable = Metadata.MetaDataFile.MetaTabls[pAttriDataset.Name] as Metadata.MetaTable;
                        if (pMetaTable != null)
                        {
                            pAttributeTalbe.GeometryType = pMetaTable.Type;
                            pAttributeTalbe.Table = pAttriDataset as ITable;
                            pAttributeTalbe.UpdateFieldIndex();
                            m_FeatureList.Add(pAttributeTalbe);
                        }
                     pAttriDataset = pAttriTalbelDataSet.Next();
                 }
                return true;
            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return false;
            }
        }

        public Dataset(IWorkspace pIWorkspace)
        {
            m_pIWorkspace = pIWorkspace;
        }

        /// <summary>
        /// �������пռ�ͼ��
        /// </summary>
        /// <param name="tableStructureNodes">VCT���ṹ�ڵ㼯��</param>
        /// <param name="featureCodeNodes">VCTҪ�ر���ڵ㼯��</param>
        public bool CreateFeatureLayers(List<TableStructureNode> tableStructureNodes, Hashtable featureCodeNodes)
        {
            //if (this.m_pIDataset != null)
            //{
                FeatureLayerFactory m_FeatureLayerFactory = new FeatureLayerFactory(this.m_pIDataset,m_pIWorkspace);
                m_FeatureList = m_FeatureLayerFactory.CreateFeatureLayers(tableStructureNodes, featureCodeNodes);
                return true;
            //}
            //return false;
        }


		~Dataset(){

		}

        /// <summary>
        /// �ͷ�esri����
        /// </summary>
		public virtual void Dispose()
        {
            if (m_FeatureList != null)
            {
                for (int i = 0; i < m_FeatureList.Count; i++)
                {
                    if (m_FeatureList[i] is FeatureLayer)
                    {
                        (m_FeatureList[i] as FeatureLayer).Dispose();
                    }
                }
                m_FeatureList.Clear();
                m_FeatureList = null;
            }
            m_Projection = null;
            if(m_pIDataset!=null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pIDataset);
            if(m_pIWorkspace!=null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pIWorkspace);
            GC.Collect();
		}

 

		/// <summary>
		/// ͨ��Ҫ�ش����ȡҪ��ͼ��
		/// </summary>
		/// <param name="strFeatureCode">Ҫ�ش���</param>
        public TableLayer GetFeatureLayerByCode(string strFeatureCode)
        {
            if (m_FeatureList == null)
                return null;
            foreach (TableLayer pFLayer in m_FeatureList)
            {
                if (pFLayer.GetFeatureCodeNode().FeatureCode == strFeatureCode)
                    return pFLayer;
            }
			return null;
		}

		/// <summary>
		/// ͨ��������ȡҪ��ͼ��
		/// </summary>
		/// <param name="nIndex">ͼ������</param>
		public TableLayer GetFeatureLayerByIndex(int nIndex)
        {
            if (m_FeatureList == null||nIndex>m_FeatureList.Count-1||nIndex<0)
                return null;

            return m_FeatureList[nIndex];
		}

		/// <summary>
		/// ͨ�����ݱ����ƻ�ȡҪ��ͼ��
		/// </summary>
		/// <param name="strTableName">���ݱ�����</param>
        public TableLayer GetFeatureLayerByName(string strTableName)
        {
            if (m_FeatureList == null)
                return null;
            foreach (TableLayer item in m_FeatureList)
            {
                if ((item.Table as IDataset).Name == strTableName)
                    return item;
            }
			return null;
		}

		/// <summary>
		/// ��ȡͼ����
		/// </summary>
		public int GetLayerCount()
        {
            //if (m_pIDataset == null)
            //    return 0;
            //else
            //{
            //    ///��ȡ�ռ����ݼ���
            //    IEnumDataset pDataSet = m_pIDataset.Subsets;
            //    IDataset set = pDataSet.Next();
            //    int count = 0;
            //    while (set!=null)
            //    {
            //        ///��������ǰ��׼�µ�����
            //        Metadata.MetaTable pMetaTable = Metadata.MetaDataFile.GetMetaTalbleByName(set.Name) as Metadata.MetaTable;
            //         if (pMetaTable != null)
            //         {
            //             IFeatureClass pFeatureClass = set as IFeatureClass;
            //             if (pFeatureClass != null && (pFeatureClass.FeatureType == esriFeatureType.esriFTAnnotation
            //                 || pFeatureClass.FeatureType == esriFeatureType.esriFTSimple))
            //                 count++;
            //         }
            //        set = pDataSet.Next();
            //    }
            //    ///��ȡ�������ݼ���
            //    IEnumDataset pAttriTalbelDataSet=  m_pIWorkspace.get_Datasets(esriDatasetType.esriDTTable);
            //    IDataset pAttriDataset = pAttriTalbelDataSet.Next();
            //    while (pAttriDataset != null)
            //    {
            //        ///��������ǰ��׼�µ�����
            //        Metadata.MetaTable pMetaTable = Metadata.MetaDataFile.GetMetaTalbleByName(pAttriDataset.Name) as Metadata.MetaTable;
            //        if (pMetaTable != null)
            //        {
            //            count++;
            //        }
            //        pAttriDataset = pAttriTalbelDataSet.Next();
            //    }
            //    return count;
            //}
            try
            {
                return m_FeatureList.Count;
            }
            catch(Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return 0;
            }
		}

		/// <summary>
		/// ��ȡ�ռ�ο�
		/// </summary>
		public Projection GetProjection()
        {
            if (m_Projection.projection == null)
                InitialProjection();
			return m_Projection;
		}

        /// <summary>
        /// ��ȡҪ�ؼ��е�����ʶ��
        /// </summary>
        /// <returns></returns>
        public int GetMaxEntityID()
        {
            try
            {
                int nMaxEntityID = -1;///��¼����ʶ��
                IEnumDataset pEnumDataset = m_pIDataset.Subsets;
                IDataset pSet = pEnumDataset.Next();
                int nEntityID = -1;
                ///�������ݼ���ȡ�������еı�ʶ��
                while (pSet != null)
                {
                    ITable pTable = pSet as ITable;

                    ///�����ñ��л�ȡ��ʶ���ֶ�
                    string sEntityFiled = "";
                    Metadata.MetaTable pMetaTable = Metadata.MetaDataFile.GetMetaTalbleByName(pSet.Name) as Metadata.MetaTable;
                    if (pMetaTable != null)
                    {   
                        sEntityFiled = pMetaTable.EntityIDFiledName;

                        ////����ȡ��ǰ��׼�µ�����
                        ICursor pCursor = pTable.Search(null, true);
                        IRow pRow = pCursor.NextRow();
                        while (pRow != null)
                        {
                            ///��ȡ���е�����ʶ��
                            int nIndex = pRow.Fields.FindField(sEntityFiled);
                            if (nIndex != -1)
                            {
                                //if (!VCTFile.ConvertToInt32(pRow.get_Value(nIndex).ToString(), out nEntityID))
                                {

                                    nEntityID = pRow.get_Value(nIndex) == null || pRow.get_Value(nIndex).ToString() == "" ? -1 : Convert.ToInt32(pRow.get_Value(nIndex));
                                }
                                if (nEntityID > nMaxEntityID)
                                    nMaxEntityID = nEntityID;
                            }
                            pRow = pCursor.NextRow();
                        }
                    }
                    pSet = pEnumDataset.Next();
                }
                return nMaxEntityID;
            }
            catch(Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return -1;
            }
        }


        /// <summary>
        /// vctתmdb��ʱ�򴴽�esri���ݼ�
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="sDatasetName"></param>
        /// <param name="pHeadnode"></param>
        /// <returns></returns>
        public bool CreateESRIDataset(string strDatasetName, HeadNode pHeadnode)
        {
            try
            {
                ISpatialReference pISpatialReference = CreateProjection(pHeadnode);
                if (pISpatialReference != null)
                {
                    IFeatureWorkspace pIFeatureWorkspace = this.m_pIWorkspace as IFeatureWorkspace;
                    this.m_pIDataset = pIFeatureWorkspace.CreateFeatureDataset(strDatasetName, pISpatialReference);
                    return true;
                }

            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
            }
            return false;
        }

        /// <summary>
        /// �����ռ�ο�
        /// </summary>
        /// <param name="headNode">VCTͷ�ڵ�</param>
        private ISpatialReference CreateProjection(HeadNode headNode)
        {
            try
            {
                string sPrjInfo = "";
                //����ͶӰ�����еĳ�����ֵ�����ж���ʲô����ϵͳ������54������80��WGS��84�������ط�����ϵ��
                //����54
                double dMajorAxis = headNode.MajorMax.Y;

                //��ȡ�ο���������� �����ƣ������ᣬ���ʵĵ�����
                string sProjection = headNode.Spheroid.Split(',')[0];
                dMajorAxis = Math.Abs(dMajorAxis - 6378245);
                //if (fabs( m_dSemiMajorAxis - 6378245) < 0.0001)
                if (dMajorAxis < 0.0001)
                {
                    sPrjInfo = string.Format("PROJCS[\"{0}\",GEOGCS[\"GCS_Beijing_1954\",DATUM[\"D_Beijing_1954\""
                        + ",SPHEROID[\"{1}\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],"
                    + "PROJECTION[\"Gauss_Kruger\"],PARAMETER[\"False_Easting\",{2}],PARAMETER[\"False_Northing\",0.0],"
                    + "PARAMETER[\"Central_Meridian\",{3}],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],"
                    + "UNIT[\"Meter\",1.0]]", sProjection, "Gauss-Krueger", headNode.Excursion, headNode.Parametetor.OriginLongtitude);

                }
                //����80
                else
                {
                    // sPrjInfo = string.Format("PROJCS["%s\",GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\",SPHEROID[\"%s\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Gauss_Kruger\"],PARAMETER[\"False_Easting\",%s],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",%f],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]", m_strProjection, m_strSpheroid, sPianYi, m_lMeridian);
                    sPrjInfo = string.Format("PROJCS[\"{0}\",GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\","
                    + "SPHEROID[\"{1}\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],"
                    + "PROJECTION[\"Gauss_Kruger\"],PARAMETER[\"False_Easting\",{2}],PARAMETER[\"False_Northing\",0.0],"
                    + "PARAMETER[\"Central_Meridian\",{3}],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],"
                    + "UNIT[\"Meter\",1.0]]", sProjection, "Gauss-Krueger", headNode.Parametetor.EastOffset, headNode.Parametetor.OriginLongtitude);
                }
                //��������Ϣд�뵽Prj�ļ�
                string sPrjFilename = Application.StartupPath + "tempPrj.prj";
                FileStream fs = File.Create(sPrjFilename);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(sPrjInfo);
                sw.Close();
                fs.Close();

                //����Prj�ļ����ɿռ�ο�
                ISpatialReferenceFactory ipSpatialFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference pSpatialReference = ipSpatialFactory.CreateESRISpatialReferenceFromPRJFile(sPrjFilename);
                pSpatialReference.SetDomain(headNode.MajorMin.X,headNode.MajorMax.X,headNode.MajorMin.Y,headNode.MajorMax.Y);//
                //���þ���,��ֹcutʧ��
                //������λС�����ȡ��Ա�֤����ʱҲ����λ
                ISpatialReferenceTolerance ipSrTolerance = pSpatialReference as ISpatialReferenceTolerance;

                ipSrTolerance.XYTolerance = 0.000001;
                ipSrTolerance.MTolerance = 0.001;
                ipSrTolerance.ZTolerance = 0.001;

                ISpatialReferenceResolution ipSrResolution = pSpatialReference as ISpatialReferenceResolution;
                ipSrResolution.MResolution = 0.001;
                ipSrResolution.set_XYResolution(true, 0.000001);
                ipSrResolution.set_ZResolution(true, 0.001);

                //ɾ�����ɵ�Prj�ļ�
                File.Delete(sPrjFilename);


                //ISpatialReference pSpatialReference;
                //ISpatialReferenceFactory pSpatialreferenceFactory;
                //pSpatialreferenceFactory = new SpatialReferenceEnvironmentClass();
                //IGeographicCoordinateSystem pGeographicCoordinateSystem;
                //pGeographicCoordinateSystem = pSpatialreferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCS3Type.esriSRGeoCS_Xian1980);
                //pSpatialReference = pGeographicCoordinateSystem as ISpatialReference;
                //pSpatialReference.SetFalseOriginAndUnits(-180, -90, 1000000);//

                return pSpatialReference;
            }
            catch (Exception ex)
            {
                LogAPI.WriteLog("�����ռ�ο�ʧ�ܣ�ϵͳĬ�ϴ����յĿռ�ο���\r\n");
                LogAPI.WriteErrorLog(ex);
                ISpatialReference pSpatialReference = new UnknownCoordinateSystemClass();
                pSpatialReference.SetDomain(headNode.MajorMin.X, headNode.MajorMax.X, headNode.MajorMin.Y, headNode.MajorMax.Y);//
                return pSpatialReference;
            }
        }

	}//end Dataset

}//end namespace ESRIData