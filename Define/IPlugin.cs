﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Define
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 系统ADO连接
        /// </summary>
        IDbConnection SysConnection { set; }

        /// <summary>
        /// 系统ESRI连接
        /// </summary>
        object GisWorkspace { set; }

        /// <summary>
        /// NHibernate连接接口
        /// </summary>
        INhibernateHelper NhibernateHelper { set; }

        /// <summary>
        /// 日志写入接口
        /// </summary>
        ILogger Logger { set; }

    }
}