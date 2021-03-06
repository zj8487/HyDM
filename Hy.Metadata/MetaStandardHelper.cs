﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hy.Metadata
{
    public class MetaStandardHelper
    {
        public MetaStandardHelper(MetaStandard metaStandard)
        {
        }

        public static MetaStandard GetStandardByName(string strName)
        {
            //IList<MetaStandard> standardList = Environment.NhibernateHelper.GetObjectsByCondition<MetaStandard>(string.Format("from MetaStandard mStandard where mStandard.Name='{0}'", strName));
            //if (standardList == null || standardList.Count == 0)
            //{
            //    return null;
            //}

            //return standardList[0];

            return Environment.NhibernateHelper.GetObject<MetaStandard>(string.Format("from MetaStandard mStandard where mStandard.Name='{0}'", strName));
        }

        public static MetaStandard GetStandardById(string id)
        {
            return  Environment.NhibernateHelper.GetObjectById<MetaStandard>(id);          
        }

        public static IList<MetaStandard> GetAll()
        {
            return Environment.NhibernateHelper.GetAll<MetaStandard>();
        }

        public static string ErrorMessage { get; private set; }

        /// <summary>
        /// 删除字典项
        /// </summary>
        /// <param name="standard"></param>
        /// <returns></returns>
        public static bool DeleteStandard(MetaStandard standard)
        {
            if (standard == null)
                return false;

            try
            {
                // 记录
                if (standard.FieldsInfo != null)
                {
                    foreach (FieldInfo fInfo in standard.FieldsInfo)
                    {
                        Environment.NhibernateHelper.DeleteObject(fInfo);
                    }
                }
                Environment.NhibernateHelper.DeleteObject(standard);
                Environment.NhibernateHelper.Flush();

                // 删除数据表
                if (Environment.AdodbHelper.TableExists(standard.TableName))
                {
                    Environment.AdodbHelper.ExecuteSQL(string.Format("Drop table {0}", standard.TableName));                   
                }
               
                return true;
            }
            catch (Exception exp)
            {
                ErrorMessage = "删除出错了!";
                Environment.Logger.AppendMessage(Define.enumLogType.Error, string.Format("删除元数据标准时出错：{0}", exp.ToString()));

                return false;
            }
        }

        /// <summary>
        /// 保存字典项
        /// </summary>
        /// <param name="standard"></param>
        /// <returns></returns>
        public static bool SaveStandard(MetaStandard standard)
        {
            if (standard == null)
                return false           ;

            try
            {

                Environment.NhibernateHelper.SaveObject(standard);
                // 记录
                if (standard.FieldsInfo != null)
                {
                    foreach (FieldInfo fInfo in standard.FieldsInfo)
                    {
                        fInfo.Layer = standard.ID;
                        Environment.NhibernateHelper.SaveObject(fInfo);
                    }
                }
                Environment.NhibernateHelper.Flush();

                // 创建表
                if (Environment.AdodbHelper.TableExists(standard.TableName))
                {
                    int eCount = Environment.AdodbHelper.ExecuteSQL(string.Format("Drop table {0}", standard.TableName));
                    if (eCount < 1)
                    {
                        ErrorMessage = "删除原有数据表时出错";
                        return false;
                    }
                }
                if (!CreateTable(standard))
                {
                    ErrorMessage = "创建新数据表时出错";
                    return false;
                }

                return true;
            }
            catch (Exception exp)
            {
                Environment.Logger.AppendMessage(Define.enumLogType.Error, string.Format("保存元数据标准时出错：{0}", exp.ToString()));

                return false;
            }
        }

        public static void ReLoadItem(MetaStandard standard)
        {
            if (standard == null)
                return;

            Environment.NhibernateHelper.RefreshObject(standard, Define.enumLockMode.UpgradeNoWait);
        }

        public static bool CreateTable(MetaStandard standard)
        {
            if (standard == null)
                return false;

            try
            {
                StringBuilder strSQL = new StringBuilder("Create Table ");
                strSQL.Append(standard.TableName);
                strSQL.Append(" (");
                strSQL.Append(GetSQLFromField(IDFieldInfo));
                strSQL.Append(",");
                foreach (FieldInfo fInfo in standard.FieldsInfo)
                {
                    strSQL.Append(GetSQLFromField(fInfo));
                    strSQL.Append(",");
                }
                strSQL.Remove(strSQL.Length - 1, 1);
                strSQL.Append(")");

                Environment.AdodbHelper.ExecuteSQL(strSQL.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static FieldInfo m_IDFieldInfo;
        private static FieldInfo IDFieldInfo
        {
            get
            {
                if (m_IDFieldInfo == null)
                {
                    m_IDFieldInfo = new FieldInfo();
                    m_IDFieldInfo.Name = "ID";
                    m_IDFieldInfo.NullAble = false;
                    m_IDFieldInfo.Type = enumFieldType.String;
                    m_IDFieldInfo.Length = 32;
                    m_IDFieldInfo.AliasName = "标识符";
                }
                return m_IDFieldInfo;
            }
        }


        public static string GetSQLFromField(FieldInfo fInfo)
        {
            if (fInfo == null)
                return null;

            string strSQL="";
            string strTypeKey = GetTypeKey(fInfo.Type);
            switch (fInfo.Type)
            {
                case enumFieldType.String:
                    if (string.IsNullOrEmpty(strTypeKey ))
                        strTypeKey = "varchar";

                    strSQL=string.Format("{0}({1}) {2}",strTypeKey,
                        fInfo.Length>0?fInfo.Length:50,fInfo.NullAble?"":"Not Null");
                    break;

                case enumFieldType.Int:
                    if (string.IsNullOrEmpty(strTypeKey))
                        strTypeKey = "Int";
                    strSQL = strTypeKey;
                    break;

                case enumFieldType.Decimal:
                    if (string.IsNullOrEmpty(strTypeKey))
                        strSQL = "numeric";
                    else
                    {
                        int fLen = fInfo.Length > 0 ? fInfo.Length : 5;
                        strSQL = string.Format("{0}({1},{2})", strTypeKey,
                           fLen,
                           fInfo.Precision > fLen ? fLen : fInfo.Precision);
                    }
                    break;

                case enumFieldType.DateTime:
                    if (string.IsNullOrEmpty(strTypeKey))
                        strTypeKey = "DateTime";

                    strSQL = strTypeKey;

                    break;

                case enumFieldType.Image:
                case enumFieldType.Binary:
                    if (string.IsNullOrEmpty(strTypeKey))
                        strTypeKey = "Image";

                    strSQL = strTypeKey;
                    break;

                default:
                    return "varchar";
            }

            return string.Format("{0} {1}",fInfo.Name, strSQL);
        }

        private static string[] m_TypeItemKey =
        {
            ConstDefine.Type_Key_String,
            ConstDefine.Type_Key_Int,
            ConstDefine.Type_Key_Decimal,
            ConstDefine.Type_Key_DateTime,
            ConstDefine.Type_Key_Binary,
            ConstDefine.Type_Key_Binary
        };
        private static string GetTypeKey(enumFieldType fType)
        {
            return Environment.NhibernateHelper.GetObject<string>(
                string.Format("select cfgItem.ItemValue from ConfigItem cfgItem where cfgItem.ItemKey='{0}'",m_TypeItemKey[(int)fType]));
        }

        private static void TransCaption(DataTable dtSource, Dictionary<string, string> dictCaption)
        {
            if (dictCaption == null || dtSource == null)
                return;

            int count = dtSource.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                if (dictCaption.ContainsKey(dtSource.Columns[i].ColumnName))
                    dtSource.Columns[i].Caption = dictCaption[dtSource.Columns[i].ColumnName];
            }
        }
        public static DataTable GetAllMetaData(MetaStandard standard)
        {
           DataTable dtResult= Environment.AdodbHelper.ExecuteDataTable(string.Format("select * from {0}",standard.TableName));
           TransCaption(dtResult, standard.GetFieldNameDictionary());

           return dtResult;
        }

        public static DataTable GetMetadata(MetaStandard standard, string strClause, int countPerPage, int pageIndex, ref int totalCount)
        {
            if (!Environment.AdodbHelper.TableExists(standard.TableName))
            {
                if (!CreateTable(standard))
                {
                    ErrorMessage = "此标准的数据表不存在，并且创建失败";
                    return null;
                }
            }
            DataTable dtResult = GetMetadata(standard.TableName, strClause, countPerPage, pageIndex, ref totalCount);
            TransCaption(dtResult, standard.GetFieldNameDictionary());

            return dtResult;
        }

        public static DataTable GetMetadata(string strTable, string strClause, int countPerPage, int pageIndex, ref int totalCount)
        {
            string strWhereClause = "";
            if (!string.IsNullOrWhiteSpace(strClause))
            {
                strWhereClause = string.Format(" where {0}", strClause);
            }
            if (totalCount < 0)
            {
                totalCount = Convert.ToInt32(Environment.AdodbHelper.ExecuteScalar(string.Format("select count(0) from {0} {1}", strTable,strWhereClause)));
                
            }

            if (totalCount == 0)
                return Environment.AdodbHelper.ExecuteDataTable(string.Format("select * from {0} {1} ", strTable,strWhereClause));

            int resultCount = countPerPage;
            if (countPerPage * (pageIndex + 1) > totalCount)
                resultCount = totalCount - countPerPage * pageIndex;

            string strSQL = Environment.NhibernateHelper.GetObject<string>(string.Format("select cfgItem.ItemValue from ConfigItem cfgItem where cfgItem.ItemKey='{0}'", strTable));
            if (string.IsNullOrEmpty(strSQL))
            {
                strSQL = "select top {3} * from (select top {5} * from {0} {4} order by id asc) order by id desc";
            }

            object[] objParams = { strTable, countPerPage, pageIndex, resultCount, strWhereClause, countPerPage*(pageIndex+1)};
            strSQL = string.Format(strSQL, objParams);

            return Environment.AdodbHelper.ExecuteDataTable(strSQL);
        }

        public static bool UpdateMeatadata(MetaStandard standard, DataTable dtData)
        {
            if (standard == null)
                return false;

            return Environment.AdodbHelper.UpdateTable(standard.TableName, dtData);
        }
    }
}
