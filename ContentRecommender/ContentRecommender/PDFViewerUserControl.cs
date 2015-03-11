using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContentRecommender
{
    public partial class PDFViewerUserControl : UserControl
    {
        public PDFViewerUserControl(string filename)
        {
            InitializeComponent();
            this.axAcroPDF1.LoadFile(filename);
        }

        private void axAcroPDF1_OnError(object sender, EventArgs e)
        {

        }
    }
}
