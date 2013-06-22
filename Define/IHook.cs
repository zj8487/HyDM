﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// Command通用Hook
    /// </summary>
    public interface IHook
    {
        /// <summary>
        /// 主窗体
        /// </summary>
        System.Windows.Forms.Form MainForm { get; }

      
        /// <summary>
        /// Tag，允许Command绑定对象以进行共享
        /// 由相应的（或同类型）Command自己进行解析
        /// </summary>
        object Tag { get; set; }
        
    }
}
