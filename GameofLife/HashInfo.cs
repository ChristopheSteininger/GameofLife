using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameofLife
{
    public partial class HashInfo : Form
    {
        public HashInfo()
        {
            InitializeComponent();

            MinimumSize = Size;
            RefreshInfo();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RefreshInfo()
        {
            int[] tableUse = NodeTable.GetTableUse();

            int usedRows = 0;
            int count = 0;

            List<string> lines = new List<string>();

            for (int i = 0; i < tableUse.Length; i++)
            {
                if (tableUse[i] != 0)
                {
                    lines.Add(i + ": " + tableUse[i]);

                    usedRows++;
                    count += tableUse[i];
                }
            }

            lblTableSize.Text = tableUse.Length.ToString();
            lblUnusedRows.Text = (tableUse.Length - usedRows).ToString();
            lblUsedRows.Text = usedRows.ToString()
                + " (" + Math.Round((float)usedRows / tableUse.Length * 100.0, 4) + "%)";

            lblSize.Text = count.ToString();
            lblChainLength.Text = Math.Round((float)count / tableUse.Length, 4).ToString();
            lblUsedChainLength.Text = Math.Round((float)count / usedRows, 4).ToString();

            txtInfo.Lines = lines.ToArray();
            txtInfo.DeselectAll();
        }
    }
}
