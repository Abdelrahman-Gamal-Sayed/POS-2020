using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONA_Stores
{
    public partial class View_Report : Form
    {
        public View_Report()
        {
            InitializeComponent();
        }

        public void View_Report_Load(object sender, EventArgs e)
        {

            this.crystalReportViewer1.RefreshReport();
        }

        public void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
