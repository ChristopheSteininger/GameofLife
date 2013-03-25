using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// DO NOT USE THIS

namespace GameofLife
{
    public partial class PerformanceData : Form
    {
        public PerformanceData()
        {
            InitializeComponent();

            MinimumSize = Size;
            //RefreshTable();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //RefreshTable();
        }

        /*
        private void RefreshTable()
        {
            KeyValuePair<string, MethodInfo>[] info = Performance.GetMethodInfo();

            table.Rows.Clear();

            long recordedRunTime = 0;
            for (int i = 0; i < info.Length; i++)
            {
                recordedRunTime += info[i].Value.TotalTimeUsed;

                table.Rows.Add(info[i].Value.MethodName, AddCommas(info[i].Value.Counter),
                    AddCommas(info[i].Value.TotalTimeUsed));
            }

            lblRecordedRunTime.Text = AddCommas(recordedRunTime) + " ms";
        }

        private string AddCommas(double n)
        {
            string sn = n.ToString();
            string[] splitN = sn.Split('.');
            string result = "";

            for (int i = splitN[0].Length - 1; i > 0; i--)
            {
                result = sn[i] + result;
                if ((splitN[0].Length - i) % 3 == 0)
                {
                    result = "," + result;
                }
            }

            return splitN[0][0] + result + (splitN.Length > 1 ? "." + splitN[1] : "");
        } */
    }
}
