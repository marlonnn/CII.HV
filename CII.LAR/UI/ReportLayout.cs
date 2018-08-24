using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class ReportLayout : Panel
    {
        public ReportLayout()
        {
            InitializeComponent();
        }

        public ReportLayout(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override Point ScrollToControl(Control activeControl)
        {
            return this.AutoScrollPosition;
        }
    }
}
