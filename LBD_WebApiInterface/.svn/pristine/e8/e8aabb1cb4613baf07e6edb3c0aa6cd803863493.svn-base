using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LBD_WebApiInterface.Api;
using lancoo.cp.basic.sysbaseclass;
using LgSoft.LgMgrCenterDOTDLL;
using LBD_WebApiInterface.Models.CloudPlatform;

namespace demo
{
    public partial class Form1 : Form
    {
        private TeachSetI ts;
        private TeachInfoI ti;
        public Form1()
        {
            InitializeComponent();
            ts = new TeachSetI();
            ti = new TeachInfoI();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ts.Initialize(textBox2.Text.Trim(), textBox3.Text.Trim(),textBox4.Text.Trim());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            long lValue=ts.SetItemValueSingle(TeachSetI.E_SetItem.智慧云网络室定制界面,0, textBox5.Text.Trim(), textBox1.Text.Trim(), "",Convert.ToInt16(textBox6.Text.Trim()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sValue=ts.GetItemValueSingle(TeachSetI.E_SetItem.智慧云网络室定制界面,0, textBox5.Text.Trim(), "", Convert.ToInt16(textBox6.Text.Trim()));
            MessageBox.Show(sValue);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ti.Initialize("http://192.168.110.3:10102/");
            //ti.Initialize("192.168.110.3", "10104/Ws_ItelEngTeach");
            ////ti.GetSubSysAddr();
            //ti.ReGetZSDBasicLibInfo();
            //string sIP=ti.ZSDZYK_HTTP_IP;
            //string sPort = ti.ZSDZYK_HTTP_Port;
            //string sVir = ti.ZSDZYK_HTTP_VirDir;
            //string sResLibVer = "error";            
            //int irtn = string.Compare(sResLibVer, "\"v5.6\"", true) ;
            //String strReturn = "255254594451544949465051514758521305752505512710810910512111212896866079971101061087612211811261114565413090835580981171111097311611283124105451003488545213069619881747577925156921181011189293535148787797807296767173898723202025252020202022232123203621222225333036333530342628272320303337263636312838242536202522242137383823303023353523352122212236212326252320232022262834303320272025202723252823242020232527";
            //strReturn = ClsParamsEncDec.LgMgr_ParamDecrypt(strReturn);

            //string sValue = textBox7.Text.Trim();
            //textBox8.Text = AITeachCloud.Helper.EncryptHelper.DESDecrypt(sValue);
            //CloudPlatformSubsystemM cts =ti.GetSubSysAddr("J10");
            //textBox7.Text = ti.EssayWSIP +ti.EssayWSPort.ToString() + ti.EssayVirDir_IR;
            //textBox7.Text= lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("1255864", "A526D827E8SS*A526D827E8SS*A526D827E8SS*A526D827E8SS*A526D827E8SS*A526D827E8SS*A526D827E8SS");
            //textBox8.Text = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.DecryptCode("1255864",textBox7.Text);
            ti.Initialize("http://192.168.110.3:10102/");
            textBox8.Text = ti.EssayWSIP_IR + "," +ti.EssayWSPort_IR.ToString()+"," +ti.EssayVirDir_IR;
            textBox7.Text = ti.EssayWSIP2 + "," + ti.EssayWSPort2.ToString() + "," + ti.EssayVirDir_IR;

        }

        private void btnLockerSet_Click(object sender, EventArgs e)
        {
            //string sValue=CP_MD5Helper.GetMd5Hash("2022021-03-17 12:21:10");//6ac85bc52b5b5ecf94846ebb6870581f
            //sValue=CP_EncryptHelper.DecryptCode("202", "600700210700400210400700220100700720500600720700400600400");
            ts.Initialize("192.168.110.3", "10104", "Ws_ItelEngTeach");
            string sErrTips = "";
            bool bRtn = ts.IsAuthorizePass("A526D827E8SE", 1, out sErrTips);//A526D827E8SS；A526D827E8SE
            if (bRtn)
            {
                MessageBox.Show(sErrTips);
            }
            else
            {
                MessageBox.Show(sErrTips);
            }
        }
    }
}
