using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Controls;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Esri.Define;

namespace Esri.Commands
{
    public class CommandTocControl : Common.Operate.CommandDockable
    {
        public CommandTocControl()
        {
            this.m_Category = "视图控制";
            this.m_Caption = "图层目录";
        }
        private ESRI.ArcGIS.Controls.AxTOCControl m_TocControl = null;
        protected override Control CreateControl()
        {
            m_TocControl= new AxTOCControl();
            return m_TocControl;
        }

        protected override Common.Operate.CommandDockable.enumDockPosition DockPosition
        {
            get { return enumDockPosition.Left; }
        }

        protected override void Init()
        {
            m_TocControl.SetBuddyControl(m_hookHelper.Hook);
        }
        protected IHookHelper m_hookHelper = null;
       
        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);
            IEsriHook esriHook = Hook as IEsriHook;
            if (esriHook != null)
                m_hookHelper = esriHook.HookHelper;
        }
    }


    //public sealed class CommandTocControl : EsriBaseCommand,ITool
    //{
    //    public CommandTocControl()
    //    {
    //        this.m_Category = "视图控制";
    //        this.m_Caption = "图层目录";
    //        this.m_Message = "打开图层目录";
    //        this.m_Tooltip = "点击以打开或关闭Toc控件"; 
    //    }

    //    private ESRI.ArcGIS.Controls.AxTOCControl m_TocControl = null;
    //    DockPanel m_DockPanel;
    //    public override void OnClick()
    //    {
    //        IUIHook uiHook = m_Hook as IUIHook;
    //        if (uiHook != null && uiHook.LeftDockPanel != null)
    //        {
    //            bool panelCreateFlag = (m_DockPanel == null);
    //            if (panelCreateFlag)
    //            {
    //                DockPanel dockLeft = uiHook.LeftDockPanel as DockPanel;
    //                m_DockPanel = dockLeft.AddPanel();
    //                m_DockPanel.Text = "图层目录";
    //                //m_DockPanel.Controls.Add(dockLeft.Container as Control);
    //                //m_DockPanel.DockTo(dockLeft);             
    //            }
    //            if (m_DockPanel.Visibility==DockVisibility.Visible && m_TocControl != null)
    //            {
    //                m_DockPanel.Visibility = DockVisibility.Hidden;
    //            }
    //            else
    //            {
    //                if (m_TocControl == null)
    //                {
    //                    m_TocControl = new AxTOCControl();
    //                    ((System.ComponentModel.ISupportInitialize)(m_TocControl)).BeginInit();
    //                    m_DockPanel.Controls.Add(m_TocControl);
    //                    m_TocControl.Dock = System.Windows.Forms.DockStyle.Fill;
    //                    ((System.ComponentModel.ISupportInitialize)(m_TocControl)).EndInit();
    //                }
    //                else
    //                {
    //                    m_DockPanel.Controls.Add(m_TocControl);
    //                    m_TocControl.Dock = System.Windows.Forms.DockStyle.Fill;
    //                }

    //                m_TocControl.SetBuddyControl(m_hookHelper.Hook);
    //                m_DockPanel.Visibility = DockVisibility.Visible;
    //                if (panelCreateFlag)
    //                {
    //                    m_DockPanel.Visibility = DockVisibility.Hidden;
    //                    m_DockPanel.Visibility = DockVisibility.Visible;
    //                }                
    //                //m_DockPanel.BringToFront();
    //                m_DockPanel.Show();
    //            }
    //        }
    //    }

    //    public override bool Enabled
    //    {
    //        get
    //        {
    //            return true;
    //        }
    //    }

    //    public override bool Checked
    //    {
    //        get
    //        {
    //            return m_TocControl!=null && m_TocControl.Visible;
    //        }
    //    }

    //    public override string Message
    //    {
    //        get
    //        {
    //            this.m_Message = this.Checked ? "关闭图层目录" : "打开图层目录";
    //            return base.Message;
    //        }
    //    }

    //    public object Resource
    //    {
    //        get { return m_TocControl; }
    //    }

    //    public bool Release()
    //    {
    //        return true;
    //    }
    //}
}
