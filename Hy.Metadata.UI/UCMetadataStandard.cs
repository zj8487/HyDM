﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hy.Metadata.UI
{
    public partial class UCMetadataStandard : UserControl
    {
        public UCMetadataStandard()
        {
            InitializeComponent();
        }

        public MetaStandard CurrentStandard
        {
            get
            {
                if (m_CurrentStandard == null)
                    m_CurrentStandard = new MetaStandard();

                m_CurrentStandard.Name = txtName.Text;
                m_CurrentStandard.TableName = this.txtTableName.Text;
                m_CurrentStandard.MappingDict = this.cmbDictItem.Text;
                m_CurrentStandard.Description = this.txtDescription.Text;
                
                return m_CurrentStandard;
            }

            set
            {
                SetEmpty();
                m_CurrentStandard = value;
                if (m_CurrentStandard == null)
                    return;
                
                this.txtName.Text = m_CurrentStandard.Name;
                this.txtTableName.Text = m_CurrentStandard.TableName;
                this.cmbDictItem.Text = m_CurrentStandard.MappingDict;
                this.txtDescription.Text = m_CurrentStandard.Description;
                if (m_CurrentStandard.FieldsInfo == null)
                    m_CurrentStandard.FieldsInfo = new List<FieldInfo>();

                gcFields.DataSource = m_CurrentStandard.FieldsInfo;
                gvFields.RefreshData();
            }
        }

        private MetaStandard m_CurrentStandard; 
        
        private void SetEmpty()
        {
            this.txtName.Text = "";
            this.txtTableName.Text = "";
            this.cmbDictItem.Text = "";
            this.txtDescription.Text = "";
            gcFields.DataSource = null;
        }

        private bool m_EditAble = true;
        public bool EditAble
        {
            set
            {
                this.m_EditAble = value;
            }
        }

        private void gvFields_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            gcolLength.OptionsColumn.AllowEdit = (this.m_EditAble && m_SelectedFieldInfo != null && m_SelectedFieldInfo.Type == enumFieldType.String);
            gcolPrecision.OptionsColumn.AllowEdit = (this.m_EditAble && m_SelectedFieldInfo != null && m_SelectedFieldInfo.Type == enumFieldType.Decimal);
        }

        private void RefreshEnabled()
        {
            if (m_EditAble)
            {
                this.txtName.Enabled = true;
                this.txtTableName.Enabled = true;
                this.cmbDictItem.Enabled = true;
                this.txtDescription.Enabled = true;
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = m_SelectedFieldInfo!=null;
                gcFields.Enabled = true;
            }
            else
            {
                this.txtName.Enabled = false;
                this.txtTableName.Enabled = false;
                this.cmbDictItem.Enabled = false;
                this.txtDescription.Enabled = false;
                simpleButton1.Enabled = false;
                simpleButton2.Enabled = false;
                gcFields.Enabled = false;
            }
        }

        private FieldInfo m_SelectedFieldInfo;
        private void gvFields_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            m_SelectedFieldInfo = gvFields.GetFocusedRow() as FieldInfo;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            m_CurrentStandard.FieldsInfo.Add(new FieldInfo());
            gvFields.RefreshData();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (m_SelectedFieldInfo != null)
                m_CurrentStandard.FieldsInfo.Remove(m_SelectedFieldInfo);

            gvFields.RefreshData();
        }
    }
}