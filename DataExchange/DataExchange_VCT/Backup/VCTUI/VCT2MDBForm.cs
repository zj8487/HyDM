///////////////////////////////////////////////////////////
//  VCT2MDBForm.cs
//  Implementation of the Class VCT2MDBForm
//  Generated by Enterprise Architect
//  Created on:      08-����-2011 13:45:34
//  Original author: Administrator
///////////////////////////////////////////////////////////




using DIST.DGP.DataExchange.VCT;
using System.Windows.Forms;
//using DIST.DGP.DataExchange.VCT.Metadata;
using System.IO;
using DIST.DGP.DataExchange.VCT.Metadata;
namespace DIST.DGP.DataExchange.VCTUI
{
    /// <summary>
    /// VCTתΪPGDB���ݴ�����
    /// </summary>
    public class VCT2MDBForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button3;

        //public VCT2MDB m_VCT2MDB;
        //public MDB2VCT m_MDB2VCT;
        //private MDB2VCT m_MDB2VCT;
        private string m_sVctPath;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label3;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Label label4;
        private RadioButton radioButton5;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label label5;
        private TextBox textBox3;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private Panel panel4;
        private RadioButton radioButton7;
        private RadioButton radioButton6;
        private Label label6;
        private FolderBrowserDialog folderBrowserDialog1;
        private string m_sMdbPath;
        private Splitter splitter1;
        private ArcDataType m_dataType= ArcDataType.MDB;
        public VCT2MDBForm()
        {
            InitializeComponent();
            LogAPI.SetLogFilePath(Application.StartupPath + "\\ErrorLog.txt");
        }

        ~VCT2MDBForm()
        {

        }

        public virtual void Dispose()
        {

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VCT2MDBForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.textBox1.Location = new System.Drawing.Point(96, 238);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(378, 21);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(96, 274);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(378, 21);
            this.textBox2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(480, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "���";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(480, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "���";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "VCT�ļ�·��";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "����ļ�·��";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(480, 308);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "��ʼת��";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(107, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(65, 16);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "����VCT";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(197, 16);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 16);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.Text = "����VCT";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "ת����ʽ��";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(108, 25);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(83, 16);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "���򼶱�׼";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(198, 25);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(71, 16);
            this.radioButton4.TabIndex = 6;
            this.radioButton4.Text = "�ؼ���׼";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "ת����׼��";
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(282, 25);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(83, 16);
            this.radioButton5.TabIndex = 6;
            this.radioButton5.Text = "�еؼ���׼";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Location = new System.Drawing.Point(18, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 46);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.radioButton3);
            this.panel2.Controls.Add(this.radioButton4);
            this.panel2.Controls.Add(this.radioButton5);
            this.panel2.Location = new System.Drawing.Point(18, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(471, 59);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.textBox3);
            this.panel3.Location = new System.Drawing.Point(18, 181);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(471, 39);
            this.panel3.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "���ݼ�����:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(78, 9);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(185, 21);
            this.textBox3.TabIndex = 0;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(475, 116);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 11;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.radioButton7);
            this.panel4.Controls.Add(this.radioButton6);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(18, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(471, 42);
            this.panel4.TabIndex = 12;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(198, 13);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(47, 16);
            this.radioButton7.TabIndex = 1;
            this.radioButton7.Text = "FGDB";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Checked = true;
            this.radioButton6.Location = new System.Drawing.Point(108, 13);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(41, 16);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "MDB";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "ת����ʽѡ��:";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 353);
            this.splitter1.TabIndex = 13;
            this.splitter1.TabStop = false;
            // 
            // VCT2MDBForm
            // 
            this.ClientSize = new System.Drawing.Size(577, 353);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "VCT2MDBForm";
            this.Text = "VCTתMDB";
            this.Load += new System.EventHandler(this.VCT2MDBForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        /// <summary>
        /// ��vct�ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ///VCTתMDB
                openFileDialog1.Filter = "VCT format.VCT|*.VCT";
                openFileDialog1.Title = "��VCT�ļ�";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_sVctPath = openFileDialog1.FileName;
                    textBox1.Text = m_sVctPath;
                }
            }
            else
            {
                //MDBתVCT
                saveFileDialog1.Filter = "VCT format.VCT|*.VCT";
                saveFileDialog1.Title = "����VCT�ļ�·��";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_sVctPath = saveFileDialog1.FileName;
                    textBox1.Text = m_sVctPath;
                }
            }
        }
        /// <summary>
        /// ����mdb�ļ�·��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, System.EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ///VCTתMDB
                if (radioButton6.Checked)
                {
                    saveFileDialog1.Filter = "MDB format.MDB|*.MDB";
                    saveFileDialog1.Title = "����MDB�ļ�·��";
                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_sMdbPath = saveFileDialog1.FileName;
                        textBox2.Text = m_sMdbPath;
                    }
                }
                ///VCTתfgdb
                else
                {
                    saveFileDialog1.Filter = "";
                    saveFileDialog1.Title = "����FGDB�ļ�·��";
                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_sMdbPath = saveFileDialog1.FileName;
                        textBox2.Text = m_sMdbPath;
                    }
                }
            }
            else
            {
                //MDBתVCT
                if (radioButton6.Checked)
                {
                    openFileDialog1.Filter = "MDB format.MDB|*.MDB";
                    openFileDialog1.Title = "��MDB�ļ�";
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_sMdbPath = openFileDialog1.FileName;
                        textBox2.Text = m_sMdbPath;
                    }
                }///fgdbתvct
                else
                {
                    if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_sMdbPath = folderBrowserDialog1.SelectedPath;
                        textBox2.Text = m_sMdbPath;
                    }
                }
            }
        }
        /// <summary>
        /// ��ʼVCT����ת��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, System.EventArgs e)
        {
            ///���ñ�׼����
            EnumDBStandard pEnumDBStandard = EnumDBStandard.XZJBZ;
            if (radioButton3.Checked)
                pEnumDBStandard = EnumDBStandard.XZJBZ;
            else if (radioButton4.Checked)
                pEnumDBStandard = EnumDBStandard.XJBZ;
            else if (radioButton5.Checked)
                pEnumDBStandard = EnumDBStandard.SJBZ;

            ///��֤����ļ��Ƿ����
            string sOutPutFilePath = "";
            if (radioButton1.Checked)
                sOutPutFilePath = m_sVctPath;
            else
                sOutPutFilePath = m_sMdbPath;
            if (!File.Exists(sOutPutFilePath) && radioButton6.Checked)
            {
                MessageBox.Show("����ָ��������ļ������ڣ�");
                return;
            }

            //bool bConvertSucceed = false;///ָʾ�Ƿ�ת���ɹ�
            if (radioButton1.Checked)
            {
                if (textBox3.Text != "")
                {
                    VCT2MDB m_VCT2MDB = new VCT2MDB(m_sVctPath, m_sMdbPath, textBox3.Text, m_dataType);

                    if (m_VCT2MDB.LoadConfigFile(pEnumDBStandard))
                    {
                        if (m_VCT2MDB.Exchange(false) ==  EnumVCT2MDBExchangeInfo.EXCHANGESUCCESS)
                            MessageBox.Show("ת��������");
                        else
                            MessageBox.Show("ת���г��ִ���");
                    }
                    else
                        MessageBox.Show("��ʼ��������Ϣʧ�ܣ�");
                    if (m_VCT2MDB != null)
                        m_VCT2MDB.Dispose();
                    m_VCT2MDB = null;
                }
                else
                {
                    MessageBox.Show("����д���ݼ����ƣ�");
                }
            }
            else
            {
                MDB2VCT m_MDB2VCT = new MDB2VCT(m_sMdbPath, m_sVctPath, m_dataType);
                //m_MDB2VCT.WriteCommplete += new WriteCommpleteEventHandler(WriteCommplete);
                if (m_MDB2VCT.LoadConfigFile(pEnumDBStandard))
                {
                    if (m_MDB2VCT.Exchange() ==  EnumMDB2VCTExchangeInfo.EXCHANGESUCCESS)
                        MessageBox.Show("ת��������");
                    else
                    {
                        MessageBox.Show("ת���г��ִ���");
                    }
                }
                else
                {
                    MessageBox.Show("��ʼ��������Ϣʧ�ܣ�");
                }
                if (m_MDB2VCT != null)
                    m_MDB2VCT.Dispose();
                m_MDB2VCT = null;

            }
        }

        //void WriteCommplete(bool IsSuccessful)
        //{
        //    if(IsSuccessful==true)
        //        MessageBox.Show("ת��������");
        //    else
        //        MessageBox.Show("ת���г��ִ���");
        //    //if (m_MDB2VCT != null)
        //    //    m_MDB2VCT.Dispose();
        //    //m_MDB2VCT = null;
        //}

        private void VCT2MDBForm_Load(object sender, System.EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
        {
            ///������������Ŀɼ���
            if (radioButton2.Checked)
            {
                panel3.Visible = false;
                label2.Text = "�����ļ�·��";
                label6.Text = "�����ʽѡ��";
            }
            else
            {
                panel3.Visible = true;
                label2.Text = "����ļ�·��";
                label6.Text = "�����ʽѡ��";
            }
        }

        private void radioButton7_CheckedChanged(object sender, System.EventArgs e)
        {
            if (radioButton7.Checked)
            {
                m_dataType = ArcDataType.FGDB;
            }
            else
                m_dataType = ArcDataType.MDB;
        }

    }//end VCT2MDBForm

}//end namespace VCTUI