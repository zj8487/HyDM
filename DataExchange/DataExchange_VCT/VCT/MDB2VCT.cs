///////////////////////////////////////////////////////////
//  MDB2VCT.cs
//  Implementation of the Class MDB2VCT
//  Generated by Enterprise Architect
//  Created on:      08-四月-2011 13:45:31
//  Original author: Administrator
///////////////////////////////////////////////////////////




using DIST.DGP.DataExchange.VCT;
using System.Collections.Generic;
using DIST.DGP.DataExchange.VCT.Metadata;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Windows.Forms;
using System.Data;
using DIST.DGP.DataExchange.VCT.TempData;
using System.ComponentModel;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
//using System.Windows.Forms;

namespace DIST.DGP.DataExchange.VCT
{
    /// <summary>
    /// PGDB数据转为VCT文件数据控制类
    /// </summary>
    public class MDB2VCT
    {
        /// <summary>
        /// MDB数据文件名称
        /// </summary>
        public string MDBFilePahtName
        {
            get
            {
                return m_strMDBFilePahtName;
            }
        }
        private string m_strMDBFilePahtName = "";
        /// <summary>
        /// VCT数据文件名称
        /// </summary>
        public string VCTFilePahtName
        {
            get
            {
                return m_strVCTFilePahtName;
            }
        }
        private string m_strVCTFilePahtName = "";

        /// <summary>
        /// 获取新的实体标识码
        /// </summary>
        private int m_nNewEntityID = 1;


        private int m_nLayerCount = 0;

        private ESRIData.Dataset m_dataset;

        /// <summary>
        /// MDB数据文件对象
        /// </summary>
        private EsriDataSource m_MDBFile;
        /// <summary>
        /// VCT数据文件对象
        /// </summary>
        private VCTFile m_VCTFile;

        private TempData.TempFile m_pTempFile;

        //public event WriteCommpleteEventHandler WriteCommplete;

        /// <summary>
        /// mdb转vct控制类构造函数
        /// </summary>
        /// <param name="strMDBFilePahtName">mdb文件路径</param>
        /// <param name="strVCTFilePahtName">vct文件路径</param>
        /// <param name="pDataType">数据源类型</param>
        public MDB2VCT(string strMDBFilePahtName, string strVCTFilePahtName,ArcDataType pDataType)
        {
            if (pDataType == ArcDataType.MDB)
                m_MDBFile = new MDBFile(true, strMDBFilePahtName);
            else
                m_MDBFile = new FGDBFile(true, strMDBFilePahtName);
            m_VCTFile = new VCTFile(false, strVCTFilePahtName);

            m_pTempFile = new DIST.DGP.DataExchange.VCT.TempData.TempFile(strVCTFilePahtName + ".tmp.mdb");

            //WriteCommplete = null;
        }

        /// <summary>
        ///  mdb转vct控制类构造函数
        /// </summary>
        /// <param name="strMDBFilePahtName">mdb文件路径</param>
        /// <param name="strVCTFilePahtName">vct文件路径</param>
        /// <param name="pDataType">数据源类型</param>
        /// <param name="pCutGeometry">裁切范围</param>
        public MDB2VCT(string strMDBFilePahtName, string strVCTFilePahtName, ArcDataType pDataType,bool bCut,IGeometry pCutGeometry)
        {
            if (pDataType == ArcDataType.MDB)
                m_MDBFile = new MDBFile(true, strMDBFilePahtName,bCut,pCutGeometry);
            else
                m_MDBFile = new FGDBFile(true, strMDBFilePahtName, bCut,pCutGeometry);
            m_VCTFile = new VCTFile(false, strVCTFilePahtName);

            m_pTempFile = new DIST.DGP.DataExchange.VCT.TempData.TempFile(strVCTFilePahtName + ".tmp.mdb");

            //WriteCommplete = null;
        }

        ~MDB2VCT()
        {

        }


/*
        /// <summary>
        /// 处理面实体中引用线对象的标识码
        /// </summary>
        /// <param name="arrLineNode">VCT线实体节点集合</param>
        /// <param name="arrFeatureCode">关联图层要素编码集合</param>
        /// <param name="arrLineNodeEx">VCT面实体中的线实体节点集合</param>
        private void MatchPolygonToLine(ref List<FileData.LineNode> arrLineNode, List<string> arrFeatureCode, ref List<FileData.LineNodeEx> arrLineNodeEx)
        {

            
            if (arrFeatureCode != null && arrFeatureCode.Count > 0)
            {
                List<FileData.LineNode> arrLinkLineNode = new List<FileData.LineNode>();
                for (int i = 0; i < arrLineNode.Count; i++)
                {
                    if (arrFeatureCode.Contains(arrLineNode[i].FeatureCode))
                    {
                        arrLinkLineNode.Add(arrLineNode[i]);
                    }
                }
                if (arrLinkLineNode.Count > 0)
                {
                    //排序
                    arrLinkLineNode.Sort();
                    arrLineNodeEx.Sort();

                    //更新构面线的标识码
                    int j = 0;
                    for (int i = 0; i < arrLinkLineNode.Count; i++)
                    {
                        for (; j < arrLineNodeEx.Count; j++)
                        {
                            //必须是未找到标识码的
                            if (arrLineNodeEx[j].EntityID == -1)
                            {
                                if (arrLinkLineNode[i] == arrLineNodeEx[j])
                                {
                                    arrLineNodeEx[j].IsFromLine = true;
                                    arrLineNodeEx[j].EntityID = arrLinkLineNode[i].EntityID;
                                }
                                else if (arrLinkLineNode[i] > arrLineNodeEx[j])
                                {

                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    //反向
                    List<FileData.LineNodeEx> arrLineNodeExReverse = new List<FileData.LineNodeEx>();
                    for (int i = 0; i < arrLineNodeEx.Count; i++)
                    {
                        //必须是未找到标识码的
                        if (arrLineNodeEx[i].EntityID == -1)
                        {
                            FileData.LineNodeEx lineNodeEx = arrLineNodeEx[i];
                            lineNodeEx.Reverse();
                            arrLineNodeExReverse.Add(lineNodeEx);
                        }
                    }
                    arrLineNodeExReverse.Sort();//反向后排序
                    j = 0;
                    for (int i = 0; i < arrLinkLineNode.Count; i++)
                    {
                        for (; j < arrLineNodeExReverse.Count; j++)
                        {
                            if (arrLinkLineNode[i] == arrLineNodeExReverse[j])
                            {
                                arrLineNodeExReverse[j].IsFromLine = true;
                                arrLineNodeExReverse[j].EntityID = arrLinkLineNode[i].EntityID;
                            }
                            else if (arrLinkLineNode[i] > arrLineNodeExReverse[j])
                            {
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    arrLineNodeEx.Clear();
                    for (int i = 0; i < arrLineNodeExReverse.Count; i++)
                    {
                        //必须是未找到标识码的
                        if (arrLineNodeExReverse[i].EntityID == -1)
                        {
                            arrLineNodeEx.Add(arrLineNodeExReverse[i]);
                        }
                    }
                }
            }
            return arrLineNodeEx;
        }
*/
        //public FileData.LineNodeEx CloneLineNodeEx(FileData.LineNodeEx lineNodeExSource)
        //{
        //    //m_InitiallyLineNode = this;
            

        //    try
        //    {
        //        MemoryStream stream = new MemoryStream();
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        formatter.Serialize(stream, lineNodeExSource);
        //        stream.Position = 0;

        //        FileData.LineNodeEx lineNodeEx = formatter.Deserialize(stream) as FileData.LineNodeEx;

        //        lineNodeEx.InitiallyLineNode = lineNodeExSource;
        //        return lineNodeEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogAPI.WriteErrorLog(ex);
        //    }
        //     /* 
        //    try
        //    {
        //        FileData.LineNodeEx lineNodeEx = new FileData.LineNodeEx();
        //        lineNodeEx.IsReverse = lineNodeExSource.IsReverse;
        //        lineNodeEx.PolygonID = lineNodeExSource.PolygonID;
        //        lineNodeEx.LineIndex = lineNodeExSource.LineIndex;
        //        lineNodeEx.IsFromLine = lineNodeExSource.IsFromLine;

        //        lineNodeEx.LineType = lineNodeExSource.LineType;
        //        lineNodeEx.SegmentNodes = new FileData.SegmentNodes();
        //        for (int i = 0; i < lineNodeExSource.SegmentNodes.Count; i++)
        //        {
        //            lineNodeEx.SegmentNodes.Add(lineNodeExSource.SegmentNodes[i]);
        //            //BrokenLineNode brokenLineNode1 = this.SegmentNodes[i] as BrokenLineNode;
        //            //if (brokenLineNode1 != null)
        //            //{
        //            //    BrokenLineNode brokenLineNode2 = new BrokenLineNode();
        //            //    brokenLineNode2.PointInfoNodes = new PointInfoNodes();
        //            //    for (int j = 0; j < brokenLineNode1.PointInfoNodes.Count; j++)
        //            //    {
        //            //        PointInfoNode pointInfoNode = new PointInfoNode();
        //            //        pointInfoNode.X = brokenLineNode1.PointInfoNodes[j].X;
        //            //        pointInfoNode.Y = brokenLineNode1.PointInfoNodes[j].Y;
        //            //        brokenLineNode2.PointInfoNodes.Add(pointInfoNode);
        //            //    }
        //            //    lineNodeEx.SegmentNodes.Add(brokenLineNode2);
        //            //}
        //        }

        //        lineNodeEx.EntityID = lineNodeExSource.EntityID;
        //        lineNodeEx.FeatureCode = lineNodeExSource.FeatureCode;
        //        lineNodeEx.Representation = lineNodeExSource.Representation;

        //        lineNodeEx.InitiallyLineNode = lineNodeExSource;
        //        return lineNodeEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogAPI.WriteErrorLog(ex);
        //    }*/
        //    return null;
        //}

/*

        /// <summary>
        /// 处理面实体中引用线对象的标识码
        /// </summary>
        /// <param name="arrLineNodeExNew">VCT面实体中的线实体节点集合（需要新建）</param>
        private void MatchPolygonToLine(ref List<FileData.LineNodeEx> arrLineNodeExNew)
        {
            List<FileData.LineNodeEx> arrLineNodeNewClone = new List<VCT.FileData.LineNodeEx>();
            for (int i = 0; i < arrLineNodeExNew.Count; i++)
            {
                //克隆对象，并反向
                FileData.LineNodeEx lineNodeEx = CloneLineNodeEx(arrLineNodeExNew[i]);//.Clone() as FileData.LineNodeEx;
                if (lineNodeEx != null)
                {
                    lineNodeEx.Reverse();
                    arrLineNodeNewClone.Add(lineNodeEx);
                }
            }

            arrLineNodeExNew.Sort();
            arrLineNodeNewClone.Sort();

            int j = 0;
            for (int i = 0; i < arrLineNodeExNew.Count; i++)
            {
                if (arrLineNodeExNew[i].EntityID == -1)
                {
                    //需要创建要素编码
                    arrLineNodeExNew[i].EntityID = this.m_nNewEntityID;
                    ////未做合并处理
                    //m_VCTFile.WritePolygonLineNode(arrLineNodeExNew[i]);
                }
                for (; j < arrLineNodeNewClone.Count; j++)
                {
                    if (arrLineNodeNewClone[j].InitiallyLineNode.EntityID == -1)
                    {
                        if (arrLineNodeExNew[i] == arrLineNodeNewClone[j])
                        {
                            arrLineNodeNewClone[j].InitiallyLineNode.EntityID = arrLineNodeExNew[i].EntityID;
                            arrLineNodeNewClone[j].InitiallyLineNode.Reverse();
                            //arrLineNodeNewClone[j].InitiallyLineNode.IsReverse = arrLineNodeNewClone[j].IsReverse;

                            arrLineNodeNewClone[j].InitiallyLineNode.OtherPolygonLineNode = arrLineNodeExNew[i];
                            arrLineNodeExNew[i].OtherPolygonLineNode = arrLineNodeNewClone[j].InitiallyLineNode;
                        }
                        if (arrLineNodeExNew[i] > arrLineNodeNewClone[j])
                        {
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }



            //for (int i = 0; i < arrLineNodeExNew.Count; i++)
            //{
            //    if ((arrLineNodeExNew[i].InitiallyLineNode == null && arrLineNodeExNew[i].EntityID == -1) ||                      //非克隆对象
            //        (arrLineNodeExNew[i].InitiallyLineNode != null && arrLineNodeExNew[i].InitiallyLineNode.EntityID == -1))      //克隆对象
            //    {
            //        if (i > 0)
            //        {
            //            if (arrLineNodeExNew[i] == arrLineNodeExNew[i - 1])
            //            {
            //                arrLineNodeExNew[i].EntityID = arrLineNodeExNew[i - 1].EntityID;
            //                if (arrLineNodeExNew[i].InitiallyLineNode != null)
            //                {
            //                    //arrLineNodeExNew[i]是克隆对象，需要更新原始对象的标识码和方向标识
            //                    arrLineNodeExNew[i].InitiallyLineNode.EntityID = arrLineNodeExNew[i].EntityID;
            //                    arrLineNodeExNew[i].InitiallyLineNode.IsReverse = arrLineNodeExNew[i].IsReverse;
            //                }



                            
            //                continue;
            //            }
            //        }
            //        //需要创建要素编码
            //        arrLineNodeExNew[i].EntityID = this.NewEntityID;
            //        if (arrLineNodeExNew[i].InitiallyLineNode != null)
            //        {
            //            //arrLineNodeExNew[i]是克隆对象，需要更新原始对象的标识码和方向标识
            //            arrLineNodeExNew[i].InitiallyLineNode.EntityID = arrLineNodeExNew[i].EntityID;
            //            arrLineNodeExNew[i].InitiallyLineNode.IsReverse = arrLineNodeExNew[i].IsReverse;
            //        }
            //        m_VCTFile.WritePolygonLineNode(arrLineNodeExNew[i]);
            //    }
            //}
        }
        */
        ///// <summary>
        ///// 处理面实体中引用线对象的标识码
        ///// </summary>
        ///// <param name="arrLineNode">VCT线实体节点集合</param>
        //private void WritePolygonLineNodes(ref List<FileData.LineNodeEx> arrLineNode)
        //{
        //    ////按标识码、在面的边界线集合中的索引排序
        //    FileData.LineNodeExComparer lineNodeExComparer = new FileData.LineNodeExComparer();
        //    arrLineNode.Sort(lineNodeExComparer);

        //    FileData.LineNodeEx lineNode = null;
        //    for (int i = 0; i < arrLineNode.Count; i++)
        //    {
        //        if (lineNode == null)
        //        {
        //            lineNode = arrLineNode[i];
        //        }
        //        else
        //        {
        //            if (arrLineNode[i].EntityID == lineNode.EntityID)
        //            {
        //                lineNode.SegmentNodes.AddRange(arrLineNode[i].SegmentNodes);
        //            }
        //            else
        //            {
        //                m_VCTFile.WritePolygonLineNode(lineNode);
        //                lineNode = arrLineNode[i];
        //            }

        //            if (i == arrLineNode.Count - 1)
        //            {
        //                m_VCTFile.WritePolygonLineNode(lineNode);
        //            }
        //        }
        //    }
        //}

 



        /// <summary>
        /// 执行转换
        /// </summary>
        public EnumMDB2VCTExchangeInfo Exchange()
        {
            ///指示当前转换的执行位置
            int nExcutePosition = 0;
            ///1.表结构 2.点 3.线 4.面
            try
            {
                //获取PGDB数据集
                m_dataset =  m_MDBFile.GetDataset();
                if (m_dataset != null)
                {
                    //获取当前数据的最大实体标识码
                    this.m_nNewEntityID = m_dataset.GetMaxEntityID() + 1;
                    //获取VCT头节点
                    ESRIData.Projection projection = m_dataset.GetProjection();
                    if (projection != null)
                    {
                        FileData.HeadNode headNode = projection.GetHeadNode();
                        ///生成头文件错误
                        if (headNode == null)
                        {
                            return EnumMDB2VCTExchangeInfo.HEADFILEERROR;
                        }
                        //1、写入VCT头节点
                        m_VCTFile.WriteHeadNode(headNode);

                        //获取图层集合
                        ESRIData.TableLayer tableLayer;
                        m_nLayerCount = m_dataset.GetLayerCount();
                        List<FileData.FeatureCodeNode> arrFeatureCodeNode = new List<FileData.FeatureCodeNode>();
                        List<FileData.TableStructureNode> arrTableStructureNode = new List<FileData.TableStructureNode>();

                        nExcutePosition++;
                        for (int i = 0; i < m_nLayerCount; i++)
                        {
                            //获取图层对象
                            tableLayer = m_dataset.GetFeatureLayerByIndex(i);
                            //获取图层要素参数信息
                            FileData.FeatureCodeNode featureCodeNode = tableLayer.GetFeatureCodeNode();
                            arrFeatureCodeNode.Add(featureCodeNode);
                            //获取图层表结构信息
                            FileData.TableStructureNode tableStructureNode = tableLayer.StructureNode;
                            arrTableStructureNode.Add(tableStructureNode);
                        }
                        //2、写入VCT要素参数节点
                        m_VCTFile.WriteFeatureCodeNodes(arrFeatureCodeNode);
                        //3、写入VCT表结构节点
                        m_VCTFile.WriteTableStructureNode(arrTableStructureNode);

                        ///释放不在使用的对象，减少内存使用率 add by 曾平
                        //m_VCTFile.Flush();
                        arrFeatureCodeNode.Clear();
                        arrTableStructureNode.Clear();

                        nExcutePosition++;

                        //4、写入点实体节点
                        for (int i = 0; i < m_nLayerCount; i++)
                        {
                            //获取图层对象
                            tableLayer = m_dataset.GetFeatureLayerByIndex(i);
                            ESRIData.PointLayer pointLayer = tableLayer as ESRIData.PointLayer;
                            if (pointLayer != null)
                            {
                                if (pointLayer.FeatureEntys != null)
                                {
                                    for (int j = 0; j < pointLayer.FeatureEntys.Count; j++)
                                    {
                                        m_VCTFile.WritePointNode(pointLayer.FeatureEntys[j].GetEntityNode() as FileData.PointNode);
                                    }
                                }
                            }
                        }

                        nExcutePosition++;
                        ////5、写入线实体节点
                        //List<FileData.LineNode> arrLineNode = new List<FileData.LineNode>();
                        for (int i = 0; i < m_nLayerCount; i++)
                        {
                            //获取图层对象
                            tableLayer = m_dataset.GetFeatureLayerByIndex(i);
                            ESRIData.LineLayer lineLayer = tableLayer as ESRIData.LineLayer;
                            if (lineLayer != null)
                            {
                                if (lineLayer.FeatureEntys != null)
                                {
                                    for (int j = 0; j < lineLayer.FeatureEntys.Count; j++)
                                    {
                                        FileData.LineNode lineNode = lineLayer.FeatureEntys[j].GetEntityNode() as FileData.LineNode;
                                        if (lineNode != null)
                                        {
                                            m_VCTFile.WriteLineNode(lineNode);
                                        }
                                    }
                                }
                            }
                        }

                        ////处理面实体中引用线对象的标识码
                        ////List<FileData.PolygonNode> arrPolygonNode = MatchPolygonToLine(dataset, ref arrLineNode);
                        ////6、写入面实体节点
                        nExcutePosition++;
                        WritePolygonNodes pWritePolygonNodes = new WritePolygonNodes(m_dataset, this.m_pTempFile, this.m_VCTFile, this.m_nNewEntityID);
                        //pWritePolygonNodes.WriteCommplete += new WriteCommpleteEventHandler(WritePolygonNodes_WriteCommplete);
                        pWritePolygonNodes.Write();

                        WritePolygonNodes_WriteCommplete(true);
                        return EnumMDB2VCTExchangeInfo.EXCHANGESUCCESS;

                    }
                    else
                    {
                        return EnumMDB2VCTExchangeInfo.PROJCTIONERROR;
                    }
                }
                else
                {
                    return EnumMDB2VCTExchangeInfo.DATASETERROR;
                }

            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog(ex);
                EnumMDB2VCTExchangeInfo pInfo = EnumMDB2VCTExchangeInfo.EXCHANGUNKNOWEERROR;
                if (nExcutePosition == 1)
                    pInfo = EnumMDB2VCTExchangeInfo.FEATUREINFOERROR;
                else if (nExcutePosition == 2)
                    pInfo = EnumMDB2VCTExchangeInfo.POINTERROR;
                else if (nExcutePosition == 3)
                    pInfo = EnumMDB2VCTExchangeInfo.LINEERROR;
                else if (nExcutePosition == 4)
                    pInfo = EnumMDB2VCTExchangeInfo.PLOYGONERROR;

                return pInfo;
            }
        }

        private void WritePolygonNodes_WriteCommplete(bool IsSuccessful)
        {
            ESRIData.TableLayer tableLayer;
            ESRIData.FeatureLayer featureLayer;
            //for (int i = 0; i < arrPolygonNode.Count; i++)
            //{
            //    m_VCTFile.WritePolygonNode(arrPolygonNode[i]);
            //}
            //7、写入注记实体节点
            for (int i = 0; i < m_nLayerCount; i++)
            {
                //获取图层对象
                tableLayer = m_dataset.GetFeatureLayerByIndex(i);
                ESRIData.AnnotationLayer annotationLayer = tableLayer as ESRIData.AnnotationLayer;
                if (annotationLayer != null)
                {
                    if (annotationLayer.FeatureEntys == null) continue;
                    for (int j = 0; j < annotationLayer.FeatureEntys.Count; j++)
                    {
                        m_VCTFile.WriteAnnotationNode(annotationLayer.FeatureEntys[j].GetEntityNode() as FileData.AnnotationNode);
                        ////add by 曾平
                        //if (j % 1000 == 0)
                        //    m_VCTFile.Flush();
                    }
                    //m_VCTFile.Flush();
                }
            }
            //8、写入表属性记录节点
            for (int i = 0; i < m_nLayerCount; i++)
            {
                //获取图层对象
                tableLayer = m_dataset.GetFeatureLayerByIndex(i);
                featureLayer = tableLayer as ESRIData.FeatureLayer;
                if (featureLayer != null)
                {
                    int j = 0;
                    m_VCTFile.WriteTableNode(featureLayer.GetTableNode());
                    if (featureLayer.FeatureEntys == null) continue;
                    for (j = 0; j < featureLayer.FeatureEntys.Count; j++)
                    {
                        m_VCTFile.WriteRecordNode(featureLayer.FeatureEntys[j].GetRecordNode(featureLayer.StructureNode), false);
                    }
                    //if (j < arrFeatureEntity.Count)
                    //    m_VCTFile.WriteRecordNode(arrFeatureEntity[j].GetRecordNode(), true);
                    //else
                    //    m_VCTFile.WriteRecordNode(null, true);

                    //m_VCTFile.Flush();
                }
                else if (tableLayer != null)
                {
                    m_VCTFile.WriteTableNode(tableLayer.GetTableNode());
                    if (tableLayer.RecordNodes != null)
                    {
                        foreach (FileData.RecordNode pRecorditem in tableLayer.RecordNodes)
                        {
                            m_VCTFile.WriteRecordNode(pRecorditem, false);
                        }
                    }

                    //m_VCTFile.Flush();
                }
                ///写入属性表结束标签
                m_VCTFile.WriteRecordNode(null, true);
            }

            //写入其他节点
            m_VCTFile.WriteVarcharNode();
            m_VCTFile.WriteStyleNode();


            m_VCTFile.CloseFile();

            //if (WriteCommplete != null)
            //    WriteCommplete(true);

            //return true
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="strStandardName">土地利用规划数据库标准名称</param>
        /// <returns></returns>
        public bool LoadConfigFile(EnumDBStandard pEnumDBStandard)
        {
            return MetaDataFile.Initial(pEnumDBStandard);
        }

        /// <summary>
        /// 设置配置文件路径 （默认在程序启动目录下）
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool SetConfigFilePath(string strPath)
        {
            return Metadata.MetaDataFile.SetConfigFilePath(strPath);
        }
        public virtual void Dispose()
        {
            if (m_VCTFile != null)
                m_VCTFile.CloseFile();
            m_VCTFile = null;

            Metadata.MetaDataFile.Dispose();

            if (m_pTempFile != null)
                m_pTempFile.Close();
            m_pTempFile = null;

            if (m_dataset != null)
                m_dataset.Dispose();
            m_dataset = null;
            
            if (m_MDBFile != null)
                m_MDBFile.Dispose();
            m_MDBFile = null;
            GC.Collect();
        }


    }//end MDB2VCT

}//end namespace VCT