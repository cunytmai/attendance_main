using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomListBox;

namespace attendance_tracker
{
    public partial class class_breakdown : Form
    {
        private CustomListBox.NormalListBoxItem _s;

        public class_breakdown(NormalListBoxItem item)
        {
            InitializeComponent();
            _s = item;
        }
    }
}
