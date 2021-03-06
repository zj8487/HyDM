﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;
using System.Data;

namespace Frame.Define
{
    public interface ILogin
    {
        /// <summary>
        /// 系统ADO连接
        /// </summary>
        IAdodbHelper AdodbHelper { set; }

        /// <summary>
        /// NHibernate连接接口
        /// </summary>
        INhibernateHelper NhibernateHelper { set; }

        /// <summary>
        /// 日志写入接口
        /// </summary>
        ILogWriter Logger { set; }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        bool Login(ref IApplication application);

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="strMsg"></param>
        void ShowMessage(string strMsg);

        /// <summary>
        /// 销毁
        /// </summary>
        void Dispose();
    }
}
